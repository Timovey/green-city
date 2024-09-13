using GreenCity.Http;
using System.Collections.Generic;

namespace TransportContracts.DataContracts.TransportShedule
{
    /// <summary>
    /// Контракт на удаление расписания транспорта
    /// </summary>
    public class DeleteTransportSheduleContract : CommonHttpContract
    {
        public IList<int> TransportSheduleIds { get; set; }
    }
}
