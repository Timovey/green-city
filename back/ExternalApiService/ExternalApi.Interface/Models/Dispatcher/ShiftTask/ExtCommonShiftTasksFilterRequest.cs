using GreenCity.Http;
using System.Collections.Generic;

namespace ExternalApi.Interface.Models.Dispatcher.ShiftTask
{
    public class ExtCommonShiftTasksFilterRequest : CommonHttpFilter
    {
        public string Date { get; set; }

        public IList<int> OrgIds { get; set; }

        public IList<int> WorkShiftIds { get; set; }

        public string TravelSheetNumber { get; set; }

        public IList<int> RouteIds { get; set; }
    }
}
