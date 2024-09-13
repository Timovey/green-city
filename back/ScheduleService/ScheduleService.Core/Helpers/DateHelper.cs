using System;
using System.Collections.Generic;
using System.Linq;

namespace ScheduleService.Core.Helpers
{
    internal class DateHelper
    {
        public static DateTime GetNextWorkDate(
            DateTime initialDate,
            IList<DateTime> weekendWorkDates = default,
            HashSet<DateTime> redDatesHashSet = default
        )
        {
            if (initialDate.Date.Year > 2100)
                return DateTime.MaxValue;

            var currentDate = initialDate.Date;
            bool isWeekend;
            while (currentDate.Date.Year < 2100)
            {
                currentDate = currentDate.Date.AddDays(1);

                isWeekend = false;
                if (currentDate.Date.DayOfWeek == DayOfWeek.Saturday || currentDate.Date.DayOfWeek == DayOfWeek.Sunday)
                {
                    //Если текущая дата есть среди рабочих выходных, то день считаем рабочим
                    if (
                        weekendWorkDates != default &&
                        weekendWorkDates.Count > 0 &&
                        weekendWorkDates.Select(x => x.Date.Date).FirstOrDefault(x => x.Date == currentDate.Date) != default)
                    {
                        isWeekend = false;
                    }
                    else
                    {
                        isWeekend = true;
                    }
                }

                //!_! Если график выпал на красный день или выходной смотрим на настройки
                if (!(isWeekend || redDatesHashSet.TryGetValue(currentDate.Date, out DateTime result)))
                {
                    return currentDate.Date;
                }
            }

            return DateTime.MaxValue;
        }

        public static DateTime GetPrevWorkDate(
            DateTime initialDate,
            IList<DateTime> weekendWorkDates = default,
            HashSet<DateTime> redDatesHashSet = default
        )
        {
            if (initialDate.Date.Year < 1970)
                return DateTime.MaxValue;

            var currentDate = initialDate.Date;
            bool isWeekend;
            while (currentDate.Date.Year > 1970)
            {
                currentDate = currentDate.Date.AddDays(-1);

                isWeekend = false;
                if (currentDate.Date.DayOfWeek == DayOfWeek.Saturday || currentDate.Date.DayOfWeek == DayOfWeek.Sunday)
                {
                    //Если текущая дата есть среди рабочих выходных, то день считаем рабочим
                    if (
                        weekendWorkDates != default &&
                        weekendWorkDates.Count > 0 &&
                        weekendWorkDates.Select(x => x.Date.Date).FirstOrDefault(x => x.Date == currentDate.Date) != default)
                    {
                        isWeekend = false;
                    }
                    else
                    {
                        isWeekend = true;
                    }
                }

                //!_! Если график выпал на красный день или выходной смотрим на настройки
                if (!(isWeekend || redDatesHashSet.TryGetValue(currentDate.Date, out DateTime result)))
                {
                    return currentDate.Date;
                }
            }

            return DateTime.MaxValue;
        }

        public static DateTime GetNearestWorkDate(
            DateTime initialDate,
            IList<DateTime> weekendWorkDates = default,
            HashSet<DateTime> redDatesHashSet = default
        )
        {
            var prev = GetPrevWorkDate(initialDate, weekendWorkDates, redDatesHashSet);
            var next = GetNextWorkDate(initialDate, weekendWorkDates, redDatesHashSet);

            return Math.Abs((initialDate.Date - prev.Date).Days) > Math.Abs((initialDate - prev.Date).Days) ? next.Date : prev.Date;
        }
    }
}
