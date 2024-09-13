using AuthContracts.Attributres;
using AuthContracts.Constants;
using ExternalApi.Interface.Models.Mission.App;
using ExternalApi.Interface.Models.Sd.Container;
using ExternalApi.Interface.Models.Sd.Contract;
using ExternalApi.Interface.Models.Sd.ContractClient;
using ExternalApi.Interface.Models.Sd.Stand;
using ExternalApiService.Operations.Interface;
using GreenCity.Http;
using GreenCity.Http.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace ExternalApiService.Controllers
{
    [AuthRoles(AuthRolesConstants.ADMIN, AuthRolesConstants.AO)]
    [ApiController]
    [Route("api/[controller]/[action]")]
    [ModelStateValidationActionFilter]
    public class SdController : Controller
    {
        private readonly ILogger<SdController> _logger;
        private readonly ISdOperations _sdOperations;

        public SdController(
            ISdOperations sdOperations,
            ILogger<SdController> logger
        )
        {
            _sdOperations = sdOperations;
            _logger = logger;
        }

        #region Stand

        [HttpPost]
        public Task<CommonHttpResponse<ExtStandViewModel[]>> GetFilteredStand(ExtStandFilterRequest filter)
        {
            return _sdOperations.GetFilteredStandsAsync(filter);
        }

        [HttpPost]
        public Task<CommonHttpResponse<ExtStandForBaseRouteViewModel[]>> GetFilteredStandForBaseRoute(ExtStandForBaseRouteFilterRequest filter)
        {
            return _sdOperations.GetFilteredStandsForBaseRouteAsync(filter);
        }

        [HttpPost]
        public Task<CommonHttpResponse<ExtOneStandViewModel[]>> GetStand(ExtGetStandRequest request)
        {
            return _sdOperations.GetStandsAsync(request);
        }

        [HttpPost]
        public Task<CommonHttpResponse<int>> CreateStand(ExtCreateStandRequest request)
        {
            return _sdOperations.CreateStandAsync(request);
        }

        [HttpPost]
        public Task<CommonHttpResponse> UpdateStand(ExtUpdateStandRequest request)
        {
            return _sdOperations.UpdateStandsAsync(request);
        }

        [HttpPost]
        public Task<CommonHttpResponse> DeleteStand(ExtDeleteStandRequest request)
        {
            return _sdOperations.DeleteStandsAsync(request);
        }

        [HttpPost]
        public Task<CommonHttpResponse<ExtStandForRouteViewModel[]>> GetFilteredStandsForRouteAsync(ExtGetFilteredStandsForRouteFilterRequest request)
        {
            return _sdOperations.GetFilteredStandsForRouteAsync(request);
        }

        [HttpPost]
        public Task<CommonHttpResponse<ExtStandForRouteViewModel[]>> GetStandsByRouteIdAsync(
            ExtGetStandsForRouteByRouteIdRequest request
        )
        {
            return _sdOperations.GetStandsByRouteIdAsync(request);
        }

        [HttpPost]
        public Task<CommonHttpResponse<ExtStandDropDownViewModel[]>> GetDropDownStandsAsync(
            ExtGetDropDownStandsFilterRequest request
        )
        {
            return _sdOperations.GetDropDownStandsAsync(request);
        }

        #endregion

        #region Contract client

        [HttpPost]
        public async Task<CommonHttpResponse<ExtContractClientViewModel[]>> GetFilteredContractClient(ExtContractClientFilterRequest filter)
        {
            return await _sdOperations.GetFilteredContractClientAsync(filter);
        }

        [HttpPost]
        public async Task<CommonHttpResponse<ExtContractClientViewModel>> GetContractClient(ExtGetContractClientRequest request)
        {
            return await _sdOperations.GetContractClientAsync(request);
        }

        [HttpPost]
        public async Task<CommonHttpResponse<int>> CreateContractClient(ExtCreateContractClientRequest request)
        {
            return await _sdOperations.CreateContractClientAsync(request);
        }

        [HttpPost]
        public async Task<CommonHttpResponse> UpdateContractClient(ExtUpdateContractClientRequest request)
        {
            return await _sdOperations.UpdateContractClientAsync(request);
        }

        [HttpPost]
        public async Task<CommonHttpResponse> DeleteContractClient(ExtDeleteContractClientRequest request)
        {
            return await _sdOperations.DeleteContractClientAsync(request);
        }

        [HttpPost]
        public async Task<CommonHttpResponse<ExtContractClientDropDownViewModel[]>> GetDropdownContractClientAsync(ExtGetDropDownContractClientFilterRequest filter)
        {
            return await _sdOperations.GetDropdownContractClientAsync(filter);
        }

        [HttpPost]
        public async Task<CommonHttpResponse<ExtContractClientDropDownViewModel[]>> GetDropDownContractClientV2Async(
            ExtGetDropDownContractClientFilterRequest filter
        )
        {
            return await _sdOperations.GetDropDownContractClientV2Async(filter, ExtAppType.ByContractClient);
        }

        [HttpPost]
        public async Task<CommonHttpResponse<ExtContractClientDropDownViewModel[]>> GetDropDownContractClientV2ForFromMapAppsAsync(
            ExtGetDropDownContractClientFilterRequest filter
        )
        {
            return await _sdOperations.GetDropDownContractClientV2Async(filter, ExtAppType.FromMap);
        }

        #endregion

        #region Containers

        [HttpPost]
        public Task<CommonHttpResponse<ExtContainerViewModel[]>> GetFilteredContainer(ExtContainerFilterRequest filter)
        {
            return _sdOperations.GetFilteredContainerAsync(filter);
        }

        [HttpPost]
        public Task<CommonHttpResponse<ExtContainerViewModel[]>> GetContainer(ExtGetContainerRequest request)
        {
            return _sdOperations.GetContainersAsync(request);
        }

        [HttpPost]
        public Task<CommonHttpResponse<int>> CreateContainer(ExtCreateContainerRequest request)
        {
            return _sdOperations.CreateContainerAsync(request);
        }

        [HttpPost]
        public Task<CommonHttpResponse> UpdateContainer(ExtUpdateContainerRequest request)
        {
            return _sdOperations.UpdateContainerAsync(request);
        }

        [HttpPost]
        public Task<CommonHttpResponse> DeleteContainer(ExtDeleteContainerRequest request)
        {
            return _sdOperations.DeleteContainerAsync(request);
        }

        [HttpPost]
        public Task<CommonHttpResponse<ExtContainerTypeViewModel[]>> GetAllActualContainerTypesAsync()
        {
            return _sdOperations.GetActualContainerTypesAsync();
        }

        [HttpPost]
        public Task<CommonHttpResponse<ExtContainerDropDownV2ViewModel[]>> GetDropDownContainersAsync(
            ExtGetDropDownContainersFilterRequest request
        )
        {
            return _sdOperations.GetDropDownContainersAsync(request);
        }

        #endregion

        #region Contract

        [HttpPost]
        public Task<CommonHttpResponse<ExtContractViewModel[]>> GetFilteredContract(ExtContractFilterRequest filter)
        {
            return _sdOperations.GetFilteredContractsAsync(filter);
        }

        [HttpPost]
        public Task<CommonHttpResponse<ExtContractViewModel>> GetContractAsync(ExtGetContractRequest request)
        {
            return _sdOperations.GetContractAsync(request);
        }

        [HttpPost]
        public Task<CommonHttpResponse<int>> CreateContract(ExtCreateContractRequest request)
        {
            return _sdOperations.CreateContractAsync(request, User);
        }

        [HttpPost]
        public Task<CommonHttpResponse> BulkUpdateContract(ExtBulkUpdateContractRequest request)
        {
            return _sdOperations.BulkUpdateContractsAsync(request);
        }

        [HttpPost]
        public Task<CommonHttpResponse> UpdateContract(ExtUpdateContractRequest request)
        {
            return _sdOperations.UpdateContractsAsync(request, User);
        }

        [HttpPost]
        public Task<CommonHttpResponse> DeleteContract(ExtDeleteContractRequest request)
        {
            return _sdOperations.DeleteContractsAsync(request);
        }

        [HttpPost]
        public Task<CommonHttpResponse<ExtContractDropDownViewModel[]>> GetDropdownContracts(ExtGetDropDownContractsFilterRequest filter)
        {
            return _sdOperations.GetDropdownContractAsync(filter, ExtAppType.ByContractClient);
        }

        [HttpPost]
        public Task<CommonHttpResponse<ExtContractDropDownViewModel[]>> GetDropdownContractsForFromMapApps(ExtGetDropDownContractsFilterRequest filter)
        {
            return _sdOperations.GetDropdownContractAsync(filter, ExtAppType.FromMap);
        }

        #endregion
    }
}
