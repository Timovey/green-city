using GreenCity.Http;
using TransportContracts.DataContracts.Enums;
using TransportContracts.DateRange;

namespace TransportContracts.DataContracts.TransportShedule
{
    /// <summary>
    /// Контракт на обновление расписания транспорта
    /// </summary>
    public class UpdateTransportSheduleContract : CommonHttpContract
    {
        /// <summary> ИД расписания транспорта</summary>
        public int TransportSheduleId { get; set; }
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
