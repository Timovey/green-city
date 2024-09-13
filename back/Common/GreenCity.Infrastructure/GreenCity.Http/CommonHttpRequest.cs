using System;

namespace GreenCity.Http
{
    /// <summary>
    /// Общий вид HTTP запроса
    /// </summary>
    public class CommonHttpRequest
    {
        public Guid? RequestId { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }
    }
}
