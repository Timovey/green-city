using System.Collections.Generic;
using GreenCity.Http;

namespace ExternalApi.Interface.Models.Logist.Route
{
    /// <summary> Контракт на создание рейса </summary>
    public class ExtCreateRouteRequest : CommonHttpContract
    {
        /// <summary> Дата рейса </summary>
        public string Date { get; set; }

        /// <summary> Готовность рейса </summary>
        public bool RouteReady { get; set; }

        /// <summary> ИД экипажа </summary>
        public int TransportWorkShiftId { get; set; }

        /// <summary> Массив ИД контейнерных площадок </summary>
        public IList<int> StandIds { get; set; }
    }
}