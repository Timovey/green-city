using System.Collections.Generic;

namespace ExternalApi.Interface.Models.Logist.Route
{
    public class ExtRouteViewModel
    {
        /// <summary> ИД рейса </summary>
        public int Id { get; set; }

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