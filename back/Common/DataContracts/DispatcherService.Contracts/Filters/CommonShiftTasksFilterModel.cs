using GreenCity.Http;
using System;
using System.Collections.Generic;

namespace DispatcherService.Contracts.Filters
{
    public class CommonShiftTasksFilterModel : CommonHttpFilter
    {
        public IList<int> ShiftTaskIds { get; set; }

        public DateTime Date { get; set; }

        public IList<int> OrgIds { get; set; }

        public IList<int> WorkShiftIds { get; set; }

        public string TravelSheetNumber { get; set; }

        public IList<int> RouteIds { get; set; }
    }
}
