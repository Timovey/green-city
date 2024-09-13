using System;

namespace ScheduleService.Contracts.ViewModels
{
    public class DateRangeViewModel
    {
        public Guid Id { get; set; }

        /// <summary> Дата создания </summary>
        public DateTime CreateDate { get; set; }

        /// <summary> Дата начала работы </summary>
        public DateTime StartDate { get; set; }

        /// <summary> Дата окончания работы </summary>
        public DateTime EndDate { get; set; }

    }
}
