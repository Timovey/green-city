using ExternalApi.Interface.Models.Common;
using ExternalApiService.Operations.Interface;
using ExternalApiService.Services;
using ExternalApiService.Settings;
using GreenCity.Http;
using GreenCity.Http.Helpers;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExternalApiService.Operations
{
    internal class CommonOperations : ICommonOperations
    {
        private readonly ISdOperations _sdOperations;
        private readonly ITestDataProvider _testDataProvider;
        private readonly bool _allowTestData = true;

        public CommonOperations(
            ISdOperations sdOperations,
            ITestDataProvider testDataProvider,
            IOptions<DeployOptions> options
        )
        {
            _sdOperations = sdOperations;
            _testDataProvider = testDataProvider;
            _allowTestData = options.Value.AllowTestData;
        }

        public async Task<CommonHttpResponse> GenerateTestDataAsync(ExtGenerateTestDataRequest request)
        {
            if (!_allowTestData)
                return CommonHttpHelper.BuildSuccessResponse(System.Net.HttpStatusCode.NoContent);

            if (request == null)
                return CommonHttpHelper.BuildErrorResponse(initialError: "Пустой запрос");

            //generate test data
            var contractClients = _testDataProvider.GetContractClients();
            var addedContractClients = new List<(int id, string name)>();
            foreach (var client in contractClients)
            {
                var contractClientId = await _sdOperations.CreateContractClientAsync(client);
                addedContractClients.Add((contractClientId.Data, client.ClientName));
            }

            var stands = _testDataProvider.GetStands();
            var addedStands = new List<(int id, string address)>();
            foreach (var stand in stands)
            {
                var standId = await _sdOperations.CreateStandAsync(stand);
                addedStands.Add((standId.Data, stand.Address));
            }

            var actualContainerTypes = (await _sdOperations.GetActualContainerTypesAsync()).Data.Select(x => x.Id).ToList();
            var containers = _testDataProvider.GetContainers(addedStands, actualContainerTypes);
            var addedContainers = new List<(int id, string name)>();
            foreach (var container in containers)
            {
                var containerId = await _sdOperations.CreateContainerAsync(container);
                addedContainers.Add((containerId.Data, container.Name));
            }

            var contracts = _testDataProvider.GetContracts(addedContractClients, addedContainers);
            foreach (var contract in contracts)
            {
                await _sdOperations.CreateContractForTestDataAsync(contract);
            }

            return CommonHttpHelper.BuildSuccessResponse(System.Net.HttpStatusCode.NoContent);
        }
    }
}
