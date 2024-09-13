using System.Collections.Generic;

namespace ReportService.Contracts.DataContracts.DoneTasksList
{
    public class DoneTaskListContract
    {
        public IList<DoneTaskListRouteInfo> Routes { get; set; }
    }
}
