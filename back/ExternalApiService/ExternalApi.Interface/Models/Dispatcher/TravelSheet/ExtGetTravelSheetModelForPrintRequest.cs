using GreenCity.Http;

namespace ExternalApi.Interface.Models.Dispatcher.TravelSheet
{
    public class ExtGetTravelSheetModelForPrintRequest : CommonHttpContract
    {
        public int TravelSheetId { get; set; }
    }
}
