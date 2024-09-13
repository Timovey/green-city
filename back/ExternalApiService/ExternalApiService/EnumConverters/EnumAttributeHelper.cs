using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace ExternalApiService.EnumConverters
{
    public static class EnumAttributeHelper
    {
        public static string DisplayName(this Enum value)
        {
            Type enumType = value.GetType();
            var enumValue = Enum.GetName(enumType, value);
            MemberInfo member = enumType.GetMember(enumValue)[0];

            var outString = enumValue;
            var attrs = member.GetCustomAttributes(typeof(DisplayAttribute), false);
            if(attrs != null && attrs.Length > 0)
            {
                outString = ((DisplayAttribute)attrs[0]).Name;

                if (((DisplayAttribute)attrs[0]).ResourceType != null)
                {
                    outString = ((DisplayAttribute)attrs[0]).GetName();
                }
            }
            return outString;
        }

        public static string GetName(this Type value)
        {
            string name = value.Name;

            int n = name.IndexOf("Ext");
            if(n >= 0) name = name.Remove(n, "Ext".Length);

            n = name.IndexOf("Type");
            if (n >= 0) name = name.Remove(n, "Type".Length);

            return name;
        }
    }
}
