using GreenCity.Http;
using System.Collections.Generic;

namespace ExternalApi.Interface.Models.Transport.Driver2Transport
{
    /// <summary>
    /// Контракт на получение сущности для данных по закреплению водителя
    /// </summary>
    public class ExtGetDriver2TransportRequest : CommonHttpContract
    {
        /// <summary> ИД сущности для данных по закреплению водителя </summary>
        public IList<int> Driver2TransportIds { get; set; }
    }
}
