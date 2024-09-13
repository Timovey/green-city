using GreenCity.Http;
using System.Collections.Generic;

namespace SDContracts.Filter.DropDown
{
    public class GetDropDownContainersFilterModel : CommonHttpFilter
    {
        public string Name { get; set; }

        public IList<int> ContractClientIds { get; set; }

        public IList<int> ContractIds { get; set; }

        public IList<int> StandIds { get; set; }
    }
}
