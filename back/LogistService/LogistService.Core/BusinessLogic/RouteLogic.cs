using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using GreenCity.CommonUtils.CommonExtensions;
using GreenCity.Http;
using GreenCity.Http.Helpers;
using LogistService.Contracts.DataContracts.Route;
using LogistService.Contracts.Filters;
using LogistService.Contracts.ViewModels;
using LogistService.Database.Interfaces;
using MissionService.Contracts.DataContracts.Mission;
using MissionService.Contracts.Interface;
using SDContracts.DataContracts.Stand;
using SDContracts.Interfaces;
using TransportContracts.DataContracts.TransportWorkShift;
using TransportContracts.Interfaces;
using TransportContracts.ViewModels;

namespace LogistService.Core.BusinessLogic
{
    public class RouteLogic
    {
        private readonly IRouteStorage _routeStorage;
        private readonly IMissionApi _missionApi;
        private readonly ITransportApi _transportApi;
        private readonly IStandApi _standApi;
        private readonly IMapper _mapper;

        public RouteLogic(
            IRouteStorage routeStorage,
            IMissionApi missionApi,
            ITransportApi transportApi,
            IStandApi standApi,
            IMapper mapper
        )
        {
            _routeStorage = routeStorage;
            _missionApi = missionApi;
            _transportApi = transportApi;
            _standApi = standApi;
            _mapper = mapper;
        }

