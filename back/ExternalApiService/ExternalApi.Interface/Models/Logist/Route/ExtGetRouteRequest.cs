using GreenCity.Http;

namespace ExternalApi.Interface.Models.Logist.Route
{
    /// <summary> Контракт на получение рейса </summary>
    public class ExtGetRouteRequest : CommonHttpContract
    {
        /// <summary> ИД рейса </summary>
        public int Id { get; set; }
    }
}