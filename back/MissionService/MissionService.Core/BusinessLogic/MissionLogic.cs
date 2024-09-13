using AutoMapper;
using GreenCity.Http;
using GreenCity.Http.Helpers;
using LogistService.Contracts.DataContracts.Route;
using LogistService.Contracts.Interfaces;
using MissionService.Contracts.DataContracts.Mission;
using MissionService.Contracts.Enums;
using MissionService.Contracts.ViewModels.Mission;
using MissionService.Core.BusinessLogic.Interface;
using MissionService.Core.Extensions;
using MissionService.Database.Models.Contracts;
using MissionService.Database.Models.DbModels;
using MissionService.Database.Models.ViewModel;
using MissionService.Database.Storage.Interface;
using SDContracts.DataContracts.Container;
using SDContracts.DataContracts.MissionInfo;
using SDContracts.Interfaces;
using SDContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using GreenCity.CommonUtils.CommonExtensions;
using MissionService.Contracts.Filters.Mission;

namespace MissionService.Core.BusinessLogic
{
    public class MissionLogic : IMissionLogic
    {
        private readonly IMissionInfoApi _missionInfoApi;
        private readonly IContainerApi _containerApi;
        private readonly IRouteApi _routeApi;
        private readonly IMissionStorage _missionStorage;
        private readonly IMapper _mapper;

        public MissionLogic(
            IMissionInfoApi missionInfoApi,
            IContainerApi containerApi,
            IRouteApi routeApi,
            IMissionStorage missionStorage,
            IMapper mapper
        )
        {
            _missionInfoApi = missionInfoApi;
            _containerApi = containerApi;
            _routeApi = routeApi;
            _missionStorage = missionStorage;
            _mapper = mapper;
        }

        public async Task<(bool IsSuccess, IList<int> MissionIds, string ErrorMessage)> BindMissionsByContainers(
            DateTime date,
            IList<int> containerIds,
            CreatorType creatorType
        )
        {
            if (containerIds == default || containerIds.Count == 0)
                return (false, default, "Не переданы идентификаторы контейнеров");

            var existsMissions = await _missionStorage.GetFilteredMissionsOnDateAsync(
                date,
                default,
                new List<MissionStatus> { MissionStatus.New, MissionStatus.InProgress },
                containerIds
            );

            var containerIdsForCreateMissions = containerIds.Except(existsMissions.Select(x => x.ContainerId).Distinct()).ToList();

            //!_! Если разница ничего не вернула, значит можно просто подвязывать все что нашлось и такие миссии уже созданы
            if (containerIdsForCreateMissions == default || containerIdsForCreateMissions.Count == 0)
                return (true, existsMissions.Select(x => x.Id).ToList(), default);

            var toStorageMissionsList = containerIdsForCreateMissions.Select(containerId =>
                new CreateOneRealMissionContract
                {
                    ContainerId = containerId,
                    Date = date,
                    CreatorType = creatorType
                }
            ).ToList();

            var result = await _missionStorage.CreateRealMissionsAsync(toStorageMissionsList);
            if (!result.IsSuccess || result.MissionIds == default || result.MissionIds.Count == 0)
                return (false, default, "Не удалось создать миссии");

            //!_! Склеиваем только что созданные и уже найденные
            var missionIds = result.MissionIds.Concat(existsMissions.Select(x => x.Id)).Distinct().ToList();

            if (missionIds.Count != containerIds.Count)
                return (false, default, "Количетсво созданных миссий, оказалось не равно количеству переданных контейнеров");

            return (true, missionIds, default);
        }

        public async Task<int> CreateMissionFromMapAsync(
            int containerId,
            DateTime date,
            CreatorType creatorType,
            int userId = 0,
            string userName = "system"
        )
        {
            var result = await _missionStorage.CreateMissionFromMapAsync(CreateModel(containerId, date, creatorType, userId, userName));

            return result;
        }

