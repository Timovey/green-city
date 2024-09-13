using DispatcherService.Contracts.DataContracts.Brigade;
using DispatcherService.Contracts.Filters;
using DispatcherService.Contracts.ViewModels;
using DispatcherService.Core.BusinessLogic.Interface;
using DispatcherService.Core.Extensions;
using DispatcherService.Core.Services.Interface;
using DispatcherService.Database.Interfaces;
using DispatcherService.Database.Models;
using GreenCity.Http;
using GreenCity.Http.Helpers;
using LogistService.Contracts.DataContracts.Route;
using LogistService.Contracts.Filters;
using LogistService.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TransportContracts.DataContracts.TransportWorkShift;
using TransportContracts.Interfaces;
using TransportContracts.ViewModels;

namespace DispatcherService.Core.BusinessLogic
{
    public class BrigadeLogic : IBrigadeLogic
    {
        private readonly ITransportApi _transportApi;
        private readonly IBrigadeStorage _brigadeStorage;
        private readonly IBrigadeOperationsValidator _brigadeOperationsValidator;
        private readonly IRouteApi _routeApi;

        public BrigadeLogic(
            ITransportApi transportApi,
            IBrigadeStorage brigadeStorage,
            IBrigadeOperationsValidator brigadeOperationsValidator,
            IRouteApi routeApi
        )
        {
            _transportApi = transportApi;
            _brigadeStorage = brigadeStorage;
            _brigadeOperationsValidator = brigadeOperationsValidator;
            _routeApi = routeApi;
        }

        public async Task<CommonHttpResponse<int>> CreateBrigadeAsync(CreateBrigadeContract contract)
        {
            var validationResult = await _brigadeOperationsValidator.ValidateCreateBrigadeAsync(contract, _brigadeStorage);

            if (!validationResult.IsSuccess)
            {
                return CommonHttpHelper.BuildErrorResponse<int>(
                    initialError: "Ошибка при создании бригады",
                    extErrors: new List<string> { validationResult.Error }
                );
            }

            var creationResult = await _brigadeStorage.CreateBrigadeAsync(contract);

            if (!creationResult.IsSuccess)
            {
                return CommonHttpHelper.BuildErrorResponse<int>(
                    initialError: "Ошибка в БД при создании бригады"
                );
            }

            return CommonHttpHelper.BuildSuccessResponse(creationResult.AddedBrigadeId, HttpStatusCode.OK);
        }

        public async Task<CommonHttpResponse<IList<CommonBrigadeViewModel>>> GetFilteredBrigadesAsync(
            CommonBrigadesFilterModel filter
        )
        {
            if (filter == default)
                return CommonHttpHelper.BuildErrorResponse<IList<CommonBrigadeViewModel>>(initialError: "Пустой запрос");

            var brigadesByFilter = await _brigadeStorage.GetCommonFilteredBrigadesAsync(filter);

            if (brigadesByFilter == default)
                return CommonHttpHelper.BuildErrorResponse<IList<CommonBrigadeViewModel>>(initialError: "Ошибка при получении бригад из бд");

            if (brigadesByFilter.Count == 0)
            {
                return CommonHttpHelper.BuildSuccessResponse<IList<CommonBrigadeViewModel>>(new List<CommonBrigadeViewModel>(), HttpStatusCode.OK);
            }

            var routeIdsToSearch = brigadesByFilter
                .Where(x => x.RouteIds != default && x.RouteIds.Count > 0)
                .SelectMany(x => x.RouteIds)
                .Distinct()
                .ToList();

            var getRoutesResult = await _routeApi.GetRoutesAsync(new GetRouteContract { Ids = routeIdsToSearch, NeedAttachStands = false });

            if (!getRoutesResult.Success || getRoutesResult.Data == default)
            {
                return CommonHttpHelper.BuildErrorResponse<IList<CommonBrigadeViewModel>>(
                    initialError: "Ошибка при получении информации по рейсам",
                    extErrors: getRoutesResult.Errors?.ToList()
                );
            }

            var routesDict = getRoutesResult.Data
                .GroupBy(x => x.Id)
                .ToDictionary(x => x.Key, value => value.Select(x => x.RouteName).FirstOrDefault());

            var getWorkShiftsResult = await _transportApi.GetWorkShiftsByIdsAsync(
                new GetWorkShiftsByIdsContract
                {
                    WorkShiftIds = brigadesByFilter.Select(x => x.Brigade.WorkShiftId).Distinct().ToList(),
                    IsFilterByUser = false
                }
            );

            if (!getWorkShiftsResult.Success ||
                getWorkShiftsResult.Data == default)
                CommonHttpHelper.BuildErrorResponse(
                    initialError: "Ошибка при получении данных от транспорт сервиса",
                    extErrors: getWorkShiftsResult.Errors?.ToList()
                );

            var workShiftsDict = getWorkShiftsResult.Data
                .GroupBy(x => x.Id)
                .ToDictionary(x => x.Key, value => value.FirstOrDefault());

            var result = CreateCommonBrigadeViewModel(brigadesByFilter, routesDict, workShiftsDict);

            return CommonHttpHelper.BuildSuccessResponse<IList<CommonBrigadeViewModel>>(result.ToList(), HttpStatusCode.OK);
        }