        public async Task<CommonHttpResponse<RouteViewModel>> CreateRouteAsync(CreateRouteContract contract)
        {
            if (contract == null)
                return CommonHttpHelper.BuildErrorResponse<RouteViewModel>(initialError: "Пустой запрос");

            try
            {
                //!_! Проверяем наличие КП
                if (contract.StandIds == default || contract.StandIds.Count == 0)
                {
                    return CommonHttpHelper.BuildErrorResponse<RouteViewModel>(initialError:
                        "Не выбраны контейнерные площадки");
                }

                // //!_! Меняем статус экипажа
                // var changeWorkshiftRouteStatus = await _transportApi.ChangeWorkshiftRouteStatusAsync(
                //     new ChangeWorkshiftRouteStatusContract
                //         { RouteStatus = true, WorkshiftIds = new List<int> { contract.TransportWorkShiftId } });
                //
                // if (!changeWorkshiftRouteStatus.Success)
                // {
                //     return CommonHttpHelper.BuildErrorResponse<RouteViewModel>(
                //         initialError: "Ошибка при запросе к сервису таксировщика",
                //         extErrors: changeWorkshiftRouteStatus.Errors?.ToList());
                // }

                var workShiftViewModel = await GetWorkShiftViewModel(contract.TransportWorkShiftId, contract.Date);

                if (!workShiftViewModel.Success)
                {
                    return CommonHttpHelper.BuildErrorResponse<RouteViewModel>(
                        initialError: "Ошибка при получении информации по экипажам",
                        extErrors: workShiftViewModel.Errors?.ToList()
                    );
                }

                //!_! Создание записи
                var result = await _routeStorage.CreateRouteAsync(contract);

                //!_! Создаем контракт для сервиса миссий на основе созданной записи
                var bindMissionsContract = _mapper.Map<BindMissionsToRouteByStandsContract>(result);
                //!_! Добавляемые поддерживаемые типы контейнеров
                bindMissionsContract.ContainerTypeIds = workShiftViewModel?.Data.TankTypes;

                //!_! Делаем запрос в сервис миссий для генерации миссий
                var bindMissions = await _missionApi.BindMissionsToRouteByStandsAsync(bindMissionsContract);

                if (!bindMissions.Success)
                {
                    await DeleteRouteAsync(new DeleteRouteContract { Id = result.Id });
                    return CommonHttpHelper.BuildErrorResponse<RouteViewModel>(
                        initialError: "Ошибка при запросе к сервису заявок на вывоз",
                        extErrors: bindMissions.Errors?.ToList());
                }

                return CommonHttpHelper.BuildSuccessResponse(result, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CommonHttpHelper.BuildErrorResponse<RouteViewModel>(
                    HttpStatusCode.InternalServerError,
                    ex.ToExceptionDetails(),
                    $"Ошибка выполнения метода {nameof(CreateRouteAsync)} ReqId : {contract.RequestId}");
            }
        }

        public async Task<CommonHttpResponse<IList<FilteredRouteViewModel>>> GetFilteredRoutesAsync(
            RouteFilterModel filter)
        {
            if (filter == default)
                return CommonHttpHelper
                    .BuildErrorResponse<IList<FilteredRouteViewModel>>(initialError: "Пустой запрос");

            try
            {
                var result = filter.IsFilter
                    ? await _routeStorage.GetFilteredRoutesAsync(filter)
                    : await _routeStorage.GetRoutesWithoutFilterAsync(filter);

                if (result == default || result.Count == 0)
                {
                    return CommonHttpHelper.BuildSuccessResponse<IList<FilteredRouteViewModel>>(new List<FilteredRouteViewModel>(), HttpStatusCode.OK);
                }

                //!_! Делаем запрос в сервис таксировщика для получения информации о машине и водителе по дате
                var actualWorkShits =
                    await _transportApi.GetActualWorkShitsAsync(_mapper.Map<GetActualWorkShitsContract>(filter));

                if (!actualWorkShits.Success || actualWorkShits.Data == default)
                {
                    return CommonHttpHelper.BuildErrorResponse<IList<FilteredRouteViewModel>>(
                        initialError: "Ошибка при запросе сведений из сервиса таксировщика",
                        extErrors: actualWorkShits.Errors?.ToList());
                }

                //!_! Если в фильтре указаны идшники экипажей дополнительно отбираем по ним
                var workShiftViewModels =
                    !filter.TransportWorkShiftIds.IsNullOrEmpty() && filter.TransportWorkShiftIds.Count > 0
                        ? actualWorkShits.Data
                            .Where(w => filter.TransportWorkShiftIds.Contains(w.Id)).ToList()
                        : actualWorkShits.Data;

                //!_! Получаем рейсы по ИД, чтобы узнать ИД прикрепленного экипажа для каждого рейса
                var routes = await GetRoutes(result.Select(x => x.Id).ToList());

                //!_! Для каждого рейса прикрепляем информацию из сервиса таксировщика
                foreach (var routeViewModel in result)
                {
                    //!_! Отбираем экипаж по ИД
                    var workShift =
                        workShiftViewModels.FirstOrDefault(x =>
                            x.Id == routes.Data.FirstOrDefault(r => r.Id == routeViewModel.Id)!.TransportWorkShiftId);

                    if (workShift != null)
                    {
                        routeViewModel.CarInfo = $"{workShift.TransportName} {workShift.RegNumber}";
                        routeViewModel.DriverInfo = workShift.DriverFio;
                    }
                }

                return CommonHttpHelper.BuildSuccessResponse(result, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CommonHttpHelper.BuildErrorResponse<IList<FilteredRouteViewModel>>(
                    HttpStatusCode.InternalServerError,
                    ex.ToExceptionDetails(),
                    $"Ошибка выполнения метода {nameof(GetFilteredRoutesAsync)}");
            }
        }

        public async Task<CommonHttpResponse<IList<RouteViewModel>>> GetRoutesAsync(GetRouteContract contract)
        {
            if (contract?.Ids == null || contract.Ids.Count == 0)
                return CommonHttpHelper.BuildErrorResponse<IList<RouteViewModel>>(initialError: "Пустой запрос");

            try
            {
                var routesViewModel = await GetRoutes(contract.Ids);

                if (!contract.NeedAttachStands)
                {
                    return CommonHttpHelper.BuildSuccessResponse(routesViewModel.Data, HttpStatusCode.OK);
                }

                var routes = routesViewModel.Data;

                foreach (var route in routes)
                {
                    //!_! Делаем запрос в АО для получения площадок
                    var stands = await _standApi.GetStandsByRouteIdAsync(new GetStandsForRouteByRouteIdContract
                    { RouteId = route.Id });

                    if (!stands.Success || stands.Data == default)
                    {
                        return CommonHttpHelper.BuildErrorResponse<IList<RouteViewModel>>(
                            initialError: "Ошибка при запросе сведений из АО",
                            extErrors: stands.Errors?.ToList());
                    }

                    route.StandIds = stands.Data.Select(s => s.StandId).ToList();
                }

                return CommonHttpHelper.BuildSuccessResponse(routes, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CommonHttpHelper.BuildErrorResponse<IList<RouteViewModel>>(
                    HttpStatusCode.InternalServerError,
                    ex.ToExceptionDetails(),
                    $"Ошибка выполнения метода {nameof(GetRoutesAsync)}");
            }
        }

        public async Task<CommonHttpResponse<RouteViewModel>> UpdateRouteAsync(UpdateRouteContract contract)
        {
            if (contract == null)
                return CommonHttpHelper.BuildErrorResponse<RouteViewModel>(initialError: "Пустой запрос");

            //!_! Получаем рейс по ИД, чтобы узнать ИД прикрепленного экипажа и дату
            var routes = await GetRoutes(new List<int> { contract.Id });
            var route = routes.Data.FirstOrDefault();

            if (route.RouteCompleted)
            {
                return CommonHttpHelper.BuildErrorResponse<RouteViewModel>(
                    initialError: "Завершенные рейсы нельзя редактировать"
                );
            }

            try
            {
                //!_! Проверяем наличие КП
                if (contract.StandIds == default || contract.StandIds.Count == 0)
                {
                    return CommonHttpHelper.BuildErrorResponse<RouteViewModel>(initialError:
                        "Не выбраны контейнерные площадки");
                }

                //!_! Делаем запрос в сервис таксировщика для получения экипажей по дате 
                var workShiftViewModel = await GetWorkShiftViewModel(route.TransportWorkShiftId, route.Date);

                if (!workShiftViewModel.Success)
                {
                    return CommonHttpHelper.BuildErrorResponse<RouteViewModel>(
                        initialError: "Ошибка при получении информации по экипажам",
                        extErrors: workShiftViewModel.Errors?.ToList()
                    );
                }

                //!_! Создаем контракт для сервиса миссий на основе созданной записи
                var updateMissionContract = _mapper.Map<UpdateMissionBindingsByRouteIdContract>(route);
                //!_! Добавляемые поддерживаемые типы контейнеров
                updateMissionContract.ContainerTypeIds = workShiftViewModel.Data.TankTypes;
                updateMissionContract.StandIds = contract.StandIds;

                //!_! Делаем запрос в сервис миссий для обновления миссий
                var updateMissionBindings =
                    await _missionApi.UpdateMissionBindingsByRouteIdAsync(updateMissionContract);

                if (!updateMissionBindings.Success)
                {
                    return CommonHttpHelper.BuildErrorResponse<RouteViewModel>(
                        initialError: "Ошибка при запросе к сервису заявок на вывоз",
                        extErrors: updateMissionBindings.Errors?.ToList());
                }

                //!_! Обновление записи
                var result = await _routeStorage.UpdateRouteAsync(contract);

                return CommonHttpHelper.BuildSuccessResponse(result, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CommonHttpHelper.BuildErrorResponse<RouteViewModel>(
                    HttpStatusCode.InternalServerError,
                    ex.ToExceptionDetails(),
                    $"Ошибка выполнения метода {nameof(CreateRouteAsync)} ReqId : {contract.RequestId}");
            }
        }


        public async Task<CommonHttpResponse> DeleteRouteAsync(DeleteRouteContract contract)
        {
            if (contract?.Id == null)
                return CommonHttpHelper.BuildErrorResponse(initialError: "Пустой запрос");

            //!_! Получаем рейс по ИД, чтобы узнать ИД прикрепленного экипажа
            //var routes = await GetRoutes(new List<int> { contract.Id });
            try
            {
                //TODO Выпилить это скорее всего
                // //!_! Меняем статус экипажа
                // var changeWorkshiftRouteStatus = await _transportApi.ChangeWorkshiftRouteStatusAsync(
                //     new ChangeWorkshiftRouteStatusContract
                //     {
                //         RouteStatus = false,
                //         WorkshiftIds = new List<int> { routes.Data.FirstOrDefault().TransportWorkShiftId }
                //     });
                //
                // if (!changeWorkshiftRouteStatus.Success)
                // {
                //     return CommonHttpHelper.BuildErrorResponse<RouteViewModel>(
                //         initialError: "Ошибка при запросе к сервису таксировщика",
                //         extErrors: changeWorkshiftRouteStatus.Errors?.ToList());
                // }

                //!_! Делаем запрос в сервис миссий для отвязки от рейса
                var unbindMissions = await _missionApi.UnbindMissionsFromRouteAsync(contract.Id);

                if (!unbindMissions.Success)
                {
                    return CommonHttpHelper.BuildErrorResponse<RouteViewModel>(
                        initialError: "Ошибка при запросе к сервису заявок на вывоз",
                        extErrors: unbindMissions.Errors?.ToList());
                }

                if (await _routeStorage.DeleteRouteAsync(contract))
                {
                    return CommonHttpHelper.BuildSuccessResponse(HttpStatusCode.OK);
                }

                return CommonHttpHelper.BuildErrorResponse(HttpStatusCode.NotFound, null, "Запись не найдена");
            }
            catch (Exception ex)
            {
                return CommonHttpHelper.BuildErrorResponse(
                    HttpStatusCode.InternalServerError,
                    ex.ToExceptionDetails(),
                    $"Ошибка выполнения метода {nameof(DeleteRouteAsync)}");
            }
        }

        public async Task<CommonHttpResponse> CloseRouteAsync(CloseRouteContract contract)
        {
            if (contract?.Id == null)
                return CommonHttpHelper.BuildErrorResponse(initialError: "Пустой запрос");

            //!_! Получаем рейс по ИД, чтобы узнать ИД прикрепленного экипажа и дату
            var routes = await GetRoutes(new List<int> { contract.Id });
            var route = routes.Data.FirstOrDefault();

            if (route.RouteCompleted)
            {
                return CommonHttpHelper.BuildErrorResponse<RouteViewModel>(
                    initialError: "Рейс уже завершен"
                );
            }

            try
            {
                if (await _routeStorage.CloseRouteAsync(contract))
                {
                    return CommonHttpHelper.BuildSuccessResponse(HttpStatusCode.OK);
                }

                return CommonHttpHelper.BuildErrorResponse(HttpStatusCode.NotFound, null, "Запись не найдена");
            }
            catch (Exception ex)
            {
                return CommonHttpHelper.BuildErrorResponse(
                    HttpStatusCode.InternalServerError,
                    ex.ToExceptionDetails(),
                    $"Ошибка выполнения метода {nameof(DeleteRouteAsync)}");
            }
        }

        public async Task<CommonHttpResponse<IList<GetRoutesByWorkShiftsIdsViewModel>>> GetRoutesByWorkShiftsIdsAsync(
            GetRoutesByWorkShiftsIdsContract contract)
        {
            if (contract?.WorkShiftsIds == null || contract.WorkShiftsIds.Count == 0)
                return CommonHttpHelper.BuildErrorResponse<IList<GetRoutesByWorkShiftsIdsViewModel>>(
                    initialError: "Пустой запрос");

            try
            {
                var routes = await _routeStorage.GetRoutesByWorkShiftsIdsAsync(contract);

                if (routes == null)
                    return CommonHttpHelper.BuildErrorResponse<IList<GetRoutesByWorkShiftsIdsViewModel>>(
                        HttpStatusCode.NotFound, null, "Запись не найдена");

                foreach (var route in routes)
                {
                    //!_! Делаем запрос в АО для получения площадок
                    var standsResponse = await _standApi.GetStandsByRouteIdAsync(new GetStandsForRouteByRouteIdContract
                    { RouteId = route.Id });

                    var stands = standsResponse.Data;

                    if (!standsResponse.Success || stands == default)
                    {
                        return CommonHttpHelper.BuildErrorResponse<IList<GetRoutesByWorkShiftsIdsViewModel>>(
                            initialError: "Ошибка при запросе сведений из АО",
                            extErrors: standsResponse.Errors?.ToList());
                    }

                    route.Stands = stands.Select(s => Tuple.Create(s.StandId, s.StandAddress)).ToList();
                }

                return CommonHttpHelper.BuildSuccessResponse(routes, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CommonHttpHelper.BuildErrorResponse<IList<GetRoutesByWorkShiftsIdsViewModel>>(
                    HttpStatusCode.InternalServerError,
                    ex.ToExceptionDetails(),
                    $"Ошибка выполнения метода {nameof(GetRoutesByWorkShiftsIdsAsync)}");
            }
        }

        private async Task<CommonHttpResponse<IList<RouteViewModel>>> GetRoutes(IList<int> ids)
        {
            var routes =
                await _routeStorage.GetRoutesAsync(new GetRouteContract { Ids = ids });

            if (routes == null || routes.Count == 0)
            {
                return CommonHttpHelper.BuildErrorResponse<IList<RouteViewModel>>(HttpStatusCode.NotFound, null,
                    "Запись не найдена");
            }

            return CommonHttpHelper.BuildSuccessResponse(routes, HttpStatusCode.OK);
        }

        private async Task<CommonHttpResponse<TransportWorkShiftViewModel>> GetWorkShiftViewModel(
            int transportWorkShiftId, DateTime date)
        {
            //!_! Делаем запрос в сервис таксировщика для получения экипажей по дате 
            var actualWorkShits =
                await _transportApi.GetActualWorkShitsAsync(new GetActualWorkShitsContract
                { DateTime = date.Date, IsFilterByUser = false });

            if (!actualWorkShits.Success || actualWorkShits.Data == default)
            {
                return CommonHttpHelper.BuildErrorResponse<TransportWorkShiftViewModel>(
                    initialError: "Ошибка при запросе сведений из сервиса таксировщика",
                    extErrors: actualWorkShits.Errors?.ToList());
            }

            if (actualWorkShits.Data.Count == 0)
            {
                return CommonHttpHelper.BuildErrorResponse<TransportWorkShiftViewModel>(
                    initialError: "Экипажи не найдены",
                    statusCode: HttpStatusCode.NotFound
                );
            }

            //!_! Отбираем экипаж по ИД
            var workShiftViewModel =
                actualWorkShits.Data.FirstOrDefault(w => w.Id == transportWorkShiftId);

            return CommonHttpHelper.BuildSuccessResponse(workShiftViewModel, HttpStatusCode.OK);
        }
    }
}