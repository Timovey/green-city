using AutoMapper;
using GreenCity.CommonUtils.CommonExtensions;
using GreenCity.Db.Extensions;
using GreenCity.Db.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SDContracts.DataContracts.Contract.ScheduleModels.Mission;
using SDContracts.Enums;
using SDContracts.Filter;
using SDContracts.SpecificModels.ContainerStand;
using SDContracts.ViewModels.ContractSchedule.Mission;
using SDService.Database.Interfaces;
using SDService.Database.Models.DbModels.ContractSchedule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SDService.Database.Implements
{
    public class ContractScheduleMissionStorage : IContractScheduleMissionStorage
    {
        private readonly SdDbContext _context;
        private readonly ILogger<ContractScheduleMissionStorage> _logger;
        private readonly FillHistoryService _fillHistoryService;
        private readonly IMapper _mapper;

        public ContractScheduleMissionStorage(
            SdDbContext context,
            ILogger<ContractScheduleMissionStorage> logger,
            FillHistoryService fillHistoryService,
            IMapper mapper
        )
        {
            _context = context;
            _logger = logger;
            _fillHistoryService = fillHistoryService;
            _mapper = mapper;
        }

        //Возможно здесь понадобятся индексы + тут не имеет значения дата окончания действия договора тк задача уже создана
        public async Task<IList<ContractScheduleMissionViewModel>> GetFilteredContractScheduleMission(ContractScheduleMissionFilterModel filter)
        {
            var resultContracts = await (
                from contractScheduleMission in _context.ContractScheduleMissions
                    .Where(x => x.IsDeleted == false)
                    .WhereIf(filter.MissionDateFrom.HasValue, x => x.MissionDate >= filter.MissionDateFrom.Value)
                    .WhereIf(filter.MissionDateTo.HasValue, x => x.MissionDate <= filter.MissionDateFrom.Value)
                    .WhereIf(filter.IsCompleted.HasValue, x => x.IsCompleted == filter.IsCompleted.Value)
                    .WhereIf(filter.IsUrgent.HasValue, x => x.IsUrgent == filter.IsUrgent.Value)
                    .WhereIf(filter.IsFailed.HasValue, x => x.IsUrgent == filter.IsFailed.Value)
                from container2Mission in _context.Mission2Containers
                    .Where(x => x.IsDeleted == false)
                    .Where(x => x.MissionId == contractScheduleMission.Id)
                from contract in _context.Contracts
                    .Where(x => x.IsDeleted == false)
                    .Where(x => x.Id == contractScheduleMission.ContractId)
                    //Может не работать
                    .WhereIf(filter.ContractIds != default && filter.ContractIds.Count > 0, x => filter.ContractIds.Contains(x.Id))
                    .WhereIf(filter.IsFilterByCreatedUser, x => x.CreatedUserId == filter.UserId)
                from contractClient in _context.ContractClients
                    .Where(x => x.IsDeleted == false)
                    .Where(x => x.Id == contract.ContractClientId)
                from stand in _context.Stands
                    .Where(x => x.IsDeleted == false)
                    .Where(x => x.Id == contractScheduleMission.StandId)
                from container in _context.Containers
                    .Where(x => x.IsDeleted == false)
                    .Where(x => x.Id == container2Mission.ContainerId)
                    .Where(x => x.ContainerStandId == stand.Id)
                    .WhereIf(filter.ContainerIds != default && filter.ContainerIds.Count > 0, x => filter.ContainerIds.Contains(x.Id))
                from c2s in _context.Container2Schedules
                    .Where(x => x.IsDeleted == false)
                    .Where(x => x.ContractId == contract.Id)
                select new
                {
                    Mission = contractScheduleMission,
                    Contract = new
                    {
                        Id = contract.Id,
                        ContractNumber = contract.ContractNumber
                    },
                    ContractClient = new
                    {
                        Id = contractClient.Id,
                        Name = contractClient.ClientName,
                        Contact = contractClient.ClientContact
                    },
                    Stand = new
                    {
                        Id = stand.Id,
                        Address = stand.Address,
                        StandType = stand.StandType,
                        Coordinates = stand.CoordinatesJson
                    },
                    Container = new
                    {
                        Id = container.Id,
                        Name = container.Name,
                        TypeId = container.ContainerTypeId,
                        TrashType = container.TrashType
                    },
                    Container2Schedule = new
                    {
                        ScheduleComment = c2s.ScheduleComment,
                        WorkTimeFrom = c2s.WorkTimeFrom,
                        WorkTimeTo = c2s.WorkTimeTo,
                        IsInRegistry = c2s.IsInRegistry,
                        IsInRegistryFromDate = c2s.InRegistryFromDate
                    }
                })
                .GroupBy(x => x.Mission.Id)
                .Select(xx => new
                {
                    Mission = xx.Select(xxx => xxx.Mission).FirstOrDefault(),
                    Contract = xx.Select(xxx => xxx.Contract).FirstOrDefault(),
                    ContractClient = xx.Select(xxx => xxx.ContractClient).FirstOrDefault(),
                    Stand = xx.Select(xxx => xxx.Stand).FirstOrDefault(),
                    AttachedContainers = xx.Select(xxx => new
                    {
                        Container = xxx.Container,
                        container2Schedule = xxx.Container2Schedule
                    })
                })
                .AsNoTracking()
                .ToListAsync();

            return resultContracts.Select(x =>
                ToContractScheduleMissionViewModel(
                    mission: x.Mission,
                    contractId: x.Contract.Id,
                    contractNumber: x.Contract.ContractNumber,
                    contractClient: ToContractClientInfoMissionViewModel(
                        x.ContractClient.Id,
                        x.ContractClient.Name,
                        x.ContractClient.Contact
                    ),
                    stand: ToContractStandMissionInfoViewModel(
                        x.Stand.Id,
                        x.Stand.Address,
                        x.Stand.Coordinates,
                        x.Stand.StandType,
                        containers: x.AttachedContainers.Select(xx =>
                            ToContractContainerInfoMissionViewModel(
                                containerId: xx.Container.Id,
                                containerName: xx.Container.Name,
                                containerTypeId: xx.Container.TypeId,
                                trashType: xx.Container.TrashType,
                                scheduleComment: xx.container2Schedule.ScheduleComment,
                                workTimeFrom: xx.container2Schedule.WorkTimeTo,
                                workTimeTo: xx.container2Schedule.WorkTimeTo,
                                isInRegistry: xx.container2Schedule.IsInRegistry,
                                inRegistryFromDate: xx.container2Schedule.IsInRegistryFromDate
                            )
                        ).ToList()
                    )
                )
            ).ToList();
        }

        private ContractScheduleMissionViewModel ToContractScheduleMissionViewModel(
            ContractScheduleMission mission,
            int contractId,
            string contractNumber,
            ContractClientInfoMissionViewModel contractClient,
            ContractStandMissionInfoViewModel stand
        )
        {
            return new ContractScheduleMissionViewModel
            {
                MissionId = mission.Id,
                ContractId = contractId,
                ContractNumber = contractNumber,
                ContractClient = contractClient,
                Stand = stand,
                IsUrgent = mission.IsUrgent,
                IsFailed = mission.IsFailed,
                IsCompleted = mission.IsCompleted
            };
        }

        private ContractClientInfoMissionViewModel ToContractClientInfoMissionViewModel(
            int contractClientId,
            string clientName,
            string clientContact
        )
        {
            return new ContractClientInfoMissionViewModel
            {
                ContractClientId = contractClientId,
                ClientName = clientName,
                ClientContact = clientContact
            };
        }

        private ContractStandMissionInfoViewModel ToContractStandMissionInfoViewModel(
            int standId,
            string address,
            string coordinatesJson,
            byte standType,
            IList<ContractContainerInfoMissionViewModel> containers
        )
        {
            return new ContractStandMissionInfoViewModel
            {
                StandId = standId,
                Address = address,
                Coordinates = coordinatesJson.SafeDeserialize<StandCoordinatesModel>(null),
                StandType = (StandType)standType,
                AttachedContainers = containers
            };
        }

        private ContractContainerInfoMissionViewModel ToContractContainerInfoMissionViewModel(
            int containerId,
            string containerName,
            int containerTypeId,
            byte trashType,
            string scheduleComment,
            TimeSpan workTimeFrom,
            TimeSpan workTimeTo,
            bool isInRegistry,
            DateTime? inRegistryFromDate
        )
        {
            return new ContractContainerInfoMissionViewModel
            {
                ContainerId = containerId,
                Name = containerName,
                ContainerTypeId = containerTypeId,
                TrashType = (TrashType)trashType,
                Container2Schedule = new Container2ScheduleInfoMissionViewModel
                {
                    ScheduleComment = scheduleComment,
                    WorkTimeFrom = workTimeFrom,
                    WorkTimeTo = workTimeTo,
                    IsInRegistry = isInRegistry,
                    InRegistryFromDate = inRegistryFromDate
                }
            };
        }

        public async Task<bool> AddMissionsWithParamsAsync(
            IList<CreateMissionContract> missions,
            DateTime from = default,
            DateTime to = default,
            bool deletePrevCompleted = false,
            bool deleteExistingNotFailedOnDate = false
        )
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    (int createdUserId, string createdUserName) = missions.Select(x => (x.UserId, x.UserName)).FirstOrDefault();

                    //Удаляем при необходимости миссии
                    var deletedMissions = new List<ContractScheduleMission>();
                    var deletedMission2Containers = new List<Mission2Container>();
                    var toDeleteMissions = _context.ContractScheduleMissions
                        .Where(x => x.IsDeleted == false)
                        .Where(x => (deletePrevCompleted && x.IsCompleted == true) || (deleteExistingNotFailedOnDate && x.IsFailed == false))
                        .Where(x => x.MissionDate.Date >= from.Date && x.MissionDate.Date <= to.Date);

                    foreach (var el in toDeleteMissions)
                    {
                        el.IsDeleted = true;
                        deletedMissions.Add(el);
                    }

                    var toDeleteMission2Containers = _context.Mission2Containers
                        .Where(x => x.IsDeleted == false)
                        .Where(x => deletedMissions.Select(xx => xx.Id).Contains(x.MissionId));

                    foreach (var el in toDeleteMission2Containers)
                    {
                        el.IsDeleted = true;
                        deletedMission2Containers.Add(el);
                    }

                    await FillMissionHistoryAsync(deletedMissions, deletedMission2Containers, createdUserId, createdUserName);
                    await _context.SaveChangesAsync();

                    //добавляем новые
                    var insertedMissions = new List<ContractScheduleMission>();
                    var insertedMission2Containers = new List<Mission2Container>();
                    foreach (var mission in missions)
                    {
                        var dbMission = _mapper.Map<ContractScheduleMission>(mission);
                        var missionId = await _context.ContractScheduleMissions.AddAsync(dbMission);

                        var mission2Containers = mission.AttachedContainers.Select(x => new Mission2Container
                        {
                            MissionId = dbMission.Id,
                            ContainerId = x.ContainerId
                        });

                        await _context.Mission2Containers.AddRangeAsync(mission2Containers);

                        insertedMissions.Add(dbMission);
                        insertedMission2Containers.AddRange(mission2Containers);
                    }

                    await FillMissionHistoryAsync(insertedMissions, insertedMission2Containers, createdUserId, createdUserName);

                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    return true;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error while creating contract");
                    await transaction.RollbackAsync();
                    return false;
                }
            }
        }       

        private async Task FillMissionHistoryAsync(
            IList<ContractScheduleMission> changedMissions,
            IList<Mission2Container> changedMission2container,
            int chanedUserId,
            string changedUsername
        )
        {
            if (changedMissions != default && changedMissions.Count > 0)
                await _fillHistoryService.FillHistoryAsync<ContractScheduleMission, ContractScheduleMissionHistory>(
                    changedMissions,
                    _context,
                    chanedUserId,
                    changedUsername
                );

            if (changedMission2container != default && changedMission2container.Count > 0)
                await _fillHistoryService.FillHistoryAsync<Mission2Container, Mission2ContainerHistory>(
                   changedMission2container,
                   _context,
                   chanedUserId,
                   changedUsername
                );
        }
    }
}
