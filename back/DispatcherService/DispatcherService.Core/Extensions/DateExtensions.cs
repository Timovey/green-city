using System;
using System.Globalization;

namespace DispatcherService.Core.Extensions
{
    internal static class DateExtensions
    {
        public static (string Day, string Month, string Year) GetRussianDateInfo(this DateTime date)
        {
            var datePartsArray = date.ToString("f", CultureInfo.GetCultureInfo("ru-RU")).Split(' ');

            return (datePartsArray[1], datePartsArray[2], datePartsArray[3]);
        }
    }
}
