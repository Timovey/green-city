using ExternalApi.Interface.Models.Dispatcher.AuxiliaryWorker;
using ExternalApiService.Operations.Interface;
using GreenCity.Http;
using GreenCity.Http.Helpers;
using System.Threading.Tasks;
using AutoMapper;
using DispatcherService.Contracts.Interfaces;
using DispatcherService.Contracts.Filters;
using System.Linq;
using DispatcherService.Contracts.DataContracts.AuxiliaryWorker;
using ExternalApi.Interface.Models.Dispatcher.Brigade;
using DispatcherService.Contracts.DataContracts.Brigade;
using ExternalApi.Interface.Models.Dispatcher.TravelSheet;
using System.Collections.Generic;
using DispatcherService.Contracts.DataContracts.TravelSheet;
using ExternalApiService.Extensions;
using ExternalApi.Interface.Models.Dispatcher.ShiftTask;
using DispatcherService.Contracts.DataContracts.ShiftTask;
using MissionService.Contracts.Interface;
using ExternalApi.Interface.Models.Dispatcher.Mission;
using MissionService.Contracts.DataContracts.Mission;
using ExternalApi.Interface.Models.Dispatcher.Route;
using System.Net;
using System.Security.Claims;
using AuthContracts.Constants;
using LogistService.Contracts.Interfaces;
using ExternalApiService.Extensions.ApiExtensions;

namespace ExternalApiService.Operations
{
    public class DispatcherOperations : IDispatcherOperations
    {
        private readonly IDispatcherApi _dispatcherApi;
        private readonly IShiftTaskApi _shiftTaskApi;
        private readonly ITravelSheetApi _travelSheetApi;
        private readonly IMissionApi _missionApi;
        private readonly IRouteApi _routeApi;
        private readonly IMapper _mapper;

        public DispatcherOperations(
            IDispatcherApi dispatcherApi,
            IShiftTaskApi shiftTaskApi,
            ITravelSheetApi travelSheetApi,
            IMissionApi missionApi,
            IRouteApi routeApi,
            IMapper mapper
        )
        {
            _dispatcherApi = dispatcherApi;
            _shiftTaskApi = shiftTaskApi;
            _travelSheetApi = travelSheetApi;
            _missionApi = missionApi;
            _routeApi = routeApi;
            _mapper = mapper;
        }

        #region [ AuxWorkers ]

        public async Task<CommonHttpResponse<ExtAuxiliaryWorkerViewModel[]>> GetFilteredAuxiliaryWorkersAsync(ExtAuxiliaryWorkerFilterRequest filter)
        {
            if (filter == null)
                return CommonHttpHelper.BuildErrorResponse<ExtAuxiliaryWorkerViewModel[]>(initialError: "Пустой запрос");

            var auxiliaryWorkerFilter = _mapper.Map<AuxiliaryWorkerFilterModel>(filter);
            var result = await _dispatcherApi.GetFilteredAuxiliaryWorkersAsync(auxiliaryWorkerFilter);

            if (result.Success)
            {
                return CommonHttpHelper.BuildSuccessResponse(
                    result.Data.Select(_mapper.Map<ExtAuxiliaryWorkerViewModel>).ToArray(),
                    System.Net.HttpStatusCode.OK);
            }

            return CommonHttpHelper.BuildErrorResponse<ExtAuxiliaryWorkerViewModel[]>(result.StatusCode, result.Errors.GetErrorsOrDefault());
        }

        public async Task<CommonHttpResponse<ExtAuxiliaryWorkerViewModel>> GetAuxiliaryWorkerAsync(ExtGetAuxiliaryWorkerRequest request)
        {
            if (request == null)
                return CommonHttpHelper.BuildErrorResponse<ExtAuxiliaryWorkerViewModel>(initialError: "Пустой запрос");

            var getAuxiliaryWorker = _mapper.Map<GetAuxiliaryWorkerContract>(request);
            var result = await _dispatcherApi.GetAuxiliaryWorkerAsync(getAuxiliaryWorker);

            if (result.Success)
            {
                return CommonHttpHelper.BuildSuccessResponse(
                    _mapper.Map<ExtAuxiliaryWorkerViewModel>(result.Data),
                    System.Net.HttpStatusCode.OK);
            }

            return CommonHttpHelper.BuildErrorResponse<ExtAuxiliaryWorkerViewModel>(result.StatusCode, result.Errors.GetErrorsOrDefault());
        }

