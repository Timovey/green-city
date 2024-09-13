using GreenCity.Http;
using Refit;
using SDContracts.DataContracts.ContractClient;
using SDContracts.Filter;
using SDContracts.Filter.DropDown;
using SDContracts.ViewModels;
using SDContracts.ViewModels.Container.ContractClient;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SDContracts.Interfaces
{
    public interface IContractClientApi
    {
        [Post("/ContractClient/GetFilteredContractClient")]
        Task<CommonHttpResponse<IList<ContractClientViewModel>>> GetFilteredContractClientAsync(
            [Body] ContractClientFilterModel filter
        );

        [Post("/ContractClient/GetContractClient")]
        Task<CommonHttpResponse<ContractClientViewModel>> GetContractClientAsync(
            [Body] GetContractClientContract getContract
        );

        [Post("/ContractClient/CreateContractClient")]
        Task<CommonHttpResponse<int>> CreateContractClientAsync(
            [Body] CreateContractClientContract createContract
        );

        [Post("/ContractClient/UpdateContractClient")]
        Task<CommonHttpResponse> UpdateContractClientAsync(
            [Body] UpdateContractClientContract updateContract
        );

        [Post("/ContractClient/DeleteContractClient")]
        Task<CommonHttpResponse> DeleteContractClientAsync(
            [Body] DeleteContractClientContract deleteContract
        );

        [Post("/ContractClient/GetDropDownContractClient")]
        Task<CommonHttpResponse<IList<ContractClientDropDownViewModel>>> GetDropDownContractClientAsync(
           [Body] GetDropDownContractClientFilterModel filter
        );
    }
}