        public async Task<CommonHttpResponse<IList<int>>> GetAvailableContainersByMissionsForRouteAsync(
            GetAvailableContainersByMissionsForRouteContract contract
        )
        {
            if (contract == default)
                return CommonHttpHelper.BuildErrorResponse<IList<int>>(initialError: "Пустой запрос");

            var includeRouteContainerMissionStatuses = new List<MissionStatus> { MissionStatus.InProgress };
            if (contract.IncludeContainersRouteId.HasValue)
            {
                var getRouteResult = await _routeApi.GetRoutesAsync(new GetRouteContract { Ids = new List<int> { contract.IncludeContainersRouteId.Value } });
                if (!getRouteResult.Success || getRouteResult.Data == default)
                {
                    return CommonHttpHelper.BuildErrorResponse<IList<int>>(
                        statusCode: getRouteResult.StatusCode,
                        extErrors: getRouteResult.Errors?.ToList(),
                        initialError: "Ошибка при получении рейса"
                    );
                }

                //!_! Если рейс завершен, то надо подтянуть и завершенные статусы тоже
                if (getRouteResult.Data.Single().RouteCompleted)
                {
                    includeRouteContainerMissionStatuses.AddRange(new List<MissionStatus> { MissionStatus.Done, MissionStatus.Failed });
                }
            }

            var containerIds = await _missionStorage.GetAvailableContainersByMissionsForRouteAsync(contract, includeRouteContainerMissionStatuses);

            if (containerIds == default)
                return CommonHttpHelper.BuildErrorResponse<IList<int>>(initialError: "Ошибка поиска доступных контейнеров");

            return CommonHttpHelper.BuildSuccessResponse(containerIds, HttpStatusCode.OK);
        }

        public async Task<CommonHttpResponse> BindMissionsToRouteByStandsAsync(
            BindMissionsToRouteByStandsContract contract
        )
        {
            if (contract == default)
                return CommonHttpHelper.BuildErrorResponse(initialError: "Пустой запрос");

            var containersGoupedByStandsResult = await _containerApi.GetFilteredContainerIdsForRouteAsync(new GetFilteredContainerIdsForRouteContract
            {
                StandIds = contract.StandIds,
                ContainerTypeIds = contract.ContainerTypeIds
            });

            if (!containersGoupedByStandsResult.Success || containersGoupedByStandsResult == default)
            {
                return CommonHttpHelper.BuildErrorResponse(
                    initialError: "Ошибка получения данных по контейнерам из АО",
                    extErrors: containersGoupedByStandsResult.Errors == default ? default : containersGoupedByStandsResult.Errors.ToList()
                );
            }

            if (containersGoupedByStandsResult.Data.Count == 0)
            {
                return CommonHttpHelper.BuildErrorResponse(
                    initialError: "Данные по контейнерам с таких площадок не найдены",
                    statusCode: HttpStatusCode.NotFound
                );
            }

            var dbBindContract = new BindMissionsToRouteDbContract { Date = contract.RouteDate, RouteId = contract.RouteId };

            var dbBindContractUpdateDatas = new List<(int Priority, IList<int> ContainerIds)>();
            int priority = 1;
            foreach (var standId in contract.StandIds)
            {
                if (containersGoupedByStandsResult.Data.TryGetValue(standId, out var getValueResult))
                {
                    if (getValueResult == default || getValueResult.Count == 0) { continue; }

                    dbBindContractUpdateDatas.Add((priority++, getValueResult));
                }
            }

            if (dbBindContractUpdateDatas.Count == 0)
            {
                return CommonHttpHelper.BuildErrorResponse(
                    initialError: "Данные для связки рейса с миссиями не найдены",
                    statusCode: HttpStatusCode.NotFound
                );
            }

            dbBindContract.UpdateDatas = dbBindContractUpdateDatas;

            var bindResult = await _missionStorage.BindMissionsToRouteByContainersAsync(dbBindContract);

            if (!bindResult)
            {
                return CommonHttpHelper.BuildErrorResponse(
                    initialError: "Ошибка при связке миссий с рейсом в БД"
                );
            }

            return CommonHttpHelper.BuildSuccessResponse();
        }

