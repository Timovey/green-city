using ExternalApi.Interface.Models.Auth;
using ExternalApiService.Operations.Interface;
using GreenCity.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace ExternalApiService.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AuthController : Controller
    {
        private readonly IAuthOperations _authOperations;
        private readonly ILogger<AuthController> _logger;

        public AuthController(
            ILogger<AuthController> logger,
            IAuthOperations authOperations
        )
        {
            _authOperations = authOperations;
            _logger = logger;
        }

        [HttpPost]
        public Task<CommonHttpResponse<ExtAuthResult>> Login(ExtAuthRequest request)
        {
            return _authOperations.Login(request);
        }

        [HttpPost]
        public Task<CommonHttpResponse<ExtAuthV2Result>> LoginV2(ExtAuthV2Request request)
        {
            return _authOperations.LoginV2(request);
        }

        [HttpPost]
        [Authorize]
        public Task<CommonHttpResponse<ExtOrganizationViewModel[]>> GetOrganizationsAsync(ExtGetOrgsRequest request)
        {
            return _authOperations.GetOrganizationsAsync(request);
        }
    }
}
