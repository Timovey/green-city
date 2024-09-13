using GreenCity.Http;
using Refit;
using SDContracts.DataContracts.Contract.ScheduleModels.Mission;
using SDContracts.ViewModels.ContractSchedule.Mission;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SDContracts.Interfaces
{
    public interface ISdMissionApi
    {
        [Post("/Mission/ShowContractSchedulesGroupedByDates")]
        Task<CommonHttpResponse<IDictionary<DateTime, IList<ContractScheduleMissionViewModel>>>> ShowContractSchedulesGroupedByDatesAsync(
            ShowMissionsContract contract
        );
    }
}
