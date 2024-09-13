using AuthContracts.Attributres;
using AuthContracts.Constants;
using ExternalApi.Interface.Models.Dispatcher.AuxiliaryWorker;
using ExternalApi.Interface.Models.Dispatcher.Brigade;
using ExternalApi.Interface.Models.Dispatcher.Mission;
using ExternalApi.Interface.Models.Dispatcher.Route;
using ExternalApi.Interface.Models.Dispatcher.ShiftTask;
using ExternalApi.Interface.Models.Dispatcher.TravelSheet;
using ExternalApiService.Operations.Interface;
using GreenCity.Http;
using GreenCity.Http.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace ExternalApiService.Controllers
{
    [AuthRoles(AuthRolesConstants.ADMIN, AuthRolesConstants.DISPATCHER)]
    [ApiController]
    [Route("api/[controller]/[action]")]
    [ModelStateValidationActionFilter]
    public class DispatcherController : ControllerBase
    {
        private readonly ILogger<DispatcherController> _logger;
        private readonly IDispatcherOperations _dispatcherOperations;

        public DispatcherController(
            IDispatcherOperations dispatcherOperations,
            ILogger<DispatcherController> logger
        )
        {
            _dispatcherOperations = dispatcherOperations;
            _logger = logger;
        }

        [HttpPost]
        public async Task<CommonHttpResponse<ExtAuxiliaryWorkerViewModel[]>> GetFilteredAuxiliaryWorkers(ExtAuxiliaryWorkerFilterRequest filter)
        {
            return await _dispatcherOperations.GetFilteredAuxiliaryWorkersAsync(filter);
        }

        [HttpPost]
        public async Task<CommonHttpResponse<ExtAuxiliaryWorkerViewModel>> GetAuxiliaryWorker(ExtGetAuxiliaryWorkerRequest request)
        {
            return await _dispatcherOperations.GetAuxiliaryWorkerAsync(request);
        }

        [HttpPost]
        public async Task<CommonHttpResponse<ExtAuxiliaryWorkerViewModel>> CreateAuxiliaryWorker(ExtCreateAuxiliaryWorkerRequest request)
        {
            return await _dispatcherOperations.CreateAuxiliaryWorkerAsync(request);
        }

        [HttpPost]
        public async Task<CommonHttpResponse<ExtAuxiliaryWorkerViewModel>> UpdateAuxiliaryWorker(ExtUpdateAuxiliaryWorkerRequest request)
        {
            return await _dispatcherOperations.UpdateAuxiliaryWorkerAsync(request);
        }

        [HttpPost]
        public async Task<CommonHttpResponse> DeleteAuxiliaryWorker(ExtDeleteAuxiliaryWorkerRequest request)
        {
            return await _dispatcherOperations.DeleteAuxiliaryWorkerAsync(request);
        }

        [HttpPost]
        public Task<CommonHttpResponse<ExtCreateBrigadeResponse>> CreateBrigadeAsync(ExtCreateBrigadeRequest request)
        {
            return _dispatcherOperations.CreateBrigadeAsync(request);
        }

        [HttpPost]
        public Task<CommonHttpResponse<ExtCommonBrigadeViewModel[]>> GetFilteredBrigadesAsync(ExtCommonBrigadesFilterRequest request)
        {
            return _dispatcherOperations.GetFilteredBrigadesAsync(request);
        }

        [HttpPost]
        public Task<CommonHttpResponse> DeleteBrigadesAsync(ExtDeleteBrigadesRequest request)
        {
            return _dispatcherOperations.DeleteBrigadesAsync(request);
        }

        [HttpPost]
        public Task<CommonHttpResponse<ExtCreateTravelSheetResponse>> CreateTravelSheetAsync(ExtCreateTravelSheetRequest request)
        {
            return _dispatcherOperations.CreateTravelSheetAsync(request);
        }

        [HttpPost]
        public Task<CommonHttpResponse<ExtTravelSheetForFrontViewModel>> GetTravelSheetByIdAsync(ExtGetTravelSheetByIdRequest request)
        {
            return _dispatcherOperations.GetTravelSheetByIdAsync(request);
        }

        [HttpPost]
        public Task<CommonHttpResponse<ExtCommonTravelSheetViewModel[]>> GetCommonFilteredTravelSheetsAsync(
            ExtCommonTravelSheetFilterRequest request
        )
        {
            return _dispatcherOperations.GetCommonFilteredTravelSheetsAsync(request);
        }

        [HttpPost]
        public Task<CommonHttpResponse> DeleteTravelSheetsAsync(ExtDeleteTravelSheetsRequest request)
        {
            return _dispatcherOperations.DeleteTravelSheetsAsync(request);
        }

        [HttpPost]
        public Task<CommonHttpResponse<ExtFixTravelSheetDriveTimesResponse>> FixTravelSheetDriveTimesAsync(
            ExtFixTravelSheetDriveTimesRequest request
        )
        {
            return _dispatcherOperations.FixTravelSheetDriveTimesAsync(request);
        }

        [HttpPost]
        public Task<CommonHttpResponse<ExtCreateShiftTaskResponse>> CreateShiftTaskAsync(ExtCreateShiftTaskRequest request)
        {
            return _dispatcherOperations.CreateShiftTaskAsync(request);
        }

        [HttpPost]
        public Task<CommonHttpResponse<ExtCommonShiftTaskViewModel[]>> GetCommonFilteredShiftTasksAsync(
            ExtCommonShiftTasksFilterRequest request
        )
        {
            return _dispatcherOperations.GetCommonFilteredShiftTasksAsync(request);
        }

        [HttpPost]
        public Task<CommonHttpResponse> DeleteShiftTasksAsync(ExtDeleteShiftTasksRequest request)
        {
            return _dispatcherOperations.DeleteShiftTasksAsync(request);
        }

        [HttpPost]
        public Task<CommonHttpResponse<ExtMissionsForDispatcherViewModel>> GetMissionsForDispatcherByRouteIdAsync(
            ExtGetMissionsByRouteIdRequest request
        )
        {
            return _dispatcherOperations.GetMissionsForDispatcherByRouteIdAsync(request, User);
        }

        [HttpPost]
        public Task<CommonHttpResponse<ExtUpdateMissionsForCloseRouteResponse>> UpdateMissionsForCloseRouteAsync(
            ExtUpdateMissionsForCloseRouteRequest request
        )
        {
            return _dispatcherOperations.UpdateMissionsForCloseRouteAsync(request, User);
        }

        [HttpPost]
        public Task<CommonHttpResponse<ExtAvailableRouteForCreateBrigadeViewModel[]>> GetAvailableRoutesForCreateBrigadeAsync(
            ExtGetAvailableRoutesForCreateBrigadeRequest request
        )
        {
            return _dispatcherOperations.GetAvailableRoutesForCreateBrigadeAsync(request);
        }

        [HttpPost]
        public Task<CommonHttpResponse<ExtAvailableRouteForCreateShiftTaskViewModel[]>> GetAvailableRouteForCreateShiftTaskAsync(
            ExtGetAvailableRoutesForCreateShiftTaskRequest request
        )
        {
            return _dispatcherOperations.GetAvailableRouteForCreateShiftTaskAsync(request);
        }

        [HttpPost]
        public Task<CommonHttpResponse<ExtOrgsDropDownForCreateShiftTaskViewModel[]>> GetOrgsDropDownForCreateShiftTaskAsync(
            ExtGetOrgsDropDownForCreateShiftTaskRequest request
        )
        {
            return _dispatcherOperations.GetOrgsDropDownForCreateShiftTaskAsync(request);
        }
    }
}
