using GreenCity.Http;
using System.Collections.Generic;

namespace ScheduleService.Contracts.DataContracts.DateRange
{
    public class GetDateRangeContract : CommonHttpContract
    {
        public IList<BaseDateRangeModel> DateRanges { get; set; }
    }
}
