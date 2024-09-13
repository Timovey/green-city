using System.Collections.Generic;

namespace ExternalApi.Interface.Models.Logist.BaseRoute
{
    public class ExtBaseRouteWithStandsViewModel
    {
        /// <summary> ИД БМ </summary>
        public int Id { get; set; }

        /// <summary> Название БМ </summary>
        public string Name { get; set; }

        /// <summary> Массив ИД контейнерных площадок </summary>
        public IList<int> Stands { get; set; }
    }
}
