using System.Collections.Generic;

namespace AuthService.ViewModels.Orgs
{
    internal class ChangeUserOrgViewModel
    {
        public int UserId { get; set; }
        public string UserEmail { get; set; }
        public IList<OrganizationViewModel> AllOrgs { get; set; }
        public IList<OrganizationViewModel> UserOrgs { get; set; }
        public ChangeUserOrgViewModel()
        {
            AllOrgs = new List<OrganizationViewModel>();
            UserOrgs = new List<OrganizationViewModel>();
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
