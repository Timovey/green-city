using GreenCity.CommonUtils.CommonModels;

namespace LogistService.Contracts.SortModels
{
    public class BaseRouteSortModel : CommonSortingModel
    {
        /// <summary> Название БМ </summary>
        public SortModel Name { get; set; }
    }
}