        public async Task<CommonHttpResponse> UnbindMissionsFromRouteAsync(int routeId)
        {
            var unbindResult = await _missionStorage.UnbindMissionsFromRouteAsync(routeId);

            if (!unbindResult)
            {
                return CommonHttpHelper.BuildErrorResponse(
                    initialError: "Ошибка при отвязке миссиий от рейса"
                );
            }

            return CommonHttpHelper.BuildSuccessResponse();
        }

        public async Task<CommonHttpResponse<IList<ContainersFromMissionsWithPrioritiesViewModel>>> GetContainersFromMissionsByRouteIdWithPrioritiesAsync(
            int routeId
        )
        {
            var prioritiesWithContainers = await _missionStorage.GetContainerIdsWithPrioritiesByRouteIdAsync(routeId);

            var containersGroupedByPriorities = prioritiesWithContainers
                .OrderBy(x => x.Priority)
                .GroupBy(x => x.Priority)
                .Select(x => (Priority: x.Key, ContainerIds: x.Select(x => x.ContainerId).Distinct().ToList()));

            var result = containersGroupedByPriorities
                .Where(x => x.ContainerIds.Count > 0)
                .Select(x => new ContainersFromMissionsWithPrioritiesViewModel
                {
                    Priority = x.Priority,
                    ContainerIds = x.ContainerIds
                })
                .ToList();

            if (result.Count == 0)
            {
                return CommonHttpHelper.BuildErrorResponse<IList<ContainersFromMissionsWithPrioritiesViewModel>>(
                    initialError: $"Не найдены контейнеры с миссий подвязанные на переданный рейс, ИД рейса - {routeId}"
                );
            }

            return CommonHttpHelper.BuildSuccessResponse<IList<ContainersFromMissionsWithPrioritiesViewModel>>(result, HttpStatusCode.OK);
        }

        public async Task<CommonHttpResponse> UpdateMissionBindingsByRouteIdAsync(UpdateMissionBindingsByRouteIdContract contract)
        {
            if (contract == default)
                return CommonHttpHelper.BuildErrorResponse(initialError: "Пустой запрос");

            var containersGoupedByStandsResult = await _containerApi.GetFilteredContainerIdsForRouteAsync(new GetFilteredContainerIdsForRouteContract
            {
                StandIds = contract.StandIds,
                ContainerTypeIds = contract.ContainerTypeIds
            });

            if (!containersGoupedByStandsResult.Success || containersGoupedByStandsResult == default)
            {
                return CommonHttpHelper.BuildErrorResponse(
                    initialError: "Ошибка получения данных по контейнерам из АО",
                    extErrors: containersGoupedByStandsResult.Errors?.ToList()
                );
            }

            if (containersGoupedByStandsResult.Data.Count == 0)
            {
                return CommonHttpHelper.BuildErrorResponse(
                    initialError: "Данные по контейнерам с таких площадок не найдены",
                    statusCode: HttpStatusCode.NotFound
                );
            }

            var dbUpdateBindingsContract = new UpdateBindingsMissionsToRouteDbContract { Date = contract.RouteDate, RouteId = contract.RouteId };

            var dbBindContractUpdateDatas = new List<(int Priority, IList<int> ContainerIds)>();
            int priority = 1;
            foreach (var standId in contract.StandIds)
            {
                if (containersGoupedByStandsResult.Data.TryGetValue(standId, out var getValueResult))
                {
                    if (getValueResult == default || getValueResult.Count == 0) { continue; }

                    dbBindContractUpdateDatas.Add((priority++, getValueResult));
                }
            }

            if (dbBindContractUpdateDatas.Count == 0)
            {
                return CommonHttpHelper.BuildErrorResponse(
                    initialError: "Данные для связки рейса с миссиями не найдены",
                    statusCode: HttpStatusCode.NotFound
                );
            }

            dbUpdateBindingsContract.UpdateDatas = dbBindContractUpdateDatas;

            var updateBindingsResult = await _missionStorage.UpdateMissionRouteBindingsAsync(dbUpdateBindingsContract);

            if (!updateBindingsResult)
            {
                return CommonHttpHelper.BuildErrorResponse(
                    initialError: "Ошибка при обновлении связок миссий с рейсом в БД"
                );
            }

            return CommonHttpHelper.BuildSuccessResponse();
        }

