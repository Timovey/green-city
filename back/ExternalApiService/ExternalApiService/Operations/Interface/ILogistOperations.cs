using System.Threading.Tasks;
using ExternalApi.Interface.Models.Logist.BaseRoute;
using ExternalApi.Interface.Models.Logist.Route;
using GreenCity.Http;

namespace ExternalApiService.Operations.Interface
{
    public interface ILogistOperations
    {
        #region BaseRoute

        Task<CommonHttpResponse<ExtBaseRouteWithStandsViewModel>> CreateBaseRouteAsync(
            ExtCreateBaseRouteRequest request);

        Task<CommonHttpResponse<ExtBaseRouteWithStandsViewModel>> GetBaseRouteAsync(ExtGetBaseRouteRequest request);

        Task<CommonHttpResponse<ExtBaseRouteViewModel[]>> GetFilteredBaseRoutesAsync(ExtBaseRouteFilterRequest filter);

        Task<CommonHttpResponse<ExtBaseRouteWithStandsViewModel>> UpdateBaseRouteAsync(
            ExtUpdateBaseRouteRequest request);

        Task<CommonHttpResponse<ExtBaseRouteViewModel[]>> GetDropdownBaseRoutesAsync(
            ExtDropdownBaseRouteFilterRequest filter);

        Task<CommonHttpResponse> DeleteBaseRouteAsync(ExtDeleteBaseRouteRequest request);

        #endregion

        #region Route

        Task<CommonHttpResponse<ExtRouteViewModel>> CreateRouteAsync(ExtCreateRouteRequest request);

        Task<CommonHttpResponse<ExtFilteredRouteViewModel[]>> GetFilteredRoutesAsync(ExtRouteFilterRequest filter);

        Task<CommonHttpResponse<ExtRouteViewModel>> GetRouteAsync(ExtGetRouteRequest request);

        Task<CommonHttpResponse<ExtRouteViewModel>> UpdateRouteAsync(ExtUpdateRouteRequest request);

        Task<CommonHttpResponse> DeleteRouteAsync(ExtDeleteRouteRequest request);

        #endregion
    }
}