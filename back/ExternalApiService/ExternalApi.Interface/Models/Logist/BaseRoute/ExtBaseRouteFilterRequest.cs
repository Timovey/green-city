using GreenCity.Http;

namespace ExternalApi.Interface.Models.Logist.BaseRoute
{
    public class ExtBaseRouteFilterRequest : CommonHttpFilter
    {
        /// <summary> Название БМ </summary>
        public string Name { get; set; }

        /// <summary> Настройки сортировки </summary>
        public ExtBaseRouteSortModel Sorting { get; set; }
    }
}
