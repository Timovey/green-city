using GreenCity.Http;
using GreenCity.Http.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SDContracts.DataContracts.Stand;
using SDContracts.Filter;
using SDContracts.Filter.DropDown;
using SDContracts.Interfaces;
using SDContracts.ViewModels.Stand;
using SDService.Core.BusinessLogic;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SDService.Main.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    [ModelStateValidationActionFilter]
    public class StandController : Controller, IStandApi
    {
        private readonly StandLogic _standLogic;
        private readonly ILogger<StandController> _logger;

        public StandController(
            ILogger<StandController> logger,
            StandLogic standLogic
        )
        {
            _logger = logger;
            _standLogic = standLogic;
        }

        [HttpPost]
        public Task<CommonHttpResponse<IList<StandViewModel>>> GetFilteredStandsAsync(StandFilterModel filter)
        {
            return _standLogic.GetFilteredStandsAsync(filter);
        }

        [HttpPost]
        public Task<CommonHttpResponse<IList<StandViewModel>>> GetStandsAsync(GetStandContract getContract)
        {
            return _standLogic.GetStandsAsync(getContract);
        }

        [HttpPost]
        public Task<CommonHttpResponse<int>> CreateStandAsync(CreateStandContract createContract)
        {
            return _standLogic.CreateStandAsync(createContract);
        }

        [HttpPost]
        public Task<CommonHttpResponse> UpdateStandsAsync(UpdateStandContract updateContract)
        {
            return _standLogic.UpdateStandsAsync(updateContract);
        }

        [HttpPost]
        public Task<CommonHttpResponse> DeleteStandsAsync(DeleteStandContract deleteContract)
        {
            return _standLogic.DeleteStandsAsync(deleteContract);
        }

        [HttpPost]
        public Task<CommonHttpResponse<IList<StandForRouteViewModel>>> GetFilteredStandsForRouteAsync(GetFilteredStandsForRouteFilterModel filter)
        {
            return _standLogic.GetFilteredStandsForRouteAsync(filter);
        }

        [HttpPost]
        public Task<CommonHttpResponse<IList<StandForRouteViewModel>>> GetStandsByRouteIdAsync(GetStandsForRouteByRouteIdContract contract)
        {
            return _standLogic.GetStandsByRouteIdAsync(contract);
        }

        [HttpPost]
        public Task<CommonHttpResponse<IList<StandDropDownViewModel>>> GetDropDownStandsAsync(GetDropDownStandsFilterModel filter)
        {
            return _standLogic.GetDropDownStandsAsync(filter);
        }
    }
}
