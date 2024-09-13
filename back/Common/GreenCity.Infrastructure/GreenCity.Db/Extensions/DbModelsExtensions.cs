using GreenCity.Db.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GreenCity.Db.Extensions
{
    public static class DbModelsExtensions
    {
        public static T FirstOrDefaultInstance<T>(this IEnumerable<T> objects) where T : BaseDbModel
        {
            var first = objects.FirstOrDefault();

            if (first != null)
                return first;

            var defaultInstance = Activator.CreateInstance(typeof(T));
            var type = typeof(T);
            var idProp = type.GetProperty("Id");
            if (idProp != null && idProp.PropertyType.IsValueType)
            {
                idProp.SetValue(defaultInstance, -1);
            }

            return (T)defaultInstance;
        }
    }
}
