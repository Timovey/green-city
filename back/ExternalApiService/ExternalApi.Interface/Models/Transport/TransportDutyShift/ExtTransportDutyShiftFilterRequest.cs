using GreenCity.Http;
using System;
using System.Collections.Generic;

namespace ExternalApi.Interface.Models.Transport.TransportDutyShift
{
    public class ExtTransportDutyShiftFilterRequest : CommonHttpFilter
    {
        public IList<int> TransportIds { get; set; }

        /// <summary> Дата выхода От </summary>
        public DateTime? DateFrom { get; set; }

        /// <summary> Дата выхода До</summary>
        public DateTime? DateTo { get; set; }

        public ExtTransportDutyShiftSortModel Sorting { get; set; }
    }
}
