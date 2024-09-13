using System;

namespace SDContracts.ViewModels.ContractSchedule
{
    /// <summary> Задание графика с начальной датой и шагом </summary>
    public class StepInitialDateScheduleViewModel
    {
        /// <summary>
        /// Начальная дата
        /// </summary>
        public DateTime InitialDate { get; set; }

        /// <summary>
        /// Величина шага
        /// </summary>
        public int? StepValue { get; set; }
    }
}