        public async Task<CommonHttpResponse<IList<CommonMissionViewModel>>> GetCommonFilteredMissionsAsync(CommonMissionFilterModel filter)
        {
            if (filter == default)
                return CommonHttpHelper.BuildErrorResponse<IList<CommonMissionViewModel>>(initialError: "Пустой запрос");

            var missions = await _missionStorage.GetCommonFilteredMissionsAsync(filter);

            var missionInfoFromSd = await _missionInfoApi.GetFilteredMissionInfoAsync(
                new GetFilteredMissionInfoContract
                {
                    ContractClientIds = missions.Apps.Select(x => x.ContractClientId).Distinct().ToList(),
                    StandIds = filter.StandIds,
                    ContainerIds = missions.Missions.Select(x => x.Mission.ContainerId).Distinct().ToList(),
                    ContractIds = missions.Apps.Select(x => x.ContractId).Distinct().ToList(),
                    Date = filter.Date.Date
                }
            );

            if (!missionInfoFromSd.Success)
                return CommonHttpHelper.BuildErrorResponse<IList<CommonMissionViewModel>>(
                    missionInfoFromSd.StatusCode, missionInfoFromSd.Errors?.ToList(), "Ошибка получения дополнительных сведений из АО");

            //!_! Single тк на одну дату не может быть две миссии с одним и тем же контейнером
            var missionInfoDict = missionInfoFromSd.Data
                .GroupBy(x => x.ContainerId)
                .ToDictionary(x => x.Key, y => y.Single());

            IList<CommonMissionViewModel> resultList = new List<CommonMissionViewModel>();
            foreach (var mission in missions.Missions)
            {
                if (missionInfoDict.TryGetValue(mission.Mission.ContainerId, out var getResult))
                {
                    resultList.Add(CreateModelCommonMissionViewModel(mission, getResult));
                }
            }

            return CommonHttpHelper.BuildSuccessResponse(resultList, HttpStatusCode.OK);
        }

        public async Task<CommonHttpResponse<MissionsForDispatcherViewModel>> GetMissionsForDispatcherByRouteIdAsync(int routeId)
        {
            var missions = (await _missionStorage.GetMissionsByRouteIdAsync(routeId))
                .OrderBy(x => x.Priority);

            var containerIds = missions.Select(x => x.ContainerId);

            var missionInfoFromSd = await _missionInfoApi.GetFilteredMissionInfoAsync(
                new GetFilteredMissionInfoContract
                {
                    ContainerIds = containerIds.ToList()
                }
            );

            if (!missionInfoFromSd.Success)
                return CommonHttpHelper.BuildErrorResponse<MissionsForDispatcherViewModel>(
                    missionInfoFromSd.StatusCode, missionInfoFromSd.Errors?.ToList(), "Ошибка получения дополнительных сведений из АО");

            //!_! Single тк на одну дату не может быть две миссии с одним и тем же контейнером
            var missionInfoDict = missionInfoFromSd.Data
                .GroupBy(x => x.ContainerId)
                .ToDictionary(x => x.Key, y => y.Single());

            IList<CommonMissionViewModel> resultList = new List<CommonMissionViewModel>();
            foreach (var mission in missions)
            {
                if (!missionInfoDict.TryGetValue(mission.ContainerId, out var getResult))
                {
                    return CommonHttpHelper.BuildErrorResponse<MissionsForDispatcherViewModel>(
                        HttpStatusCode.InternalServerError, default, $"Найдена миссия, по которой нету информации в АО. Идентификатор миссии - {mission.Id}");
                }

                resultList.Add(CreateModelCommonMissionViewModel(mission, getResult));
            }

            var getRouteResult = await _routeApi.GetRoutesAsync(new GetRouteContract { Ids = new List<int> { routeId } });
            if (!getRouteResult.Success)
                return CommonHttpHelper.BuildErrorResponse<MissionsForDispatcherViewModel>(
                    getRouteResult.StatusCode, getRouteResult.Errors?.ToList(), "Ошибка получения дополнительных сведений по рейсу");

            var route = getRouteResult.Data.FirstOrDefault();

            var result = new MissionsForDispatcherViewModel
            {
                RouteId = routeId,
                RouteName = route.RouteName,
                RouteDate = route.Date.Date,
                Missions = resultList,
                RouteFinishPlace = route.RouteFinishPlace,
                RouteTotalVolumeOnFinish = route.RouteTotalVolumeOnFinish
            };

            return CommonHttpHelper.BuildSuccessResponse(result, HttpStatusCode.OK);
        }

