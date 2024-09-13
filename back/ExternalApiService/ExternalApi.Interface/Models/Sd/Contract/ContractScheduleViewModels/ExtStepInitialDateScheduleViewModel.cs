using System;

namespace ExternalApi.Interface.Models.Sd.Contract.ContractScheduleViewModels
{
    /// <summary> Задание графика с начальной датой и шагом </summary>
    public class ExtStepInitialDateScheduleViewModel
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
