using ExternalApi.Interface.Models.Transport.DateRange;
using ExternalApi.Interface.Models.Transport.Transport;

namespace ExternalApi.Interface.Models.Transport.TransportShedule
{
    public class ExtTransportSheduleViewModel
    {
        /// <summary> ИД расписания транспорта</summary>
        public int TransportSheduleId { get; set; }

        /// <summary> ИД транспортного средства </summary>
        public int TransportId { get; set; }

        /// <summary> Марка транспорта </summary>
        public string TransportName { get; set; }

        /// <summary> ИД Организации транспорта</summary>
        public int? TransportOrganizationId { get; set; }

        /// <summary> Тип транспорта</summary>
        public ExtTransportType TransportType { get; set; }

        /// <summary> Регистрационный номер </summary>
        public string RegNumber { get; set; }

        /// <summary> Настройка времени существования расписания транспорта</summary>
        public ExtDateRangeBaseModel DateRange { get; set; }

        /// <summary>
        /// Типовой трафик
        /// </summary>
        public ExtWorkScheduleType WorkSheduleType { get; set; }
    }
}
