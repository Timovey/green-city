using GreenCity.Http;
using System;
using System.Collections.Generic;

namespace DispatcherService.Contracts.Filters
{
    public class CommonBrigadesFilterModel : CommonHttpFilter
    {
        public DateTime Date { get; set; }

        public IList<int> WorkShiftIds { get; set; }

        public IList<int> RouteIds { get; set; }
    }
}
