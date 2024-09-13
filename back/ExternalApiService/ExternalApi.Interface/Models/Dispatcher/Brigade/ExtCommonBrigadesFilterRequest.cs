using GreenCity.Http;
using System.Collections.Generic;

namespace ExternalApi.Interface.Models.Dispatcher.Brigade
{
    public class ExtCommonBrigadesFilterRequest : CommonHttpFilter
    {
        public string Date { get; set; }

        public IList<int> WorkShiftIds { get; set; }

        public IList<int> RouteIds { get; set; }
    }
}
