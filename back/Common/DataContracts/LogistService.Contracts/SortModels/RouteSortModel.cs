using GreenCity.CommonUtils.CommonModels;

namespace LogistService.Contracts.SortModels
{
    public class RouteSortModel : CommonSortingModel
    {
        /// <summary> Дата рейса </summary>
        public SortModel Date { get; set; }

        /// <summary> ИД экипажей </summary>
        public SortModel TransportWorkShiftIds { get; set; }
    }
}