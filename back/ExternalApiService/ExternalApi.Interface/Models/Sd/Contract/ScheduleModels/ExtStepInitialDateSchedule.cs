using System;

namespace ExternalApi.Interface.Models.Sd.Contract.ScheduleModels
{
    /// <summary>
    /// Задание расписания с помощью начальной даты и шага
    /// </summary>
    public class ExtStepInitialDateSchedule
    {
        /// <summary>
        /// Начальная дата
        /// </summary>
        public DateTime InitialDate { get; set; }

        /// <summary>
        /// Величина шага
        /// </summary>
        public string StepValue { get; set; }
    }
}
