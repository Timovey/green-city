using GreenCity.Http;
using GreenCity.Http.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SDContracts.DataContracts.ContractClient;
using SDContracts.Filter;
using SDContracts.Filter.DropDown;
using SDContracts.Interfaces;
using SDContracts.ViewModels;
using SDContracts.ViewModels.Container.ContractClient;
using SDService.Core.BusinessLogic;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SDService.Main.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    [ModelStateValidationActionFilter]
    public class ContractClientController : Controller, IContractClientApi
    {
        private readonly ContractClientLogic _contractClientLogic;
        private readonly ILogger<ContractClientController> _logger;

        public ContractClientController(
            ILogger<ContractClientController> logger,
            ContractClientLogic contractClientLogic
        )
        {
            _logger = logger;
            _contractClientLogic = contractClientLogic;
        }

        [HttpPost]
        public async Task<CommonHttpResponse<IList<ContractClientViewModel>>> GetFilteredContractClientAsync(ContractClientFilterModel filter)
        {
            return await _contractClientLogic.GetFilteredContractClientAsync(filter);
        }

        [HttpPost]
        public async Task<CommonHttpResponse<ContractClientViewModel>> GetContractClientAsync(GetContractClientContract getContract)
        {
            return await _contractClientLogic.GetContractClientAsync(getContract);
        }

        [HttpPost]
        public async Task<CommonHttpResponse<int>> CreateContractClientAsync(CreateContractClientContract createContract)
        {
            return await _contractClientLogic.CreateContractClientAsync(createContract);
        }

        [HttpPost]
        public async Task<CommonHttpResponse> UpdateContractClientAsync(UpdateContractClientContract updateContract)
        {
            return await _contractClientLogic.UpdateContractClientAsync(updateContract);
        }

        [HttpPost]
        public async Task<CommonHttpResponse> DeleteContractClientAsync(DeleteContractClientContract deleteContract)
        {
            return await _contractClientLogic.DeleteContractClientAsync(deleteContract);
        }

        [HttpPost]
        public async Task<CommonHttpResponse<IList<ContractClientDropDownViewModel>>> GetDropDownContractClientAsync(
            GetDropDownContractClientFilterModel filter
        )
        {
            return await _contractClientLogic.GetDropDownContractClientAsync(filter);
        }
    }
}
