using System.Collections.Generic;

namespace DispatcherService.Contracts.DataContracts.ShiftTask.Report
{
    public class ShiftTaskRouteReportModel
    {
        /// <summary>
        /// Номер рейса
        /// </summary>
        public string RouteNumber { get; set; }

        /// <summary>
        /// КП в рейсе
        /// </summary>
        public IList<ShiftTaskStandReportModel> ShiftTaskStands { get; set; }
    }
}
