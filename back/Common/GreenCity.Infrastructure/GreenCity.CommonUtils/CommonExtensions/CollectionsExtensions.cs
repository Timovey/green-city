using System.Collections.Generic;
using System.Linq;

namespace GreenCity.CommonUtils.CommonExtensions
{
    public static class CollectionsExtensions
    {
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> collection)
        {
            if (collection == null)
                return true;

            if (!collection.Any())
                return true;

            return false;
        }

        public static IEnumerable<TSource> Paging<TSource>(this IEnumerable<TSource> source, int itemsOnPage, int page)
        {
            if (itemsOnPage == 0)
            {
                return source;
            }

            source = source.Skip(itemsOnPage * page);
            source = source.Take(itemsOnPage);
            return source;
        }
    }
}
