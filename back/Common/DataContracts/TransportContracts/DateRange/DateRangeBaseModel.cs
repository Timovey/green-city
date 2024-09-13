using System;

namespace TransportContracts.DateRange
{
    public class DateRangeBaseModel
    {
        /// <summary> Дата создания сущности </summary>
        public DateTime CreateDate { get; set; }

        /// <summary> Дата начала работы </summary>
        public DateTime StartDate { get; set; }

        /// <summary> Дата окончания работы </summary>
        public DateTime? EndDate { get; set; }
    }
}
