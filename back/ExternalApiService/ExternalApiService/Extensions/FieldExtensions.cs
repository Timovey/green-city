using GreenCity.CommonUtils.CommonExtensions;
using System;

namespace ExternalApiService.Extensions
{
    internal static class FieldExtensions
    {
        internal static DateTime GetDateTimeOrDefault(this string date)
        {
            if (date.IsNullOrEmpty())
                return default(DateTime);

            try
            {
                return DateTime.Parse(date);
            }
            catch
            {
                return default(DateTime);
            }
        }

        internal static string GetStringDateTime(this DateTime date)
        {
            return date.ToString();
        }
    }
}
