using GreenCity.Http;
using System.Collections.Generic;

namespace SDContracts.DataContracts.Container
{
    public class GetContainerContract : CommonHttpContract
    {
        public IList<int> ContainerIds { get; set; }
    }
}
