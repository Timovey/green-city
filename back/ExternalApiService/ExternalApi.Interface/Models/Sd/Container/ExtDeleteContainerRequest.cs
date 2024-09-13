using GreenCity.Http;
using System.Collections.Generic;

namespace ExternalApi.Interface.Models.Sd.Container
{
    public class ExtDeleteContainerRequest : CommonHttpContract
    {
        public IList<int> ContainerIds { get; set; }
    }
}
