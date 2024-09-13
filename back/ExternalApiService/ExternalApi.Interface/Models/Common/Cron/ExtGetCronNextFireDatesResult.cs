using System;
using System.Collections.Generic;

namespace ExternalApi.Interface.Models.Common.Cron
{
    public class ExtGetCronNextFireDatesResult
    {
        public IList<DateTime> Dates { get; set; }
    }
}
