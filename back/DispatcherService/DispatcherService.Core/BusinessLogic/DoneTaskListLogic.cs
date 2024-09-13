using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using DispatcherService.Contracts.DataContracts.DoneTaskList.Report;
using DispatcherService.Contracts.Filters;
using DispatcherService.Contracts.ViewModels;
using DispatcherService.Core.BusinessLogic.Interface;
using GreenCity.Http;
using GreenCity.Http.Helpers;
using LogistService.Contracts.DataContracts.Route;
using LogistService.Contracts.Interfaces;
using LogistService.Contracts.ViewModels;
using MissionService.Contracts.Interface;
using MissionService.Contracts.ViewModels.Mission;
using TransportContracts.DataContracts.TransportWorkShift;
using TransportContracts.Interfaces;
using TransportContracts.ViewModels;

namespace DispatcherService.Core.BusinessLogic
{
    public class DoneTaskListLogic : IDoneTaskListLogic
    {
        private readonly ITransportApi _transportApi;
        private readonly IRouteApi _routeApi;
        private readonly IMissionApi _missionApi;
        private readonly ITravelSheetLogic _travelSheetLogic;

        public DoneTaskListLogic(
            ITransportApi transportApi,
            IRouteApi routeApi,
            IMissionApi missionApi,
            ITravelSheetLogic travelSheetLogic
        )
        {
            _transportApi = transportApi;
            _routeApi = routeApi;
            _missionApi = missionApi;
            _travelSheetLogic = travelSheetLogic;
        }

        public async Task<CommonHttpResponse<DoneTaskListReportModel>> GetDoneTaskListModelForPrintAsync(
            DateTime dateFrom, DateTime dateTo, IList<int> carIds)
        {
            var workShiftsResponse = await _transportApi.GetWorkShiftsForDoneTaskListAsync(
                new GetWorkShiftsForDoneTaskListContract { CarIds = carIds, DateFrom = dateFrom, DateTo = dateTo });

            var workShifts = workShiftsResponse.Data;

            if (!workShiftsResponse.Success || workShifts == default)
            {
                return CommonHttpHelper.BuildErrorResponse<DoneTaskListReportModel>(
                    initialError: "Ошибка при запросе сведений из сервиса таксировщика",
                    extErrors: workShiftsResponse.Errors?.ToList());
            }

            var workShiftsInfoDict = workShifts
                .GroupBy(x => x.Id)
                .ToDictionary(x => x.Key, y => y.Single());

            var routesResponse = await _routeApi.GetRoutesByWorkShiftsIdsAsync(new GetRoutesByWorkShiftsIdsContract
            { WorkShiftsIds = workShiftsInfoDict.Keys.ToList() });

            var routes = routesResponse.Data;

            if (!routesResponse.Success || routes == default)
            {
                return CommonHttpHelper.BuildErrorResponse<DoneTaskListReportModel>(
                    initialError: "Ошибка при запросе сведений из сервиса логиста",
                    extErrors: routesResponse.Errors?.ToList());
            }

            var resultRoutes = new List<DoneTaskListRouteReportModel>();

            foreach (var route in routes)
            {
                if (!workShiftsInfoDict.TryGetValue(route.TransportWorkShiftId, out var workShiftResult))
                {
                    continue;
                }

                //!_! Получаем информацию по миссиям
                var getMissionsResponse =
                    await _missionApi.GetMissionsForBuildDoneTaskListReportByRouteIdAsync(route.Id);

                var getMissions = getMissionsResponse.Data;

                if (!getMissionsResponse.Success || getMissions == default)
                {
                    return CommonHttpHelper.BuildErrorResponse<DoneTaskListReportModel>(
                        initialError: "Ошибка при запросе сведений из сервиса заявок на вывоз",
                        extErrors: getMissionsResponse.Errors?.ToList());
                }

                //!_! Получаем информацию по путевому листу
                var filteredTravelSheetsResponse =
                    await _travelSheetLogic.GetCommonFilteredTravelSheetsAsync(new CommonTravelSheetFilterModel
                    {
                        CarIds = new List<int> { workShiftResult.TransportId },
                        DateFrom = route.Date.Date,
                        DateTo = route.Date.Date,
                        DriverIds = new List<int> { workShiftResult.DriverId },
                        OrgIds = workShiftResult.OrganizationIds
                    });

                var travelSheets = filteredTravelSheetsResponse.Data;

                if (!filteredTravelSheetsResponse.Success || travelSheets == default)
                {
                    return CommonHttpHelper.BuildErrorResponse<DoneTaskListReportModel>(
                        initialError: "Ошибка при получении путевых листов",
                        extErrors: filteredTravelSheetsResponse.Errors?.ToList());
                }

                var requiredTravelSheet = travelSheets.FirstOrDefault();

                var resultRoute =
                    GetDoneTaskListRouteReportModel(route, workShiftResult, getMissions, requiredTravelSheet);
                resultRoutes.Add(resultRoute);
            }

            var result = new DoneTaskListReportModel
            {
                Routes = resultRoutes
            };

            return CommonHttpHelper.BuildSuccessResponse(result, HttpStatusCode.OK);
        }

        private DoneTaskListRouteReportModel GetDoneTaskListRouteReportModel(
            GetRoutesByWorkShiftsIdsViewModel route,
            TransportWorkShiftViewModel workShift,
            MissionsForBuildDoneTaskListReportViewModel missions,
            CommonTravelSheetViewModel travelSheet
        )
        {
            var missionsInfoDict = missions.Missions.GroupBy(x => x.StandId)
                .ToDictionary(x => x.Key, y => y.Sum(m => m.FactVolume));

            var resultStands = new List<DoneTaskListStandReportModel>();

            foreach (var standInfo in route.Stands)
            {
                missionsInfoDict.TryGetValue(standInfo.Item1, out var volume);
                var resultStand = new DoneTaskListStandReportModel
                {
                    StandAddress = standInfo.Item2,
                    TotalVolumeByStand = $"{volume}"
                };
                resultStands.Add(resultStand);
            }

            return new DoneTaskListRouteReportModel
            {
                RouteDate = $"{route.Date}",
                RouteName = route.RouteName,
                CarInfo = workShift.RegNumber,
                DriverShortName = workShift.DriverFio,
                TransportType = $"{workShift.TransportType}",
                DepartureTime = travelSheet.StartDriveTime,
                StandsInfo = resultStands,
                RouteFinishPlace = missions.RouteFinishPlace,
                RouteTotalVolumeOnFinish = $"{route.RouteTotalVolumeOnFinish}"
            };
        }
    }
}