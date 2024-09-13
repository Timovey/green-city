using GreenCity.Http;

namespace ExternalApi.Interface.Models.Dispatcher.Mission
{
    public class ExtGetMissionsByRouteIdRequest : CommonHttpContract
    {
        public int Id { get; set; }
    }
}
