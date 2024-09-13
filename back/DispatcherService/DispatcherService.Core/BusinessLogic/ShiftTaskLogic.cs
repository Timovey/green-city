using AuthContracts.DataContracts;
using AuthContracts.Interfaces;
using AutoMapper;
using DispatcherService.Contracts.DataContracts.ShiftTask;
using DispatcherService.Contracts.DataContracts.ShiftTask.Report;
using DispatcherService.Contracts.DataContracts.TravelSheet;
using DispatcherService.Contracts.Filters;
using DispatcherService.Contracts.ViewModels;
using DispatcherService.Core.BusinessLogic.Interface;
using DispatcherService.Core.Extensions;
using DispatcherService.Database.Interfaces;
using DispatcherService.Database.Models;
using GreenCity.Http;
using GreenCity.Http.Helpers;
using LogistService.Contracts.DataContracts.Route;
using LogistService.Contracts.Filters;
using LogistService.Contracts.Interfaces;
using MissionService.Contracts.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TransportContracts.DataContracts.TransportWorkShift;
using TransportContracts.Interfaces;

namespace DispatcherService.Core.BusinessLogic
{
    public class ShiftTaskLogic : IShiftTaskLogic
    {
        private readonly IShiftTaskStorage _shiftTaskStorage;
        private readonly ITravelSheetLogic _travelSheetLogic;
        private readonly ITransportApi _transportApi;
        private readonly IMissionApi _missionApi;
        private readonly IRouteApi _routeApi;
        private readonly IAuthApi _authApi;
        private readonly IMapper _mapper;

        public ShiftTaskLogic(
            IShiftTaskStorage shiftTaskStorage,
            ITravelSheetLogic travelSheetLogic,
            ITransportApi transportApi,
            IMissionApi missionApi,
            IRouteApi routeApi,
            IAuthApi authApi,
            IMapper mapper
        )
        {
            _shiftTaskStorage = shiftTaskStorage;
            _travelSheetLogic = travelSheetLogic;
            _transportApi = transportApi;
            _missionApi = missionApi;
            _routeApi = routeApi;
            _authApi = authApi;
            _mapper = mapper;
        }

        public async Task<CommonHttpResponse<CreateShiftTaskResponse>> CreateShiftTaskAsync(
            CreateShiftTaskContract contract
        )
        {
            if (contract == default)
                return CommonHttpHelper.BuildErrorResponse<CreateShiftTaskResponse>(initialError: "Пустой запрос");

            var getWorkShiftInfoResult = await _transportApi.GetActualWorkShitsAsync(new GetActualWorkShitsContract
            {
                DateTime = contract.Date.Date,
                IsFilterByUser = false
            });

            if (!getWorkShiftInfoResult.Success || getWorkShiftInfoResult.Data == default)
            {
                return CommonHttpHelper.BuildErrorResponse<CreateShiftTaskResponse>(
                    initialError: "Ошибка при получении информации по экипажу",
                    extErrors: getWorkShiftInfoResult.Errors?.ToList()
                );
            }

            var workShiftInfo = getWorkShiftInfoResult.Data.Where(x => x.Id == contract.WorkShiftId).FirstOrDefault();
            if (workShiftInfo == default)
            {
                return CommonHttpHelper.BuildErrorResponse<CreateShiftTaskResponse>(
                    initialError: $"Экипажи {contract.WorkShiftId} не найден"
                );
            }

            var createTravelSheetResult = await _travelSheetLogic.CreateTravelSheetAsync(new CreateTravelSheetContract
            {
                Date = contract.Date.Date,
                CarId = workShiftInfo.TransportId,
                DriverId = workShiftInfo.DriverId,
                OrgId = contract.OrgId,
                UserId = contract.UserId,
                UserName = contract.UserName
            });

            if (!createTravelSheetResult.Success || createTravelSheetResult.Data == default)
            {
                return CommonHttpHelper.BuildErrorResponse<CreateShiftTaskResponse>(
                    initialError: "Ошибка при создании путевого листа",
                    extErrors: createTravelSheetResult.Errors?.ToList()
                );
            }

            var createShiftTaskResult = await _shiftTaskStorage.CreateShiftTaskAsync(contract, createTravelSheetResult.Data);

            if (!createShiftTaskResult.IsSuccess)
            {
                //!_! Удалим уже созданный путевой лист
                await _travelSheetLogic.DeleteTravelSheetsAsync(new DeleteTravelSheetsContract { TravelSheetIds = new List<int> { createTravelSheetResult.Data } });

                CommonHttpHelper.BuildErrorResponse<int>(
                    initialError: "Ошибка при создании сменного задания",
                    extErrors: createTravelSheetResult.Errors?.ToList()
                );
            }

            return CommonHttpHelper.BuildSuccessResponse(
                new CreateShiftTaskResponse
                {
                    ShiftTaskTaskId = createShiftTaskResult.AddedShiftTaskId,
                    TravelSheetId = createTravelSheetResult.Data
                },
                HttpStatusCode.OK
            );
        }

