using GreenCity.CommonUtils.CommonModels;

namespace ExternalApi.Interface.Models.Logist.Route
{
    public class ExtRouteSortModel
    {
        /// <summary> Дата рейса </summary>
        public SortModel Date { get; set; }

        /// <summary> ИД экипажей </summary>
        public SortModel TransportWorkShiftIds { get; set; }
    }
}