using GreenCity.Http;
using System.Collections.Generic;

namespace ExternalApi.Interface.Models.Sd.Stand
{
    public class ExtGetDropDownStandsFilterRequest : CommonHttpFilter
    {
        public string Address { get; set; }

        public IList<int> ContractClientIds { get; set; }

        public IList<int> ContractIds { get; set; }
    }
}
