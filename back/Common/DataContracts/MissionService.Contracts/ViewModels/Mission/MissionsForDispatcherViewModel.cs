using System;
using System.Collections.Generic;

namespace MissionService.Contracts.ViewModels.Mission
{
    public class MissionsForDispatcherViewModel
    {
        public int RouteId { get; set; }

        public string RouteName { get; set; }
        
        public DateTime RouteDate { get; set; }

        public IList<CommonMissionViewModel> Missions { get; set; }

        /// <summary> Место выгрузки </summary>
        public string RouteFinishPlace { get; set; }

        /// <summary> Объем выгрузки </summary>
        public decimal RouteTotalVolumeOnFinish { get; set; }
    }
}
