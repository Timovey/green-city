using GreenCity.Http;
using System;
using System.Collections.Generic;

namespace ExternalApi.Interface.Models.Dispatcher.TravelSheet
{
    public class ExtCommonTravelSheetFilterRequest : CommonHttpFilter
    {
        public IList<int> TravelSheetIds { get; set; }

        public string NumberToShow { get; set; }

        public DateTime Date { get; set; }

        public IList<int> OrgIds { get; set; }

        public IList<int> DriverIds { get; set; }

        public IList<int> CarIds { get; set; }
    }
}
