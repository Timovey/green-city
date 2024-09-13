using AuthContracts.DataContracts;
using AuthContracts.ViewModels;
using GreenCity.Http;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuthContracts.Interfaces
{
    public interface IAuthApi
    {
        [Post("/Auth/AuthUser")]
        Task<CommonHttpResponse> AuthUserAsync(
            [Body] AuthUserContract contract
        );

        [Post("/Auth/GetUserInfo")]
        Task<CommonHttpResponse<AuthUserResult>> GetUserInfoAsync();

        [Post("/Auth/AuthUserV2")]
        Task<CommonHttpResponse> AuthUserV2Async(
            [Body] AuthUserV2Contract contract
        );

        [Post("/Auth/GetUserInfoV2")]
        Task<CommonHttpResponse<AuthUserV2Result>> GetUserInfoV2Async();

        [Post("/Auth/GetOrgsContract")]
        Task<CommonHttpResponse<IList<OrganizationApiViewModel>>> GetOrgsContractAsync(GetOrgsContract getContract);

        [Post("/Auth/GetOrgsByIds")]
        Task<CommonHttpResponse<IList<OrganizationApiViewModel>>> GetOrgsByIdsAsync(GetOrgsByIdsContract getContract);
    }
}
