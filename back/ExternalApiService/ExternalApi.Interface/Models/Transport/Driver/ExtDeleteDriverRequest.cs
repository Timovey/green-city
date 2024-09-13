using GreenCity.Http;
using System.Collections.Generic;

namespace ExternalApi.Interface.Models.Transport.Driver
{
    public class ExtDeleteDriverRequest : CommonHttpContract
    {
        /// <summary> ИД водителей </summary>
        public IList<int> DriverIds { get; set; }
    }
}
