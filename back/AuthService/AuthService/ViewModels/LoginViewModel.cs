using System;
using System.ComponentModel.DataAnnotations;

namespace AuthService.ViewModels
{
    public class LoginViewModel : IEquatable<LoginViewModel>
    {
        [Display(Name = "Login")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Display(Name = "Запомнить?")]
        public bool RememberMe { get; set; }

        public string? ReturnUrl { get; set; }

        public bool Equals(LoginViewModel other)
        {
            if (other == null || !other.Password.Equals(Password))
                return false;

            return true;
        }
    }
}
