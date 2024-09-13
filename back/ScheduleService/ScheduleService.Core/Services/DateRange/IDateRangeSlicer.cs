using ScheduleService.Contracts.DataContracts.DateRange;
using System;
using System.Collections.Generic;

namespace ScheduleService.Core.Services.DateRange
{
    /// <summary>
    /// Слоторезка
    /// </summary>
    public interface IDateRangeSlicer
    {
        /// <summary>
        /// Нарезать список диапазонов на слоты
        /// </summary>
        /// <typeparam name="T"> Наследник BaseDateRangeModel </typeparam>
        /// <param name="originalSlots"></param>
        /// <param name="borderOffsetValue"></param>
        /// <returns></returns>
        IList<T> GetSortedTimeSlots<T>(IList<T> originalSlots, TimeSpan? borderOffsetValue = null)
             where T : BaseDateRangeModel, new();
    }
}
