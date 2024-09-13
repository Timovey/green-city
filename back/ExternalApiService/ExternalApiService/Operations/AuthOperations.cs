using AuthContracts.DataContracts;
using AuthContracts.Interfaces;
using AutoMapper;
using ExternalApi.Interface.Models.Auth;
using ExternalApiService.Controllers;
using ExternalApiService.Operations.Interface;
using GreenCity.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace ExternalApiService.Operations
{
    internal class AuthOperations : IAuthOperations
    {
        private readonly IAuthApi _authApi;
        private readonly IMapper _mapper;
        private readonly ILogger<AuthController> _logger;

        public AuthOperations(
            ILogger<AuthController> logger,
            IMapper mapper,
            IAuthApi authApi
        )
        {
            _mapper = mapper;
            _authApi = authApi;
            _logger = logger;
        }

        public async Task<CommonHttpResponse<ExtAuthResult>> Login(ExtAuthRequest request)
        {
            var contract = _mapper.Map<AuthUserContract>(request);
            var result = await _authApi.AuthUserAsync(contract);

            if (result.Success)
            {
                var credResult = await _authApi.GetUserInfoAsync();
                if (credResult.Success)
                {
                    return new CommonHttpResponse<ExtAuthResult>
                    {
                        Success = true,
                        StatusCode = System.Net.HttpStatusCode.OK,
                        Data = _mapper.Map<ExtAuthResult>(credResult.Data)
                    };
                }

                return new CommonHttpResponse<ExtAuthResult>
                {
                    Success = false,
                    StatusCode = credResult.StatusCode,
                    Errors = credResult.Errors
                };
            }

            return new CommonHttpResponse<ExtAuthResult>
            {
                Success = false,
                StatusCode = result.StatusCode,
                Errors = result.Errors
            };
        }

        public async Task<CommonHttpResponse<ExtAuthV2Result>> LoginV2(ExtAuthV2Request request)
        {
            var contract = _mapper.Map<AuthUserV2Contract>(request);
            var result = await _authApi.AuthUserV2Async(contract);

            if (result.Success)
            {
                var credResult = await _authApi.GetUserInfoV2Async();
                if (credResult.Success)
                {
                    return new CommonHttpResponse<ExtAuthV2Result>
                    {
                        Success = true,
                        StatusCode = System.Net.HttpStatusCode.OK,
                        Data = _mapper.Map<ExtAuthV2Result>(credResult.Data)
                    };
                }

                return new CommonHttpResponse<ExtAuthV2Result>
                {
                    Success = false,
                    StatusCode = credResult.StatusCode,
                    Errors = credResult.Errors
                };
            }

            return new CommonHttpResponse<ExtAuthV2Result>
            {
                Success = false,
                StatusCode = result.StatusCode,
                Errors = result.Errors
            };
        }

        public async Task<CommonHttpResponse<ExtOrganizationViewModel[]>> GetOrganizationsAsync(ExtGetOrgsRequest request)
        {
            var contract = _mapper.Map<GetOrgsContract>(request);
            var result = await _authApi.GetOrgsContractAsync(contract);

            if (result.Success)
            {
                return new CommonHttpResponse<ExtOrganizationViewModel[]>
                {
                    Success = true,
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Data = _mapper.Map<ExtOrganizationViewModel[]>(result.Data)
                };
            }

            return new CommonHttpResponse<ExtOrganizationViewModel[]>
            {
                Success = false,
                StatusCode = result.StatusCode,
                Errors = result.Errors
            };
        }
    }
}
