using ExternalApi.Interface.Models.Mission.App;
using ExternalApi.Interface.Models.Sd.Container;
using ExternalApi.Interface.Models.Sd.Contract;
using ExternalApi.Interface.Models.Sd.ContractClient;
using ExternalApi.Interface.Models.Sd.Stand;
using GreenCity.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ExternalApiService.Operations.Interface
{
    public interface ISdOperations
    {
        Task<CommonHttpResponse<ExtContractClientViewModel[]>> GetFilteredContractClientAsync(ExtContractClientFilterRequest filter);

        Task<CommonHttpResponse<ExtContractClientViewModel>> GetContractClientAsync(ExtGetContractClientRequest request);

        Task<CommonHttpResponse<int>> CreateContractClientAsync(ExtCreateContractClientRequest request);

        Task<CommonHttpResponse> UpdateContractClientAsync(ExtUpdateContractClientRequest request);

        Task<CommonHttpResponse> DeleteContractClientAsync(ExtDeleteContractClientRequest request);

        Task<CommonHttpResponse<ExtContractClientDropDownViewModel[]>> GetDropdownContractClientAsync(ExtGetDropDownContractClientFilterRequest filter);

        Task<CommonHttpResponse<ExtContractClientDropDownViewModel[]>> GetDropDownContractClientV2Async(
            ExtGetDropDownContractClientFilterRequest filter,
            ExtAppType extAppType
        );

        Task<CommonHttpResponse<ExtContainerViewModel[]>> GetFilteredContainerAsync(ExtContainerFilterRequest filter);

        Task<CommonHttpResponse<ExtContainerViewModel[]>> GetContainersAsync(ExtGetContainerRequest request);

        Task<CommonHttpResponse<int>> CreateContainerAsync(ExtCreateContainerRequest request);

        Task<CommonHttpResponse> UpdateContainerAsync(ExtUpdateContainerRequest request);

        Task<CommonHttpResponse> DeleteContainerAsync(ExtDeleteContainerRequest request);

        Task<CommonHttpResponse<ExtContainerTypeViewModel[]>> GetActualContainerTypesAsync();

        Task<CommonHttpResponse<ExtContainerDropDownV2ViewModel[]>> GetDropDownContainersAsync(ExtGetDropDownContainersFilterRequest filter);

        Task<CommonHttpResponse<ExtStandViewModel[]>> GetFilteredStandsAsync(ExtStandFilterRequest filter);

        Task<CommonHttpResponse<ExtStandForBaseRouteViewModel[]>> GetFilteredStandsForBaseRouteAsync(ExtStandForBaseRouteFilterRequest filter);

        Task<CommonHttpResponse<ExtOneStandViewModel[]>> GetStandsAsync(ExtGetStandRequest request);

        Task<CommonHttpResponse<int>> CreateStandAsync(ExtCreateStandRequest request);

        Task<CommonHttpResponse> UpdateStandsAsync(ExtUpdateStandRequest request);

        Task<CommonHttpResponse> DeleteStandsAsync(ExtDeleteStandRequest request);

        Task<CommonHttpResponse<ExtStandDropDownViewModel[]>> GetDropDownStandsAsync(ExtGetDropDownStandsFilterRequest filter);

        Task<CommonHttpResponse<ExtStandForRouteViewModel[]>> GetStandsByRouteIdAsync(
            ExtGetStandsForRouteByRouteIdRequest request
        );

        Task<CommonHttpResponse<ExtStandForRouteViewModel[]>> GetFilteredStandsForRouteAsync(
            ExtGetFilteredStandsForRouteFilterRequest request
        );

        Task<CommonHttpResponse<ExtContractViewModel[]>> GetFilteredContractsAsync(ExtContractFilterRequest filter);

        Task<CommonHttpResponse<ExtContractViewModel>> GetContractAsync(ExtGetContractRequest request);

        Task<CommonHttpResponse<int>> CreateContractAsync(ExtCreateContractRequest request, ClaimsPrincipal user);

        Task<CommonHttpResponse<int>> CreateContractForTestDataAsync(ExtCreateContractRequest request);

        Task<CommonHttpResponse> BulkUpdateContractsAsync(ExtBulkUpdateContractRequest request);

        Task<CommonHttpResponse> UpdateContractsAsync(ExtUpdateContractRequest request, ClaimsPrincipal user);

        Task<CommonHttpResponse> DeleteContractsAsync(ExtDeleteContractRequest request);

        Task<CommonHttpResponse<ExtContractDropDownViewModel[]>> GetDropdownContractAsync(
            ExtGetDropDownContractsFilterRequest filter,
            ExtAppType extAppType
        );
    }
}
