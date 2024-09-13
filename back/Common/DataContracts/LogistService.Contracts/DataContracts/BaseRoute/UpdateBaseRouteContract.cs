using GreenCity.Http;
using GreenCity.Http.Attributes.Validation;
using System.Collections.Generic;

namespace LogistService.Contracts.DataContracts.BaseRoute
{
    /// <summary> Контракт на обновление информации по базовому маршруту </summary>
    public class UpdateBaseRouteContract : CommonHttpContract
    {
        /// <summary> ИД обновляемого БМ </summary>
        [GCRequired("ИД БМ")]
        public int Id { get; set; }

        /// <summary> Название БМ </summary>
        [GCRequired("Название БМ"), GCMaxLength(512, "Название БМ")]
        public string Name { get; set; }

        /// <summary> Массив ИД контейнерных площадок </summary>
        [GCRequired("Массив ИД контейнерных площадок")]
        public IList<int> Stands { get; set; }
    }
}