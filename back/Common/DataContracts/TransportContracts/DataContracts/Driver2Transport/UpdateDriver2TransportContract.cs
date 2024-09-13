using GreenCity.Http;
using TransportContracts.DataContracts.Enums;
using TransportContracts.DateRange;

namespace TransportContracts.DataContracts.Driver2Transport
{
    /// <summary>
    /// Контракт на обновление сущности для данных по закреплению водителя
    /// </summary>
    public class UpdateDriver2TransportContract : CommonHttpContract
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
        public WorkShift WorkShift { get; set; }

        /// <summary> Настройка времени существования графика водителя</summary>
        public DateRangeBaseModel DateRange { get; set; }
    }
}
