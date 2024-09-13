using GreenCity.Http;
using System;
using System.Collections.Generic;

namespace ExternalApi.Interface.Models.Mission.App
{
    public class ExtRunAppGenerationInSingleThreadRequest : CommonHttpContract
    {
        public DateTime Date { get; set; }

        public IList<int> ContractClients { get; set; }

        public IList<int> ContractIds { get; set; }
    }
}
