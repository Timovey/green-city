using ExternalApi.Interface.Models.Transport.DateRange;
using GreenCity.Http;

namespace ExternalApi.Interface.Models.Transport.TransportShedule
{
    /// <summary>
    /// Контракт на обновление расписания транспорта
    /// </summary>
    public class ExtUpdateTransportSheduleRequest : CommonHttpContract
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
        public ExtWorkScheduleType WorkSheduleType { get; set; }

        /// <summary> Настройка времени существования расписания транспорта</summary>
        public ExtDateRangeBaseModel DateRange { get; set; }
    }
}
