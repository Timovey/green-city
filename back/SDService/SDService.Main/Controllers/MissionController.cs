using GreenCity.Http;
using GreenCity.Http.Attributes;
using Microsoft.AspNetCore.Mvc;
using SDContracts.DataContracts.Contract.ScheduleModels.Mission;
using SDContracts.Interfaces;
using SDContracts.ViewModels.ContractSchedule.Mission;
using SDService.Core.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SDService.Main.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    [ModelStateValidationActionFilter]
    public class MissionController : Controller, ISdMissionApi
    {
        private readonly MissionSingletonLogic _missionSingletonLogic;

        public MissionController(
            MissionSingletonLogic missionSingletonLogic
        )
        {
            _missionSingletonLogic = missionSingletonLogic;
        }

        [HttpPost]
        public Task<CommonHttpResponse<IDictionary<DateTime, IList<ContractScheduleMissionViewModel>>>> ShowContractSchedulesGroupedByDatesAsync(
            ShowMissionsContract contract
        )
        {
            return _missionSingletonLogic.ShowContractSchedulesGroupedByDatesAsync(contract);
        }
    }
}
