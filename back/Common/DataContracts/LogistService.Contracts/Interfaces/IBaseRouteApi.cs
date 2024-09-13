using GreenCity.Http;
using LogistService.Contracts.DataContracts.BaseRoute;
using LogistService.Contracts.Filters;
using LogistService.Contracts.ViewModels;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LogistService.Contracts.Interfaces
{
    public interface IBaseRouteApi
    {
        [Post("/BaseRoute/CreateBaseRoute")]
        Task<CommonHttpResponse<BaseRouteViewModel>> CreateBaseRouteAsync(
            [Body] CreateBaseRouteContract createContract
        );

        [Post("/BaseRoute/GetFilteredBaseRoutes")]
        Task<CommonHttpResponse<IList<BaseRouteViewModel>>> GetFilteredBaseRoutesAsync(
            [Body] BaseRouteFilterModel filter
        );

        [Post("/BaseRoute/GetBaseRoutes")]
        Task<CommonHttpResponse<IList<BaseRouteViewModel>>> GetBaseRoutesAsync(
            [Body] GetBaseRouteContract getContract
        );

        [Post("/BaseRoute/UpdateBaseRoute")]
        Task<CommonHttpResponse<BaseRouteViewModel>> UpdateBaseRouteAsync(
           [Body] UpdateBaseRouteContract updateContract
        );
        
        [Post("/BaseRoute/DeleteBaseRoutes")]
        Task<CommonHttpResponse> DeleteBaseRoutesAsync(
            [Body] DeleteBaseRouteContract deleteContract
        );

    }
}
