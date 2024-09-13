using System.Collections.Generic;

namespace ExternalApi.Interface.Models.Mission.App
{
    public class ExtAppByContractClientViewModel
    {
        public int Id { get; set; }

        public string Date { get; set; }

        public int ContractClientId { get; set; }

        public int ContractId { get; set; }

        public string Comment { get; set; }

        public IList<ExtContainerInAppInfo> Containers { get; set; }
    }
}