        public async Task<CommonHttpResponse> DeleteShiftTasksAsync(DeleteShiftTasksContract contract)
        {
            if (contract == default)
                return CommonHttpHelper.BuildErrorResponse<int>(initialError: "Пустой запрос");

            var deleteResult = await _shiftTaskStorage.DeleteShiftTasksAsync(contract);

            if (!deleteResult)
            {
                return CommonHttpHelper.BuildErrorResponse(initialError: "Ошибка при удалении данных в бд");
            }

            return CommonHttpHelper.BuildSuccessResponse();
        }

        public async Task<CommonHttpResponse<IList<CommonShiftTaskViewModel>>> GetCommonFilteredShiftTasksAsync(
            CommonShiftTasksFilterModel filter
        )
        {
            if (filter == default)
                return CommonHttpHelper.BuildErrorResponse<IList<CommonShiftTaskViewModel>>(initialError: "Пустой запрос");

            var shiftTasks = await _shiftTaskStorage.GetCommonFilteredShiftTasksAsync(filter);

            if (shiftTasks == default)
            {
                return CommonHttpHelper.BuildErrorResponse<IList<CommonShiftTaskViewModel>>(initialError: "Ошибка при получении данных из бд");
            }

            //!_! Если ничего не нашли, то отдаем пустой список
            if (shiftTasks.Count == 0)
            {
                return CommonHttpHelper.BuildSuccessResponse<IList<CommonShiftTaskViewModel>>(new List<CommonShiftTaskViewModel>(), HttpStatusCode.OK);
            }

            var travelSheetsInfo = await _travelSheetLogic.GetCommonFilteredTravelSheetsAsync(
                new CommonTravelSheetFilterModel
                {
                    TravelSheetIds = shiftTasks.Select(x => x.ShiftTask.TravelSheetId).Distinct().ToList(),
                    Date = filter.Date.Date
                }
            );

            if (!travelSheetsInfo.Success || travelSheetsInfo.Data == default)
            {
                return CommonHttpHelper.BuildErrorResponse<IList<CommonShiftTaskViewModel>>(
                    initialError: "Ошибка при получении информации по ПЛ",
                    extErrors: travelSheetsInfo.Errors?.ToList()
                );
            }

            var travelSheetInfoDict = travelSheetsInfo.Data
                .GroupBy(x => x.Id)
                .ToDictionary(x => x.Key, value => value.FirstOrDefault());

            var routeIdsToSearch = shiftTasks
                .Where(x => x.RouteIds != default && x.RouteIds.Count > 0)
                .SelectMany(x => x.RouteIds)
                .Distinct()
                .ToList();

            var getRoutesResult = await _routeApi.GetRoutesAsync(new GetRouteContract { Ids = routeIdsToSearch, NeedAttachStands = false });

            if (!getRoutesResult.Success || getRoutesResult.Data == default)
            {
                return CommonHttpHelper.BuildErrorResponse<IList<CommonShiftTaskViewModel>>(
                    initialError: "Ошибка при получении информации по рейсам",
                    extErrors: getRoutesResult.Errors?.ToList()
                );
            }

            var routesDict = getRoutesResult.Data
                .GroupBy(x => x.Id)
                .ToDictionary(x => x.Key, value => value.Select(x => x.RouteName).FirstOrDefault());

            var result = CreateCommonShiftTaskViewModel(shiftTasks, routesDict, travelSheetInfoDict);

            return CommonHttpHelper.BuildSuccessResponse<IList<CommonShiftTaskViewModel>>(result.ToList(), HttpStatusCode.OK);
        }

