using GreenCity.Http;
using GreenCity.Http.Attributes;
using Microsoft.AspNetCore.Mvc;
using MissionService.Contracts.DataContracts.App;
using MissionService.Contracts.Filters.App;
using MissionService.Contracts.Interface;
using MissionService.Contracts.ViewModels.App;
using MissionService.Core.BusinessLogic.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MissionService.Main.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    [ModelStateValidationActionFilter]
    public class AppController : Controller, IAppApi
    {
        private readonly IAppLogic _appLogic;

        public AppController(IAppLogic appLogic)
        {
            _appLogic = appLogic;
        }

        [HttpPost]
        public Task<CommonHttpResponse<AppFromMapViewModel>> GetFromMapAppAsync(
            int appId
        )
        {
            return _appLogic.GetFromMapAppAsync(appId);
        }

        [HttpPost]
        public Task<CommonHttpResponse<IList<AppByContractClientViewModel>>> GetRealAppsByIdsAsync(
            IList<int> appIds
        )
        {
            return _appLogic.GetRealAppsByIdsAsync(appIds);
        }

        [HttpPost]
        public Task<CommonHttpResponse<IList<AppCommonViewModel>>> GetCommonFilteredAppsAsync(
            CommonAppFilterModel filter
        )
        {
            return _appLogic.GetCommonFilteredAppsAsync(filter);
        }

        [HttpPost]
        public Task<CommonHttpResponse<int>> CreateAppByContractClientAsync(CreateAppByContractClientContract contract)
        {
            return _appLogic.CreateAppByContractClientAsync(contract);
        }

        [HttpPost]
        public Task<CommonHttpResponse<int>> CreateAppFromMapAsync(CreateAppFromMapContract contract)
        {
            return _appLogic.CreateAppFromMapAsync(contract);
        }

        [HttpPost]
        public Task<CommonHttpResponse> UpdateAppByContractClientAsync(UpdateAppByContractClient contract)
        {
            return _appLogic.UpdateAppByContractClientAsync(contract);
        }

        [HttpPost]
        public Task<CommonHttpResponse> DeleteAppsAsync(DeleteAppsContract contract)
        {
            return _appLogic.DeleteAppsAsync(contract);
        }

        [HttpPost]
        public Task<CommonHttpResponse> ReGenerateAppsAsync(RunAppGenerationInSingleThreadContract contract)
        {
            return _appLogic.ReGenerateAppsAsync(contract);
        }
    }
}
