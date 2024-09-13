using GreenCity.CommonUtils.CommonModels;
using GreenCity.Db.BaseModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace GreenCity.Db.Extensions
{
    public static class DbQueryExtensions
    {
        public static IQueryable<TSource> WhereIf<TSource>(
            this IQueryable<TSource> source,
            bool condition,
            Expression<Func<TSource, bool>> predicate
        )
        {
            if (condition)
            {
                return source.Where(predicate);
            }

            return source;
        }

        /// <summary>
        /// Применение пагинации
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="itemsOnPage"> Элементов на странице (0 - вернуть все что есть) </param>
        /// <param name="page"> Страница (начиная с 0) </param>
        /// <returns></returns>
        public static IQueryable<TSource> Paging<TSource>(
            this IQueryable<TSource> source,
            int itemsOnPage,
            int page
        )
        {
            if (itemsOnPage == 0)
            {
                return source;
            }

            source = source.Skip(itemsOnPage * page);
            source = source.Take(itemsOnPage);

            return source;
        }

        /// <summary>
        /// Сортировка с учетом приоритета и типа
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="query"></param>
        /// <param name="sortingModel"></param>
        /// <param name="innerSortProperties"> Внутренние объекты сортировки в формате {Название поля}.{Название поля} </param>
        /// <returns></returns>
        public static IQueryable<TSource> OrderByWithSortingModel<TSource>(
            this IQueryable<TSource> query,
            CommonSortingModel sortingModel,
            string innerSortProperties = null
        )
        {
            if (sortingModel == null)
            {
                sortingModel.Id = new SortModel { SortType = SortType.Desc };
            }

            var sortModelType = sortingModel.GetType();
            var entityType = typeof(TSource);

            Type innerType = entityType;
            ParameterExpression arg = Expression.Parameter(entityType, "x");
            MemberExpression buildedLambdaProperty = null;

            var isFirst = true;
            if (!string.IsNullOrEmpty(innerSortProperties))
            {
                innerSortProperties = innerSortProperties.Trim('.');
                foreach (var prop in innerSortProperties.Split('.'))
                {
                    var sourceProp = innerType.GetProperty(prop);
                    if (sourceProp == null)
                    {
                        return query;
                    }
                    innerType = sourceProp.PropertyType;

                    if (isFirst)
                    {
                        buildedLambdaProperty = Expression.Property(arg, prop);
                        isFirst = false;
                    }
                    else
                    {
                        buildedLambdaProperty = Expression.Property(buildedLambdaProperty, prop);
                    }
                }
            }

            isFirst = true;
            foreach (var prop in sortModelType.GetProperties()
                .Where(x => x.GetValue(sortingModel) != null)
                .OrderBy(x =>
                    (int)x.GetValue(sortingModel)
                    .GetType()
                    .GetProperty(nameof(SortModel.SortPriority))
                    .GetValue(sortModelType.GetProperty(x.Name).GetValue(sortingModel))))
            {
                var sortType = (SortType)prop.GetValue(sortingModel).GetType().GetProperty(nameof(SortModel.SortType)).GetValue(sortModelType.GetProperty(prop.Name).GetValue(sortingModel));
                if (sortType == SortType.NoSort)
                    continue;

                var sortPropertyName = prop.Name;

                var propertyInfo = innerType.GetProperty(sortPropertyName);
                if (propertyInfo == null)
                    continue;

                var property = !string.IsNullOrEmpty(innerSortProperties) ? Expression.Property(buildedLambdaProperty, sortPropertyName) :
                    Expression.Property(arg, sortPropertyName);

                var selector = Expression.Lambda(property, new ParameterExpression[] { arg });

                var orderBy = MakeGenericLinqExtensionMethod("OrderBy", entityType, propertyInfo);
                var orderByDesc = MakeGenericLinqExtensionMethod("OrderByDescending", entityType, propertyInfo);

                var thenBy = MakeGenericLinqExtensionMethod("ThenBy", entityType, propertyInfo);
                var thenByDesc = MakeGenericLinqExtensionMethod("ThenByDescending", entityType, propertyInfo);

                if (!isFirst)
                {
                    query = sortType == SortType.Asc ? (IOrderedQueryable<TSource>)thenBy.Invoke(thenBy, new object[] { query, selector }) :
                        (IOrderedQueryable<TSource>)thenByDesc.Invoke(thenByDesc, new object[] { query, selector });
                }
                else
                {
                    query = sortType == SortType.Asc ? (IOrderedQueryable<TSource>)orderBy.Invoke(orderBy, new object[] { query, selector }) :
                        (IOrderedQueryable<TSource>)orderByDesc.Invoke(orderByDesc, new object[] { query, selector });

                    isFirst = false;
                }
            }

            return query;
        }

        /// <summary>
        /// Собрать кастомную перегрузку метода linq
        /// </summary>
        /// <param name="methodName"> Имя метода расширения </param>
        /// <param name="entityType"> Query type </param>
        /// <param name="propertyInfo"> Свойсво, которое будет использовано для подстановки в сортировку </param>
        /// <returns></returns>
        private static MethodInfo MakeGenericLinqExtensionMethod(string methodName, Type entityType, PropertyInfo propertyInfo)
        {
            var enumarableType = typeof(Queryable);
            var method = enumarableType.GetMethods()
                 .Where(m => m.Name == methodName && m.IsGenericMethodDefinition)
                 .Where(m =>
                 {
                     var parameters = m.GetParameters().ToList();
                     return parameters.Count == 2;
                 }).Single();

            MethodInfo genericMethod = method
                 .MakeGenericMethod(entityType, propertyInfo.PropertyType);

            return genericMethod;
        }

        /// <summary>
        /// Производит операцию мержа набора записей в базу, по определенным правилам (требует SaveChanges!!!)
        /// </summary>
        /// <typeparam name="TDbItem"> Объект бд </typeparam>
        /// <param name="dbSet"> Набор объектов с которыми ведется работа </param>
        /// <param name="equalExpression"> Expression по которому ищем пересечение существующего набора с пришедшим на обновление </param>
        /// <param name="searchFunc"> Делегат осуществляющий поиск обновителя </param>
        /// <param name="updateAction"> Делегат выполняющий обновление сущности в бд </param>
        /// <param name="mergeItems"> Набор элементов пришедших на обновление </param>
        /// <param name="needDelete"> Нужно ли удалять непришедшие записи </param>
        /// <returns> Возвращает кортеж состоящий из 3 списков (удаленные, новые, обновленные) </returns>
        /// <exception cref="ArgumentException"> Выбрасывается в случае если элемент, который необходимо обновить не нашел своего обновителя </exception>
        public static async Task<(IList<TDbItem> DeletedItems, IList<TDbItem> InsertedItems, IList<TDbItem> UpdatedItems)> MergeAsync<TDbItem>(
            this DbSet<TDbItem> dbSet,
            Expression<Func<TDbItem, bool>> equalExpression,
            Func<IList<TDbItem>, TDbItem, TDbItem> searchFunc,
            Action<TDbItem, TDbItem> updateAction,
            IList<TDbItem> mergeItems,
            bool needDelete = false
        )
            where TDbItem : BaseDbModel
        {
            var deletedItems = new List<TDbItem>();
            var updatedItems = new List<TDbItem>();
            var insertedItems = new List<TDbItem>();

            //!_! Собираем делегат, который идентичен условию, только меняет результат на противоположный
            Func<TDbItem, bool> invariantExpression = (source) => !equalExpression.Compile().Invoke(source);
            var invariatExpression = Expression.Lambda<Func<TDbItem, bool>>(Expression.Call(invariantExpression.Method));

            //!_! Элементы на удаление - те которые прошли по основному условию и не прошли по условию операции
            var itemsToDelete = dbSet.Where(invariatExpression);

            if (needDelete)
            {
                await dbSet.ForEachAsync(x =>
                {
                    x.IsDeleted = true;
                    deletedItems.Add(x);
                });
            }

            //!_! Новые элементы - разница между пришедшими на вход и существующими в базе
            var newItems = mergeItems.Except(await dbSet.Where(equalExpression).ToListAsync());

            await dbSet.AddRangeAsync(newItems);
            insertedItems.AddRange(newItems);

            //!_! Обновление одинаковых - одинаковость выясняется по operationEqualExpression (условие операции)
            var itemsToUpdate = dbSet.Where(equalExpression);

            foreach (var el in itemsToUpdate)
            {
                var newEl = searchFunc(mergeItems, el);

                if (newEl != default)
                {
                    updateAction(newEl, el);
                    updatedItems.Add(el);
                }
                else
                {
                    throw new ArgumentException($"Некорректные объекта мержа, либо делегат {nameof(searchFunc)} задан неверно");
                }
            }

            return (deletedItems, insertedItems, updatedItems);
        }
    }
}
