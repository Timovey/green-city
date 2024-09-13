namespace AuthService.Models.Settings
{
    public class RoleInitSettings
    {
        public string AdminRoleName { get; set; }

        public string DefaultRoleName { get; set; }

        public AdminInitData AdminInitData { get; set; }
    }

    public class AdminInitData
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}

