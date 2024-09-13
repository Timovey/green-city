using GreenCity.Http;
using System.Collections.Generic;

namespace ExternalApi.Interface.Models.Dispatcher.Brigade
{
    public class ExtCreateBrigadeRequest : CommonHttpContract
    {
        public string Date { get; set; }

        public int WorkShiftId { get; set; }

        public IList<int> AuxWorkerIds { get; set; }

        public IList<int> RouteIds { get; set; }
    }
}
