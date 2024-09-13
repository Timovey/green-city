using System.Collections.Generic;

namespace MissionService.Contracts.ViewModels.Mission
{
    public class MissionsForBuildShiftTaskReportViewModel
    {
        public int RouteId { get; set; }

        public string RouteName { get; set; }

        public IList<MissionForBuildShiftTaskReportViewModel> Missions { get; set; }
    }
}
