using GreenCity.Http;

namespace ExternalApi.Interface.Models.Dispatcher.TravelSheet
{
    public class ExtGetTravelSheetByIdRequest : CommonHttpContract
    {
        public int Id { get; set; }
    }
}
