using System.Collections.Generic;
using System.Threading.Tasks;
using GreenCity.Http;
using LogistService.Contracts.DataContracts.Route;
using LogistService.Contracts.Filters;
using LogistService.Contracts.ViewModels;
using Refit;

namespace LogistService.Contracts.Interfaces
{
    public interface IRouteApi
    {
        [Post("/Route/CreateRoute")]
        Task<CommonHttpResponse<RouteViewModel>> CreateRouteAsync(
            [Body] CreateRouteContract createContract
        );

        [Post("/Route/GetFilteredRoutes")]
        Task<CommonHttpResponse<IList<FilteredRouteViewModel>>> GetFilteredRoutesAsync(
            [Body] RouteFilterModel filter
        );

        [Post("/Route/GetRoutes")]
        Task<CommonHttpResponse<IList<RouteViewModel>>> GetRoutesAsync(
            [Body] GetRouteContract getContract
        );

        [Post("/Route/UpdateRoute")]
        Task<CommonHttpResponse<RouteViewModel>> UpdateRouteAsync(
            [Body] UpdateRouteContract updateContract
        );

        [Post("/Route/DeleteRoute")]
        Task<CommonHttpResponse> DeleteRouteAsync(
            [Body] DeleteRouteContract deleteContract
        );

        [Post("/Route/CloseRoute")]
        Task<CommonHttpResponse> CloseRouteAsync(
            [Body] CloseRouteContract closeContract
        );

        [Post("/Route/GetRoutesByWorkShiftsIds")]
        Task<CommonHttpResponse<IList<GetRoutesByWorkShiftsIdsViewModel>>> GetRoutesByWorkShiftsIdsAsync(
            [Body] GetRoutesByWorkShiftsIdsContract getContract
        );
    }
}