using ExternalApi.Interface.Models.Transport.DateRange;
using ExternalApi.Interface.Models.Transport.RepairCard;
using GreenCity.Http;

namespace ExternalApi.Interface.Models.Transport.Driver2Transport
{
    /// <summary>
    /// Контракт на обновление сущности для данных по закреплению водителя
    /// </summary>
    public class ExtUpdateDriver2TransportRequest : CommonHttpContract
    {
        /// <summary> ИД сущности для данных по закреплению водителя </summary>
        public int Driver2TransportId { get; set; }

        /// <summary> ИД водителя </summary>
        public int DriverId { get; set; }

        /// <summary> ИД транспорта</summary>
        public int TransportId { get; set; }

        /// <summary>
        /// С какой смены выходит водитель 
        /// </summary>
        public ExtWorkShift WorkShift { get; set; }

        /// <summary> Настройка времени существования графика водителя</summary>
        public ExtDateRangeBaseModel DateRange { get; set; }
    }
}
