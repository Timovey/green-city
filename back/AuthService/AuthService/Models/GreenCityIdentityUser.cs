using Microsoft.AspNetCore.Identity;

namespace AuthService.Models
{
    public class GreenCityIdentityUser : IdentityUser<int>
    {
        public string GreenCityUserName { get; set; }
    }
}
