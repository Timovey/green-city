using GreenCity.Http;
using System.Collections.Generic;

namespace ExternalApi.Interface.Models.Sd.Stand
{
    public class ExtGetStandRequest : CommonHttpContract
    {
        /// <summary>
        /// ИД необходимых контейнерных площадок
        /// </summary>
        public IList<int> StandIds { get; set; }
    }
}
