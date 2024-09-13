using AuthService.Models.Orgs;
using AuthService.ViewModels.Orgs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuthService.Repository
{
    public interface IOrganizationRepository
    {
        Task CreateOrgAsync(
            CreateOrgModel createModel,
            int createdUserId = 0,
            string createdUserName = "system"
        );

        Task<IList<OrganizationViewModel>> GetOrgsAsync();

        Task<IList<OrganizationViewModel>> GetOrgByFilterAsync(
            IList<int> organizationIds = null,
            string name = null,
            string address = null,
            string phone = null,
            string ogrn = null
        );

        Task DeleteOrgAsync(IList<int> orgIds, int deletedUserId = 0, string deletedUserName = "system");

        Task UpdateOrganizationAsync(
            UpdateOrgModel updateModel,
            int updatedUserId = 0,
            string updatedUserName = "system"
        );
    }
}