        public async Task<CommonHttpResponse<ExtAuxiliaryWorkerViewModel>> CreateAuxiliaryWorkerAsync(ExtCreateAuxiliaryWorkerRequest request)
        {
            if (request == null)
                return CommonHttpHelper.BuildErrorResponse<ExtAuxiliaryWorkerViewModel>(initialError: "Пустой запрос");

            var createAuxiliaryWorker = _mapper.Map<CreateAuxiliaryWorkerContract>(request);
            var result = await _dispatcherApi.CreateAuxiliaryWorkerAsync(createAuxiliaryWorker);

            if (result.Success)
            {
                return CommonHttpHelper.BuildSuccessResponse(
                    _mapper.Map<ExtAuxiliaryWorkerViewModel>(result.Data),
                    System.Net.HttpStatusCode.OK);
            }

            return CommonHttpHelper.BuildErrorResponse<ExtAuxiliaryWorkerViewModel>(result.StatusCode, result.Errors.GetErrorsOrDefault());
        }

        public async Task<CommonHttpResponse<ExtAuxiliaryWorkerViewModel>> UpdateAuxiliaryWorkerAsync(ExtUpdateAuxiliaryWorkerRequest request)
        {
            if (request == null)
                return CommonHttpHelper.BuildErrorResponse<ExtAuxiliaryWorkerViewModel>(initialError: "Пустой запрос");

            var updateAuxiliaryWorker = _mapper.Map<UpdateAuxiliaryWorkerContract>(request);
            var result = await _dispatcherApi.UpdateAuxiliaryWorkerAsync(updateAuxiliaryWorker);

            if (result.Success)
            {
                return CommonHttpHelper.BuildSuccessResponse(
                    _mapper.Map<ExtAuxiliaryWorkerViewModel>(result.Data),
                    System.Net.HttpStatusCode.OK);
            }

            return CommonHttpHelper.BuildErrorResponse<ExtAuxiliaryWorkerViewModel>(result.StatusCode, result.Errors.GetErrorsOrDefault());
        }

        public async Task<CommonHttpResponse> DeleteAuxiliaryWorkerAsync(ExtDeleteAuxiliaryWorkerRequest request)
        {
            if (request == null)
                return CommonHttpHelper.BuildErrorResponse(initialError: "Пустой запрос");

            var deleteAuxiliaryWorker = _mapper.Map<DeleteAuxiliaryWorkerContract>(request);
            var result = await _dispatcherApi.DeleteAuxiliaryWorkerAsync(deleteAuxiliaryWorker);

            if (result.Success)
            {
                return CommonHttpHelper.BuildSuccessResponse();
            }

            return CommonHttpHelper.BuildErrorResponse(result.StatusCode, result.Errors.GetErrorsOrDefault());
        }

        #endregion

        #region [ Brigades ]

        public async Task<CommonHttpResponse<ExtCreateBrigadeResponse>> CreateBrigadeAsync(ExtCreateBrigadeRequest request)
        {
            if (request == null)
                return CommonHttpHelper.BuildErrorResponse<ExtCreateBrigadeResponse>(initialError: "Пустой запрос");

            var contract = _mapper.Map<CreateBrigadeContract>(request);
            var result = await _dispatcherApi.CreateBrigadeAsync(contract);

            if (result.Success)
            {
                var response = _mapper.Map<ExtCreateBrigadeResponse>(request);
                response.Id = result.Data;
                return CommonHttpHelper.BuildSuccessResponse(
                    response,
                    HttpStatusCode.OK);
            }

            return CommonHttpHelper.BuildErrorResponse<ExtCreateBrigadeResponse>(result.StatusCode, result.Errors.GetErrorsOrDefault());
        }

