using System.Collections.Generic;

namespace ExternalApi.Interface.Models.Dispatcher.Mission
{
    public class ExtUpdateMissionsForCloseRouteResponse
    {
        public int Id { get; set; }

        public string RouteName { get; set; }

        public IList<ExtFrontMissionUpdateModel> Missions { get; set; }

        /// <summary> Место выгрузки </summary>
        public string RouteFinishPlace { get; set; }

        /// <summary> Объем выгрузки </summary>
        public decimal RouteTotalVolumeOnFinish { get; set; }
    }
}
