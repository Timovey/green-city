using GreenCity.Http;
using System.Collections.Generic;

namespace TransportContracts.DataContracts.Driver
{
    public class DeleteDriverContract : CommonHttpContract
    {
        /// <summary> ИД водителей </summary>
        public IList<int> DriverIds { get; set; }
    }
}
