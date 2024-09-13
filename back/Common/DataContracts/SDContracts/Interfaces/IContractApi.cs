using GreenCity.Http;
using Refit;
using SDContracts.DataContracts.Contract;
using SDContracts.Filter;
using SDContracts.Filter.DropDown;
using SDContracts.ViewModels;
using SDContracts.ViewModels.Contract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SDContracts.Interfaces
{
    public interface IContractApi
    {
        [Post("/Contract/GetFilteredContract")]
        Task<CommonHttpResponse<IList<ContractViewModel>>> GetFilteredContractAsync(
            [Body] ContractFilterModel filter
        );

        [Post("/Contract/GetContract")]
        Task<CommonHttpResponse<ContractViewModel>> GetContractAsync(
            [Body] GetContractContract getContract
        );

        [Post("/Contract/CreateContract")]
        Task<CommonHttpResponse<int>> CreateContractAsync(
            [Body] CreateContractContract createContract
        );

        [Post("/Contract/BulkUpdateContract")]
        Task<CommonHttpResponse> BulkUpdateContractAsync(
            BulkUpdateContractContract bilkUpdateContract
        );

        [Post("/Contract/UpdateContract")]
        Task<CommonHttpResponse> UpdateContractAsync(
            [Body] UpdateContractContract updateContract
        );

        [Post("/Contract/DeleteContract")]
        Task<CommonHttpResponse> DeleteContractAsync(
            [Body] DeleteContractContract deleteContract
        );

        [Post("/Contract/GetDropDownContracts")]
        Task<CommonHttpResponse<IList<ContractDropDownViewModel>>> GetDropDownContractsAsync(
           [Body] GetDropDownContractsFilterModel filter
        );
    }
}
