using System.Collections.Generic;

namespace DispatcherService.Contracts.DataContracts.DoneTaskList.Report
{
    public class DoneTaskListReportModel
    {
        /// <summary> Рейсы </summary>
        public IList<DoneTaskListRouteReportModel> Routes { get; set; }
    }
}