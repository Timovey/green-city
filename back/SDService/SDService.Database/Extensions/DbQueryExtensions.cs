using System.Linq;

namespace SDService.Database.Extensions
{
    internal static class DbQueryExtensions
    {
        public static IQueryable<TSource> DefaultIfEmptyIf<TSource>(this IQueryable<TSource> source, bool condition)
        {
            if (condition)
            {
                return source.DefaultIfEmpty();
            }

            return source;
        }
    }
}
