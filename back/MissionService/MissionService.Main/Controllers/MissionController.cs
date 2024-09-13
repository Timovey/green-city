using GreenCity.Http;
using GreenCity.Http.Attributes;
using Microsoft.AspNetCore.Mvc;
using MissionService.Contracts.DataContracts.Mission;
using MissionService.Contracts.Filters.Mission;
using MissionService.Contracts.Interface;
using MissionService.Contracts.ViewModels.Mission;
using MissionService.Core.BusinessLogic.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MissionService.Main.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    [ModelStateValidationActionFilter]
    public class MissionController : Controller, IMissionApi
    {
        private readonly IMissionLogic _missionLogic;

        public MissionController(
            IMissionLogic missionLogic
        )
        {
            _missionLogic = missionLogic;
        }

        [HttpPost]
        public Task<CommonHttpResponse<IList<int>>> GetAvailableContainersByMissionsForRouteAsync(
            GetAvailableContainersByMissionsForRouteContract contract
        )
        {
            return _missionLogic.GetAvailableContainersByMissionsForRouteAsync(contract);
        }

        [HttpPost]
        public Task<CommonHttpResponse> BindMissionsToRouteByStandsAsync(
            BindMissionsToRouteByStandsContract contract
        )
        {
            return _missionLogic.BindMissionsToRouteByStandsAsync(contract);
        }

        [HttpPost]
        public Task<CommonHttpResponse> UnbindMissionsFromRouteAsync(
            int routeId
        )
        {
            return _missionLogic.UnbindMissionsFromRouteAsync(routeId);
        }

        [HttpPost]
        public Task<CommonHttpResponse<IList<ContainersFromMissionsWithPrioritiesViewModel>>> GetContainersFromMissionsByRouteIdWithPrioritiesAsync(
            int routeId
        )
        {
            return _missionLogic.GetContainersFromMissionsByRouteIdWithPrioritiesAsync(routeId);
        }

        [HttpPost]
        public Task<CommonHttpResponse> UpdateMissionBindingsByRouteIdAsync(
            UpdateMissionBindingsByRouteIdContract contract
        )
        {
            return _missionLogic.UpdateMissionBindingsByRouteIdAsync(contract);
        }

        [HttpPost]
        public Task<CommonHttpResponse> UpdateMissionsForCloseRouteAsync(
            UpdateMissionsForCloseRouteContract contract
        )
        {
            return _missionLogic.UpdateMissionsForCloseRouteAsync(contract);
        }

        [HttpPost]
        public Task<CommonHttpResponse<MissionsForDispatcherViewModel>> GetMissionsForDispatcherByRouteIdAsync(
            int routeId
        )
        {
            return _missionLogic.GetMissionsForDispatcherByRouteIdAsync(routeId);
        }

        [HttpPost]
        public Task<CommonHttpResponse<MissionsForBuildShiftTaskReportViewModel>> GetMissionsForBuildShiftTaskReportByRouteIdAsync(int routeId)
        {
            return _missionLogic.GetMissionsForBuildShiftTaskReportByRouteIdAsync(routeId);
        }

        [HttpPost]
        public Task<CommonHttpResponse<MissionsForBuildDoneTaskListReportViewModel>> GetMissionsForBuildDoneTaskListReportByRouteIdAsync(
            int routeId
        )
        {
            return _missionLogic.GetMissionsForBuildDoneTaskListReportByRouteIdAsync(routeId);
        }

        [HttpPost]
        public Task<CommonHttpResponse<IList<CommonMissionViewModel>>> GetCommonFilteredMissionsAsync(
            CommonMissionFilterModel filter
        )
        {
            return _missionLogic.GetCommonFilteredMissionsAsync(filter);
        }
    }
}
