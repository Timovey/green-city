using SDContracts.ViewModels.ContractSchedule;
using System.Collections.Generic;

namespace SDContracts.ViewModels
{
    public class Container2ScheduleForMissionsViewModel
    {
        public int ContainerId { get; set; }

        public int ContractId { get; set; }

        public int ContractClientId { get; set; }

        /// <summary> Расписание </summary>
        public IList<ContractScheduleCollectionViewModel> ContractScheduleCollection { get; set; }
    }
}