        public async Task<CommonHttpResponse<IList<AvailableRouteForCreateShiftTaskViewModel>>> GetAvailableRouteForCreateShiftTaskAsync(
            GetAvailableRoutesForCreateShiftTaskContract contract
        )
        {
            var getRoutesResult = await _routeApi.GetFilteredRoutesAsync(
                new RouteFilterModel
                {
                    Date = contract.Date.Date,
                    TransportWorkShiftIds = new List<int> { contract.WorkShiftId },
                    IsFilter = true,
                    IsReady = true
                }
            );

            if (!getRoutesResult.Success || getRoutesResult.Data == default)
            {
                return CommonHttpHelper.BuildErrorResponse<IList<AvailableRouteForCreateShiftTaskViewModel>>(
                    initialError: "Ошибка при получении информации по рейсам",
                    extErrors: getRoutesResult.Errors?.ToList()
                );
            }

            if (getRoutesResult.Data.Count == 0)
            {
                return CommonHttpHelper.BuildSuccessResponse<IList<AvailableRouteForCreateShiftTaskViewModel>>(
                    new List<AvailableRouteForCreateShiftTaskViewModel>(),
                    HttpStatusCode.OK
                );
            }

            var excludeRouteIds = await _shiftTaskStorage.GetRouteIdsFromShiftTasksWithFilterAsync(contract.Date.Date);

            var result = getRoutesResult.Data
                .Where(x => !excludeRouteIds.Contains(x.Id))
                .Select(x => new AvailableRouteForCreateShiftTaskViewModel
                {
                    Id = x.Id,
                    RouteName = x.RouteName
                })
                .Distinct()
                .ToList();

            return CommonHttpHelper.BuildSuccessResponse<IList<AvailableRouteForCreateShiftTaskViewModel>>(result, HttpStatusCode.OK);
        }

        public async Task<CommonHttpResponse<ShiftTaskReportModel>> GetShiftTaskModelForPrintAsync(int shiftTaskId)
        {
            var dbShiftTasks = await _shiftTaskStorage.GetCommonFilteredShiftTasksAsync(
                new CommonShiftTasksFilterModel
                {
                    ShiftTaskIds = new List<int> { shiftTaskId }
                }
            );

            if (dbShiftTasks == default || dbShiftTasks.Count != 1)
            {
                return CommonHttpHelper.BuildErrorResponse<ShiftTaskReportModel>(
                    initialError: $"Ошибка при получении СЗ"
                );
            }

            var shiftTask = dbShiftTasks.Single();

            var travelSheetsInfo = await _travelSheetLogic.GetCommonFilteredTravelSheetsAsync(
                new CommonTravelSheetFilterModel
                {
                    TravelSheetIds = new List<int> { shiftTask.ShiftTask.TravelSheetId },
                    Date = shiftTask.ShiftTask.Date.Date
                }
            );

            if (!travelSheetsInfo.Success || travelSheetsInfo.Data == default)
            {
                return CommonHttpHelper.BuildErrorResponse<ShiftTaskReportModel>(
                    initialError: "Ошибка при получении информации по ПЛ",
                    extErrors: travelSheetsInfo.Errors?.ToList()
                );
            }

            var travelSheet = travelSheetsInfo.Data.Single();

            var result = new ShiftTaskReportModel
            {
                Number = travelSheet.NumberToShow,
                Organization = travelSheet.OrgInfo,
                Date = shiftTask.ShiftTask.Date.ToString("dd.MM.yyyy"),
                Transport = travelSheet.CarInfo,
                Driver = travelSheet.DriverInfo
            };

            var shiftTaskRoutes = new List<ShiftTaskRouteReportModel>();
            foreach (var routeId in shiftTask.RouteIds)
            {
                var routeInfo = new ShiftTaskRouteReportModel();

                var missionsByRouteResult = await _missionApi.GetMissionsForBuildShiftTaskReportByRouteIdAsync(routeId);

                if (!missionsByRouteResult.Success)
                {
                    return CommonHttpHelper.BuildErrorResponse<ShiftTaskReportModel>(
                        initialError: "Ошибка при получении информации по миссиям СЗ",
                        extErrors: missionsByRouteResult.Errors?.ToList()
                    );
                }

                routeInfo.RouteNumber = missionsByRouteResult.Data.RouteName;

                var missionInfoGroupedByStands = missionsByRouteResult.Data.Missions
                    .GroupBy(x => x.StandId)
                    .Select(x => new
                    {
                        StandInfo = x.Select(x => (Address: x.Address, Contracts: x.Contacts)).FirstOrDefault(),
                        Containers = x.Select(x => new ShiftTaskContainerReportModel
                        {
                            Contragents = x.ContractClients,
                            ContainerName = x.ContainerName,
                            TrashType = x.TrashType,
                            StandType = x.StandType,
                            Comment = x.MissionComment
                        }
                        )
                        .ToList()
                    });

                routeInfo.ShiftTaskStands = missionInfoGroupedByStands.Select(x => new ShiftTaskStandReportModel
                {
                    StandAdress = x.StandInfo.Address,
                    StandContacts = x.StandInfo.Contracts,
                    ShiftTaskContainers = x.Containers
                })
                .ToList();

                shiftTaskRoutes.Add(routeInfo);
            }

            result.ShiftTaskRoutes = shiftTaskRoutes;

            return CommonHttpHelper.BuildSuccessResponse(result, HttpStatusCode.OK);
        }

