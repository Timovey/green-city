namespace SDContracts.ViewModels.ContractSchedule.Mission
{
    /// <summary> Задание на вывоз через договоры </summary>
    public class ContractScheduleMissionViewModel
    {
        /// <summary> ИД договора </summary>
        public int MissionId { get; set; }
        
        /// <summary> ИД договора </summary>
        public int ContractId { get; set; }

        /// <summary> Номер договора </summary>
        public string ContractNumber { get; set; }

        /// <summary> ИД контрагента </summary>
        public ContractClientInfoMissionViewModel ContractClient { get; set; }

        /// <summary> КП </summary>
        public ContractStandMissionInfoViewModel Stand { get; set; }

        /// <summary> Является ли задача срочной </summary>
        public bool IsUrgent { get; set; }

        /// <summary> Была ли задача провалена (пропущен вызов) </summary>
        public bool IsFailed { get; set; }

        /// <summary> Была ли выполнена задача </summary>
        public bool IsCompleted { get; set; }
    }
}
