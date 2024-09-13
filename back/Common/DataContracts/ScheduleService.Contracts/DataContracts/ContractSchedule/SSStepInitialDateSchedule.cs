using GreenCity.Http.Attributes.Validation;
using System;

namespace ScheduleService.Contracts.DataContracts.ContractSchedule
{
    /// <summary>
    /// Задание расписания с помощью начальной даты и шага
    /// </summary>
    public class SSStepInitialDateSchedule
    {
        /// <summary>
        /// Начальная дата
        /// </summary>
        public DateTime InitialDate { get; set; }

        /// <summary>
        /// Величина шага
        /// </summary>
        [GCRange(1, 2000)]
        public int? StepValue { get; set; }
    }
}
