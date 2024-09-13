using System.Collections.Generic;

namespace ExternalApi.Interface.Models.Dispatcher.Brigade
{
    public class ExtCreateBrigadeResponse
    {
        public int Id { get; set; }

        public string Date { get; set; }

        public int WorkShiftId { get; set; }

        public IList<int> AuxWorkerIds { get; set; }

        public IList<int> RouteIds { get; set; }
    }
}
