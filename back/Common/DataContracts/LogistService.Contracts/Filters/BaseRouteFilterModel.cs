using GreenCity.Http;
using LogistService.Contracts.SortModels;

namespace LogistService.Contracts.Filters
{
    public class BaseRouteFilterModel : CommonHttpFilter
    {
        /// <summary> Название БМ </summary>
        public string Name { get; set; }

        /// <summary> Настройки сортировки </summary>
        public BaseRouteSortModel Sorting { get; set; }
    }
}
