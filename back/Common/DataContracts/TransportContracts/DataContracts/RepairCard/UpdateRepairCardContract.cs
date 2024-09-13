using GreenCity.Http;
using System.Collections.Generic;
using TransportContracts.DataContracts.Enums;
using TransportContracts.DateRange;

namespace TransportContracts.DataContracts.RepairCard
{
    public class UpdateRepairCardContract : CommonHttpContract
    {
        /// <summary> ИД карточки ремонта </summary>
        public IList<int> RepairCardIds { get; set; }

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
