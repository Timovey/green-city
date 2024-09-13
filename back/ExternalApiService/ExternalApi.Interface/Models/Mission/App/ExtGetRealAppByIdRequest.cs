using GreenCity.Http;

namespace ExternalApi.Interface.Models.Mission.App
{
    public class ExtGetRealAppByIdRequest : CommonHttpContract
    {
        public int Id { get; set; }
    }
}