        public async Task<CommonHttpResponse> UpdateMissionsForCloseRouteAsync(
            UpdateMissionsForCloseRouteContract contract
        )
        {
            if (contract == default || contract.UpdateContracts.Count == 0)
                return CommonHttpHelper.BuildErrorResponse(initialError: "Пустой запрос");

            //!_! Все миссии которые переводят в статус НОВАЯ - должны быть отвязаны от рейса
            var missionsToUnbindFromRoute = contract.UpdateContracts
                .Where(x => x.MissionStatus == MissionStatus.New)
                .Select(x => x.MissionId)
                .Distinct()
                .ToList();

            var updateResult = await _missionStorage.UpdateMissionsForCloseRouteAsync(
                //!_! Обновлять будем только те миссии, которые не попали в отвязку
                contract.UpdateContracts.Where(x => !missionsToUnbindFromRoute.Contains(x.MissionId)).ToList(),
                missionsToUnbindFromRoute,
                contract.UserId,
                contract.UserName
            );

            if (!updateResult) { return CommonHttpHelper.BuildErrorResponse(initialError: "Ошибка при обновлении миссий рейса"); }

            //!_! Если все миссии перевели в статус НОВАЯ - то в рейсе ничего не останется и его надо удалить
            if (contract.UpdateContracts.All(x => x.MissionStatus == MissionStatus.New))
            {
                var deleteRouteResult = await _routeApi.DeleteRouteAsync(
                    new DeleteRouteContract
                    {
                        RequestId = contract.RequestId,
                        Id = contract.RouteId,
                        UserId = 0,
                        UserName = nameof(UpdateMissionsForCloseRouteAsync)
                    }
                );

                if (!deleteRouteResult.Success)
                {
                    return CommonHttpHelper.BuildErrorResponse(
                        statusCode: deleteRouteResult.StatusCode,
                        extErrors: deleteRouteResult.Errors.ToList(),
                        initialError: "Ошибка при удалении рейса"
                    );
                }

                return CommonHttpHelper.BuildSuccessResponse();
            }

            //!_! Если не нужно закрывать рейс - то на этом все
            if (!contract.NeedCloseRoute)
                return CommonHttpHelper.BuildSuccessResponse();

            var closeRouteResult = await _routeApi.CloseRouteAsync(new CloseRouteContract
            {
                Id = contract.RouteId,
                RouteFinishPlace = contract.RouteFinishPlace,
                RouteTotalVolumeOnFinish = contract.RouteTotalVolumeOnFinish,
                UserId = contract.UserId,
                UserName = contract.UserName,
                RequestId = contract.RequestId
            });

            if (!closeRouteResult.Success)
                return CommonHttpHelper.BuildErrorResponse(
                    closeRouteResult.StatusCode, closeRouteResult.Errors?.ToList(), "Ошибка при закрытии рейса");

            return CommonHttpHelper.BuildSuccessResponse();
        }

