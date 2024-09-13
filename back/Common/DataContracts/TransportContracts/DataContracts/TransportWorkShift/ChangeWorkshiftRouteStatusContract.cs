using GreenCity.Http;
using System.Collections.Generic;

namespace TransportContracts.DataContracts.TransportWorkShift
{
    public class ChangeWorkshiftRouteStatusContract : CommonHttpContract
    {
        /// <summary>
        /// идшники рабочих смен
        /// </summary>
        public IList<int>  WorkshiftIds { get; set; }

        /// <summary>
        /// На какой статус меняем
        /// true - в рейсе
        /// </summary>
        public bool RouteStatus { get; set; }
    }
}
