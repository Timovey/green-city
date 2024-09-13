using System;
using System.Collections.Generic;

namespace SDContracts.ViewModels.ContractSchedule
{
    public class ContractScheduleViewModel
    {
        /// <summary> Задание графика с начальной датой и шагом </summary>
        public StepInitialDateScheduleViewModel StepInitialDateSchedule { get; set; }

        /// <summary> Задание графика датами (сочетается с кронами) </summary>
        public IList<DateTime> ScheduleDates { get; set; }

        /// <summary> Даты в которые не надо вывозить </summary>
        public IList<DateTime> ExcludeDates { get; set; }

        /// <summary> Крон графики </summary>
        public IList<string> Crons { get; set; }

        /// <summary> Исключающие кроны </summary>
        public IList<string> ExcludeCrons { get; set; }

        /// <summary> Настройки графика </summary>
        public ContractScheduleSettingsViewModel ContractScheduleSettings { get; set; }
    }
}
