using GreenCity.Http.Attributes.Validation;
using System;

namespace SDContracts.DataContracts.Contract.ScheduleModels
{
    /// <summary>
    /// Задание расписания с помощью начальной даты и шага
    /// </summary>
    public class StepInitialDateScheduleModel
    {
        /// <summary>
        /// Начальная дата
        /// </summary>
        public DateTime InitialDate { get; set; }

        /// <summary>
        /// Величина шага
        /// </summary>
        [GCRange(1, int.MaxValue)]
        public int? StepValue { get; set; }
    }
}
