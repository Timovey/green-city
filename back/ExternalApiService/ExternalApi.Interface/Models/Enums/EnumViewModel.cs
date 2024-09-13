using System.Collections.Generic;

namespace ExternalApi.Interface.Models.Enums
{
    public class EnumViewModel
    {
        /// <summary>
        /// Имена всех возможных перечислений
        /// </summary>
        public string[] Names { get; set; }
        /// <summary>
        /// имя перечисления - словарь (значение перечисления - строковое представление)
        /// </summary>
        public IDictionary<string,IDictionary<int,string>> EnumValues { get; set; }
    }
}
