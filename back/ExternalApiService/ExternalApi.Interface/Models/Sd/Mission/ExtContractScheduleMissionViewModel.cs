namespace ExternalApi.Interface.Models.Sd.Mission
{
    public class ExtContractScheduleMissionViewModel
    {
        /// <summary> ИД договора </summary>
        public int MissionId { get; set; }

        /// <summary> ИД договора </summary>
        public int ContractId { get; set; }

        /// <summary> Номер договора </summary>
        public string ContractNumber { get; set; }

        /// <summary> ИД контрагента </summary>
        public ExtContractClientInfoMissionViewModel ContractClient { get; set; }

        /// <summary> КП </summary>
        public ExtContractStandMissionInfoViewModel Stand { get; set; }

        /// <summary> Является ли задача срочной </summary>
        public bool IsUrgent { get; set; }

        /// <summary> Была ли задача провалена (пропущен вызов) </summary>
        public bool IsFailed { get; set; }

        /// <summary> Была ли выполнена задача </summary>
        public bool IsCompleted { get; set; }
    }
}
