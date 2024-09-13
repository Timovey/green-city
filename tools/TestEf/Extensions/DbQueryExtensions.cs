using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace TestEf.Extensions
{
    public static class DbQueryExtensions
    {
        public static IQueryable<TSource> DefaultIfEmptyIf<TSource>(this IQueryable<TSource> source, bool condition)
        {
            if (condition)
            {
                return source.DefaultIfEmpty();
            }

            return source;
        }

        public static IEnumerable<TSource> DefaultIfEmptyIf<TSource>(this IEnumerable<TSource> source, bool condition)
        {
            if (condition)
            {
                return source.DefaultIfEmpty();
            }

            return source;
        }

        public static IQueryable<TSource> WhereIf<TSource>(this IQueryable<TSource> source, bool condition, Expression<Func<TSource, bool>> predicate)
        {
            if (condition)
            {
                return source.Where(predicate);
            }

            return source;
        }
    }
}
