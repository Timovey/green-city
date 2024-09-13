using AuthService.Models;
using AuthService.Models.Settings;
using AuthService.Services.Interface;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace AuthService.Services
{
    internal class GenerateJwtService : IGenerateJwtService
    {
        private readonly JwtSettings _settings;

        public GenerateJwtService(IOptions<JwtSettings> options)
        {
            _settings = options.Value;
        }

        public string GenerateJwtToken(GreenCityIdentityUser user, IList<string> roles)
        {
            var jwt = new JwtSecurityToken(
                    issuer: _settings.Issuer,
                    audience: _settings.Audience,
                    expires: DateTime.UtcNow.AddMinutes(_settings.TokenExpiresMinutes),
                    claims: GetIdentity(user, roles),
                    signingCredentials: new SigningCredentials(_settings.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            return encodedJwt;
        }

        private IEnumerable<Claim> GetIdentity(GreenCityIdentityUser user, IList<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email)
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimsIdentity.DefaultRoleClaimType, role));
            }

            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));

            return claims;
        }
    }
}
