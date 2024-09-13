using System;

namespace SDContracts.DataContracts.Contract.ScheduleModels
{
    public class ContractScheduleCollectionModel
    {
        /// <summary>
        /// Расписание договора
        /// </summary>
        public ContractScheduleModel ContractSchedule { get; set; }

        public DateTime CreateDate { get; set; }

        /// <summary>
        /// Дата действия начала действия расписания
        /// </summary>
        public DateTime ScheduleDateFrom { get; set; }

        /// <summary>
        /// Дата окончания действия расписания
        /// </summary>
        public DateTime? ScheduleDateTo { get; set; }
    }
}
