using GreenCity.Http;
using TransportContracts.DataContracts.Enums;
using TransportContracts.DateRange;

namespace TransportContracts.DataContracts.TransportShedule
{
    /// <summary>
    /// Контракт на создание расписания транспорта
    /// </summary>
    public class CreateTransportSheduleContract : CommonHttpContract
    {
        /// <summary>
        /// ID транспорта
        /// </summary>
        public int TransportId { get; set; }

        /// <summary>
        /// Типовой трафик
        /// </summary>
        public WorkScheduleType WorkSheduleType { get; set; }

        /// <summary> Настройка времени существования расписания транспорта</summary>
        public DateRangeBaseModel DateRange { get; set; }
    }
}
