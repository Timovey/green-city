using GreenCity.Http;
using TransportContracts.DataContracts.Enums;
using TransportContracts.DateRange;

namespace TransportContracts.DataContracts.RepairCard
{
    public class CreateRepairCardContract : CommonHttpContract
    {
        /// <summary> Событие </summary>
        public RepairType Event { get; set; }

        /// <summary> ИД организации </summary>
        public int? OrganizationId { get; set; }

        /// <summary> ИД транспортного средства </summary>
        public int TransportId { get; set; }

        /// <summary>
        /// В какую смену производиться ремонт/ТО при графике 6/3
        /// </summary>
        public WorkShift WorkShift { get; set; }

        /// <summary> Настройка времени существования ТО/Ремонта транспорта</summary>
        public DateRangeBaseModel DateRange { get; set; }
    }
}