        public async Task<CommonHttpResponse> DeleteBrigadesAsync(DeleteBrigadesContract contract)
        {
            if (contract == default)
                return CommonHttpHelper.BuildErrorResponse(initialError: "Пустой запрос");

            var deleteResult = await _brigadeStorage.DeleteBrigadesAsync(contract);

            if (!deleteResult)
            {
                return CommonHttpHelper.BuildErrorResponse(initialError: "Ошибка при удалении данных в бд");
            }

            return CommonHttpHelper.BuildSuccessResponse();
        }

        public async Task<CommonHttpResponse<IList<AvailableRoutesForCreateBrigadeViewModel>>> GetAvailableRoutesForCreateBrigadeAsync(
            GetAvailableRoutesForCreateBrigadeContract contract
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
                return CommonHttpHelper.BuildErrorResponse<IList<AvailableRoutesForCreateBrigadeViewModel>>(
                    initialError: "Ошибка при получении информации по рейсам"
                );
            }

            if (getRoutesResult.Data.Count == 0)
            {
                return CommonHttpHelper.BuildSuccessResponse<IList<AvailableRoutesForCreateBrigadeViewModel>>(
                    new List<AvailableRoutesForCreateBrigadeViewModel>(),
                    HttpStatusCode.OK
                );
            }

            var excludeRouteIds = await _brigadeStorage.GetRouteIdsFromBrigadesWithFilterAsync(contract.Date.Date);

            var result = getRoutesResult.Data
                .Where(x => !excludeRouteIds.Contains(x.Id))
                .Select(x => new AvailableRoutesForCreateBrigadeViewModel
                {
                    Id = x.Id,
                    RouteName = x.RouteName
                })
                .ToList();

            return CommonHttpHelper.BuildSuccessResponse<IList<AvailableRoutesForCreateBrigadeViewModel>>(result, HttpStatusCode.OK);
        }

        private IEnumerable<CommonBrigadeViewModel> CreateCommonBrigadeViewModel(
            IList<(Brigade Brigade, IList<AuxiliaryWorker> AuxWorkers, IList<int> RouteIds)> brigades,
            IDictionary<int, string> routesDict,
            IDictionary<int, TransportWorkShiftViewModel> workShiftsDict
        )
        {
            foreach (var brigade in brigades)
            {
                if (!workShiftsDict.TryGetValue(brigade.Brigade.WorkShiftId, out var getResult))
                    continue;

                yield return new CommonBrigadeViewModel
                {
                    Id = brigade.Brigade.Id,
                    WorkShiftInfo = $"{getResult.TransportName} {getResult.RegNumber} {getResult.DriverFio}",
                    RouteInfo = brigade.RouteIds.GetRouteInfoString(routesDict),
                    AuxWorkersInfo = string.Join(';', brigade.AuxWorkers.DistinctBy(x => x.Id).Select(x => (x.Surname, x.Name, x.Patronymic).GetFio()))
                };
            }
        }
    }
}
