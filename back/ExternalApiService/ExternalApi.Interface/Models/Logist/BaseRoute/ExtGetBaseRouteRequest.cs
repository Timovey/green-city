using GreenCity.Http;

namespace ExternalApi.Interface.Models.Logist.BaseRoute
{
    /// <summary> Контракт на получение базового маршрута </summary>
    public class ExtGetBaseRouteRequest : CommonHttpContract
    {
        /// <summary> ИД базового маршрута </summary>
        public int Id { get; set; }
    }
}