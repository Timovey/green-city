using GreenCity.Http;
using System.Collections.Generic;

namespace SDContracts.Filter.DropDown
{
    public class GetDropDownStandsFilterModel : CommonHttpFilter
    {
        public string Address { get; set; }

        public IList<int> ContractClientIds { get; set; }

        public IList<int> ContractIds { get; set; }
    }
}
