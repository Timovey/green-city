using SDContracts.DataContracts.Contract.ScheduleModels.Mission;
using SDContracts.ViewModels.ContractSchedule.Mission;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SDService.Core.Services.ContractSchedule
{
    public interface IContractScheduleJobService
    {
        Task<IDictionary<DateTime, IList<ContractScheduleMissionViewModel>>> ShowContractSchedulesGroupedByDatesAsync(ShowMissionsContract contract);

        Task GenerateMissionsDailyJobAsync();
    }
}
