using ExternalApi.Interface.Models.Enums;
using ExternalApiService.Operations.Interface;
using GreenCity.Http;
using GreenCity.Http.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace ExternalApiService.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ModelStateValidationActionFilter]
    public class EnumController : Controller
    {
        private readonly ILogger<EnumController> _logger;
        private readonly IEnumOperations _enumOperations;

        public EnumController(
            ILogger<EnumController> logger,
            IEnumOperations enumOperations
        )
        {
            _logger = logger;
            _enumOperations = enumOperations;
        }

        [HttpPost]
        public async Task<CommonHttpResponse<EnumViewModel>> GetTransportEnums()
        {
            return await _enumOperations.GetEnums(ModuleType.Transport);
        }

        [HttpPost]
        public async Task<CommonHttpResponse<EnumViewModel>> GetSdEnums()
        {
            return await _enumOperations.GetEnums(ModuleType.SD);
        }

        [HttpPost]
        public async Task<CommonHttpResponse<EnumViewModel>> GetMissionEnums()
        {
            return await _enumOperations.GetEnums(ModuleType.Mission);
        }

        [HttpPost]
        public async Task<CommonHttpResponse<EnumViewModel>> GetLogistEnums()
        {
            return await _enumOperations.GetEnums(ModuleType.Logist);
        }
    }
}
