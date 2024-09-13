using System;
using System.Collections.Generic;
using GreenCity.Http;
using GreenCity.Http.Attributes.Validation;

namespace LogistService.Contracts.DataContracts.Route
{
    /// <summary> Контракт на создание рейса </summary>
    public class CreateRouteContract : CommonHttpContract
    {
        /// <summary> Дата рейса </summary>
        [GCRequired("Дата рейса")]
        public DateTime Date { get; set; }

        /// <summary> Готовность рейса </summary>
        public bool RouteReady { get; set; }

        /// <summary> ИД экипажа </summary>
        [GCRequired("ИД экипажа")]
        public int TransportWorkShiftId { get; set; }

        /// <summary> Массив ИД контейнерных площадок </summary>
        [GCRequired("Массив ИД контейнерных площадок")]
        public IList<int> StandIds { get; set; }
    }
}