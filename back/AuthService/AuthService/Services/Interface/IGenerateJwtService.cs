using AuthService.Models;
using System.Collections.Generic;

namespace AuthService.Services.Interface
{
    public interface IGenerateJwtService
    {
        string GenerateJwtToken(GreenCityIdentityUser user, IList<string> roles);
    }
}
