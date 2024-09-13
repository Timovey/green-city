using System.Threading.Tasks;
using AuthContracts.Attributres;
using AuthContracts.Constants;
using ExternalApi.Interface.Models.Logist.BaseRoute;
using ExternalApi.Interface.Models.Logist.Route;
using ExternalApiService.Operations.Interface;
using GreenCity.Http;
using GreenCity.Http.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace ExternalApiService.Controllers
{
    [AuthRoles(AuthRolesConstants.ADMIN, AuthRolesConstants.LOGIST)]
    [ApiController]
    [Route("api/[controller]/[action]")]
    [ModelStateValidationActionFilter]
    public class LogistController : Controller
    {
        private readonly ILogistOperations _logistOperations;

        public LogistController(
            ILogistOperations logistOperations
        )
        {
            _logistOperations = logistOperations;
        }

        #region BaseRoute

        [HttpPost]
        public Task<CommonHttpResponse<ExtBaseRouteWithStandsViewModel>> CreateBaseRouteAsync(
            ExtCreateBaseRouteRequest request)
        {
            return _logistOperations.CreateBaseRouteAsync(request);
        }

        [HttpPost]
        public Task<CommonHttpResponse<ExtBaseRouteViewModel[]>> GetFilteredBaseRoutesAsync(
            ExtBaseRouteFilterRequest filter)
        {
            return _logistOperations.GetFilteredBaseRoutesAsync(filter);
        }

        [HttpPost]
        public Task<CommonHttpResponse<ExtBaseRouteWithStandsViewModel>> GetBaseRouteByIdAsync(
            ExtGetBaseRouteRequest request)
        {
            return _logistOperations.GetBaseRouteAsync(request);
        }

        [HttpPost]
        public Task<CommonHttpResponse<ExtBaseRouteWithStandsViewModel>> UpdateBaseRouteAsync(
            ExtUpdateBaseRouteRequest request)
        {
            return _logistOperations.UpdateBaseRouteAsync(request);
        }

        [HttpPost]
        public Task<CommonHttpResponse> DeleteBaseRouteAsync(ExtDeleteBaseRouteRequest request)
        {
            return _logistOperations.DeleteBaseRouteAsync(request);
        }

        [HttpPost]
        public Task<CommonHttpResponse<ExtBaseRouteViewModel[]>> GetDropdownBaseRoutesAsync(
            ExtDropdownBaseRouteFilterRequest filter)
        {
            return _logistOperations.GetDropdownBaseRoutesAsync(filter);
        }

        #endregion

        #region Route

        [HttpPost]
        public Task<CommonHttpResponse<ExtRouteViewModel>> CreateRouteAsync(ExtCreateRouteRequest request)
        {
            return _logistOperations.CreateRouteAsync(request);
        }

        [HttpPost]
        public Task<CommonHttpResponse<ExtFilteredRouteViewModel[]>> GetFilteredRoutesAsync(
            ExtRouteFilterRequest filter)
        {
            return _logistOperations.GetFilteredRoutesAsync(filter);
        }

        [HttpPost]
        public Task<CommonHttpResponse<ExtRouteViewModel>> GetRouteByIdAsync(
            ExtGetRouteRequest request)
        {
            return _logistOperations.GetRouteAsync(request);
        }

        [HttpPost]
        public Task<CommonHttpResponse<ExtRouteViewModel>> UpdateRouteAsync(
            ExtUpdateRouteRequest request)
        {
            return _logistOperations.UpdateRouteAsync(request);
        }

        [HttpPost]
        public Task<CommonHttpResponse> DeleteRouteAsync(ExtDeleteRouteRequest request)
        {
            return _logistOperations.DeleteRouteAsync(request);
        }

        #endregion
    }
}