using GreenCity.Http;
using System;
using System.Collections.Generic;
using TransportContracts.SortModels;

namespace TransportContracts.Filters
{
    public class TransportDutyShiftFilterModel : CommonHttpFilter
    {
        public IList<int> TransportIds { get; set; }

        /// <summary> Дата выхода От </summary>
        public DateTime? DateFrom { get; set; }

        /// <summary> Дата выхода До</summary>
        public DateTime? DateTo { get; set; }

        public TransportDutyShiftSortModel Sorting { get; set; }
    }
}
