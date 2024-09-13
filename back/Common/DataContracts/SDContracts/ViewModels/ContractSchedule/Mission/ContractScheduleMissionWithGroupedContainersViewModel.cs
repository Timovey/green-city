using System.Collections.Generic;

namespace SDContracts.ViewModels.ContractSchedule.Mission
{
    /// <summary>
    /// Аналог модели миссии, только с группированными по площадкам контейнерами
    /// </summary>
    public class ContractScheduleMissionWithGroupedContainersViewModel
    {
        /// <summary> ИД договора </summary>
        public int MissionId { get; set; }

        /// <summary> ИД договора </summary>
        public int ContractId { get; set; }

        /// <summary> Номер договора </summary>
        public string ContractNumber { get; set; }

        /// <summary> ИД контрагента </summary>
        public ContractClientInfoMissionViewModel ContractClient { get; set; }

        /// <summary> Прикрепленные к договорам площадки с контейнерами </summary>
        public IList<ContractStandMissionInfoViewModel> Stands { get; set; }

        /// <summary> Является ли задача срочной </summary>
        public bool IsUrgent { get; set; }

        /// <summary> Была ли задача провалена (пропущен вызов) </summary>
        public bool IsFailed { get; set; }

        /// <summary> Была ли выполнена задача </summary>
        public bool IsCompleted { get; set; }
    }
}
