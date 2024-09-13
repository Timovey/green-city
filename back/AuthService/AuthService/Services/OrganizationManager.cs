using AuthService.Models.Orgs;
using AuthService.Models.UserToOrgs;
using AuthService.Repository;
using AuthService.Services.Interface;
using AuthService.ViewModels.Orgs;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.Services
{
    internal class OrganizationManager : IOrganizationManager
    {
        private readonly IOrganizationRepository _organizationRepository;
        private readonly IUserToOrgsRepository _userToOrgsRepository;
        private readonly IMapper _mapper;

        public OrganizationManager(
            IOrganizationRepository organizationRepository,
            IUserToOrgsRepository userToOrgsRepository,
            IMapper mapper
        )
        {
            _organizationRepository = organizationRepository;
            _userToOrgsRepository = userToOrgsRepository;
            _mapper = mapper;
        }

        public async Task AddOrgAsync(CreateOrgModel model, int createdUserId, string createdUserName)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            await _organizationRepository.CreateOrgAsync(model, createdUserId, createdUserName);
        }

        public Task<IList<OrganizationViewModel>> GetOrgListAsync()
        {
            return _organizationRepository.GetOrgsAsync();
        }

        public async Task<OrganizationViewModel> GetOrgByIdAsync(int organizationId)
        {
            var result = await _organizationRepository.GetOrgByFilterAsync(organizationIds: new List<int> { organizationId });
            return result.Single();
        }

        public Task DeleteOrgAsync(int organizationId, int deletedUserId, string deletedUserName)
        {
            return _organizationRepository.DeleteOrgAsync(new List<int> { organizationId }, deletedUserId, deletedUserName);
        }

        public async Task UpdateOrgAsync(EditOrgViewModel editViewModel, int updatedUserId, string updatedUserName)
        {
            var updateModel = _mapper.Map<UpdateOrgModel>(editViewModel);
            if (updateModel.OrganizationIds.Count < 1)
                return;

            await _organizationRepository.UpdateOrganizationAsync(updateModel, updatedUserId, updatedUserName);
        }

        public async Task<IList<OrganizationViewModel>> GetUserOrgsAsync(int userId)
        {
            var orgIds = (await _userToOrgsRepository.GetFilteredUserToOrgsAsync(new List<int> { userId })).SingleOrDefault();

            if (orgIds?.OrgIds == default || orgIds.OrgIds.Count == 0)
                return new List<OrganizationViewModel>();

            var orgs = await _organizationRepository.GetOrgByFilterAsync(organizationIds: orgIds.OrgIds);

            return orgs;
        }

        public async Task CreateOrUpdateUserToOrgsAsync(int userId, IList<int> orgIds, int updatedUserId, string updatedUserName)
        {
            if (orgIds == default)
                orgIds = new List<int>();

            var existingUserToOrg = (await _userToOrgsRepository.GetFilteredUserToOrgsAsync(userIds: new List<int> { userId })).SingleOrDefault();
            if (existingUserToOrg == default)
            {
                await _userToOrgsRepository.CreateUserToOrgAsync(new CreateUserToOrgsModel
                {
                    UserId = userId,
                    OrgIds = orgIds,
                    CreatedUserId = updatedUserId,
                    CreatedUserName = updatedUserName
                });

                return;
            }

            await _userToOrgsRepository.UpdateUserToOrgAsync(new UpdateUserToOrgsModel
            {
                UserToOrgsId = existingUserToOrg.UserToOrgsId,
                UserId = userId,
                OrgIds = orgIds,
                UpdatedUserId = updatedUserId,
                UpdatedUserName = updatedUserName
            });
        }

        public async Task<EditOrgViewModel> GetOrgForEditByIdAsync(int organizationId)
        {
            var org = (await _organizationRepository.GetOrgByFilterAsync(organizationIds: new List<int> { organizationId })).SingleOrDefault();
            if (org == default)
                return default;

            return _mapper.Map<EditOrgViewModel>(org);
        }

        public async Task<IList<OrganizationViewModel>> GetOrgByIdsAsync(IList<int> orgIds, int? userId = default)
        {
            var orgs = await _organizationRepository.GetOrgByFilterAsync(organizationIds: orgIds);

            if (userId.HasValue)
            {
                var orgsAvailableToUser = (await _userToOrgsRepository.GetFilteredUserToOrgsAsync(new List<int> { userId.Value })).SingleOrDefault();

                if (orgsAvailableToUser == default || orgsAvailableToUser.OrgIds == default)
                    return new List<OrganizationViewModel>();

                var finalOrgs = orgs.IntersectBy(orgsAvailableToUser.OrgIds, x => x.OrganizationId).ToList();

                return finalOrgs;
            }

            return orgs;
        }
    }
}
