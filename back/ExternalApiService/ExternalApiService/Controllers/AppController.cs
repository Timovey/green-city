using AuthContracts.Attributres;
using AuthContracts.Constants;
using ExternalApi.Interface.Models.Mission.App;
using ExternalApi.Interface.Models.Mission.Mission;
using ExternalApiService.Extensions;
using ExternalApiService.Operations.Interface;
using GreenCity.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExternalApiService.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AppController : Controller
    {
        private readonly IAppOperations _appOperations;

        public AppController(
            IAppOperations appOperations
        )
        {
            _appOperations = appOperations;
        }

        [HttpPost]
        [AuthRoles(AuthRolesConstants.ADMIN, AuthRolesConstants.AO, AuthRolesConstants.DISPATCHER, AuthRolesConstants.LOGIST)]
        public Task<CommonHttpResponse<ExtAppFromMapViewModel>> GetFromMapAppAsync(ExtGetFromMapAppRequest request)
        {
            return _appOperations.GetFromMapAppAsync(request);
        }

        [HttpPost]
        [AuthRoles(AuthRolesConstants.ADMIN, AuthRolesConstants.AO, AuthRolesConstants.DISPATCHER, AuthRolesConstants.LOGIST)]
        public Task<CommonHttpResponse<ExtAppByContractClientViewModel>> GetRealAppByIdAsync(ExtGetRealAppByIdRequest request)
        {
            return _appOperations.GetRealAppByIdAsync(request);
        }

        [HttpPost]
        [AuthRoles(AuthRolesConstants.ADMIN, AuthRolesConstants.AO, AuthRolesConstants.DISPATCHER, AuthRolesConstants.LOGIST)]
        public Task<CommonHttpResponse<ExtAppCommonViewModel[]>> GetCommonFilteredAppsAsync(ExtCommonAppFilterRequest request)
        {
            return _appOperations.GetCommonFilteredAppsAsync(request);
        }

        [HttpPost]
        [AuthRoles(AuthRolesConstants.ADMIN, AuthRolesConstants.LOGIST)]
        public Task<CommonHttpResponse<ExtCreateAppByContractClientResponse>> CreateAppByContractClientByLogistAsync(
            ExtCreateAppByContractClientRequest request
        )
        {
            return _appOperations.CreateAppByContractClientAsync(request, User.GetAppActionsPermission(), ExtCreatorType.Logist);
        }

        [HttpPost]
        [AuthRoles(AuthRolesConstants.ADMIN, AuthRolesConstants.AO)]
        public Task<CommonHttpResponse<ExtCreateAppByContractClientResponse>> CreateAppByContractClientBySdAsync(
            ExtCreateAppByContractClientRequest request
        )
        {
            return _appOperations.CreateAppByContractClientAsync(request, User.GetAppActionsPermission(), ExtCreatorType.Sd);
        }

        [HttpPost]
        [AuthRoles(AuthRolesConstants.ADMIN, AuthRolesConstants.LOGIST)]
        public Task<CommonHttpResponse<ExtCreateAppFromMapResponse>> CreateAppFromMapByLogistAsync(
            ExtCreateAppFromMapRequest request
        )
        {
            return _appOperations.CreateAppFromMapAsync(request, User.GetAppActionsPermission(), ExtCreatorType.Logist);
        }

        [HttpPost]
        [AuthRoles(AuthRolesConstants.ADMIN, AuthRolesConstants.AO)]
        public Task<CommonHttpResponse<ExtCreateAppFromMapResponse>> CreateAppFromMapBySdAsync(
            ExtCreateAppFromMapRequest request
        )
        {
            return _appOperations.CreateAppFromMapAsync(request, User.GetAppActionsPermission(), ExtCreatorType.Sd);
        }

        [HttpPost]
        [AuthRoles(AuthRolesConstants.ADMIN, AuthRolesConstants.AO)]
        public Task<CommonHttpResponse<ExtUpdateAppByContractClientResponse>> UpdateAppByContractClientAsync(
            ExtUpdateAppByContractClientRequest request
        )
        {
            return _appOperations.UpdateAppByContractClientAsync(request, User.GetAppActionsPermission(), ExtCreatorType.Sd);
        }

        [HttpPost]
        [AuthRoles(AuthRolesConstants.ADMIN, AuthRolesConstants.AO)]
        public Task<CommonHttpResponse> DeleteAppByIdAsync(
            ExtDeleteAppsRequest request
        )
        {
            return _appOperations.DeleteAppByIdAsync(request);
        }

        [HttpPost]
        [AuthRoles(AuthRolesConstants.ADMIN, AuthRolesConstants.LOGIST)]
        public Task<CommonHttpResponse<IList<ExtCommonMissionViewModel>>> GetCommonFilteredMissionsAsync(
            ExtCommonMissionFilterRequest request
        )
        {
            return _appOperations.GetCommonFilteredMissionsAsync(request);
        }

        [HttpPost]
        [AuthRoles(AuthRolesConstants.ADMIN, AuthRolesConstants.LOGIST)]
        public Task<CommonHttpResponse> ReGenerateAppsAsync(
            ExtRunAppGenerationInSingleThreadRequest request
        )
        {
            return _appOperations.ReGenerateAppsAsync(request);
        }
    }
}
