using System;
using System.Collections.Generic;

namespace MissionService.Contracts.ViewModels.App
{
    public class AppByContractClientViewModel
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public int ContractClientId { get; set; }

        public int ContractId { get; set; }

        public string Comment { get; set; }

        public IList<ContainerInAppInfo> Containers { get; set; }
    }
}
