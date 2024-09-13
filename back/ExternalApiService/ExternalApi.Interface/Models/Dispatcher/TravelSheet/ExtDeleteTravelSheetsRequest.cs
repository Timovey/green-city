using GreenCity.Http;

namespace ExternalApi.Interface.Models.Dispatcher.TravelSheet
{
    public class ExtDeleteTravelSheetsRequest : CommonHttpContract
    {
        public int Id { get; set; }
    }
}
