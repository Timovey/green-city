using ExternalApi.Interface.Models.Transport.DateRange;
using GreenCity.Http;
using System.Collections.Generic;

namespace ExternalApi.Interface.Models.Transport.RepairCard
{
    public class ExtUpdateRepairCardRequest : CommonHttpContract
    {
        /// <summary> ИД карточки ремонта </summary>
        public IList<int> RepairCardIds { get; set; }

        /// <summary> Событие </summary>
        public ExtRepairType Event { get; set; }

        /// <summary> ИД организации </summary>
        public int? OrganizationId { get; set; }

        /// <summary> ИД транспортного средства </summary>
        public int TransportId { get; set; }

        /// <summary>
        /// В какую смену производиться ремонт/ТО при графике 6/3
        /// </summary>
        public ExtWorkShift WorkShift { get; set; }

        /// <summary> Настройка времени существования ТО/Ремонта транспорта</summary>
        public ExtDateRangeBaseModel DateRange { get; set; }
    }
}
