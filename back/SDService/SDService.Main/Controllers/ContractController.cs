using GreenCity.Http;
using GreenCity.Http.Attributes;
using Microsoft.AspNetCore.Mvc;
using SDContracts.DataContracts.Contract;
using SDContracts.Filter;
using SDContracts.Filter.DropDown;
using SDContracts.Interfaces;
using SDContracts.ViewModels;
using SDContracts.ViewModels.Contract;
using SDService.Core.BusinessLogic;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SDService.Main.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    [ModelStateValidationActionFilter]
    public class ContractController : Controller, IContractApi
    {
        private readonly ContractLogic _contractLogic;

        public ContractController(
            ContractLogic contractLogic
        )
        {
            _contractLogic = contractLogic;
        }

        [HttpPost]
        public Task<CommonHttpResponse<IList<ContractViewModel>>> GetFilteredContractAsync(ContractFilterModel filter)
        {
            return _contractLogic.GetFilteredContractsAsync(filter);
        }

        [HttpPost]
        public Task<CommonHttpResponse<ContractViewModel>> GetContractAsync(GetContractContract getContract)
        {
            return _contractLogic.GetContractAsync(getContract);
        }

        [HttpPost]
        public Task<CommonHttpResponse<int>> CreateContractAsync(CreateContractContract createContract)
        {
            return _contractLogic.CreateContractAsync(createContract);
        }

        [HttpPost]
        public Task<CommonHttpResponse> BulkUpdateContractAsync(BulkUpdateContractContract bilkUpdateContract)
        {
            return _contractLogic.BulkUpdateContractsAsync(bilkUpdateContract);
        }

        [HttpPost]
        public Task<CommonHttpResponse> UpdateContractAsync(UpdateContractContract updateContract)
        {
            return _contractLogic.UpdateContractsAsync(updateContract);
        }

        [HttpPost]
        public Task<CommonHttpResponse> DeleteContractAsync(DeleteContractContract deleteContract)
        {
            return _contractLogic.DeleteContractsAsync(deleteContract);
        }

        [HttpPost]
        public Task<CommonHttpResponse<IList<ContractDropDownViewModel>>> GetDropDownContractsAsync(
            GetDropDownContractsFilterModel filter
        )
        {
            return _contractLogic.GetDropDownContractsAsync(filter);
        }
    }
}
