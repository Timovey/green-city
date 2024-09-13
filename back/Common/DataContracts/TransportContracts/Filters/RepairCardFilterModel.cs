using GreenCity.Http;
using System;
using System.Collections.Generic;
using TransportContracts.SortModels;

namespace TransportContracts.Filters
{
    public class RepairCardFilterModel : CommonHttpFilter
    {
        /// <summary> Событие </summary>
        public IList<byte> Events { get; set; }

        /// <summary> ИД организации </summary>
        public IList<int> OrganizationIds { get; set; }

        /// <summary> ИД транспортного средства </summary>
        public IList<int> TransportIds { get; set; }

        /// <summary>
        /// В какую смену производится ремонт
        /// </summary>
        public IList<byte> WorkShifts { get; set; }

        /// <summary> Дата создания сущности От </summary>
        public DateTime? CreateDateFrom { get; set; }

        /// <summary> Дата создания сущности До</summary>
        public DateTime? CreateDateTo { get; set; }

        /// <summary> Дата начала работы От</summary>
        public DateTime? StartDateFrom { get; set; }

        /// <summary> Дата начала работы до</summary>
        public DateTime? StartDateTo { get; set; }

        /// <summary> Дата окончания работы От</summary>
        public DateTime? EndDateFrom { get; set; }

        /// <summary> Дата окончания работы До</summary>
        public DateTime? EndDateTo { get; set; }

        public RepairCardSortModel Sorting { get; set; }
    }
}
