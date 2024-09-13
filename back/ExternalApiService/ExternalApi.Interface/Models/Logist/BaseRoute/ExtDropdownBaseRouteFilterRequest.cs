using GreenCity.Http;

namespace ExternalApi.Interface.Models.Logist.BaseRoute
{
    public class ExtDropdownBaseRouteFilterRequest : CommonHttpFilter
    {
        /// <summary> Название БМ </summary>
        public string Name { get; set; }
    }
}
