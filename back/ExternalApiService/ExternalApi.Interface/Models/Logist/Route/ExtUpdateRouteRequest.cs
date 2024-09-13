using System.Collections.Generic;
using GreenCity.Http;

namespace ExternalApi.Interface.Models.Logist.Route
{
    /// <summary> Контракт на обновление информации по рейсу </summary>
    public class ExtUpdateRouteRequest : CommonHttpContract
    {
        /// <summary> ИД обновляемого рейса </summary>
        public int Id { get; set; }

        /// <summary> Готовность рейса </summary>
        public bool RouteReady { get; set; }

        /// <summary> Массив ИД контейнерных площадок </summary>
        public IList<int> StandIds { get; set; }
    }
}