        public async Task<CommonHttpResponse<ExtCommonBrigadeViewModel[]>> GetFilteredBrigadesAsync(ExtCommonBrigadesFilterRequest request)
        {
            if (request == null)
                return CommonHttpHelper.BuildErrorResponse<ExtCommonBrigadeViewModel[]>(initialError: "Пустой запрос");

            var filter = _mapper.Map<CommonBrigadesFilterModel>(request);
            var result = await _dispatcherApi.GetFilteredBrigadesAsync(filter);

            if (result.Success)
            {
                return CommonHttpHelper.BuildSuccessResponse(
                    _mapper.Map<ExtCommonBrigadeViewModel[]>(result.Data),
                    System.Net.HttpStatusCode.OK);
            }

            return CommonHttpHelper.BuildErrorResponse<ExtCommonBrigadeViewModel[]>(result.StatusCode, result.Errors.GetErrorsOrDefault());
        }

        public async Task<CommonHttpResponse> DeleteBrigadesAsync(ExtDeleteBrigadesRequest request)
        {
            if (request == null)
                return CommonHttpHelper.BuildErrorResponse(initialError: "Пустой запрос");

            var contract = _mapper.Map<DeleteBrigadesContract>(request);
            var result = await _dispatcherApi.DeleteBrigadesAsync(contract);

            if (result.Success)
            {
                return CommonHttpHelper.BuildSuccessResponse();
            }

            return CommonHttpHelper.BuildErrorResponse(result.StatusCode, result.Errors.GetErrorsOrDefault());
        }

        #endregion

        #region [ TravelSheets ]

        public async Task<CommonHttpResponse<ExtCreateTravelSheetResponse>> CreateTravelSheetAsync(ExtCreateTravelSheetRequest request)
        {
            if (request == null)
                return CommonHttpHelper.BuildErrorResponse<ExtCreateTravelSheetResponse>(initialError: "Пустой запрос");

            var contract = _mapper.Map<CreateTravelSheetContract>(request);
            var result = await _travelSheetApi.CreateTravelSheetAsync(contract);

            if (result.Success)
            {
                var response = _mapper.Map<ExtCreateTravelSheetResponse>(request);
                response.Id = result.Data;
                return CommonHttpHelper.BuildSuccessResponse(
                    response,
                    HttpStatusCode.OK);
            }

            return CommonHttpHelper.BuildErrorResponse<ExtCreateTravelSheetResponse>(result.StatusCode, result.Errors.GetErrorsOrDefault());
        }

        public async Task<CommonHttpResponse<ExtTravelSheetForFrontViewModel>> GetTravelSheetByIdAsync(ExtGetTravelSheetByIdRequest request)
        {
            if (request == null)
                return CommonHttpHelper.BuildErrorResponse<ExtTravelSheetForFrontViewModel>(initialError: "Пустой запрос");

            var result = await _travelSheetApi.GetTravelSheetByIdsForFrontAsync(new List<int> { request.Id });

            if (result.Success)
            {
                return CommonHttpHelper.BuildSuccessResponse(
                    _mapper.Map<ExtTravelSheetForFrontViewModel>(result.Data.FirstOrDefault()),
                    System.Net.HttpStatusCode.OK);
            }

            return CommonHttpHelper.BuildErrorResponse<ExtTravelSheetForFrontViewModel>(result.StatusCode, result.Errors.GetErrorsOrDefault());
        }

        public async Task<CommonHttpResponse<ExtCommonTravelSheetViewModel[]>> GetCommonFilteredTravelSheetsAsync(
            ExtCommonTravelSheetFilterRequest request
        )
        {
            if (request == null)
                return CommonHttpHelper.BuildErrorResponse<ExtCommonTravelSheetViewModel[]>(initialError: "Пустой запрос");

            var filter = _mapper.Map<CommonTravelSheetFilterModel>(request);
            var result = await _travelSheetApi.GetCommonFilteredTravelSheetsAsync(filter);

            if (result.Success)
            {
                return CommonHttpHelper.BuildSuccessResponse(
                    _mapper.Map<ExtCommonTravelSheetViewModel[]>(result.Data),
                    System.Net.HttpStatusCode.OK);
            }

            return CommonHttpHelper.BuildErrorResponse<ExtCommonTravelSheetViewModel[]>(result.StatusCode, result.Errors.GetErrorsOrDefault());
        }