        public async Task<CommonHttpResponse<MissionsForBuildShiftTaskReportViewModel>> GetMissionsForBuildShiftTaskReportByRouteIdAsync(int routeId)
        {
            var missionsFromDb = (await _missionStorage.GetMissionsByRouteIdWithAppInfoAsync(
                routeId,
                new List<MissionStatus> { MissionStatus.InProgress, MissionStatus.Failed, MissionStatus.Done })
            )
            .OrderBy(x => x.Mission.Priority);

            var missions = CreateMissionViewModels(missionsFromDb);

            var containerIds = missions.Select(x => x.ContainerId);

            var missionInfoFromSd = await _missionInfoApi.GetFilteredMissionInfoAsync(
                new GetFilteredMissionInfoContract
                {
                    ContainerIds = containerIds.ToList()
                }
            );

            if (!missionInfoFromSd.Success)
                return CommonHttpHelper.BuildErrorResponse<MissionsForBuildShiftTaskReportViewModel>(
                    missionInfoFromSd.StatusCode, missionInfoFromSd.Errors?.ToList(), "Ошибка получения дополнительных сведений из АО");

            //!_! Single тк на одну дату не может быть две миссии с одним и тем же контейнером
            var missionInfoDict = missionInfoFromSd.Data
                .GroupBy(x => x.ContainerId)
                .ToDictionary(x => x.Key, y => y.Single());

            IList<MissionForBuildShiftTaskReportViewModel> resultList = new List<MissionForBuildShiftTaskReportViewModel>();
            foreach (var mission in missions)
            {
                if (!missionInfoDict.TryGetValue(mission.ContainerId, out var getResult))
                {
                    return CommonHttpHelper.BuildErrorResponse<MissionsForBuildShiftTaskReportViewModel>(
                        HttpStatusCode.InternalServerError, default, $"Найдена миссия, по которой нету информации в АО. Идентификатор миссии - {mission.Id}");
                }

                resultList.Add(CreateMissionsForBuildShiftTaskReportViewModel(mission, getResult));
            }

            var getRouteResult = await _routeApi.GetRoutesAsync(new GetRouteContract { Ids = new List<int> { routeId } });
            if (!getRouteResult.Success)
                return CommonHttpHelper.BuildErrorResponse<MissionsForBuildShiftTaskReportViewModel>(
                    getRouteResult.StatusCode, getRouteResult.Errors?.ToList(), "Ошибка получения дополнительных сведений из АО");

            var route = getRouteResult.Data.FirstOrDefault();

            var result = new MissionsForBuildShiftTaskReportViewModel
            {
                RouteId = routeId,
                RouteName = route.RouteName,
                Missions = resultList
            };

            return CommonHttpHelper.BuildSuccessResponse(result, HttpStatusCode.OK);
        }

        public async Task<CommonHttpResponse> FailMissionsAsync(DateTime date)
        {
            try
            {
                await _missionStorage.FailMissionsAsync(date);
                return CommonHttpHelper.BuildSuccessResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CommonHttpHelper.BuildErrorResponse(
                    HttpStatusCode.InternalServerError,
                    ex.ToExceptionDetails(),
                    $"Ошибка выполнения метода {nameof(FailMissionsAsync)}");
            }
        }

