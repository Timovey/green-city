using System;
using System.Collections.Generic;

namespace SDContracts.DataContracts.Contract.ScheduleModels
{
    /// <summary>
    /// Модель расписания для договора (несериазлизуемая часть)
    /// </summary>
    public class ContractScheduleModel : ContractScheduleSerializedModel
    {
        /// <summary> Задание графика датами (сочетается с кронами) </summary>
        public IList<DateTime> ScheduleDates { get; set; }

        /// <summary> Даты в которые не надо вывозить </summary>
        public IList<DateTime> ExcludeDates { get; set; }
    }
}
