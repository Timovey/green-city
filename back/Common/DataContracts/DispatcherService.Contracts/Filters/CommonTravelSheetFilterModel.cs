using GreenCity.Http;
using System;
using System.Collections.Generic;

namespace DispatcherService.Contracts.Filters
{
    public class CommonTravelSheetFilterModel : CommonHttpFilter
    {
        public IList<int> TravelSheetIds { get; set; }

        public string NumberToShow { get; set; }

        public DateTime? Date { get; set; }

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }

        public IList<int> OrgIds { get; set; }

        public IList<int> DriverIds { get; set; }
        
        public IList<int> CarIds { get; set; }

        public bool? WithNotEmptyStartDriveTime { get; set; }
    }
}
