using ExternalApi.Interface.Models.Enums;
using GreenCity.Http;
using System.Threading.Tasks;

namespace ExternalApiService.Operations.Interface
{
    public interface IEnumOperations
    {
        Task<CommonHttpResponse<EnumViewModel>> GetEnums(ModuleType moduleType);
    }
}
