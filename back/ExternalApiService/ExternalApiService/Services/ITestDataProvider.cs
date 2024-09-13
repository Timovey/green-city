using ExternalApi.Interface.Models.Sd.Container;
using ExternalApi.Interface.Models.Sd.Contract;
using ExternalApi.Interface.Models.Sd.ContractClient;
using ExternalApi.Interface.Models.Sd.Stand;
using System.Collections.Generic;

namespace ExternalApiService.Services
{
    internal interface ITestDataProvider
    {
        IList<ExtCreateStandRequest> GetStands();

        IList<ExtCreateContainerRequest> GetContainers(
            IList<(int id, string address)> stands,
            IList<int> containerTypeIds
        );

        IList<ExtCreateContractClientRequest> GetContractClients();

        IList<ExtCreateContractRequest> GetContracts(
            IList<(int id, string name)> contractClients,
            IList<(int id, string name)> containers
        );
    }
}