        public async Task<CommonHttpResponse> DeleteTravelSheetsAsync(ExtDeleteTravelSheetsRequest request)
        {
            if (request == null)
                return CommonHttpHelper.BuildErrorResponse(initialError: "Пустой запрос");

            var contract = _mapper.Map<DeleteTravelSheetsContract>(request);
            contract.TravelSheetIds = new List<int> { request.Id };

            var result = await _travelSheetApi.DeleteTravelSheetsAsync(contract);

            if (result.Success)
            {
                return CommonHttpHelper.BuildSuccessResponse();
            }

            return CommonHttpHelper.BuildErrorResponse(result.StatusCode, result.Errors.GetErrorsOrDefault());
        }

        public async Task<CommonHttpResponse<ExtFixTravelSheetDriveTimesResponse>> FixTravelSheetDriveTimesAsync(
            ExtFixTravelSheetDriveTimesRequest request
        )
        {
            if (request == null)
                return CommonHttpHelper.BuildErrorResponse<ExtFixTravelSheetDriveTimesResponse>(initialError: "Пустой запрос");

            var contract = _mapper.Map<FixTravelSheetDriveTimesContract>(request);

            var result = await _travelSheetApi.FixTravelSheetDriveTimesAsync(contract);

            if (result.Success)
            {
                var response = _mapper.Map<ExtFixTravelSheetDriveTimesResponse>(request);
                return CommonHttpHelper.BuildSuccessResponse(
                    response,
                    System.Net.HttpStatusCode.OK);
            }

            return CommonHttpHelper.BuildErrorResponse<ExtFixTravelSheetDriveTimesResponse>(result.StatusCode, result.Errors.GetErrorsOrDefault());
        }

        #endregion

        #region [ ShiftTasks ]

        public async Task<CommonHttpResponse<ExtCreateShiftTaskResponse>> CreateShiftTaskAsync(ExtCreateShiftTaskRequest request)
        {
            if (request == null)
                return CommonHttpHelper.BuildErrorResponse<ExtCreateShiftTaskResponse>(initialError: "Пустой запрос");

            var contract = _mapper.Map<CreateShiftTaskContract>(request);
            var result = await _shiftTaskApi.CreateShiftTaskAsync(contract);

            if (result.Success && result != default)
            {
                var response = _mapper.Map<ExtCreateShiftTaskResponse>(request);
                response.Id = result.Data.ShiftTaskTaskId;
                response.TravelSheetId = result.Data.TravelSheetId;
                return CommonHttpHelper.BuildSuccessResponse(
                    response,
                    HttpStatusCode.OK);
            }

            return CommonHttpHelper.BuildErrorResponse<ExtCreateShiftTaskResponse>(result.StatusCode, result.Errors.GetErrorsOrDefault());
        }

        public async Task<CommonHttpResponse<ExtCommonShiftTaskViewModel[]>> GetCommonFilteredShiftTasksAsync(
            ExtCommonShiftTasksFilterRequest request
        )
        {
            if (request == null)
                return CommonHttpHelper.BuildErrorResponse<ExtCommonShiftTaskViewModel[]>(initialError: "Пустой запрос");

            var filter = _mapper.Map<CommonShiftTasksFilterModel>(request);
            var result = await _shiftTaskApi.GetCommonFilteredShiftTasksAsync(filter);

            if (result.Success)
            {
                return CommonHttpHelper.BuildSuccessResponse(
                    _mapper.Map<ExtCommonShiftTaskViewModel[]>(result.Data),
                    System.Net.HttpStatusCode.OK);
            }

            return CommonHttpHelper.BuildErrorResponse<ExtCommonShiftTaskViewModel[]>(result.StatusCode, result.Errors.GetErrorsOrDefault());
        }

