using AuthService.Data;
using AuthService.Models.Db;
using AuthService.Models.Orgs;
using AuthService.ViewModels.Orgs;
using AutoMapper;
using GreenCity.CommonUtils.CommonExtensions;
using GreenCity.Db.Extensions;
using GreenCity.Db.Services;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.Repository
{
    internal class OrganizationRepository : IOrganizationRepository
    {
        private readonly IMapper _mapper;
        private readonly AuthDbContext _authDbContext;
        private readonly FillHistoryService _fillHistoryService;

        public OrganizationRepository(
            IMapper mapper,
            AuthDbContext authDbContext,
            FillHistoryService fillHistoryService
        )
        {
            _mapper = mapper;
            _authDbContext = authDbContext;
            _fillHistoryService = fillHistoryService;
        }

        public async Task CreateOrgAsync(
            CreateOrgModel createModel,
            int createdUserId = 0,
            string createdUserName = "system"
        )
        {
            var dbModel = _mapper.Map<DbOrganization>(createModel);
            await _authDbContext.AddAsync(dbModel);

            await _authDbContext.SaveChangesAsync();

            await _fillHistoryService.FillHistoryAsync<DbOrganization, DbOrganizationHistory>(
                new List<DbOrganization> { dbModel },
                _authDbContext,
                createdUserId,
                createdUserName
            );
        }

        public async Task<IList<OrganizationViewModel>> GetOrgsAsync()
        {
            var dbResult = await _authDbContext.Organizations
                .Where(x => x.IsDeleted == false)
                .ToListAsync();

            return dbResult.Select(_mapper.Map<OrganizationViewModel>).ToList();
        }

        public async Task<IList<OrganizationViewModel>> GetOrgByFilterAsync(
            IList<int> organizationIds = null,
            string name = null,
            string address = null,
            string phone = null,
            string ogrn = null
        )
        {
            var dbResult = await _authDbContext.Organizations
                .Where(x => x.IsDeleted == false)
                .WhereIf(organizationIds != default && organizationIds.Count > 0, x => organizationIds.Contains(x.Id))
                .WhereIf(!name.IsNullOrEmpty(), x => x.Name.Contains(name))
                .WhereIf(!address.IsNullOrEmpty(), x => x.Address.Contains(address))
                .WhereIf(!phone.IsNullOrEmpty(), x => x.Phone.Contains(phone))
                .WhereIf(!ogrn.IsNullOrEmpty(), x => x.Ogrn.Contains(ogrn))
                .ToListAsync();

            return dbResult.Select(_mapper.Map<OrganizationViewModel>).ToList();
        }

        public async Task DeleteOrgAsync(IList<int> orgIds, int deletedUserId = 0, string deletedUserName = "system")
        {
            var deleted = new List<DbOrganization>();
            var toDelete = _authDbContext.Organizations
                .Where(x => x.IsDeleted == false)
                .Where(x => orgIds.Contains(x.Id));

            foreach (var org in toDelete)
            {
                org.IsDeleted = true;
                deleted.Add(org);
            }

            await _fillHistoryService.FillHistoryAsync<DbOrganization, DbOrganizationHistory>(
                deleted,
                _authDbContext,
                deletedUserId,
                deletedUserName
            );
        }

        public async Task UpdateOrganizationAsync(
            UpdateOrgModel updateModel,
            int updatedUserId = 0,
            string updatedUserName = "system"
        )
        {
            var updated = new List<DbOrganization>();

            var toUpdate = _authDbContext.Organizations
                .Where(x => x.IsDeleted == false)
                .Where(x => updateModel.OrganizationIds.Contains(x.Id));

            var newDb = _mapper.Map<DbOrganization>(updateModel);

            foreach (var dbOld in toUpdate)
            {
                newDb.Id = dbOld.Id;
                _mapper.Map(newDb, dbOld);
                updated.Add(dbOld);
            }

            await _fillHistoryService.FillHistoryAsync<DbOrganization, DbOrganizationHistory>(
                updated,
                _authDbContext,
                updatedUserId,
                updatedUserName
            );
        }
    }
}
