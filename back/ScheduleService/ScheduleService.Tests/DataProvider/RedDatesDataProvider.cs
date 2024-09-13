using ScheduleService.Contracts.ViewModels.RedDates;
using System;
using System.Collections.Generic;

namespace ScheduleService.Tests.DataProvider
{
    internal class RedDatesDataProvider
    {
        public static IList<RedDateViewModel> GetNullDates()
        {
            return null;
        }

        public static IList<RedDateViewModel> GetEmptyDates()
        {
            return new List<RedDateViewModel>();
        }
        
        public static IList<RedDateViewModel> GetRedDatesForMostUnusualScheduleNum2()
        {
            return new List<RedDateViewModel>
            {
                new RedDateViewModel { Date = DateTime.Parse($"2022-10-03"), Name = "Test_Red_Date" },
                new RedDateViewModel { Date = DateTime.Parse($"2023-01-02"), Name = "Test_Red_Date" },
                new RedDateViewModel { Date = DateTime.Parse($"2023-01-03"), Name = "Test_Red_Date" },
                new RedDateViewModel { Date = DateTime.Parse($"2023-01-04"), Name = "Test_Red_Date" },
                new RedDateViewModel { Date = DateTime.Parse($"2023-01-05"), Name = "Test_Red_Date" },
                new RedDateViewModel { Date = DateTime.Parse($"2023-01-06"), Name = "Test_Red_Date" }
            };
        }

        public static IList<RedDateViewModel> GetRedDatesDefaultSet()
        {
            return new List<RedDateViewModel>
            {
                new RedDateViewModel { Date = DateTime.Parse($"2022-01-01"), Name = "Test_Red_Date" },
                new RedDateViewModel { Date = DateTime.Parse($"2022-03-15"), Name = "Test_Red_Date" },
                new RedDateViewModel { Date = DateTime.Parse($"2022-04-18"), Name = "Test_Red_Date" },
                new RedDateViewModel { Date = DateTime.Parse($"2022-05-12"), Name = "Test_Red_Date" },
                new RedDateViewModel { Date = DateTime.Parse($"2022-06-13"), Name = "Test_Red_Date" },
                new RedDateViewModel { Date = DateTime.Parse($"2022-09-17"), Name = "Test_Red_Date" },
                new RedDateViewModel { Date = DateTime.Parse($"2022-12-31"), Name = "Test_Red_Date" }
            };
        }
    }
}
