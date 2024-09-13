using GreenCity.Http;
using System.Collections.Generic;

namespace ExternalApi.Interface.Models.Transport.TransportShedule
{
    /// <summary>
    /// Контракт на удаление расписания транспорта
    /// </summary>
    public class ExtDeleteTransportSheduleRequest : CommonHttpContract
    {
        public IList<int> TransportSheduleIds { get; set; }
    }
}
