using GreenCity.Http;
using GreenCity.Http.Attributes.Validation;
using System.Collections.Generic;

namespace AuthContracts.DataContracts
{
    public class GetOrgsByIdsContract : CommonHttpContract
    {
        [GCRequired("Организации")]
        public IList<int> OrgIds { get; set; }

        public bool IsFilterByUserId { get; set; }
    }
}
