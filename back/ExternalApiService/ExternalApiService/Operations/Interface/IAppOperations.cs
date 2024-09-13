using ExternalApi.Interface.Models.Mission.App;
using ExternalApi.Interface.Models.Mission.Mission;
using GreenCity.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExternalApiService.Operations.Interface
{
    public interface IAppOperations
    {
        Task<CommonHttpResponse<ExtAppFromMapViewModel>> GetFromMapAppAsync(ExtGetFromMapAppRequest request);

        Task<CommonHttpResponse<ExtAppByContractClientViewModel>> GetRealAppByIdAsync(ExtGetRealAppByIdRequest request);

        Task<CommonHttpResponse<ExtAppCommonViewModel[]>> GetCommonFilteredAppsAsync(ExtCommonAppFilterRequest request);

        Task<CommonHttpResponse<ExtCreateAppByContractClientResponse>> CreateAppByContractClientAsync(
            ExtCreateAppByContractClientRequest request,
            ExtAppActionsPermission appActionPermission,
            ExtCreatorType creatorType
        );

        Task<CommonHttpResponse<ExtCreateAppFromMapResponse>> CreateAppFromMapAsync(
            ExtCreateAppFromMapRequest request,
            ExtAppActionsPermission appActionPermission,
            ExtCreatorType creatorType
        );

        Task<CommonHttpResponse<ExtUpdateAppByContractClientResponse>> UpdateAppByContractClientAsync(
            ExtUpdateAppByContractClientRequest request,
            ExtAppActionsPermission appActionPermission,
            ExtCreatorType updaterType
        );

        Task<CommonHttpResponse> DeleteAppByIdAsync(
            ExtDeleteAppsRequest request
        );

        Task<CommonHttpResponse<IList<ExtCommonMissionViewModel>>> GetCommonFilteredMissionsAsync(
            ExtCommonMissionFilterRequest request
        );

        Task<CommonHttpResponse> ReGenerateAppsAsync(
            ExtRunAppGenerationInSingleThreadRequest request
        );
    }
}
