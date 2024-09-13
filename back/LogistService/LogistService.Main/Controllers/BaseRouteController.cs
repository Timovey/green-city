using GreenCity.Http;
using GreenCity.Http.Attributes;
using LogistService.Contracts.DataContracts.BaseRoute;
using LogistService.Contracts.Filters;
using LogistService.Contracts.Interfaces;
using LogistService.Contracts.ViewModels;
using LogistService.Core.BusinessLogic;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LogistService.Main.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    [ModelStateValidationActionFilter]
    public class BaseRouteController : Controller, IBaseRouteApi
    {
        private readonly BaseRouteLogic _baseRouteLogic;

        public BaseRouteController(
            BaseRouteLogic baseRouteLogic
        )
        {
            _baseRouteLogic = baseRouteLogic;
        }

        [HttpPost]
        public Task<CommonHttpResponse<BaseRouteViewModel>> CreateBaseRouteAsync(CreateBaseRouteContract createBaseRoute)
        {
            return _baseRouteLogic.CreateBaseRouteAsync(createBaseRoute);
        }

        [HttpPost]
        public Task<CommonHttpResponse<IList<BaseRouteViewModel>>> GetBaseRoutesAsync(GetBaseRouteContract getBaseRoute)
        {
            return _baseRouteLogic.GetBaseRoutesAsync(getBaseRoute);
        }

        [HttpPost]
        public Task<CommonHttpResponse<IList<BaseRouteViewModel>>> GetFilteredBaseRoutesAsync(BaseRouteFilterModel filter)
        {
            return _baseRouteLogic.GetFilteredBaseRoutesAsync(filter);
        }

        [HttpPost]
        public Task<CommonHttpResponse<BaseRouteViewModel>> UpdateBaseRouteAsync(UpdateBaseRouteContract updateContract)
        {
            return _baseRouteLogic.UpdateBaseRouteAsync(updateContract);
        }

        [HttpPost]
        public Task<CommonHttpResponse> DeleteBaseRoutesAsync(DeleteBaseRouteContract deleteContract)
        {
            return _baseRouteLogic.DeleteBaseRoutesAsync(deleteContract);
        }
    }
}
