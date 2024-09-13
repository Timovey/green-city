using GreenCity.Http;

namespace ExternalApi.Interface.Models.Auth
{
    /// <summary>
    /// Запрос на получение организаций
    /// </summary>
    public class ExtGetOrgsRequest : CommonHttpContract
    {
        public bool IsFilterByUserId { get; set; } = true;
    }
}
