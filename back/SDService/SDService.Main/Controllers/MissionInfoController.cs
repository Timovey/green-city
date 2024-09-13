using GreenCity.Http;
using GreenCity.Http.Attributes;
using Microsoft.AspNetCore.Mvc;
using SDContracts.DataContracts.MissionInfo;
using SDContracts.Interfaces;
using SDContracts.ViewModels;
using SDContracts.ViewModels.MissionInfo;
using SDContracts.ViewModels.Stand;
using SDService.Core.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SDService.Main.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    [ModelStateValidationActionFilter]
    public class MissionInfoController : Controller, IMissionInfoApi
    {
        private readonly MissionInfoLogic _missionInfoLogic;

        public MissionInfoController(MissionInfoLogic missionLogic)
        {
            _missionInfoLogic = missionLogic;
        }

        [HttpPost]
        public Task<CommonHttpResponse<IEnumerable<MissionInfoViewModel>>> GetFilteredMissionInfoAsync(
            GetFilteredMissionInfoContract contract
        )
        {
            return _missionInfoLogic.GetFilteredMissionInfoAsync(contract);
        }

        [HttpPost]
        public Task<CommonHttpResponse<CreateFakeDataForMissionInfoResponse>> CreateFakeDataForMissionInfoAsync(
            CreateFakeDataForMissionInfoContract createContract
        )
        {
            return _missionInfoLogic.CreateFakeDataForMissionInfoAsync(createContract);
        }

        [HttpPost]
        public Task<CommonHttpResponse<ContractsWithContractClientsShortViewModel>> GetContractsWithContractClientsShortInfoAsync(
            GetContractsWithContractClientsShortInfoContract contract
        )
        {
            return _missionInfoLogic.GetContractsWithContractClientsShortInfoAsync(contract.ContractClientIds, contract.ContractIds);
        }

        [HttpPost]
        public Task<CommonHttpResponse<IList<StandForMissionInfoViewModel>>> GetStandsForMissionInfoAsync(
            IList<int> containerIds
        )
        {
            return _missionInfoLogic.GetStandsForMissionInfoAsync(containerIds);
        }
    }
}
