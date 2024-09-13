using GreenCity.Http;
using GreenCity.Http.Attributes.Validation;
using System.Collections.Generic;

namespace LogistService.Contracts.DataContracts.BaseRoute
{
    /// <summary> Контракт на создание базового маршрута </summary>
    public class CreateBaseRouteContract : CommonHttpContract
    {
        /// <summary> Название БМ </summary>
        [GCRequired("Название БМ"), GCMaxLength(512, "Название БМ")]
        public string Name { get; set; }

        /// <summary> Массив ИД контейнерных площадок </summary>
        [GCRequired("Массив ИД контейнерных площадок")]
        public IList<int> Stands { get; set; }
    }
}
