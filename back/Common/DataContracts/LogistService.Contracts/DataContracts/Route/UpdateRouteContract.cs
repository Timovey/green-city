using System.Collections.Generic;
using GreenCity.Http;
using GreenCity.Http.Attributes.Validation;

namespace LogistService.Contracts.DataContracts.Route
{
    /// <summary> Контракт на обновление информации по рейсу </summary>
    public class UpdateRouteContract : CommonHttpContract
    {
        /// <summary> ИД обновляемого рейса </summary>
        [GCRequired("ИД рейса")]
        public int Id { get; set; }

        /// <summary> Готовность рейса </summary>
        public bool RouteReady { get; set; }

        /// <summary> Массив ИД контейнерных площадок </summary>
        [GCRequired("Массив ИД контейнерных площадок")]
        public IList<int> StandIds { get; set; }
    }
}