using GreenCity.Http.Attributes.Validation;
using System.ComponentModel.DataAnnotations;

namespace AuthService.ViewModels
{
    public class ChangePasswordViewModel
    {
        public int Id { get; set; }

        public string Email { get; set; }

        [GCRequired]
        [DataType(DataType.Password)]
        [MinLength(13)]
        [Display(Name = "Пароль")]
        public string NewPassword { get; set; }
    }
}
