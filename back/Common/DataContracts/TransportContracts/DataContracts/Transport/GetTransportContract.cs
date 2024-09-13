using GreenCity.Http;
using System.Collections.Generic;

namespace TransportContracts.DataContracts.Transport
{
    public class GetTransportContract : CommonHttpContract
    {
        public IList<int> TransportIds { get; set; }

        /// <summary>
        /// false - если не найдено то ошибка, true - если не найдено то пустой список
        /// </summary>
        public bool ReturnEmptyResultIfNotFound { get; set; }
    }
}
