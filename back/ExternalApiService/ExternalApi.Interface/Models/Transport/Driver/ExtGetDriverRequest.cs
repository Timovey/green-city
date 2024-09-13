using GreenCity.Http;
using System.Collections.Generic;

namespace ExternalApi.Interface.Models.Transport.Driver
{
    /// <summary> Получение водителей </summary>
    public class ExtGetDriverRequest : CommonHttpContract
    {
        /// <summary> ИД водителей </summary>
        public IList<int> DriverIds { get; set; }
    }
}
