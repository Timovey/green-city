using GreenCity.Http;

namespace ExternalApi.Interface.Models.Logist.Route
{
    public class ExtDeleteRouteRequest : CommonHttpContract
    {
        /// <summary> ИД рейса </summary>
        public int Id { get; set; }
    }
}