using AuthService.Models.Orgs;
using AuthService.ViewModels.Orgs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuthService.Services.Interface
{
    public interface IOrganizationManager
    {
        Task AddOrgAsync(CreateOrgModel model, int createdUserId, string createdUserName);

        Task<IList<OrganizationViewModel>> GetOrgListAsync();

        Task<OrganizationViewModel> GetOrgByIdAsync(int organizationId);

        Task DeleteOrgAsync(int organizationId, int deletedUserId, string deletedUserName);

        Task UpdateOrgAsync(EditOrgViewModel editViewModel, int updatedUserId, string updatedUserName);

        Task<IList<OrganizationViewModel>> GetUserOrgsAsync(int userId);

        Task CreateOrUpdateUserToOrgsAsync(int userId, IList<int> orgIds, int updatedUserId, string updatedUserName);

        Task<EditOrgViewModel> GetOrgForEditByIdAsync(int organizationId);

        Task<IList<OrganizationViewModel>> GetOrgByIdsAsync(IList<int> orgIds, int? userId = default);
    }
}
