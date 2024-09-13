using GreenCity.Http;
using System.Collections.Generic;

namespace SDContracts.DataContracts.Container
{
    public class DeleteContainerContract : CommonHttpContract
    {
        public IList<int> ContainerIds { get; set; }
    }
}
