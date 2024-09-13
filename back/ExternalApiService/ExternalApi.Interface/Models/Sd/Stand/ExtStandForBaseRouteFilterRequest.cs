using GreenCity.Http;
using GreenCity.Http.Attributes.Validation;

namespace ExternalApi.Interface.Models.Sd.Stand
{
    /// <summary>
    /// Фильтр модель контейнерных площадок для создания базового маршрута
    /// </summary>
    public class ExtStandForBaseRouteFilterRequest : CommonHttpFilter
    {
        /// <summary> Адрес </summary>
        [GCMaxLength(300, "Адрес")]
        public string Address { get; set; }

    }
}
