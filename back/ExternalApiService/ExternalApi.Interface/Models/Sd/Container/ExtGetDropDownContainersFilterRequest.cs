using GreenCity.Http;
using System.Collections.Generic;

namespace ExternalApi.Interface.Models.Sd.Container
{
    public class ExtGetDropDownContainersFilterRequest : CommonHttpFilter
    {
        public string Name { get; set; }

        public IList<int> ContractClientIds { get; set; }

        public IList<int> ContractIds { get; set; }

        public IList<int> StandIds { get; set; }
    }
}
