using System.Collections.Generic;

namespace SDService.Database.JsonModels.Contract.ContractSchedule
{
    public class ContractScheduleJsonModel
    {
        /// <summary> Задание графика с начальной датой и шагом </summary>
        public StepInitialDateJsonModel StepInitialDateSchedule { get; set; }

        /// <summary> Задание графика датами (сочетается с кронами) </summary>
        public IList<string> ScheduleDates { get; set; }

        /// <summary> Даты в которые не надо вывозить </summary>
        public IList<string> ExcludeDates { get; set; }

        /// <summary> Крон графики </summary>
        public IList<string> Crons { get; set; }

        /// <summary> Исключающие кроны </summary>
        public IList<string> ExcludeCrons { get; set; }

        /// <summary> Настройки графика </summary>
        public ContractScheduleSettingsJsonModel ContractScheduleSettings { get; set; }
    }
}
