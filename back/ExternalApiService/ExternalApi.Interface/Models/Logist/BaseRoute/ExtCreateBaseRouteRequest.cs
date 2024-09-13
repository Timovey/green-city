using GreenCity.Http;
using System.Collections.Generic;

namespace ExternalApi.Interface.Models.Logist.BaseRoute
{
    /// <summary> Контракт на создание базового маршрута </summary>
    public class ExtCreateBaseRouteRequest : CommonHttpContract
    {
        /// <summary> Название БМ </summary>
        public string Name { get; set; }

        /// <summary> Массив ИД контейнерных площадок </summary>
        public IList<int> Stands { get; set; }
    }
}
