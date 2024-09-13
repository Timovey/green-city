using GreenCity.Http;

namespace ExternalApi.Interface.Models.Mission.App
{
    public class ExtDeleteAppsRequest : CommonHttpContract
    {
        public int Id { get; set; }
    }
}
