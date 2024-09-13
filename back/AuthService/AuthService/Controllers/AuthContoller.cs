using AuthContracts.DataContracts;
using AuthContracts.Interfaces;
using AuthContracts.ViewModels;
using AuthService.Models;
using AuthService.Services.Interface;
using AutoMapper;
using GreenCity.Http;
using GreenCity.Http.Attributes;
using GreenCity.Http.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    [ModelStateValidationActionFilter]
    public class AuthController : Controller, IAuthApi
    {
        private readonly UserManager<GreenCityIdentityUser> _userManager;
        private readonly SignInManager<GreenCityIdentityUser> _signInManager;
        private readonly IGenerateJwtService _jwtGenerator;
        private readonly IOrganizationManager _organizationManager;
        private readonly IMapper _mapper;

        public AuthController(
            UserManager<GreenCityIdentityUser> userManager,
            SignInManager<GreenCityIdentityUser> signInManager,
            IGenerateJwtService jwtGenerator,
            IOrganizationManager organizationManager,
            IMapper mapper
        )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtGenerator = jwtGenerator;
            _organizationManager = organizationManager;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<CommonHttpResponse> AuthUserAsync(AuthUserContract contract)
        {
            if (!ModelState.IsValid)
            {
                return ModelState.GetBadRequestWithErrors<AuthUserResult>();
            }

            var result = await _signInManager.PasswordSignInAsync(contract.Login, contract.Password, true, false);
            if (result.Succeeded)
            {
                return new CommonHttpResponse<AuthUserResult>
                {
                    Success = true,
                    StatusCode = System.Net.HttpStatusCode.NoContent
                };
            }
            else
            {
                ModelState.AddModelError("", "Неправильный логин и (или) пароль");
            }

            return new CommonHttpResponse<AuthUserResult>
            {
                Success = false,
                StatusCode = System.Net.HttpStatusCode.Unauthorized,
                Errors = ModelState.GetErrors()
            };
        }

        [HttpPost]
        public async Task<CommonHttpResponse<AuthUserResult>> GetUserInfoAsync()
        {
            if (User.Identity.IsAuthenticated)
            {
                var creds = await GetCredsAsync();
                return new CommonHttpResponse<AuthUserResult>
                {
                    Success = true,
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Data = new AuthUserResult
                    {
                        Token = creds.Token,
                        Role = creds.Roles.FirstOrDefault(),
                        UserId = creds.UserId,
                        UserName = creds.UserName
                    }
                };
            }
            else
            {
                ModelState.AddModelError("", "Необходимо авторизоваться");
            }

            return new CommonHttpResponse<AuthUserResult>
            {
                Success = false,
                StatusCode = System.Net.HttpStatusCode.Unauthorized,
                Errors = ModelState.GetErrors()
            };
        }

        [HttpPost]
        public async Task<CommonHttpResponse> AuthUserV2Async(AuthUserV2Contract contract)
        {
            if (!ModelState.IsValid)
            {
                return ModelState.GetBadRequestWithErrors<AuthUserResult>();
            }

            var result = await _signInManager.PasswordSignInAsync(contract.Login, contract.Password, true, false);
            if (result.Succeeded)
            {
                return new CommonHttpResponse<AuthUserResult>
                {
                    Success = true,
                    StatusCode = System.Net.HttpStatusCode.NoContent
                };
            }
            else
            {
                ModelState.AddModelError("", "Неправильный логин и (или) пароль");
            }

            return new CommonHttpResponse<AuthUserResult>
            {
                Success = false,
                StatusCode = System.Net.HttpStatusCode.Unauthorized,
                Errors = ModelState.GetErrors()
            };
        }

        [HttpPost]
        public async Task<CommonHttpResponse<AuthUserV2Result>> GetUserInfoV2Async()
        {
            if (User.Identity.IsAuthenticated)
            {
                var creds = await GetCredsAsync();
                return new CommonHttpResponse<AuthUserV2Result>
                {
                    Success = true,
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Data = new AuthUserV2Result
                    {
                        Token = creds.Token,
                        Roles = creds.Roles,
                        UserId = creds.UserId,
                        UserName = creds.UserName
                    }
                };
            }
            else
            {
                ModelState.AddModelError("", "Необходимо авторизоваться");
            }

            return new CommonHttpResponse<AuthUserV2Result>
            {
                Success = false,
                StatusCode = System.Net.HttpStatusCode.Unauthorized,
                Errors = ModelState.GetErrors()
            };
        }

        private async Task<(string Token, IList<string> Roles, int UserId, string UserName)> GetCredsAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            var roles = await _userManager.GetRolesAsync(user);

            var token = _jwtGenerator.GenerateJwtToken(user, await _userManager.GetRolesAsync(user));

            return ($"Bearer {token}", roles, user.Id, user.GreenCityUserName);
        }

        [HttpPost]
        public async Task<CommonHttpResponse<IList<OrganizationApiViewModel>>> GetOrgsContractAsync(GetOrgsContract getContract)
        {
            var orgs = getContract.IsFilterByUserId ?
                await _organizationManager.GetUserOrgsAsync(getContract.UserId) :
                await _organizationManager.GetOrgListAsync();

            return new CommonHttpResponse<IList<OrganizationApiViewModel>>
            {
                Success = true,
                StatusCode = System.Net.HttpStatusCode.OK,
                Data = orgs.Select(_mapper.Map<OrganizationApiViewModel>).ToList(),
            };
        }

        [HttpPost]
        public async Task<CommonHttpResponse<IList<OrganizationApiViewModel>>> GetOrgsByIdsAsync(GetOrgsByIdsContract getContract)
        {
            var orgs = await _organizationManager.GetOrgByIdsAsync(getContract.OrgIds, getContract.IsFilterByUserId ? getContract.UserId : null);

            return new CommonHttpResponse<IList<OrganizationApiViewModel>>
            {
                Success = true,
                StatusCode = System.Net.HttpStatusCode.OK,
                Data = orgs.Select(_mapper.Map<OrganizationApiViewModel>).ToList(),
            };
        }
    }
}
