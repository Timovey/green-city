using System;
using System.Collections.Generic;

namespace ExternalApi.Interface.Models.Common.Cron
{
    public class ExtGetCronCollectionNextFireDatesResult
    {
        public IList<DateTime> Dates { get; set; }
    }
}
