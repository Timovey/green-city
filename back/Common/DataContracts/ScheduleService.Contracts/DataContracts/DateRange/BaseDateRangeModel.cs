using System;

namespace ScheduleService.Contracts.DataContracts.DateRange
{
    public class BaseDateRangeModel
    {
        public BaseDateRangeModel()
        {
            Id = Guid.NewGuid();
        }

        /// <summary> Технический гуид для внутренней работы алгоритма нарезки слотов </summary>
        public Guid Id { get; set; }

        /// <summary> Дата создания </summary>
        public DateTime CreateDate { get; set; }

        /// <summary> Дата начала работы </summary>
        public DateTime StartDate { get; set; }

        /// <summary> Дата окончания работы </summary>
        public DateTime EndDate { get; set; }

        public virtual BaseDateRangeModel Clone()
        {
            return new BaseDateRangeModel
            {
                Id = Id,
                CreateDate = CreateDate,
                StartDate = StartDate,
                EndDate = EndDate
            };
        }
    }
}