        public async Task<CommonHttpResponse<MissionsForBuildDoneTaskListReportViewModel>> GetMissionsForBuildDoneTaskListReportByRouteIdAsync(
            int routeId
        )
        {
            var missionsFromDb = (await _missionStorage.GetMissionsByRouteIdWithAppInfoAsync(
                routeId,
                new List<MissionStatus> { MissionStatus.Done })
            )
            .OrderBy(x => x.Mission.Priority);

            var missions = CreateMissionViewModels(missionsFromDb);

            var containerIds = missions.Select(x => x.ContainerId);

            var missionInfoFromSd = await _missionInfoApi.GetFilteredMissionInfoAsync(
                new GetFilteredMissionInfoContract
                {
                    ContainerIds = containerIds.ToList()
                }
            );

            if (!missionInfoFromSd.Success)
                return CommonHttpHelper.BuildErrorResponse<MissionsForBuildDoneTaskListReportViewModel>(
                    missionInfoFromSd.StatusCode, missionInfoFromSd.Errors?.ToList(), "Ошибка получения дополнительных сведений из АО");

            //!_! Single тк на одну дату не может быть две миссии с одним и тем же контейнером
            var missionInfoDict = missionInfoFromSd.Data
                .GroupBy(x => x.ContainerId)
                .ToDictionary(x => x.Key, y => y.Single());

            IList<MissionForBuildDoneTaskListReportViewModel> resultList = new List<MissionForBuildDoneTaskListReportViewModel>();
            foreach (var mission in missions)
            {
                if (!missionInfoDict.TryGetValue(mission.ContainerId, out var getResult))
                {
                    return CommonHttpHelper.BuildErrorResponse<MissionsForBuildDoneTaskListReportViewModel>(
                        HttpStatusCode.InternalServerError, default, $"Найдена миссия, по которой нету информации в АО. Идентификатор миссии - {mission.Id}");
                }

                resultList.Add(CreateMissionsForBuildDoneTaskListReportViewModel(mission, getResult));
            }

            var getRouteResult = await _routeApi.GetRoutesAsync(new GetRouteContract { Ids = new List<int> { routeId } });
            if (!getRouteResult.Success)
                return CommonHttpHelper.BuildErrorResponse<MissionsForBuildDoneTaskListReportViewModel>(
                    getRouteResult.StatusCode, getRouteResult.Errors?.ToList(), "Ошибка получения дополнительных сведений из АО");

            var route = getRouteResult.Data.FirstOrDefault();

            var result = new MissionsForBuildDoneTaskListReportViewModel
            {
                RouteId = routeId,
                RouteName = route.RouteName,
                Missions = resultList,
                RouteFinishPlace = route.RouteFinishPlace,
                RouteTotalVolumeOnFinish = route.RouteTotalVolumeOnFinish
            };

            return CommonHttpHelper.BuildSuccessResponse(result, HttpStatusCode.OK);
        }

        private CreateOneMissionFromMapDbContract CreateModel(
            int containerId,
            DateTime date,
            CreatorType creatorType,
            int userId,
            string userName
        )
        {
            return new CreateOneMissionFromMapDbContract
            {
                ContainerId = containerId,
                Date = date,
                CreatorType = creatorType,
                UserId = userId,
                UserName = userName
            };
        }

        private IEnumerable<MissionViewModel> CreateMissionViewModels(IEnumerable<(Mission Mission, IEnumerable<App> Apps)> missions)
        {
            foreach (var mission in missions)
            {
                var firstApp = mission.Apps.FirstOrDefault();

                yield return new MissionViewModel
                {
                    Id = mission.Mission.Id,
                    ContainerId = mission.Mission.ContainerId,
                    Date = mission.Mission.Date.Date,
                    Type = firstApp == default ? (AppType)firstApp.Type : AppType.Unknown,
                    CreatorType = firstApp == default ? (CreatorType)firstApp.CreatorType : CreatorType.Unknown,
                    RouteId = mission.Mission.RouteId,
                    Priority = mission.Mission.Priority,
                    MissionComment = string.Join(';', mission.Apps.Select(x => x.Comment).Distinct()),
                    Status = (MissionStatus)mission.Mission.Status,
                    StatusComment = mission.Mission.StatusComment,
                    FactVolume = mission.Mission.FactVolume,
                    CreatedUserId = firstApp == default ? firstApp.CreatedUserId : -1
                };
            }
        }

        private MissionForBuildShiftTaskReportViewModel CreateMissionsForBuildShiftTaskReportViewModel(
            MissionViewModel missionFromDbViewModel,
            MissionInfoViewModel missionInfoFromSd
        )
        {
            if (missionFromDbViewModel == default || missionInfoFromSd == default || missionInfoFromSd.StandInfo == default)
                return default;

            return new MissionForBuildShiftTaskReportViewModel
            {
                Id = missionFromDbViewModel.Id,
                MissionComment = missionFromDbViewModel.MissionComment,
                ContractClients = missionInfoFromSd.ContractClientNames == default || missionInfoFromSd.ContractClientNames.Count == 0 ?
                    string.Empty :
                    string.Join(';', missionInfoFromSd.ContractClientNames),
                StandId = missionInfoFromSd.StandInfo.StandId,
                Address = missionInfoFromSd.StandInfo.Address,
                TrashType = missionInfoFromSd.TrashType.ToTrashTypeText(),
                StandType = missionInfoFromSd.StandInfo.StandType.ToStandTypeText(),
                Contacts = missionInfoFromSd.StandInfo.Contacts,
                ContainerId = missionFromDbViewModel.ContainerId,
                ContainerName = missionInfoFromSd.ContainerName
            };
        }

