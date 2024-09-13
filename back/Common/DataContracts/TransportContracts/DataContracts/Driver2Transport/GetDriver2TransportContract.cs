using GreenCity.Http;
using System.Collections.Generic;

namespace TransportContracts.DataContracts.Driver2Transport
{
    /// <summary>
    /// Контракт на получение сущности для данных по закреплению водителя
    /// </summary>
    public class GetDriver2TransportContract : CommonHttpContract
    {
        /// <summary> ИД сущности для данных по закреплению водителя </summary>
        public IList<int> Driver2TransportIds { get; set; }
    }
}
