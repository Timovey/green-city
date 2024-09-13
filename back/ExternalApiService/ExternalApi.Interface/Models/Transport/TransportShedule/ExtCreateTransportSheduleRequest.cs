using ExternalApi.Interface.Models.Transport.DateRange;
using GreenCity.Http;

namespace ExternalApi.Interface.Models.Transport.TransportShedule
{
    /// <summary>
    /// Контракт на создание расписания транспорта
    /// </summary>
    public class ExtCreateTransportSheduleRequest : CommonHttpContract
    {
        /// <summary>
        /// ID транспорта
        /// </summary>
        public int TransportId { get; set; }

        /// <summary>
        /// Типовой трафик
        /// </summary>
        public ExtWorkScheduleType WorkSheduleType { get; set; }

        /// <summary> Настройка времени существования расписания транспорта</summary>
        public ExtDateRangeBaseModel DateRange { get; set; }
    }
}
