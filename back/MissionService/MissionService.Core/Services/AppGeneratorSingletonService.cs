using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using MissionService.Contracts.DataContracts.App;
using MissionService.Contracts.Enums;
using MissionService.Contracts.Filters.App;
using MissionService.Core.BusinessLogic.Interface;
using MissionService.Core.Converters;
using MissionService.Core.Services.Interface;
using ScheduleService.Contracts.DataContracts.ContractSchedule.Mission;
using ScheduleService.Contracts.Interfaces;
using SDContracts.Enums;
using SDContracts.Filter;
using SDContracts.Interfaces;
using SDContracts.ViewModels;
using SDContracts.ViewModels.ContractSchedule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MissionService.Core.Services
{
    public class AppGeneratorSingletonService : IAppGeneratorSingletonService
    {
        private readonly object _locker = new object();
        private readonly IServiceProvider _serviceProvider;

        public AppGeneratorSingletonService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public (bool IsSuccess, string[] Errors) RunContractScheduleAppGenerationInSingleThread(
            RunAppGenerationInSingleThreadContract contract
        )
        {
            if (contract.Date < DateTime.UtcNow.Date)
            {
                return (false, new string[] { $"На дату {contract.Date} уже нельзя генерировать заявки" });
            }

            lock (_locker)
            {
                return GenerateMissionsOnDateAsync(contract).GetAwaiter().GetResult();
            }
        }

        private async Task<(bool IsSuccess, string[] Errors)> GenerateMissionsOnDateAsync(
            RunAppGenerationInSingleThreadContract contract
        )
        {
            //!_! Достаем из сервис провайдера все необходимые нам сервисы
            IContainer2ScheduleApi container2ScheduleApi = default;
            IScheduleServiceApi scheduleServiceApi = default;
            IAppLogic appLogic = default;

            using (var scope = _serviceProvider.CreateScope())
            {
                container2ScheduleApi = scope.ServiceProvider.GetRequiredService<IContainer2ScheduleApi>();
                scheduleServiceApi = scope.ServiceProvider.GetRequiredService<IScheduleServiceApi>();
                appLogic = scope.ServiceProvider.GetRequiredService<IAppLogic>();

                //!_! Получаем по фильтру все связки контейнеров с активными договорами на дату
                var container2Schedules = new List<Container2ScheduleForMissionsViewModel>();
                var sdRequestResult = await container2ScheduleApi.GetFilteredContainer2SchedulesForMissionsAsync(new Container2ScheduleFilterModel
                {
                    RequestId = contract.RequestId,
                    Date = contract.Date.Date,
                    ContractClientIds = contract.ContractClients,
                    ContractIds = contract.ContractIds,
                    //!_! Только реальные записи интересуют для автогенерации
                    EntityType = MissionInfoEntityType.Real
                });

                if (sdRequestResult.Data == null || sdRequestResult.Data.Count == 0)
                {
                    return (true, default);
                }

                container2Schedules.AddRange(sdRequestResult.Data);

                //!_! Проходимся по всем полученным
                var preApps = new List<(int ContractClientId, int ContractId, int ContainerId)>();
                foreach (var container2Schedule in container2Schedules)
                {
                    if (container2Schedule.ContractScheduleCollection == default || container2Schedule.ContractScheduleCollection.Count == 0)
                        continue;

                    //!_! Проверяем выпадает ли график контейнера на дату
                    var isFireResultApiResult = await scheduleServiceApi.IsContractScheduleFireOnDateAsync(
                        CreateScheduleRequest(
                            contract.RequestId,
                            contract.UserName,
                            contract.UserId,
                            contract.Date.Date,
                            container2Schedule.ContractScheduleCollection
                        )
                    );

                    if (!isFireResultApiResult.Success)
                        return (false, isFireResultApiResult.Errors);

                    //!_! Если выпадает то на основе этого контейнера будем создавать миссию
                    if (isFireResultApiResult.Data)
                    {
                        preApps.Add((container2Schedule.ContractClientId, container2Schedule.ContractId, container2Schedule.ContainerId));
                    }
                }

                //!_! Убираем полные дубли на всякий случай
                var preAppsGrouped = preApps.Distinct()
                    .GroupBy(x => (x.ContractClientId, x.ContractId))
                    .Select(x => (
                        ContractClientId: x.Select(xx => xx.ContractClientId).FirstOrDefault(),
                        ContractId: x.Select(xx => xx.ContractId).FirstOrDefault(),
                        ContainerIds: x.Select(xx => xx.ContainerId).ToList()
                    ))
                    .ToList();

                var mergeResult = await MergeAppsByPreAppsAsync(
                    preAppsGrouped,
                    contract.ContractClients,
                    contract.ContractIds,
                    contract.Date.Date,
                    appLogic,
                    (contract.UserId, contract.UserName)
                );

                if (!mergeResult.IsSuccess) return (false, mergeResult.Errors);

                return (true, default);
            }
        }

        private async Task<(bool IsSuccess, string[] Errors)> MergeAppsByPreAppsAsync(
            List<(int ContractClientId, int ContractId, List<int> ContainerIds)> preAppsGrouped,
            IList<int> contractClientIdsFromParams,
            IList<int> contractIdsFromParams,
            DateTime date,
            IAppLogic appLogic,
            (int UserId, string UserName) changeUserInfo
        )
        {
            if (preAppsGrouped == default)
                return (true, default);

            //!_! Если не создалось миссий нужно будет удалить существующие подходящие по параметрам
            var contractClientIdsToSearch = contractClientIdsFromParams ?? new List<int>();
            var contractIdsToSearch = contractIdsFromParams ?? new List<int>();

            var existAppsResult = await appLogic.GetFilteredAppsForAutoGenerationAsync(new AppsForAutoGenerationFilterModel
            {
                AppDate = date.Date,
                ContractClientIds = contractClientIdsToSearch,
                ContractIds = contractIdsToSearch,
                AppType = AppType.ByContractClient
            });

            if (!existAppsResult.Success) return (false, existAppsResult.Errors);

            var existAppsDict = existAppsResult.Data
                .GroupBy(x => (x.ContractClientId, x.ContractId))
                .ToDictionary(x => x.Key, value => value.ToList());

            var updatedAppIds = new List<int>();

            foreach (var preApp in preAppsGrouped)
            {
                //!_! Если нашли инфу по миссии, значит нужно обновить ее контейнеры
                if (existAppsDict.TryGetValue((preApp.ContractClientId, preApp.ContractId), out var getAppResult))
                {
                    foreach (var app in getAppResult)
                    {
                        var updateResult = await appLogic.UpdateAppByContractClientAsync(
                            new UpdateAppByContractClient
                            {
                                AppId = app.Id,
                                Date = date.Date,
                                ContainerIds = preApp.ContainerIds,
                                UpdaterType = CreatorType.System,
                                UpdatePermission = AppActionsPermission.Absolute,
                                UserId = changeUserInfo.UserId,
                                UserName = changeUserInfo.UserName
                            }
                        );

                        if (!updateResult.Success) return (false, existAppsResult.Errors);

                        updatedAppIds.Add(app.Id);
                    }
                }
                else
                {
                    var createAppResult = await appLogic.CreateAppByContractClientAsync(new CreateAppByContractClientContract
                    {
                        Date = date.Date,
                        ContractClientId = preApp.ContractClientId,
                        ContractId = preApp.ContractId,
                        CreatorType = CreatorType.System,
                        ContainerIds = preApp.ContainerIds,
                        CreatePermission = AppActionsPermission.Absolute,
                        UserId = changeUserInfo.UserId,
                        UserName = changeUserInfo.UserName
                    });

                    if (!createAppResult.Success) return (false, existAppsResult.Errors);
                }
            }

            //!_! Удаляем все заявки на дату по фильтру среди которых нету измененных
            var appIdsToDelete = existAppsResult.Data
                .Where(x => !updatedAppIds.Distinct().Contains(x.Id))
                .Select(x => x.Id)
                .Distinct()
                .ToList();

            if (appIdsToDelete != default && appIdsToDelete.Count > 0)
            {
                var deleteResult = await appLogic.DeleteAppsAsync(new DeleteAppsContract
                {
                    AppIds = appIdsToDelete,
                    DeletePermission = AppActionsPermission.Absolute,
                    UserId = changeUserInfo.UserId,
                    UserName = changeUserInfo.UserName
                });

                if (!deleteResult.Success) return (false, existAppsResult.Errors);
            }

            return (true, default);
        }

        private GetFireInfoOnDateContractScheduleCollectionContract CreateScheduleRequest(
            Guid? requestId,
            string createdUserName,
            int createdUserId,
            DateTime date,
            IList<ContractScheduleCollectionViewModel> contractScheduleCollection,
            IList<DateTime> urgentDates = default,
            IList<DateTime> urgentCancelDates = default
        )
        {
            return new GetFireInfoOnDateContractScheduleCollectionContract
            {
                RequestId = requestId ?? Guid.NewGuid(),
                UserId = createdUserId,
                UserName = createdUserName,
                NotSlicedContractScheduleCollection = contractScheduleCollection.Select(x => x.ToSSContractScheduleCollectionModel()).ToList(),
                Date = date,
                UrgentDates = urgentDates,
                UrgentCancelDates = urgentCancelDates
            };
        }
    }
}
