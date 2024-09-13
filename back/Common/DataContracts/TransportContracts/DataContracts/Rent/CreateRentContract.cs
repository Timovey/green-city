using GreenCity.Http;
using TransportContracts.DateRange;

namespace TransportContracts.DataContracts.Rent
{
    public class CreateRentContract : CommonHttpContract
    {
        /// <summary> ИД транспортного средства </summary>
        public int TransportId { get; set; }

        /// <summary> ИД организации </summary>
        public int OrganizationId { get; set; }

        /// <summary> Настройка времени существования Аренды транспорта</summary>
        public DateRangeBaseModel DateRange { get; set; }
    }
}
