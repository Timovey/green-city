using ExternalApi.Interface.Models.Dispatcher.AuxiliaryWorker;
using ExternalApi.Interface.Models.Dispatcher.Brigade;
using ExternalApi.Interface.Models.Dispatcher.Mission;
using ExternalApi.Interface.Models.Dispatcher.Route;
using ExternalApi.Interface.Models.Dispatcher.ShiftTask;
using ExternalApi.Interface.Models.Dispatcher.TravelSheet;
using GreenCity.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ExternalApiService.Operations.Interface
{
    public interface IDispatcherOperations
    {
        Task<CommonHttpResponse<ExtAuxiliaryWorkerViewModel[]>> GetFilteredAuxiliaryWorkersAsync(ExtAuxiliaryWorkerFilterRequest filter);

        Task<CommonHttpResponse<ExtAuxiliaryWorkerViewModel>> GetAuxiliaryWorkerAsync(ExtGetAuxiliaryWorkerRequest request);

        Task<CommonHttpResponse<ExtAuxiliaryWorkerViewModel>> CreateAuxiliaryWorkerAsync(ExtCreateAuxiliaryWorkerRequest request);

        Task<CommonHttpResponse<ExtAuxiliaryWorkerViewModel>> UpdateAuxiliaryWorkerAsync(ExtUpdateAuxiliaryWorkerRequest request);

        Task<CommonHttpResponse> DeleteAuxiliaryWorkerAsync(ExtDeleteAuxiliaryWorkerRequest request);

        Task<CommonHttpResponse<ExtCreateBrigadeResponse>> CreateBrigadeAsync(ExtCreateBrigadeRequest request);

        Task<CommonHttpResponse<ExtCommonBrigadeViewModel[]>> GetFilteredBrigadesAsync(ExtCommonBrigadesFilterRequest request);

        Task<CommonHttpResponse> DeleteBrigadesAsync(ExtDeleteBrigadesRequest request);

        Task<CommonHttpResponse<ExtCreateTravelSheetResponse>> CreateTravelSheetAsync(ExtCreateTravelSheetRequest request);

        Task<CommonHttpResponse<ExtTravelSheetForFrontViewModel>> GetTravelSheetByIdAsync(ExtGetTravelSheetByIdRequest request);

        Task<CommonHttpResponse<ExtCommonTravelSheetViewModel[]>> GetCommonFilteredTravelSheetsAsync(
            ExtCommonTravelSheetFilterRequest request
        );

        Task<CommonHttpResponse> DeleteTravelSheetsAsync(ExtDeleteTravelSheetsRequest request);

        Task<CommonHttpResponse<ExtFixTravelSheetDriveTimesResponse>> FixTravelSheetDriveTimesAsync(
            ExtFixTravelSheetDriveTimesRequest request
        );

        Task<CommonHttpResponse<ExtCreateShiftTaskResponse>> CreateShiftTaskAsync(ExtCreateShiftTaskRequest request);

        Task<CommonHttpResponse<ExtCommonShiftTaskViewModel[]>> GetCommonFilteredShiftTasksAsync(
            ExtCommonShiftTasksFilterRequest request
        );

        Task<CommonHttpResponse> DeleteShiftTasksAsync(ExtDeleteShiftTasksRequest request);

        Task<CommonHttpResponse<ExtMissionsForDispatcherViewModel>> GetMissionsForDispatcherByRouteIdAsync(
            ExtGetMissionsByRouteIdRequest request,
            ClaimsPrincipal user
        );

        Task<CommonHttpResponse<ExtUpdateMissionsForCloseRouteResponse>> UpdateMissionsForCloseRouteAsync(
            ExtUpdateMissionsForCloseRouteRequest request,
            ClaimsPrincipal user
        );

        Task<CommonHttpResponse<ExtAvailableRouteForCreateBrigadeViewModel[]>> GetAvailableRoutesForCreateBrigadeAsync(
            ExtGetAvailableRoutesForCreateBrigadeRequest request
        );

        Task<CommonHttpResponse<ExtAvailableRouteForCreateShiftTaskViewModel[]>> GetAvailableRouteForCreateShiftTaskAsync(
            ExtGetAvailableRoutesForCreateShiftTaskRequest request
        );

        Task<CommonHttpResponse<ExtOrgsDropDownForCreateShiftTaskViewModel[]>> GetOrgsDropDownForCreateShiftTaskAsync(
            ExtGetOrgsDropDownForCreateShiftTaskRequest request
        );
    }
}
