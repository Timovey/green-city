using System.Collections.Generic;
using System.Threading.Tasks;
using GreenCity.Http;
using GreenCity.Http.Attributes;
using LogistService.Contracts.DataContracts.Route;
using LogistService.Contracts.Filters;
using LogistService.Contracts.Interfaces;
using LogistService.Contracts.ViewModels;
using LogistService.Core.BusinessLogic;
using Microsoft.AspNetCore.Mvc;

namespace LogistService.Main.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    [ModelStateValidationActionFilter]
    public class RouteController : Controller, IRouteApi
    {
        private readonly RouteLogic _routeLogic;

        public RouteController(
            RouteLogic routeLogic
        )
        {
            _routeLogic = routeLogic;
        }

        [HttpPost]
        public Task<CommonHttpResponse<RouteViewModel>> CreateRouteAsync(CreateRouteContract createContract)
        {
            return _routeLogic.CreateRouteAsync(createContract);
        }

        [HttpPost]
        public Task<CommonHttpResponse<IList<FilteredRouteViewModel>>> GetFilteredRoutesAsync(RouteFilterModel filter)
        {
            return _routeLogic.GetFilteredRoutesAsync(filter);
        }

        [HttpPost]
        public Task<CommonHttpResponse> DeleteRouteAsync(DeleteRouteContract deleteContract)
        {
            return _routeLogic.DeleteRouteAsync(deleteContract);
        }

        [HttpPost]
        public Task<CommonHttpResponse<RouteViewModel>> UpdateRouteAsync(UpdateRouteContract updateContract)
        {
            return _routeLogic.UpdateRouteAsync(updateContract);
        }

        [HttpPost]
        public Task<CommonHttpResponse<IList<RouteViewModel>>> GetRoutesAsync(GetRouteContract getContract)
        {
            return _routeLogic.GetRoutesAsync(getContract);
        }

        [HttpPost]
        public Task<CommonHttpResponse> CloseRouteAsync(CloseRouteContract closeContract)
        {
            return _routeLogic.CloseRouteAsync(closeContract);
        }

        [HttpPost]
        public Task<CommonHttpResponse<IList<GetRoutesByWorkShiftsIdsViewModel>>> GetRoutesByWorkShiftsIdsAsync(
            GetRoutesByWorkShiftsIdsContract getContract
        )
        {
            return _routeLogic.GetRoutesByWorkShiftsIdsAsync(getContract);
        }
    }
}