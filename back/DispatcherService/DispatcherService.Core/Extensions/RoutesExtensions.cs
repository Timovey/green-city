using System.Collections.Generic;
using System.Linq;

namespace DispatcherService.Core.Extensions
{
    internal static class RoutesExtensions
    {
        public static string GetRouteInfoString(
            this IList<int> routeIds,
            IDictionary<int, string> routeDict
        )
        {
            var resultList = new List<string>();

            foreach (var routeId in routeIds.Distinct())
            {
                if (!routeDict.TryGetValue(routeId, out var getResult))
                    continue;

                resultList.Add(getResult);
            }

            return string.Join(';', resultList);
        }
    }
}
