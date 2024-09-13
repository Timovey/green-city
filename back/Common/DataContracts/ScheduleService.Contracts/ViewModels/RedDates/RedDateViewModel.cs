using System;

namespace ScheduleService.Contracts.ViewModels.RedDates
{
    /// <summary> Красный день каледаря </summary>
    public class RedDateViewModel
    {
        /// <summary> Дата праздника </summary>
        public DateTime Date { get; set; }

        /// <summary> Название праздника </summary>
        public string Name { get; set; }
    }
}
