using GreenCity.Http;
using GreenCity.Http.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SDContracts.DataContracts.Container;
using SDContracts.Filter;
using SDContracts.Filter.DropDown;
using SDContracts.Interfaces;
using SDContracts.ViewModels;
using SDContracts.ViewModels.Container;
using SDService.Core.BusinessLogic;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SDService.Main.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    [ModelStateValidationActionFilter]
    public class ContainerController : Controller, IContainerApi
    {
        private readonly ContainerLogic _containerLogic;
        private readonly ILogger<ContainerController> _logger;

        public ContainerController(
            ILogger<ContainerController> logger,
            ContainerLogic containerLogic
        )
        {
            _logger = logger;
            _containerLogic = containerLogic;
        }

        [HttpPost]
        public Task<CommonHttpResponse<IList<ContainerViewModel>>> GetFilteredContainerAsync(ContainerFilterModel filter)
        {
            return _containerLogic.GetFilteredContainersAsync(filter);
        }

        [HttpPost]
        public Task<CommonHttpResponse<IList<ContainerViewModel>>> GetContainerAsync(GetContainerContract getContract)
        {
            return _containerLogic.GetContainersAsync(getContract);
        }

        [HttpPost]
        public Task<CommonHttpResponse<int>> CreateContainerAsync(CreateContainerContract createContract)
        {
            return _containerLogic.CreateContainerAsync(createContract);
        }

        [HttpPost]
        public Task<CommonHttpResponse> UpdateContainerAsync(UpdateContainerContract updateContract)
        {
            return _containerLogic.UpdateContainersAsync(updateContract);
        }

        [HttpPost]
        public Task<CommonHttpResponse> DeleteContainerAsync(DeleteContainerContract deleteContract)
        {
            return _containerLogic.DeleteContainersAsync(deleteContract);
        }
        
        [HttpPost]
        public Task<CommonHttpResponse<IList<ContainerTypeViewModel>>> GetActualContainerTypesAsync()
        {
            return _containerLogic.GetActualContainerTypesAsync();
        }

        [HttpPost]
        public Task<CommonHttpResponse<IList<ContainerTypeViewModel>>> GetContainerTypesByIdsAsync(IList<int> containerTypeIds)
        {
            return _containerLogic.GetContainerTypesByIdsAsync(containerTypeIds);
        }

        [HttpPost]
        public Task<CommonHttpResponse<IDictionary<int, IList<int>>>> GetFilteredContainerIdsForRouteAsync(
            GetFilteredContainerIdsForRouteContract contract
        )
        {
            return _containerLogic.GetFilteredContainerIdsForRouteAsync(contract);
        }

        [HttpPost]
        public Task<CommonHttpResponse<IList<ContainerDropDownV2ViewModel>>> GetDropDownContainersAsync(
            GetDropDownContainersFilterModel filter
        )
        {
            return _containerLogic.GetDropDownContainersAsync(filter);
        }
    }
}
