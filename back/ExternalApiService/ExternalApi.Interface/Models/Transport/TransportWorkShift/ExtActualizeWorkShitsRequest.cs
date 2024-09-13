using GreenCity.Http;
using System;

namespace ExternalApi.Interface.Models.Transport.TransportWorkShift
{
    public class ExtActualizeWorkShitsRequest : CommonHttpContract
    {
        /// <summary>
        /// Дата актуализации
        /// </summary>
        public DateTime? DateTime { get; set; }
    }
}
