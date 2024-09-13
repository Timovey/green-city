using System.Collections.Generic;
using System.Linq;

namespace ExternalApiService.Extensions
{
    internal static class CommonHttpResponseExtensions
    {
        public static List<string> GetErrorsOrDefault(this string[] errors)
        {
            return errors == default ? default : errors.ToList();
        }
    }
}
