using AutoMapper;
using GreenCity.Db.Extensions;
using GreenCity.Db.Services.History.Interface;
using Microsoft.EntityFrameworkCore;
using MissionService.Contracts.Enums;
using MissionService.Contracts.Filters.App;
using MissionService.Database.Models.Contracts;
using MissionService.Database.Models.DbModels;
using MissionService.Database.Storage.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MissionService.Database.Storage
{
    public class AppStorage : IAppStorage
    {
        private readonly MissionDbContext _context;
        private readonly IFillHistoryService _fillHistoryService;
        private readonly IMapper _mapper;

        public AppStorage(
            MissionDbContext context,
            IFillHistoryService historyService,
            IMapper mapper
        )
        {
            _context = context;
            _fillHistoryService = historyService;
            _mapper = mapper;
        }

        public async Task<(bool IsSuccess, int CreatedAppId)> CreateAppAsync(CreateAppDbContract dbContract)
        {
            var dbModelApp = _mapper.Map<App>(dbContract);

            using (var tran = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    //!_! Создаем заявку
                    await _context.Apps.AddAsync(dbModelApp);
                    await _context.SaveChangesAsync();

                    //!_! Создаем связки заявки с миссиями
                    var appToMissionDbModels = CreateApp2MissionDbModel(dbModelApp.Id, dbContract.MissionIds);
                    await _context.AddRangeAsync(appToMissionDbModels);
                    await _context.SaveChangesAsync();

                    await _fillHistoryService.FillHistoryAsync<App, AppHistory>(
                        new List<App> { dbModelApp },
                        _context,
                        dbContract.CreatedUserId,
                        dbContract.CreatedUserName,
                        false
                    );

                    await _fillHistoryService.FillHistoryAsync<App2Mission, App2MissionHistory>(
                        appToMissionDbModels.ToList(),
                        _context,
                        dbContract.CreatedUserId,
                        dbContract.CreatedUserName,
                        true
                    );

                    await tran.CommitAsync();
                    return (true, dbModelApp.Id);
                }
                catch (Exception)
                {
                    await tran.RollbackAsync();
                    return (false, default);
                }
            }
        }

        public async Task<IList<int>> GetMissionsThanCantBeDeletedAsync(
            IList<int> appsToDelete
        )
        {
            var result = await (
                from app in _context.Apps
                join app2Mission in _context.App2Missions
                    on app.Id equals app2Mission.AppId
                join app2MissionNoDelete in _context.App2Missions
                    on app2Mission.MissionId equals app2MissionNoDelete.MissionId
                join appNoDelete in _context.Apps
                    on app2MissionNoDelete.AppId equals appNoDelete.Id
                where 1 == 1
                    && app.IsDeleted == false
                    && appsToDelete.Contains(app.Id)
                    && app2Mission.IsDeleted == false
                    && app2MissionNoDelete.IsDeleted == false
                    && appNoDelete.IsDeleted == false
                    && !appsToDelete.Contains(appNoDelete.Id)
                select app2MissionNoDelete.MissionId)
                .Distinct()
                .ToListAsync();

            return result;
        }

        public async Task<bool> DeleteAppsAsync(
            IList<int> appIds,
            IList<int> excludeMissionIds,
            int deleteUserId = 0,
            string deleteUserName = nameof(AppStorage)
        )
        {
            var deletedApps = new List<App>();
            var deletedApp2Missions = new List<App2Mission>();
            var deletedMissions = new List<Mission>();

            //!_! Нужно удалить все заявки, все связки с миссиями (если есть) и все связанные миссии, которых нету в исключающем списке
            var dataToUpdate = await (
                from app in _context.Apps
                join app2Mission in _context.App2Missions
                    on app.Id equals app2Mission.AppId into subApp2Missions
                from subApp2Mission in subApp2Missions.DefaultIfEmpty()
                join mission in _context.Missions
                    on subApp2Mission.MissionId equals mission.Id into subMissions
                from subMission in subMissions.DefaultIfEmpty()
                where 1 == 1
                    && app.IsDeleted == false
                    && appIds.Contains(app.Id)
                    && (1 == 0
                        || subApp2Mission == default
                        || subApp2Mission.IsDeleted == false
                    )
                    && (1 == 0
                        || subMission == default
                        || subMission.IsDeleted == false
                    )
                select new
                {
                    App = app,
                    App2Mission = subApp2Mission,
                    Mission = subMission
                })
                .ToListAsync();

            using (var tran = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    foreach (var app in dataToUpdate.Select(x => x.App).DistinctBy(x => x.Id))
                    {
                        app.IsDeleted = true;
                        deletedApps.Add(app);
                    }

                    foreach (var app2Mission in dataToUpdate.Where(x => x.App2Mission != default).Select(x => x.App2Mission).DistinctBy(x => x.Id))
                    {
                        app2Mission.IsDeleted = true;
                        deletedApp2Missions.Add(app2Mission);
                    }

                    foreach (var mission in dataToUpdate
                        .Where(x => x.Mission != default)
                        .Select(x => x.Mission)
                        .DistinctBy(x => x.Id)
                        .Where(x => !excludeMissionIds.Contains(x.Id))
                    )
                    {
                        mission.IsDeleted = true;
                        deletedMissions.Add(mission);
                    }

                    await _context.SaveChangesAsync();

                    await _fillHistoryService.FillHistoryAsync<App, AppHistory>(
                        deletedApps, _context, deleteUserId, deleteUserName, execContextSaveChanges: false
                    );

                    await _fillHistoryService.FillHistoryAsync<App2Mission, App2MissionHistory>(
                        deletedApp2Missions, _context, deleteUserId, deleteUserName, execContextSaveChanges: false
                    );

                    await _fillHistoryService.FillHistoryAsync<Mission, MissionHistory>(
                        deletedMissions, _context, deleteUserId, deleteUserName, execContextSaveChanges: false
                    );

                    await _context.SaveChangesAsync();

                    await tran.CommitAsync();
                    return true;
                }
                catch (Exception)
                {
                    await tran.RollbackAsync();
                    return false;
                }
            }
        }

        public async Task<IList<(App App, byte MissionStatus, DateTime MissionDate)>> GetFilteredAppsAsync(
            CommonAppFilterModel filter
        )
        {
            var result = await (
                from app in _context.Apps
                join app2Mission in _context.App2Missions
                    on app.Id equals app2Mission.AppId
                join mission in _context.Missions
                    on app2Mission.MissionId equals mission.Id
                where 1 == 1
                    && app.IsDeleted == false
                    && app2Mission.IsDeleted == false
                    && mission.IsDeleted == false
                    && app.Date.Date == filter.AppDate.Date
                    && app.Type == (byte)filter.AppType
                select new
                {
                    App = app,
                    MissionStatus = mission.Status,
                    MissionDate = mission.Date
                })
                .AsNoTracking()
                .WhereIf(
                    filter.ContractClientIds != default && filter.ContractClientIds.Count > 0,
                    x => filter.ContractClientIds.Contains(x.App.ContractClientId)
                )
                .WhereIf(
                    filter.ContractIds != default && filter.ContractIds.Count > 0,
                    x => filter.ContractIds.Contains(x.App.ContractClientId)
                )
                .WhereIf(
                    filter.CreatorTypes != default && filter.CreatorTypes.Count > 0,
                    x => filter.CreatorTypes.Contains((CreatorType)x.App.CreatorType)
                )
                .Paging(filter.ItemsOnPage, filter.Page)
                .ToListAsync();

            return result.Select(x => (x.App, x.MissionStatus, x.MissionDate)).ToList();
        }

        public async Task<IList<App>> GetFilteredAppsForAutoGenerationAsync(
            AppsForAutoGenerationFilterModel filter
        )
        {
            var result = await (
                from app in _context.Apps
                join app2Mission in _context.App2Missions
                    on app.Id equals app2Mission.AppId
                join mission in _context.Missions
                    on app2Mission.MissionId equals mission.Id
                where 1 == 1
                    && app.IsDeleted == false
                    && app2Mission.IsDeleted == false
                    && mission.IsDeleted == false
                    && app.Date.Date == filter.AppDate.Date
                    && app.Type == (byte)filter.AppType
                select app)
                .AsNoTracking()
                .WhereIf(
                    filter.ContractClientIds != default && filter.ContractClientIds.Count > 0,
                    x => filter.ContractClientIds.Contains(x.ContractClientId)
                )
                .WhereIf(
                    filter.ContractIds != default && filter.ContractIds.Count > 0,
                    x => filter.ContractIds.Contains(x.ContractId)
                )
                .WhereIf(
                    filter.CreatorTypes != default && filter.CreatorTypes.Count > 0,
                    x => filter.CreatorTypes.Contains((CreatorType)x.CreatorType)
                )
                .Paging(filter.ItemsOnPage, filter.Page)
                .ToListAsync();

            return result;
        }

        public async Task<IList<(App App, IList<Mission> AttachedMissions)>> GetAppsByIdAsync(IList<int> appIds)
        {
            var apps = await (
                from app in _context.Apps
                join app2Mission in _context.App2Missions
                    on app.Id equals app2Mission.AppId
                join mission in _context.Missions
                    on app2Mission.MissionId equals mission.Id
                where 1 == 1
                    && app.IsDeleted == false
                    && app2Mission.IsDeleted == false
                    && mission.IsDeleted == false
                    && appIds.Contains(app.Id)
                select new
                {
                    App = app,
                    Mission = mission
                })
                .AsNoTracking()
                .ToListAsync();

            var result = apps
                .GroupBy(x => x.App.Id)
                .Select(x =>
                    CreateModel(x.Select(xx => xx.App).FirstOrDefault(), x.Select(xx => xx.Mission).DistinctBy(xx => xx.Id).ToList())
                );

            return result.ToList();
        }

        public async Task<bool> UpdateAppAsync(
            int appId,
            IList<int> missionsToDelete,
            IList<int> newMissionIds,
            DateTime? appDate = default,
            int changeUserId = 0,
            string changeUserName = nameof(AppStorage)
        )
        {
            var deletedMissions = new List<Mission>();
            var deletedApp2Missions = new List<App2Mission>();
            var toAddApp2Missions = newMissionIds.Select(x => new App2Mission { AppId = appId, MissionId = x }).ToList();

            var toDeleteMissions = await _context.Missions
                .Where(x => x.IsDeleted == false)
                .Where(x => missionsToDelete.Contains(x.Id))
                .ToListAsync();

            var toDeleteApp2Missions = await _context.App2Missions
                .Where(x => x.IsDeleted == false)
                .Where(x => x.AppId == appId)
                .Where(x => missionsToDelete.Contains(x.MissionId))
                .ToListAsync();

            using (var tran = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var updatedApp = await _context.Apps
                        .Where(x => x.IsDeleted == false)
                        .Where(x => x.Id == appId)
                        .FirstOrDefaultAsync();

                    if (updatedApp == default)
                    {
                        await tran.CommitAsync();
                        return true;
                    }

                    if (appDate != default)
                    {
                        updatedApp.Date = appDate.Value.Date;

                        await _fillHistoryService.FillHistoryAsync<App, AppHistory>(
                            new List<App> { updatedApp },
                            _context,
                            changeUserId,
                            changeUserName,
                            false
                        );
                    }

                    foreach (var mission in toDeleteMissions)
                    {
                        mission.IsDeleted = true;
                        deletedMissions.Add(mission);
                    }

                    foreach (var app2Mission in toDeleteApp2Missions)
                    {
                        app2Mission.IsDeleted = true;
                        deletedApp2Missions.Add(app2Mission);
                    }

                    await _context.App2Missions.AddRangeAsync(toAddApp2Missions);

                    await _fillHistoryService.FillHistoryAsync<Mission, MissionHistory>(
                        deletedMissions,
                        _context,
                        changeUserId,
                        changeUserName,
                        false
                    );

                    await _fillHistoryService.FillHistoryAsync<App2Mission, App2MissionHistory>(
                        deletedApp2Missions,
                        _context,
                        changeUserId,
                        changeUserName,
                        true
                    );

                    await _fillHistoryService.FillHistoryAsync<App2Mission, App2MissionHistory>(
                        toAddApp2Missions,
                        _context,
                        changeUserId,
                        changeUserName,
                        true
                    );

                    await tran.CommitAsync();
                    return true;
                }
                catch (Exception)
                {
                    await tran.RollbackAsync();
                    return false;
                }
            }
        }

        private (App App, IList<Mission>) CreateModel(App app, IList<Mission> missions)
        {
            return (app, missions);
        }

        private IEnumerable<App2Mission> CreateApp2MissionDbModel(int appId, IList<int> missionIds)
        {
            foreach (var missionId in missionIds)
                yield return new App2Mission
                {
                    AppId = appId,
                    MissionId = missionId
                };
        }
    }
}
