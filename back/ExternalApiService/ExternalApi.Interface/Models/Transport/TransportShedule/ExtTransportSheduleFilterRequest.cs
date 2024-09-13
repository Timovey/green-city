using ExternalApi.Interface.Http;
using System;
using System.Collections.Generic;

namespace ExternalApi.Interface.Models.Transport.TransportShedule
{
    public class ExtTransportSheduleFilterRequest : CommonHttpFilterV2
    {
        public IList<int> TransportIds { get; set; }

        public IList<byte> WorkSheduleTypes { get; set; }

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

        public ExtTransportSheduleSortModel Sorting { get; set; }
    }
}