        public async Task<CommonHttpResponse<IList<OrgsDropDownForCreateShiftTaskViewModel>>> GetOrgsDropDownForCreateShiftTaskAsync(
            GetOrgsDropDownForCreateShiftTaskContract contract
        )
        {
            if (contract == default)
                return CommonHttpHelper.BuildErrorResponse<IList<OrgsDropDownForCreateShiftTaskViewModel>>(initialError: "Пустой запрос");

            var orgsIdsFromWorkShifts = await _transportApi.GetWorkShiftsByIdsAsync(
                _mapper.Map<GetWorkShiftsByIdsContract>(contract)
            );

            if (!orgsIdsFromWorkShifts.Success)
            {
                return CommonHttpHelper.BuildErrorResponse<IList<OrgsDropDownForCreateShiftTaskViewModel>>(
                    initialError: "Ошибка при получении информации по экипажам",
                    extErrors: orgsIdsFromWorkShifts.Errors?.ToList()
                );
            }

            var getOrgsContract = _mapper.Map<GetOrgsByIdsContract>(contract);
            getOrgsContract.OrgIds = orgsIdsFromWorkShifts.Data.SelectMany(x => x.OrganizationIds).Distinct().ToList();
            var getOrgsResult = await _authApi.GetOrgsByIdsAsync(getOrgsContract);

            if (!getOrgsResult.Success || getOrgsResult.Data == default)
            {
                return CommonHttpHelper.BuildErrorResponse<IList<OrgsDropDownForCreateShiftTaskViewModel>>(
                    initialError: "Ошибка при получении информации по организациям",
                    extErrors: orgsIdsFromWorkShifts.Errors?.ToList()
                );
            }

            var result = getOrgsResult.Data.Select(x => new OrgsDropDownForCreateShiftTaskViewModel { OrgId = x.OrganizationId, OrgName = x.Name }).ToList();

            return CommonHttpHelper.BuildSuccessResponse<IList<OrgsDropDownForCreateShiftTaskViewModel>>(result, HttpStatusCode.OK);
        }

        private IEnumerable<CommonShiftTaskViewModel> CreateCommonShiftTaskViewModel(
            IList<(ShiftTask ShiftTask, IList<int> RouteIds)> shiftTasks,
            IDictionary<int, string> routesDict,
            IDictionary<int, CommonTravelSheetViewModel> travelSheetsDict
        )
        {
            foreach (var shiftTask in shiftTasks)
            {
                if (!travelSheetsDict.TryGetValue(shiftTask.ShiftTask.TravelSheetId, out var getResult))
                    continue;

                yield return new CommonShiftTaskViewModel
                {
                    Id = shiftTask.ShiftTask.Id,
                    TravelSheetId = getResult.Id,
                    TravelSheetInfo = getResult.NumberToShow,
                    OrgInfo = getResult.OrgInfo,
                    CarWithDriverInfo = $"{getResult.CarInfo} {getResult.DriverInfo}",
                    RoutesInfo = shiftTask.RouteIds.GetRouteInfoString(routesDict)
                };
            }
        }
    }
}
