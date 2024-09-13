using ExternalApi.Interface.Models.Common;
using GreenCity.Http;
using System.Threading.Tasks;

namespace ExternalApiService.Operations.Interface
{
    public interface ICommonOperations
    {
        Task<CommonHttpResponse> GenerateTestDataAsync(ExtGenerateTestDataRequest request);
    }
}
