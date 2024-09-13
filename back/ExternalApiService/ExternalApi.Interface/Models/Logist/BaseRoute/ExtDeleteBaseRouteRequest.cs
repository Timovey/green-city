using GreenCity.Http;

namespace ExternalApi.Interface.Models.Logist.BaseRoute
{
    /// <summary> Контракт на удаление базового маршрута </summary>
    public class ExtDeleteBaseRouteRequest : CommonHttpContract
    {
        /// <summary> ИД базового маршрута </summary>
        public int Id { get; set; }
    }
}