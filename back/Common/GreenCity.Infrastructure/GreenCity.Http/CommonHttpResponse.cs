using System.Net;

namespace GreenCity.Http
{
    /// <summary>
    /// HTTP ответ в общем виде
    /// </summary>
    public class CommonHttpResponse
    {
        public bool Success { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        public string[] Errors { get; set; }
    }

    /// <summary>
    /// HTTP ответ с полезной нагрузкой
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CommonHttpResponse<T> : CommonHttpResponse
    {
        public T Data { get; set; }
    }
}
