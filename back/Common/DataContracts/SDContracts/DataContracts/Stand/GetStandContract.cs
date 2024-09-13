using GreenCity.Http;
using System.Collections.Generic;

namespace SDContracts.DataContracts.Stand
{
    public class GetStandContract : CommonHttpContract
    {
        /// <summary>
        /// ИД необходимых контейнерных площадок
        /// </summary>
        public IList<int> StandIds { get; set; }
    }
}
