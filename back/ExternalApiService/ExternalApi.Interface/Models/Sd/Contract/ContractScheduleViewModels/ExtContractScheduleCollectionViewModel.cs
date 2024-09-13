using System;

namespace ExternalApi.Interface.Models.Sd.Contract.ContractScheduleViewModels
{
    public class ExtContractScheduleCollectionViewModel
    {
        /// <summary>
        /// Расписание договора
        /// </summary>
        public ExtContractScheduleViewModel ContractSchedule { get; set; }

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