        private MissionForBuildDoneTaskListReportViewModel CreateMissionsForBuildDoneTaskListReportViewModel(
            MissionViewModel missionFromDbViewModel,
            MissionInfoViewModel missionInfoFromSd
        )
        {
            if (missionFromDbViewModel == default || missionInfoFromSd == default || missionInfoFromSd.StandInfo == default)
                return default;

            return new MissionForBuildDoneTaskListReportViewModel
            {
                Id = missionFromDbViewModel.Id,
                StandId = missionInfoFromSd.StandInfo.StandId,
                Address = missionInfoFromSd.StandInfo.Address,
                ContainerId = missionFromDbViewModel.ContainerId,
                ContainerName = missionInfoFromSd.ContainerName,
                FactVolume = missionFromDbViewModel.FactVolume ?? 0.0M
            };
        }

        private CommonMissionViewModel CreateModelCommonMissionViewModel(
            MissionViewModel missionFromDbViewModel,
            MissionInfoViewModel missionInfoFromSd
        )
        {
            if (missionFromDbViewModel == default || missionInfoFromSd == default || missionInfoFromSd.StandInfo == default)
                return default;

            return new CommonMissionViewModel
            {
                Id = missionFromDbViewModel.Id,
                MissionComment = missionFromDbViewModel.MissionComment,
                Status = missionFromDbViewModel.Status,
                StatusComment = missionFromDbViewModel.StatusComment,
                ContractClients = missionInfoFromSd.ContractClientNames == default || missionInfoFromSd.ContractClientNames.Count == 0 ?
                    string.Empty :
                    string.Join(';', missionInfoFromSd.ContractClientNames),
                StandId = missionInfoFromSd.StandInfo.StandId,
                Address = missionInfoFromSd.StandInfo.Address,
                ContainerId = missionFromDbViewModel.ContainerId,
                ContainerName = missionInfoFromSd.ContainerName,
                TrashType = (MissionTrashType)missionInfoFromSd.TrashType,
                ContainerTypeId = missionInfoFromSd.ContainerTypeId,
                ContainerTypeName = missionInfoFromSd.ContainerTypeName,
                PlanVolume = missionInfoFromSd.Volume,
                FactVolume = missionFromDbViewModel.FactVolume
            };
        }

        private CommonMissionViewModel CreateModelCommonMissionViewModel(
            (Mission Mission, string MissionComment) missionFromDb,
            MissionInfoViewModel missionInfoFromSd
        )
        {
            if (missionFromDb == default || missionInfoFromSd == default || missionInfoFromSd.StandInfo == default)
                return default;

            return new CommonMissionViewModel
            {
                Id = missionFromDb.Mission.Id,
                MissionComment = missionFromDb.MissionComment,
                Status = (MissionStatus)missionFromDb.Mission.Status,
                StatusComment = missionFromDb.Mission.StatusComment,
                ContractClients = missionInfoFromSd.ContractClientNames == default || missionInfoFromSd.ContractClientNames.Count == 0 ?
                    string.Empty :
                    string.Join(';', missionInfoFromSd.ContractClientNames),
                StandId = missionInfoFromSd.StandInfo.StandId,
                Address = missionInfoFromSd.StandInfo.Address,
                ContainerId = missionFromDb.Mission.ContainerId,
                ContainerName = missionInfoFromSd.ContainerName,
                TrashType = (MissionTrashType)missionInfoFromSd.TrashType,
                ContainerTypeId = missionInfoFromSd.ContainerTypeId,
                ContainerTypeName = missionInfoFromSd.ContainerTypeName,
                PlanVolume = missionInfoFromSd.Volume,
                FactVolume = missionFromDb.Mission.FactVolume
            };
        }
    }
}
