using ExternalApi.Interface.Models.Transport.DateRange;
using ExternalApi.Interface.Models.Transport.Transport;

namespace ExternalApi.Interface.Models.Transport.Rent
{
    public class ExtRentViewModel
    {
        /// <summary> ИД аренды транспорта</summary>
        public int RentId { get; set; }

        /// <summary> ИД транспортного средства </summary>
        public int TransportId { get; set; }

        /// <summary>
        /// Название транспортного средства
        /// </summary>
        public string TransportName { get; set; }

        /// <summary> ИД Организации транспорта</summary>
        public int? TransportOrganizationId { get; set; }

        /// <summary> Тип транспорта</summary>
        public ExtTransportType TransportType { get; set; }

        /// <summary> Регистрационный номер </summary>
        public string RegNumber { get; set; }

        /// <summary> ИД Организации в которую сдали транспорт </summary>
        public int OrganizationId { get; set; }

        /// <summary> Настройка времени существования расписания транспорта</summary>
        public ExtDateRangeBaseModel DateRange { get; set; }
    }
}
