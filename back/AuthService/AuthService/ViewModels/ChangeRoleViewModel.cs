using AuthService.Models;
using System;
using System.Collections.Generic;

namespace AuthService.ViewModels
{
    public class ChangeRoleViewModel : IEquatable<ChangeRoleViewModel>
    {
        public int UserId { get; set; }
        public string UserEmail { get; set; }
        public List<GreenCityIdentityRole> AllRoles { get; set; }
        public IList<string> UserRoles { get; set; }
        public ChangeRoleViewModel()
        {
            AllRoles = new List<GreenCityIdentityRole>();
            UserRoles = new List<string>();
        }

        public bool Equals(ChangeRoleViewModel other)
        {
            if (other == null || !other.UserId.Equals(UserId) |
                !other.UserEmail.Equals(UserEmail))
                return false;

            return true;
        }
    }
}
