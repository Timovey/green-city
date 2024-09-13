using ExternalApi.Interface.Models.Transport.DateRange;
using GreenCity.Http;

namespace ExternalApi.Interface.Models.Transport.Rent
{
    public class ExtCreateRentRequest : CommonHttpContract
    {
        /// <summary> ИД транспортного средства </summary>
        public int TransportId { get; set; }

        /// <summary> ИД организации </summary>
        public int OrganizationId { get; set; }

        /// <summary> Настройка времени существования Аренды транспорта</summary>
        public ExtDateRangeBaseModel DateRange { get; set; }
    }
}
