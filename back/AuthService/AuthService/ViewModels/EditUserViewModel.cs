using GreenCity.Http.Attributes.Validation;
using System;
using System.ComponentModel.DataAnnotations;

namespace AuthService.ViewModels
{
    public class EditUserViewModel : IEquatable<EditUserViewModel>
    {
        public int Id { get; set; }

        [GCRequired]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [GCRequired]
        [Display(Name = "GreenCityUserName")]
        public string GreenCityUserName { get; set; }

        public bool Equals(EditUserViewModel other)
        {
            if (other == null || other.Id != Id || !other.Email.Equals(Email))
                return false;

            return true;
        }
    }
}
