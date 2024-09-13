using ExternalApi.Interface.Models.Auth;
using GreenCity.Http;
using System.Threading.Tasks;

namespace ExternalApiService.Operations.Interface
{
    public interface IAuthOperations
    {
        Task<CommonHttpResponse<ExtAuthResult>> Login(ExtAuthRequest request);

        Task<CommonHttpResponse<ExtAuthV2Result>> LoginV2(ExtAuthV2Request request);

        Task<CommonHttpResponse<ExtOrganizationViewModel[]>> GetOrganizationsAsync(ExtGetOrgsRequest request);
    }
}
