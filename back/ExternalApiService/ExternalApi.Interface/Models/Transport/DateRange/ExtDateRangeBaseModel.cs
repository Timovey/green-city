using System;

namespace ExternalApi.Interface.Models.Transport.DateRange
{
    public class ExtDateRangeBaseModel
    {
        /// <summary> Дата создания сущности </summary>
        public DateTime CreateDate { get; set; }

        /// <summary> Дата начала работы </summary>
        public DateTime StartDate { get; set; }

        /// <summary> Дата окончания работы </summary>
        public DateTime? EndDate { get; set; }
    }
}
