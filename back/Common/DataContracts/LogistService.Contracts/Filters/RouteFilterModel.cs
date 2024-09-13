using System;
using System.Collections.Generic;
using GreenCity.Http;
using LogistService.Contracts.SortModels;

namespace LogistService.Contracts.Filters
{
    public class RouteFilterModel : CommonHttpFilter
    {
        /// <summary> Дата рейса </summary>
        public DateTime Date { get; set; }

        /// <summary> ИД экипажей </summary>
        public IList<int> TransportWorkShiftIds { get; set; }

        public bool? IsReady { get; set; }

        /// <summary> Настройки сортировки </summary>
        public RouteSortModel Sorting { get; set; }
    }
}