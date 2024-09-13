using System;

namespace GreenCity.Http
{
    /// <summary>
    /// Общий вид HTTP контракта
    /// </summary>
    public class CommonHttpContract
    {
        public Guid? RequestId { get; set; }

        public int UserId { get; set; }

        public string UserName { get; set; }
    }
}
