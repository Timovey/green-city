using GreenCity.Http;
using System.Collections.Generic;

namespace LogistService.Contracts.DataContracts.BaseRoute
{
    /// <summary> Контракт на получение базовых маршрутов </summary>
    public class GetBaseRouteContract : CommonHttpContract
    {
        /// <summary> ИД базовых маршрутов </summary>
        public IList<int> Ids { get; set; }
    }
}
