using System.Collections.Generic;

namespace MissionService.Contracts.ViewModels.Mission
{
    public class MissionsForBuildDoneTaskListReportViewModel
    {
        public int RouteId { get; set; }

        public string RouteName { get; set; }

        public IList<MissionForBuildDoneTaskListReportViewModel> Missions { get; set; }

        /// <summary> Место выгрузки </summary>
        public string RouteFinishPlace { get; set; }

        /// <summary> Объем выгрузки </summary>
        public decimal RouteTotalVolumeOnFinish { get; set; }
    }
}
