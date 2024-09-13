using System;
using System.Collections.Generic;

namespace ExternalApi.Interface.Models.Dispatcher.ShiftTask
{
    public class ExtCreateShiftTaskResponse
    {
        public int Id { get; set; }

        public int TravelSheetId { get; set; }

        public DateTime Date { get; set; }

        public int WorkShiftId { get; set; }

        public int OrgId { get; set; }

        public IList<int> RouteIds { get; set; }
    }
}
