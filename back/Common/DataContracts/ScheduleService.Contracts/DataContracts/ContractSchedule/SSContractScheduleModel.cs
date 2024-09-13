using System;
using System.Collections.Generic;

namespace ScheduleService.Contracts.DataContracts.ContractSchedule
{
    /// <summary>
    /// Модель расписания для договора (несериазлизуемая часть)
    /// </summary>
    public class SSContractScheduleModel : SSContractScheduleSerializedModel
    {
        /// <summary> Задание графика датами (сочетается с кронами) </summary>
        public IList<DateTime> ScheduleDates { get; set; }

        /// <summary> Даты в которые не надо вывозить </summary>
        public IList<DateTime> ExcludeDates { get; set; }
    }
}
