using AutoMapper;
using GreenCity.Db.Extensions;
using GreenCity.Db.Services.History.Interface;
using Microsoft.EntityFrameworkCore;
using MissionService.Contracts.DataContracts.Mission;
using MissionService.Contracts.Enums;
using MissionService.Contracts.Filters.Mission;
using MissionService.Database.Models.Contracts;
using MissionService.Database.Models.DbModels;
using MissionService.Database.Models.ViewModel;
using MissionService.Database.Storage.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MissionService.Database.Storage
{
    public class MissionStorage : IMissionStorage
    {
        private readonly MissionDbContext _context;
        private readonly IFillHistoryService _fillHistoryService;
        private readonly IMapper _mapper;

        public MissionStorage(
            MissionDbContext context,
            IFillHistoryService historyService,
            IMapper mapper
        )
        {
            _context = context;
            _fillHistoryService = historyService;
            _mapper = mapper;
        }

        public async Task<(bool IsSuccess, IList<int> MissionIds)> CreateRealMissionsAsync(IList<CreateOneRealMissionContract> missionsContracts)
        {
            var dbMissions = missionsContracts.Select(_mapper.Map<Mission>).ToList();
            (int createdUserId, string createdUserName) = missionsContracts.Select(x => (x.UserId, x.UserName)).FirstOrDefault();

            await _context.Missions.AddRangeAsync(dbMissions);
            await _context.SaveChangesAsync();

            await _fillHistoryService.FillHistoryAsync<Mission, MissionHistory>(
                dbMissions,
                _context,
                createdUserId,
                createdUserName
            );

            return (true, dbMissions.Select(x => x.Id).ToList());
        }

        public async Task<int> CreateMissionFromMapAsync(CreateOneMissionFromMapDbContract missionContract)
        {
            var dbMission = _mapper.Map<Mission>(missionContract);

            await _context.Missions.AddAsync(dbMission);
            await _context.SaveChangesAsync();

            await _fillHistoryService.FillHistoryAsync<Mission, MissionHistory>(
                new List<Mission> { dbMission },
                _context,
                missionContract.UserId,
                missionContract.UserName
            );

            return dbMission.Id;
        }

        public async Task<IList<int>> GetAvailableContainersByMissionsForRouteAsync(
            GetAvailableContainersByMissionsForRouteContract contract,
            IList<MissionStatus> includeInRouteMissionStatuses = default
        )
        {
            //!_! Свободные контейнеры
            var result = await _context.Missions
                .Where(x => x.IsDeleted == false)
                .Where(x => x.Date.Date == contract.Date.Date)
                .Where(x => contract.ContainerIds.Contains(x.ContainerId))
                .Where(x => contract.MissionStatuses.Contains((MissionStatus)x.Status))
                //!_! Не берем миссии которые уже есть в рейсах
                .Where(x => x.RouteId.HasValue == false)
                .AsNoTracking()
                .Select(x => x.ContainerId)
                .ToListAsync();

            //!_! Для рейса
            if (contract.IncludeContainersRouteId.HasValue)
            {
                var containerIdsByRouteId = await _context.Missions
                    .Where(x => x.IsDeleted == false)
                    .Where(x => x.RouteId.HasValue && x.RouteId.Value == contract.IncludeContainersRouteId.Value)
                    .Where(x => x.Date.Date == contract.Date.Date)
                    .WhereIf(
                        includeInRouteMissionStatuses != default && includeInRouteMissionStatuses.Count > 0,
                        x => includeInRouteMissionStatuses.Contains((MissionStatus)x.Status)
                    )
                    .AsNoTracking()
                    .Select(x => x.ContainerId)
                    .ToListAsync();

                result.AddRange(containerIdsByRouteId);
            }

            return result.Distinct().ToList();
        }

        public async Task<bool> BindMissionsToRouteByContainersAsync(BindMissionsToRouteDbContract contract)
        {
            var updatedMissions = new List<Mission>();

            using (var tran = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    foreach (var updateData in contract.UpdateDatas)
                    {
                        var toBindMissions = _context.Missions
                            .Where(x =>
                                x.IsDeleted == false &&
                                x.Date.Date == contract.Date.Date &&
                                x.Status == (byte)MissionStatus.New &&
                                x.RouteId.HasValue == false &&
                                updateData.ContainerIds.Contains(x.ContainerId)
                            );

                        foreach (var mission in toBindMissions)
                        {
                            mission.Status = (byte)MissionStatus.InProgress;
                            mission.RouteId = contract.RouteId;
                            mission.Priority = updateData.Priority;

                            updatedMissions.Add(mission);
                        }
                    }

                    //!_! SaveChanges внутри этой штуки сделается
                    await _fillHistoryService.FillHistoryAsync<Mission, MissionHistory>(
                        updatedMissions,
                        _context,
                        changeUserName: nameof(MissionStorage)
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

        public async Task<bool> UnbindMissionsFromRouteAsync(
            int routeId,
            IList<int> missionIds = default
        )
        {
            var updatedMissions = new List<Mission>();

            using (var tran = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var toUnbindMissions = _context.Missions
                        .Where(x =>
                            x.IsDeleted == false &&
                            x.RouteId == routeId)
                        .WhereIf(missionIds != default && missionIds.Count > 0, x => missionIds.Contains(x.Id));

                    foreach (var mission in toUnbindMissions)
                    {
                        mission.Status = (byte)MissionStatus.New;
                        mission.RouteId = default;
                        mission.Priority = default;

                        updatedMissions.Add(mission);
                    }

                    await _fillHistoryService.FillHistoryAsync<Mission, MissionHistory>(
                        updatedMissions,
                        _context,
                        changeUserName: nameof(MissionStorage)
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

        public async Task<IList<(int Priority, int ContainerId)>> GetContainerIdsWithPrioritiesByRouteIdAsync(int routeId)
        {
            var dbResult = await _context.Missions
                .Where(x => x.IsDeleted == false)
                .Where(x => !new List<byte> { (byte)MissionStatus.Unknown, (byte)MissionStatus.New }.Contains(x.Status))
                .Where(x => x.RouteId == routeId)
                .Select(x => new
                {
                    Priority = x.Priority,
                    ContainerId = x.ContainerId
                })
                .AsNoTracking()
                .Distinct()
                .ToListAsync();

            return dbResult.Select(x => (x.Priority, x.ContainerId)).ToList();
        }

        public async Task<bool> UpdateMissionRouteBindingsAsync(
            UpdateBindingsMissionsToRouteDbContract contract
        )
        {
            var updatedMissions = new List<Mission>();

            using (var tran = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    //!_! Сначала обнуляем существующие связки
                    var toUnbindMissions = _context.Missions
                        .Where(x =>
                            x.IsDeleted == false &&
                            x.RouteId == contract.RouteId);

                    foreach (var mission in toUnbindMissions)
                    {
                        //!_! Не меняем статус миссии если она уже была завершена или по какой то причине еще новая
                        mission.Status = mission.Status == (byte)MissionStatus.InProgress ?
                            (byte)MissionStatus.New :
                            mission.Status;
                        mission.RouteId = null;
                        mission.Priority = default;

                        updatedMissions.Add(mission);
                    }

                    await _context.SaveChangesAsync();

                    //!_! Связываем со всем, что пришло на вход
                    foreach (var updateData in contract.UpdateDatas)
                    {
                        var toBindMissions = _context.Missions
                            .Where(x => x.IsDeleted == false)
                            .Where(x => x.Date.Date == contract.Date.Date)
                            .Where(x => x.Status == (byte)MissionStatus.New)
                            .Where(x => x.RouteId.HasValue == false)
                            .Where(x => updateData.ContainerIds.Contains(x.ContainerId));

                        foreach (var mission in toBindMissions)
                        {
                            mission.Status = (byte)MissionStatus.InProgress;
                            mission.RouteId = contract.RouteId;
                            mission.Priority = updateData.Priority;

                            updatedMissions.Add(mission);
                        }
                    }

                    //!_! SaveChanges внутри этой штуки сделается
                    await _fillHistoryService.FillHistoryAsync<Mission, MissionHistory>(
                        updatedMissions,
                        _context,
                        changeUserName: nameof(MissionStorage)
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

        public async Task<bool> UpdateMissionsForCloseRouteAsync(
            IList<UpdateOneMissionForCloseRouteContract> contracts,
            IList<int> missionIdsToUnbind = default,
            int changeUserId = 0,
            string changeUserName = nameof(MissionStorage)
        )
        {
            var updatedMissions = new List<Mission>();

            var missionsToUpdate = await _context.Missions
                .Where(x => x.IsDeleted == false)
                //TODO возможно стоит разрешить обновлять повторно рейс (если статус уже поменялся с InProgress на что то другое)
                //.Where(x => x.Status == (byte)MissionStatus.InProgress)
                .Where(x => x.RouteId.HasValue)
                .Where(x => contracts.Select(xx => xx.MissionId).Distinct().Contains(x.Id))
                .ToListAsync();

            var updateSet = (
                from missionToUpdate in missionsToUpdate
                join updateData in contracts
                    on missionToUpdate.Id equals updateData.MissionId
                select new
                {
                    MissionToUpdate = missionToUpdate,
                    UpdateData = updateData
                })
                .Distinct();

            using (var tran = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    if (missionIdsToUnbind != default && missionIdsToUnbind.Count > 0)
                    {
                        var toUnbindMissions = _context.Missions
                            .Where(x => x.IsDeleted == false)
                            .Where(x => missionIdsToUnbind.Contains(x.Id));

                        foreach (var mission in toUnbindMissions)
                        {
                            mission.Status = (byte)MissionStatus.New;
                            mission.RouteId = default;
                            mission.Priority = default;

                            updatedMissions.Add(mission);
                        }
                    }

                    foreach (var updateSetElem in updateSet)
                    {
                        updateSetElem.MissionToUpdate.Status = (byte)updateSetElem.UpdateData.MissionStatus;
                        updateSetElem.MissionToUpdate.StatusComment = updateSetElem.UpdateData.StatusComment;
                        updateSetElem.MissionToUpdate.FactVolume = updateSetElem.UpdateData.FactVolume;

                        updatedMissions.Add(updateSetElem.MissionToUpdate);
                    }

                    //!_! SaveChanges тут
                    await _fillHistoryService.FillHistoryAsync<Mission, MissionHistory>(
                        updatedMissions,
                        _context,
                        changeUserId,
                        changeUserName
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

        public async Task<IList<MissionViewModel>> GetMissionsByRouteIdAsync(
            int routeId,
            IList<MissionStatus> missionStatuses = default
        )
        {
            var result = await _context.Missions
                .Where(x => x.IsDeleted == false)
                .Where(x => x.RouteId == routeId)
                .WhereIf(missionStatuses != default && missionStatuses.Count > 0, x => missionStatuses.Contains((MissionStatus)x.Status))
                .AsNoTracking()
                .ToListAsync();

            return result.Select(_mapper.Map<MissionViewModel>).ToList();
        }

        public async Task<IEnumerable<(Mission Mission, IEnumerable<App> Apps)>> GetMissionsByRouteIdWithAppInfoAsync(
            int routeId,
            IList<MissionStatus> missionStatuses = default
        )
        {
            var result = await (
                from mission in _context.Missions
                join app2Mission in _context.App2Missions
                    on mission.Id equals app2Mission.MissionId
                join app in _context.Apps
                    on app2Mission.AppId equals app.Id
                where 1 == 1
                    && mission.IsDeleted == false
                    && mission.RouteId == routeId
                    && app2Mission.IsDeleted == false
                    && app.IsDeleted == false
                select new
                {
                    Mission = mission,
                    App = app
                })
                .AsNoTracking()
                .WhereIf(missionStatuses != default && missionStatuses.Count > 0, x => missionStatuses.Contains((MissionStatus)x.Mission.Status))
                .ToListAsync();

            return result
                .DistinctBy(x => x.Mission.Id)
                .GroupBy(x => x.Mission.Id)
                .Select(x => (x.Select(xx => xx.Mission).FirstOrDefault(), x.Select(xx => xx.App).DistinctBy(xx => xx.Id)));
        }

        public async Task<IList<MissionViewModel>> GetFilteredMissionsOnDateAsync(
            DateTime date,
            IList<CreatorType> creatorTypes = default,
            IList<MissionStatus> statuses = default,
            IList<int> containerIds = default
        )
        {
            var result = await _context.Missions
                .Where(x => x.IsDeleted == false)
                .Where(x => x.Date.Date == date.Date)
                //.WhereIf(creatorTypes != default && creatorTypes.Count > 0, x => creatorTypes.Contains((CreatorType)x.CreatorType))
                .WhereIf(statuses != default && statuses.Count > 0, x => statuses.Contains((MissionStatus)x.Status))
                .WhereIf(containerIds != default && containerIds.Count > 0, x => containerIds.Contains(x.ContainerId))
                .ToListAsync();

            return result.Select(_mapper.Map<MissionViewModel>).ToList();
        }

        public async Task<bool> FailMissionsAsync(DateTime date, int changeUserId = 0, string changeUserName = nameof(MissionStorage))
        {
            var failedMissions = new List<Mission>();

            var missionsToFail = await _context.Missions
                .Where(x => x.IsDeleted == false)
                .Where(x => x.Status != (byte)MissionStatus.Done && x.Status != (byte)MissionStatus.Failed)
                .Where(x => x.Date.Date < date.Date)
                .ToListAsync();

            using (var tran = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    foreach (var mission in missionsToFail)
                    {
                        mission.Status = (byte)MissionStatus.Failed;
                        mission.StatusComment = "Время вышло";
                        mission.FactVolume = 0;

                        failedMissions.Add(mission);
                    }

                    await _fillHistoryService.FillHistoryAsync<Mission, MissionHistory>(
                        failedMissions,
                        _context,
                        changeUserId,
                        changeUserName
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

        public async Task<(IList<(Mission Mission, string MissionComment)> Missions, IList<App> Apps)> GetCommonFilteredMissionsAsync(
            CommonMissionFilterModel filter
        )
        {
            var result = await (
                from mission in _context.Missions
                join app2Mission in _context.App2Missions
                    on mission.Id equals app2Mission.MissionId
                join app in _context.Apps
                    on app2Mission.AppId equals app.Id
                where 1 == 1
                    && mission.IsDeleted == false
                    && app2Mission.IsDeleted == false
                    && app.IsDeleted == false
                    && mission.Date.Date == filter.Date.Date
                select new
                {
                    Mission = mission,
                    App = app
                })
                .AsNoTracking()
                .WhereIf(filter.Statuses != default && filter.Statuses.Count > 0,
                    x => filter.Statuses.Contains((MissionStatus)x.Mission.Status)
                )
                .WhereIf(filter.ContainerIds != default && filter.ContainerIds.Count > 0,
                    x => filter.ContainerIds.Contains(x.Mission.ContainerId)
                )
                .WhereIf(filter.ContractClientIds != default && filter.ContractClientIds.Count > 0,
                    x => filter.ContractClientIds.Contains(x.App.ContractClientId)
                )
                .WhereIf(filter.ContractIds != default && filter.ContractIds.Count > 0,
                    x => filter.ContractIds.Contains(x.App.ContractId)
                )
                .ToListAsync();

            return (result.Select(x => (x.Mission, x.App.Comment)).DistinctBy(x => x.Mission.Id).ToList(), result.Select(x => x.App).DistinctBy(x => x.Id).ToList());
        }
    }
}