        public async Task<CommonHttpResponse> DeleteShiftTasksAsync(ExtDeleteShiftTasksRequest request)
        {
            if (request == null)
                return CommonHttpHelper.BuildErrorResponse(initialError: "Пустой запрос");

            var contract = _mapper.Map<DeleteShiftTasksContract>(request);
            contract.ShiftTaskIds = new List<int> { request.Id };

            var result = await _shiftTaskApi.DeleteShiftTasksAsync(contract);

            if (result.Success)
            {
                return CommonHttpHelper.BuildSuccessResponse();
            }

            return CommonHttpHelper.BuildErrorResponse(result.StatusCode, result.Errors.GetErrorsOrDefault());
        }

        #endregion

        #region [ Missions ]

        public async Task<CommonHttpResponse<ExtMissionsForDispatcherViewModel>> GetMissionsForDispatcherByRouteIdAsync(
            ExtGetMissionsByRouteIdRequest request,
            ClaimsPrincipal user
        )
        {
            if (request == null)
                return CommonHttpHelper.BuildErrorResponse<ExtMissionsForDispatcherViewModel>(initialError: "Пустой запрос");

            if (user == default)
                return CommonHttpHelper.BuildErrorResponse<ExtMissionsForDispatcherViewModel>(
                    statusCode: HttpStatusCode.Unauthorized,
                    initialError: "Необходимо пройти авторизацию"
                );

            //!_! Если у пользователя нету роли логист или админ, то надо проверить рейс на завершенность
            if (!(user.IsInOneOfRoles(AuthRolesConstants.ADMIN, AuthRolesConstants.LOGIST)))
            {
                var getRouteResult = await _routeApi.ValidateGetRouteByIdAsync<ExtMissionsForDispatcherViewModel>(request.Id);
                if (!getRouteResult.IsSuccess)
                    return getRouteResult.ErrorResponse;

                if (getRouteResult.Data.RouteCompleted)
                {
                    return CommonHttpHelper.BuildErrorResponse<ExtMissionsForDispatcherViewModel>(
                        HttpStatusCode.Forbidden,
                        initialError: "Завершенный рейс нельзя редактировать, обратитесь к логисту или администартору"
                    );
                }
            }

            var result = await _missionApi.GetMissionsForDispatcherByRouteIdAsync(request.Id);

            if (result.Success)
            {
                return CommonHttpHelper.BuildSuccessResponse(
                    _mapper.Map<ExtMissionsForDispatcherViewModel>(result.Data),
                    HttpStatusCode.OK);
            }

            return CommonHttpHelper.BuildErrorResponse<ExtMissionsForDispatcherViewModel>(result.StatusCode, result.Errors.GetErrorsOrDefault());
        }

        public async Task<CommonHttpResponse<ExtUpdateMissionsForCloseRouteResponse>> UpdateMissionsForCloseRouteAsync(
            ExtUpdateMissionsForCloseRouteRequest request,
            ClaimsPrincipal user
        )
        {
            if (request == null)
                return CommonHttpHelper.BuildErrorResponse<ExtUpdateMissionsForCloseRouteResponse>(initialError: "Пустой запрос");

            if (user == default)
                return CommonHttpHelper.BuildErrorResponse<ExtUpdateMissionsForCloseRouteResponse>(
                    statusCode: HttpStatusCode.Unauthorized,
                    initialError: "Необходимо пройти авторизацию"
                );

            var contract = _mapper.Map<UpdateMissionsForCloseRouteContract>(request);

            //!_! Если у пользователя есть роль логист или админ, то в случае если рейс уже закрыт, то его не нужно закрывать повторно
            if (user.IsInOneOfRoles(AuthRolesConstants.ADMIN, AuthRolesConstants.LOGIST))
            {
                var getRouteResult = await _routeApi.ValidateGetRouteByIdAsync<ExtUpdateMissionsForCloseRouteResponse>(request.Id);
                if (!getRouteResult.IsSuccess)
                    return getRouteResult.ErrorResponse;

                if (getRouteResult.Data.RouteCompleted)
                    contract.NeedCloseRoute = false;
            }

            var result = await _missionApi.UpdateMissionsForCloseRouteAsync(contract);

            if (result.Success)
            {
                return CommonHttpHelper.BuildSuccessResponse(
                    _mapper.Map<ExtUpdateMissionsForCloseRouteResponse>(request),
                    HttpStatusCode.OK
                );
            }

            return CommonHttpHelper.BuildErrorResponse<ExtUpdateMissionsForCloseRouteResponse>(result.StatusCode, result.Errors.GetErrorsOrDefault());
        }

