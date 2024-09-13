using GreenCity.Http;
using System.Collections.Generic;

namespace SDContracts.DataContracts.Stand
{
    public class GetStandsForRouteByRouteIdContract : CommonHttpContract
    {
        /// <summary>
        /// ИД рейса
        /// </summary>
        public int RouteId { get; set; }

        /// <summary>
        /// Идентификаторы КП, по которым нужно отфильтровать ответ
        /// </summary>
        public IList<int> StandIds { get; set; }
    }
}
