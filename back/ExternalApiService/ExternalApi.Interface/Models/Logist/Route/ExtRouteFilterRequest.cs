using System.Collections.Generic;
using GreenCity.Http;

namespace ExternalApi.Interface.Models.Logist.Route
{
    public class ExtRouteFilterRequest : CommonHttpFilter
    {
        /// <summary> Дата рейса </summary>
        public string Date { get; set; }

        /// <summary> ИД экипажей </summary>
        public IList<int> TransportWorkShiftIds { get; set; }

        /// <summary> Настройки сортировки </summary>
        public ExtRouteSortModel Sorting { get; set; }
    }
}