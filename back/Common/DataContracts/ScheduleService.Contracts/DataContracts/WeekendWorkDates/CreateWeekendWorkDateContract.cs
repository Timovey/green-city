using GreenCity.Http;
using System;

namespace ScheduleService.Contracts.DataContracts.WeekendWorkDates
{
    public class CreateWeekendWorkDateContract : CommonHttpContract
    {
        public DateTime Date { get; set; }
    }
}
