using AuthService.Data;
using AuthService.Models.Db;
using AuthService.Models.UserToOrgs;
using AuthService.ViewModels.Orgs;
using AutoMapper;
using GreenCity.Db.Extensions;
using GreenCity.Db.Services;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.Repository
{
    public class UserToOrgsRepository : IUserToOrgsRepository
    {
        private readonly IMapper _mapper;
        private readonly AuthDbContext _authDbContext;
        private readonly FillHistoryService _fillHistoryService;

        public UserToOrgsRepository(
            IMapper mapper,
            AuthDbContext authDbContext,
            FillHistoryService fillHistoryService
        )
        {
            _mapper = mapper;
            _authDbContext = authDbContext;
            _fillHistoryService = fillHistoryService;
        }

        public async Task<IList<UserToOrgsViewModel>> GetFilteredUserToOrgsAsync(
            IList<int> userIds = null
        )
        {
            var data = await _authDbContext.DbUserToOrgs
                .Where(x => x.IsDeleted == false)
                .WhereIf(userIds != default && userIds.Count > 0, x => userIds.Contains(x.UserId))
                .AsNoTracking()
                .ToListAsync();

            return data.Select(_mapper.Map<UserToOrgsViewModel>).ToList();
        }

        public async Task<int> CreateUserToOrgAsync(CreateUserToOrgsModel createModel)
        {
            var dbModel = _mapper.Map<DbUserToOrgs>(createModel);

            await _authDbContext.DbUserToOrgs.AddAsync(dbModel);
            await _authDbContext.SaveChangesAsync();

            await _fillHistoryService.FillHistoryAsync<DbUserToOrgs, DbUserToOrgsHistory>(
                new List<DbUserToOrgs> { dbModel },
                _authDbContext,
                createModel.CreatedUserId,
                createModel.CreatedUserName
            );

            return dbModel.Id;
        }

        public async Task UpdateUserToOrgAsync(UpdateUserToOrgsModel updateModel)
        {
            var toUpdate = await _authDbContext.DbUserToOrgs
                .Where(x => x.IsDeleted == false)
                .Where(x => x.Id == updateModel.UserToOrgsId)
                .FirstOrDefaultAsync();

            if (toUpdate == default)
                return;

            _mapper.Map(updateModel, toUpdate);

            await _fillHistoryService.FillHistoryAsync<DbUserToOrgs, DbUserToOrgsHistory>(
                new List<DbUserToOrgs> { toUpdate },
                _authDbContext,
                updateModel.UpdatedUserId,
                updateModel.UpdatedUserName
            );
        }
    }
}
