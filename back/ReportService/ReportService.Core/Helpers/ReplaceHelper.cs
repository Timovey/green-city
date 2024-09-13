using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using ReportService.Contracts.DataContracts.DoneTasksList;

namespace ReportService.Core.Helpers
{
    public static class ReplaceHelper
    {
        /// <summary>
        /// Метод получения поля и значения для шаблона подстановки из класса 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static List<(string, string)> GetClassProps<T>(T obj) where T : class
        {
            var result = new List<(string, string)>();

            Type type = obj.GetType();

            PropertyInfo[] properties = type.GetProperties();

            foreach (var property in properties)
            {
                var propertyValue = property.GetValue(obj, null);

                var value = propertyValue == null ? "" : propertyValue.ToString();
                result.Add((property.Name, value));
            }

            return result;
        }

        /// <summary>
        /// Метод получения описания поля и значения для сменного задания
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static List<PropertyData> GetPropertiesInfo<T>(T obj) where T : class
        {
            var result = new List<PropertyData>();
            var properties = obj.GetType().GetProperties();

            foreach (var property in properties)
            {
                if (property.PropertyType != typeof(string))
                {
                    if (property.PropertyType == typeof(IList<DoneTaskListStandsInfo>))
                    {
                        var standProps = typeof(DoneTaskListStandsInfo).GetProperties();

                        result.AddRange(from prop in standProps
                            let propAttributes = GetAttributesInfo(prop)
                            select new PropertyData
                            {
                                Description = propAttributes.Item1,
                                IsMerge = propAttributes.Item2,
                                IsEndListValue = propAttributes.Item3,
                                Name = prop.Name
                            });
                    }

                    continue;
                }

                var propertyValue = property.GetValue(obj, null);
                var value = propertyValue == null ? "" : propertyValue.ToString();
                var attributesInfo = GetAttributesInfo(property);

                result.Add(new PropertyData
                {
                    Description = attributesInfo.Item1, Value = value, IsMerge = attributesInfo.Item2,
                    IsEndListValue = attributesInfo.Item3, Name = property.Name
                });
            }

            return result;
        }

        private static (string, bool, bool) GetAttributesInfo(PropertyInfo property)
        {
            var attributes = property.GetCustomAttributes().ToList();
            var descriptionAttribute = (DescriptionAttribute)attributes[0];

            var mergeAttribute = attributes.FirstOrDefault(a => a.GetType().Name.Equals("MergeAttribute"));
            var isMerge = mergeAttribute != null;
            var endListValueAttribute =
                attributes.FirstOrDefault(a => a.GetType().Name.Equals("EndListValueAttribute"));
            var isEndListValue = endListValueAttribute != null;

            return (descriptionAttribute.Description, isMerge, isEndListValue);
        }
    }
}