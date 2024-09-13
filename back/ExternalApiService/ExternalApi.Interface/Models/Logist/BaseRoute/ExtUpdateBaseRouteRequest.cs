using GreenCity.Http;
using System.Collections.Generic;

namespace ExternalApi.Interface.Models.Logist.BaseRoute
{
    /// <summary> Контракт на обновление информации по базовому маршруту </summary>
    public class ExtUpdateBaseRouteRequest : CommonHttpContract
    {
        /// <summary> ИД обновляемого БМ </summary>
        public int Id { get; set; }

        /// <summary> Название БМ </summary>
        public string Name { get; set; }

        /// <summary> Массив ИД контейнерных площадок </summary>
        public IList<int> Stands { get; set; }
    }
}
