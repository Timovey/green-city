using GreenCity.Http.Attributes.Validation;
using System.ComponentModel.DataAnnotations;

namespace AuthService.ViewModels
{
    public class RegisterViewModel
    {
        [GCRequired]
        [Display(Name = "GreenCityUserName")]
        public string GreenCityUserName { get; set; }

        [GCRequired]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [GCRequired]
        [DataType(DataType.Password)]
        [MinLength(13)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [GCRequired]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        [MinLength(13)]
        [Display(Name = "Подтвердить пароль")]
        public string ConfirmPassword { get; set; }
    }
}
