using GreenCity.Http;
using System.Collections.Generic;

namespace ExternalApi.Interface.Models.Sd.Container
{
    public class ExtGetFilteredContainerIdsForRouteRequest : CommonHttpContract
    {
        public IList<int> StandIds { get; set; }

        public IList<int> ContainerTypeIds { get; set; }
    }
}
