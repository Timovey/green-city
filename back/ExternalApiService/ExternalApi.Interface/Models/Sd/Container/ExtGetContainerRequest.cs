using GreenCity.Http;
using System.Collections.Generic;

namespace ExternalApi.Interface.Models.Sd.Container
{
    public class ExtGetContainerRequest : CommonHttpContract
    {
        public IList<int> ContainerIds { get; set; }
    }
}