        #endregion

        #region [ Dropdowns ]

        public async Task<CommonHttpResponse<ExtAvailableRouteForCreateBrigadeViewModel[]>> GetAvailableRoutesForCreateBrigadeAsync(
            ExtGetAvailableRoutesForCreateBrigadeRequest request
        )
        {
            if (request == null)
                return CommonHttpHelper.BuildErrorResponse<ExtAvailableRouteForCreateBrigadeViewModel[]>(initialError: "Пустой запрос");

            var contract = _mapper.Map<GetAvailableRoutesForCreateBrigadeContract>(request);
            var result = await _dispatcherApi.GetAvailableRoutesForCreateBrigadeAsync(contract);

            if (result.Success)
            {
                return CommonHttpHelper.BuildSuccessResponse(
                    _mapper.Map<ExtAvailableRouteForCreateBrigadeViewModel[]>(result.Data),
                    HttpStatusCode.OK);
            }

            return CommonHttpHelper.BuildErrorResponse<ExtAvailableRouteForCreateBrigadeViewModel[]>(
                result.StatusCode,
                result.Errors.GetErrorsOrDefault()
            );
        }

        public async Task<CommonHttpResponse<ExtAvailableRouteForCreateShiftTaskViewModel[]>> GetAvailableRouteForCreateShiftTaskAsync(
            ExtGetAvailableRoutesForCreateShiftTaskRequest request
        )
        {
            if (request == null)
                return CommonHttpHelper.BuildErrorResponse<ExtAvailableRouteForCreateShiftTaskViewModel[]>(initialError: "Пустой запрос");

            var contract = _mapper.Map<GetAvailableRoutesForCreateShiftTaskContract>(request);
            var result = await _shiftTaskApi.GetAvailableRouteForCreateShiftTaskAsync(contract);

            if (result.Success)
            {
                return CommonHttpHelper.BuildSuccessResponse(
                    _mapper.Map<ExtAvailableRouteForCreateShiftTaskViewModel[]>(result.Data),
                    HttpStatusCode.OK);
            }

            return CommonHttpHelper.BuildErrorResponse<ExtAvailableRouteForCreateShiftTaskViewModel[]>(
                result.StatusCode,
                result.Errors.GetErrorsOrDefault()
            );
        }

        public async Task<CommonHttpResponse<ExtOrgsDropDownForCreateShiftTaskViewModel[]>> GetOrgsDropDownForCreateShiftTaskAsync(
            ExtGetOrgsDropDownForCreateShiftTaskRequest request
        )
        {
            if (request == null)
                return CommonHttpHelper.BuildErrorResponse<ExtOrgsDropDownForCreateShiftTaskViewModel[]>(initialError: "Пустой запрос");

            var contract = _mapper.Map<GetOrgsDropDownForCreateShiftTaskContract>(request);
            contract.WorkShiftIds = new List<int> { request.WorkShiftId };

            var result = await _shiftTaskApi.GetOrgsDropDownForCreateShiftTaskAsync(contract);

            if (result.Success)
            {
                return CommonHttpHelper.BuildSuccessResponse(
                    _mapper.Map<ExtOrgsDropDownForCreateShiftTaskViewModel[]>(result.Data),
                    HttpStatusCode.OK);
            }

            return CommonHttpHelper.BuildErrorResponse<ExtOrgsDropDownForCreateShiftTaskViewModel[]>(
                result.StatusCode,
                result.Errors.GetErrorsOrDefault()
            );
        }

        #endregion
    }
}
