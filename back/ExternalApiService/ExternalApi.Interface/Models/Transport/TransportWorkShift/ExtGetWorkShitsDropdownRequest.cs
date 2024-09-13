using GreenCity.Http;
using System;

namespace ExternalApi.Interface.Models.Transport.TransportWorkShift
{
    public class ExtGetWorkShitsDropdownRequest : CommonHttpContract
    {
        /// <summary>
        /// Дату
        /// </summary>
        public DateTime DateTime { get; set; }

        /// <summary>
        /// Включать в фильтр организации пользователя
        /// </summary>
        public bool IsFilterByUser { get; set; }
    }
}
