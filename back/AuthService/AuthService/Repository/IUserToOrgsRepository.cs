using AuthService.Models.UserToOrgs;
using AuthService.ViewModels.Orgs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuthService.Repository
{
    public interface IUserToOrgsRepository
    {
        Task<IList<UserToOrgsViewModel>> GetFilteredUserToOrgsAsync(
            IList<int> userIds = null
        );

        Task<int> CreateUserToOrgAsync(CreateUserToOrgsModel createModel);

        Task UpdateUserToOrgAsync(UpdateUserToOrgsModel updateModel);
    }
}
