using GreenCity.Http;
using System.Collections.Generic;

namespace ExternalApi.Interface.Models.Dispatcher.Brigade
{
    public class ExtDeleteBrigadesRequest : CommonHttpContract
    {
        public IList<int> BrigadeIds { get; set; }
    }
}
