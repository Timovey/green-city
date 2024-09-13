using System;
using System.Collections.Generic;

namespace ExternalApi.Interface.Models.Sd.Contract.ContractScheduleViewModels
{
    public class ExtContractScheduleViewModel
    {
        /// <summary> Задание графика с начальной датой и шагом </summary>
        public ExtStepInitialDateScheduleViewModel StepInitialDateSchedule { get; set; }

        /// <summary> Задание графика датами (сочетается с кронами) </summary>
        public IList<DateTime> ScheduleDates { get; set; }

        /// <summary> Даты в которые не надо вывозить </summary>
        public IList<DateTime> ExcludeDates { get; set; }

        /// <summary> Крон графики </summary>
        public IList<string> Crons { get; set; }

        /// <summary> Исключающие кроны </summary>
        public IList<string> ExcludeCrons { get; set; }

        /// <summary> Настройки графика </summary>
        public ExtContractScheduleSettingsViewModel ContractScheduleSettings { get; set; }
    }
}
