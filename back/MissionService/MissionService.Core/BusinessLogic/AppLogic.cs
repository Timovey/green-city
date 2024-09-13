using AutoMapper;
using GreenCity.CommonUtils.CommonExtensions;
using GreenCity.Http;
using GreenCity.Http.Helpers;
using MissionService.Contracts.DataContracts.App;
using MissionService.Contracts.Enums;
using MissionService.Contracts.Filters.App;
using MissionService.Contracts.ViewModels.App;
using MissionService.Core.BusinessLogic.Interface;
using MissionService.Core.Extensions;
using MissionService.Core.Services.Interface;
using MissionService.Database.Models.Contracts;
using MissionService.Database.Models.DbModels;
using MissionService.Database.Storage.Interface;
using SDContracts.DataContracts.Container;
using SDContracts.DataContracts.MissionInfo;
using SDContracts.Interfaces;
using SDContracts.ViewModels.Stand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MissionService.Core.BusinessLogic
{
    public class AppLogic : IAppLogic
    {
        private readonly IMissionLogic _missionLogic;
        private readonly IAppStorage _appStorage;
        private readonly IMissionInfoApi _missionInfoApi;
        private readonly IContainerApi _containerApi;
        private readonly IAppActionsValidationService _appValidationService;
        private readonly IAppGeneratorSingletonService _appGeneratorSingletonService;
        private readonly IMapper _mapper;

        public AppLogic(
            IMissionLogic missionLogic,
            IAppStorage appStorage,
            IMissionInfoApi missionInfoApi,
            IContainerApi contaienerApi,
            IAppActionsValidationService appValidationService,
            IAppGeneratorSingletonService appGeneratorSingletonService,
            IMapper mapper
        )
        {
            _missionLogic = missionLogic;
            _appStorage = appStorage;
            _missionInfoApi = missionInfoApi;
            _containerApi = contaienerApi;
            _appValidationService = appValidationService;
            _appGeneratorSingletonService = appGeneratorSingletonService;
            _mapper = mapper;
        }

        public async Task<CommonHttpResponse<AppFromMapViewModel>> GetFromMapAppAsync(int appId)
        {
            var appFromDb = (await _appStorage.GetAppsByIdAsync(new List<int> { appId })).FirstOrDefault();

            if (appFromDb == default ||
                appFromDb.App.Type != (byte)AppType.FromMap)
            {
                return CommonHttpHelper.BuildErrorResponse<AppFromMapViewModel>(
                    initialError: "Заявка не найдена",
                    statusCode: HttpStatusCode.NotFound
                );
            }

            if (appFromDb.AttachedMissions == default || appFromDb.AttachedMissions.Count != 1)
            {
                return CommonHttpHelper.BuildErrorResponse<AppFromMapViewModel>(
                    initialError: $"Миссии по заявке не найдены или их количество не равно нулю, актуальное значение - {appFromDb.AttachedMissions.Count}",
                    statusCode: HttpStatusCode.NotFound
                );
            }

            var getStandResult = await _missionInfoApi.GetStandsForMissionInfoAsync(new List<int> { appFromDb.AttachedMissions.Single().ContainerId });
            if (!getStandResult.Success || getStandResult.Data == default)
            {
                return CommonHttpHelper.BuildErrorResponse<AppFromMapViewModel>(
                    initialError: $"Ошибка при получении данных по КП",
                    extErrors: getStandResult.Errors == default ? default : getStandResult.Errors.ToList()
                );
            }

            if (getStandResult.Data.Count != 1)
            {
                return CommonHttpHelper.BuildErrorResponse<AppFromMapViewModel>(
                    initialError: $"Ошибка при получении данных по КП - Неверное количество КП - {getStandResult.Data.Count}",
                    extErrors: getStandResult.Errors == default ? default : getStandResult.Errors.ToList()
                );
            }

            var result = CreateAppFromMapViewModel(appFromDb.App, appFromDb.AttachedMissions.Single(), getStandResult.Data.Single());

            return CommonHttpHelper.BuildSuccessResponse(result, HttpStatusCode.OK);
        }

        public async Task<CommonHttpResponse<IList<AppByContractClientViewModel>>> GetRealAppsByIdsAsync(IList<int> appIds)
        {
            if (appIds == default || appIds.Count == 0)
                return CommonHttpHelper.BuildErrorResponse<IList<AppByContractClientViewModel>>(initialError: "Пустой запрос");

            var appsFromDb = await _appStorage.GetAppsByIdAsync(appIds);

            if (appsFromDb == default || appsFromDb.Count == 0)
            {
                return CommonHttpHelper.BuildErrorResponse<IList<AppByContractClientViewModel>>(
                    initialError: "Ошибка при получении информации по заявке - данные не найдены",
                    statusCode: HttpStatusCode.NotFound
                );
            }

            appsFromDb = appsFromDb.Where(x => x.AttachedMissions != default && x.AttachedMissions.Count > 0).ToList();

            var result = new List<AppByContractClientViewModel>();

            var getContainersResult = await _containerApi.GetContainerAsync(new GetContainerContract
            {
                ContainerIds = appsFromDb
                    .SelectMany(x => x.AttachedMissions.Select(xx => xx.ContainerId))
                    .Distinct()
                    .ToList()
            });

            if (!getContainersResult.Success ||
                getContainersResult.Data == default ||
                getContainersResult.Data.Count == 0)
            {
                return CommonHttpHelper.BuildErrorResponse<IList<AppByContractClientViewModel>>(
                    initialError: "Ошибка при получении информации по контейнерам",
                    extErrors: getContainersResult.Errors == default ? default : getContainersResult.Errors.ToList()
                );
            }

            var containersDict = getContainersResult.Data
                .GroupBy(x => x.ContainerId)
                .ToDictionary(x => x.Key, value => value.FirstOrDefault());

            foreach (var app in appsFromDb)
            {
                if (app.App.Type != (byte)AppType.ByContractClient)
                    continue;

                var appViewModel = new AppByContractClientViewModel
                {
                    Id = app.App.Id,
                    Date = app.App.Date.Date,
                    ContractClientId = app.App.ContractClientId,
                    ContractId = app.App.ContractId,
                    Comment = app.App.Comment
                };

                var containerInAppInfos = new List<ContainerInAppInfo>();
                foreach (var mission in app.AttachedMissions.DistinctBy(x => x.ContainerId))
                {
                    if (containersDict.TryGetValue(mission.ContainerId, out var containerResult))
                    {
                        containerInAppInfos.Add(new ContainerInAppInfo
                        {
                            ContainerId = mission.ContainerId,
                            ContainerName = containerResult.Name,
                            MissionStatus = (MissionStatus)mission.Status
                        });
                    }
                }

                if (containerInAppInfos.Count == 0)
                    continue;

                appViewModel.Containers = containerInAppInfos;

                result.Add(appViewModel);
            }

            return CommonHttpHelper.BuildSuccessResponse<IList<AppByContractClientViewModel>>(result, HttpStatusCode.OK);
        }

        public async Task<CommonHttpResponse<IList<AppCommonViewModel>>> GetCommonFilteredAppsAsync(
            CommonAppFilterModel filter
        )
        {
            if (filter == default)
                return CommonHttpHelper.BuildErrorResponse<IList<AppCommonViewModel>>(initialError: "Пустой запрос");

            var appsFromDbResult = await _appStorage.GetFilteredAppsAsync(filter);

            if (appsFromDbResult == default)
            {
                return CommonHttpHelper.BuildErrorResponse<IList<AppCommonViewModel>>(
                    initialError: "Ошибки при получении данных из БД"
                );
            }

            var badApp = appsFromDbResult.Where(x => x.MissionDate.Date != filter.AppDate.Date).FirstOrDefault();
            if (badApp != default)
            {
                return CommonHttpHelper.BuildErrorResponse<IList<AppCommonViewModel>>(
                    initialError: $"По заявке - {badApp.App.Id} нарушена связанность с миссиями"
                );
            }

            var appsGroupedByAppId = appsFromDbResult
                .GroupBy(x => x.App.Id)
                .Select(x => (App: x.Select(xx => xx.App).FirstOrDefault(), MissionStatuses: x.Select(xx => (MissionStatus)xx.MissionStatus).ToList()));

            //!_! Если заявка с карты то должна быть ровно одна миссия
            var badAppFromMap = appsGroupedByAppId.Where(x => x.MissionStatuses.Count != 1).FirstOrDefault();
            if (filter.AppType == AppType.FromMap &&
                badAppFromMap != default)
            {
                return CommonHttpHelper.BuildErrorResponse<IList<AppCommonViewModel>>(
                    initialError: $"Для заявок с карты должна быть ровно одна миссия, по факту - {badAppFromMap.MissionStatuses.Count}"
                );
            }

            var getShortContracClientsAndContractInfoResult = await _missionInfoApi.GetContractsWithContractClientsShortInfoAsync(
                new GetContractsWithContractClientsShortInfoContract
                {
                    ContractClientIds = appsGroupedByAppId.Select(x => x.App.ContractClientId).Distinct().ToList(),
                    ContractIds = appsGroupedByAppId.Select(x => x.App.ContractId).Distinct().ToList()
                }
            );

            if (!getShortContracClientsAndContractInfoResult.Success || getShortContracClientsAndContractInfoResult.Data == default)
            {
                return CommonHttpHelper.BuildErrorResponse<IList<AppCommonViewModel>>(initialError: "Ошибка при запросе сведений из АО");
            }

            var result = new List<AppCommonViewModel>();
            foreach (var el in appsGroupedByAppId)
            {
                if (getShortContracClientsAndContractInfoResult.Data.ContractClientShortInfo.TryGetValue(el.App.ContractClientId, out var contractClientInfo) &&
                    getShortContracClientsAndContractInfoResult.Data.ContractShortInfo.TryGetValue(el.App.ContractId, out var contractInfo))
                {
                    result.Add(new AppCommonViewModel
                    {
                        Id = el.App.Id,
                        ContractClientName = contractClientInfo.ContractClientName,
                        ContractNumber = contractInfo.ContractNumber,
                        AppStatus = el.MissionStatuses.GetAppComputedStatus(),
                        Comment = el.App.Comment
                    });
                }
            }

            return CommonHttpHelper.BuildSuccessResponse<IList<AppCommonViewModel>>(result, HttpStatusCode.OK);
        }

        public async Task<CommonHttpResponse<IList<AppsForAutoGenerationViewModel>>> GetFilteredAppsForAutoGenerationAsync(
            AppsForAutoGenerationFilterModel filter
        )
        {
            if (filter == default)
                return CommonHttpHelper.BuildErrorResponse<IList<AppsForAutoGenerationViewModel>>(initialError: "Пустой запрос");

            var appsFromDb = await _appStorage.GetFilteredAppsForAutoGenerationAsync(filter);

            if (appsFromDb == default)
            {
                return CommonHttpHelper.BuildErrorResponse<IList<AppsForAutoGenerationViewModel>>(
                    initialError: "Ошибки при получении данных из БД"
                );
            }

            if (appsFromDb.Count == 0)
            {
                return CommonHttpHelper.BuildSuccessResponse<IList<AppsForAutoGenerationViewModel>>(
                    new List<AppsForAutoGenerationViewModel>(),
                    HttpStatusCode.OK
                );
            }

            var result = appsFromDb.DistinctBy(x => x.Id).Select(x => new AppsForAutoGenerationViewModel
            {
                Id = x.Id,
                ContractClientId = x.ContractClientId,
                ContractId = x.ContractId
            })
            .ToList();

            return CommonHttpHelper.BuildSuccessResponse<IList<AppsForAutoGenerationViewModel>>(
                result,
                HttpStatusCode.OK
            );
        }

        public async Task<CommonHttpResponse<int>> CreateAppByContractClientAsync(CreateAppByContractClientContract contract)
        {
            if (contract == default || contract.ContainerIds == default || contract.ContainerIds.Count == 0)
                return CommonHttpHelper.BuildErrorResponse<int>(initialError: "Пустой запрос");

            if (!contract.CreatePermission.CanCreateOrChangeMissionByTime(contract.Date))
            {
                return CommonHttpHelper.BuildErrorResponse<int>(
                    statusCode: HttpStatusCode.BadRequest,
                    initialError: $"Слишком поздно для создания заявки на {contract.Date.Date}"
                );
            }

            var bindMissionResult = await _missionLogic.BindMissionsByContainers(contract.Date, contract.ContainerIds, contract.CreatorType);
            if (!bindMissionResult.IsSuccess)
                return CommonHttpHelper.BuildErrorResponse<int>(
                    extErrors: new List<string> { bindMissionResult.ErrorMessage },
                    initialError: $"Ошибка при создании миссий для заявки"
                );

            var createdApp = await _appStorage.CreateAppAsync(CreateAppDbContract(contract, bindMissionResult.MissionIds));
            if (!createdApp.IsSuccess)
                return CommonHttpHelper.BuildErrorResponse<int>(
                    initialError: $"Ошибка при заявки"
                );

            return CommonHttpHelper.BuildSuccessResponse(createdApp.CreatedAppId, HttpStatusCode.OK);
        }

        public async Task<CommonHttpResponse<int>> CreateAppFromMapAsync(CreateAppFromMapContract contract)
        {
            if (contract == default)
                return CommonHttpHelper.BuildErrorResponse<int>(initialError: "Пустой запрос");

            if (!contract.CreatePermission.CanCreateOrChangeMissionByTime(contract.Date))
            {
                return CommonHttpHelper.BuildErrorResponse<int>(
                    statusCode: HttpStatusCode.BadRequest,
                    initialError: $"Слишком поздно для создания заявки на {contract.Date.Date}"
                );
            }

            var createFakeDataResult = await _missionInfoApi.CreateFakeDataForMissionInfoAsync(_mapper.Map<CreateFakeDataForMissionInfoContract>(contract));
            if (!createFakeDataResult.Success || createFakeDataResult.Data == default)
            {
                return CommonHttpHelper.BuildErrorResponse<int>(
                    createFakeDataResult.StatusCode,
                    createFakeDataResult.Errors.ToList(),
                    "Ошибка при создании дополнительной информации в АО");
            }

            var fromMapMissionId = await _missionLogic.CreateMissionFromMapAsync(
                createFakeDataResult.Data.FakeContainerId,
                contract.Date,
                contract.CreatorType,
                contract.UserId,
                contract.UserName
            );

            var createdApp = await _appStorage.CreateAppAsync(
                CreateAppDbContract(contract, fromMapMissionId, createFakeDataResult.Data.ContractClientId, createFakeDataResult.Data.ContractId)
            );
            if (!createdApp.IsSuccess)
                return CommonHttpHelper.BuildErrorResponse<int>(
                    initialError: $"Ошибка при заявки"
                );

            return CommonHttpHelper.BuildSuccessResponse(createdApp.CreatedAppId, HttpStatusCode.OK);
        }

        public async Task<CommonHttpResponse> UpdateAppByContractClientAsync(
            UpdateAppByContractClient contract
        )
        {
            if (contract == default)
                return CommonHttpHelper.BuildErrorResponse(initialError: "Пустой запрос");

            var existApps = await _appStorage.GetAppsByIdAsync(new List<int> { contract.AppId });

            var validationResult = _appValidationService.ValidateUpdateApp(contract, existApps);

            if (!validationResult.IsSuccess)
                return CommonHttpHelper.BuildErrorResponse(initialError: validationResult.Error, statusCode: validationResult.RecomendedErrorCode);

            //!_! Нужно найти миссии, которые подвязаны не только на переданные заявки, чтобы дальше их исключить при удалении
            var excludeMissionIds = (await _appStorage.GetMissionsThanCantBeDeletedAsync(new List<int> { contract.AppId })) ?? new List<int>();

            var missionsToDelete = new List<int>();
            IList<int> containerIdsToCreateNewMissions = new List<int>();
            DateTime? appDate = contract.Date.Date;

            var existApp = existApps.Single();
            if (contract.Date.Date == existApp.App.Date.Date)
            {
                missionsToDelete = existApp.AttachedMissions
                    .ExceptBy(contract.ContainerIds, x => x.ContainerId).Select(x => x.Id)
                    .Except(excludeMissionIds)
                    .Distinct()
                    .ToList();

                containerIdsToCreateNewMissions = contract.ContainerIds.Except(existApp.AttachedMissions.Select(x => x.ContainerId)).Distinct().ToList();

                appDate = default;
            }
            else
            {

                //!_! Если дата другая, то нам нужно удалить все что можно с даты на которой был и привязать на новую дату
                missionsToDelete = existApp.AttachedMissions.Select(x => x.Id).Except(excludeMissionIds).Distinct().ToList();

                //!_! Просто будем пробовать привязывать все контейнеры что пришли
                containerIdsToCreateNewMissions = contract.ContainerIds;
            }

            IList<int> toAddMissionIds = new List<int>();
            if (containerIdsToCreateNewMissions != default && containerIdsToCreateNewMissions.Count > 0)
            {
                var missionIdsToBindResult = await _missionLogic.BindMissionsByContainers(contract.Date.Date, containerIdsToCreateNewMissions, contract.UpdaterType);

                if (!missionIdsToBindResult.IsSuccess ||
                    missionIdsToBindResult.MissionIds == default ||
                    missionIdsToBindResult.MissionIds.Count == 0)
                {
                    return CommonHttpHelper.BuildErrorResponse(
                        initialError: "Ошибка при обновлении миссий по заявке",
                        extErrors: missionIdsToBindResult.ErrorMessage.IsNullOrEmpty() ? default : new List<string> { missionIdsToBindResult.ErrorMessage }
                    );
                }

                toAddMissionIds = missionIdsToBindResult.MissionIds;
            }

            var updateResult = await _appStorage.UpdateAppAsync(
                contract.AppId,
                missionsToDelete,
                toAddMissionIds,
                appDate,
                contract.UserId,
                contract.UserName
            );

            if (!updateResult)
            {
                return CommonHttpHelper.BuildErrorResponse(
                    initialError: "Ошибка при обновлении заявки"
                );
            }

            return CommonHttpHelper.BuildSuccessResponse();
        }

        public async Task<CommonHttpResponse> DeleteAppsAsync(DeleteAppsContract contract)
        {
            if (contract == default || contract.AppIds.Count == 0)
                return CommonHttpHelper.BuildErrorResponse<int>(initialError: "Пустой запрос");

            var existApps = await _appStorage.GetAppsByIdAsync(contract.AppIds);

            var validationResult = _appValidationService.ValidateDeleteApp(contract, existApps);

            if (!validationResult.IsSuccess)
                return CommonHttpHelper.BuildErrorResponse(initialError: validationResult.Error, statusCode: validationResult.RecomendedErrorCode);

            //!_! Нужно найти миссии, которые подвязаны не только на переданные заявки, чтобы дальше их исключить при удалении
            var excludeMissionIds = (await _appStorage.GetMissionsThanCantBeDeletedAsync(contract.AppIds)) ?? new List<int>();

            var deleteAppResult = await _appStorage.DeleteAppsAsync(
                contract.AppIds,
                excludeMissionIds,
                contract.UserId,
                contract.UserName
            );

            if (!deleteAppResult)
            {
                return CommonHttpHelper.BuildErrorResponse(initialError: "Ошибка при удалении заявки");
            }

            return CommonHttpHelper.BuildSuccessResponse();
        }

        public Task<CommonHttpResponse> ReGenerateAppsAsync(
            RunAppGenerationInSingleThreadContract contract
        )
        {
            return Task.Run(() =>
            {
                var validationResult = _appValidationService.ValidateRegenerationApps(contract);

                if (!validationResult.IsSuccess)
                    return CommonHttpHelper.BuildErrorResponse(initialError: validationResult.Error, statusCode: validationResult.RecomendedErrorCode);

                var generationResult = _appGeneratorSingletonService.RunContractScheduleAppGenerationInSingleThread(contract);

                if (!generationResult.IsSuccess)
                    return CommonHttpHelper.BuildErrorResponse(initialError: validationResult.Error, statusCode: validationResult.RecomendedErrorCode);

                return CommonHttpHelper.BuildSuccessResponse();
            });
        }

        private AppFromMapViewModel CreateAppFromMapViewModel(App app, Mission mission, StandForMissionInfoViewModel stand)
        {
            return new AppFromMapViewModel
            {
                Id = app.Id,
                Date = app.Date.Date,
                ContractClientId = app.ContractClientId,
                ContractId = app.ContractId,
                Comment = app.Comment,
                Longitude = stand.Longitude,
                Latitude = stand.Latitude,
                CustomVolume = stand.CustomVolume,
                MissionStatus = (MissionStatus)mission.Status
            };
        }

        private CreateAppDbContract CreateAppDbContract(
            CreateAppByContractClientContract contract,
            IList<int> missionIds
        )
        {
            var result = _mapper.Map<CreateAppDbContract>(contract);
            result.MissionIds = missionIds;

            return result;
        }

        private CreateAppDbContract CreateAppDbContract(
            CreateAppFromMapContract contract,
            int missionId,
            int contractClientId,
            int contractId
        )
        {
            return new CreateAppDbContract
            {
                Date = contract.Date,
                ContractClientId = contractClientId,
                ContractId = contractId,
                Type = AppType.FromMap,
                CreatorType = contract.CreatorType,
                Comment = contract.Comment,
                MissionIds = new List<int> { missionId },
                CreatedUserId = contract.UserId,
                CreatedUserName = contract.UserName
            };
        }
    }
}
