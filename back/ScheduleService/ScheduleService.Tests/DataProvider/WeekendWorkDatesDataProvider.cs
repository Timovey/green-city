using ScheduleService.Contracts.ViewModels.WeekendWorkDates;
using System;
using System.Collections.Generic;

namespace ScheduleService.Tests.DataProvider
{
    internal class WeekendWorkDatesDataProvider
    {
        public static IList<WeekendWorkDateViewModel> GetNullDates()
        {
            return null;
        }

        public static IList<WeekendWorkDateViewModel> GetEmptyDates()
        {
            return new List<WeekendWorkDateViewModel>();
        }

        public static IList<WeekendWorkDateViewModel> GetWeekendWorkDatesForMostUnusualScheduleNum2()
        {
            return new List<WeekendWorkDateViewModel>
            {
                new WeekendWorkDateViewModel { Date = DateTime.Parse("2022-10-15") }
            };
        }

        public static IList<WeekendWorkDateViewModel> GetWeekendWorkDatesDefaultSet()
        {
            return new List<WeekendWorkDateViewModel>
            {
                new WeekendWorkDateViewModel { Date = DateTime.Parse("2022-01-03") },
                new WeekendWorkDateViewModel { Date = DateTime.Parse("2022-01-01") },
                new WeekendWorkDateViewModel { Date = DateTime.Parse("2022-03-12") },
                new WeekendWorkDateViewModel { Date = DateTime.Parse("2022-09-18") }
            };
        }
    }
}
