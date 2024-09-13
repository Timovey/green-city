using GreenCity.Http;
using System;

namespace TransportContracts.DataContracts.TransportWorkShift
{
    public class ActualizeWorkShitsContract : CommonHttpContract
    {
        /// <summary>
        /// Дата актуализации
        /// </summary>
        public DateTime? DateTime { get; set; }
    }
}
