using Microsoft.AspNetCore.Identity;

namespace AuthService.Models
{
    public class GreenCityIdentityRole : IdentityRole<int>
    {
        public GreenCityIdentityRole() { }

        public GreenCityIdentityRole(string roleName) : base(roleName) { }
    }
}
