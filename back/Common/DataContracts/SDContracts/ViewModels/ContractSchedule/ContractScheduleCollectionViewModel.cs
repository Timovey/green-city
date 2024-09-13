using System;

namespace SDContracts.ViewModels.ContractSchedule
{
    public class ContractScheduleCollectionViewModel
    {
        /// <summary>
        /// Расписание договора
        /// </summary>
        public ContractScheduleViewModel ContractSchedule { get; set; }

        /// <summary>
        /// Дата создания записи
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// Дата действия начала действия расписания
        /// </summary>
        public DateTime? ScheduleDateFrom { get; set; }

        /// <summary>
        /// Дата окончания действия расписания
        /// </summary>
        public DateTime? ScheduleDateTo { get; set; }
    }
}
