using System.Collections.Generic;

namespace ExternalApi.Interface.Models.Dispatcher.Mission
{
    public class ExtMissionsForDispatcherViewModel
    {
        public int Id { get; set; }

        public string RouteName { get; set; }

        public string Date { get; set; }

        public IList<ExtCommonDispatcherMissionViewModel> Missions { get; set; }

        /// <summary> Место выгрузки </summary>
        public string RouteFinishPlace { get; set; }

        /// <summary> Объем выгрузки </summary>
        public decimal RouteTotalVolumeOnFinish { get; set; }
    }
}
