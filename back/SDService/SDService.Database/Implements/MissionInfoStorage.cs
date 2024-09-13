using AutoMapper;
using GreenCity.CommonUtils.CommonExtensions;
using GreenCity.CommonUtils.CommonModels;
using GreenCity.Db.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SDContracts.DataContracts.MissionInfo;
using SDContracts.Enums;
using SDContracts.SpecificModels.ContainerStand;
using SDService.Database.Interfaces;
using SDService.Database.Models.DbModels;
using SDService.Database.Models.DbModels.ContractSchedule;
using SDService.Database.Models.ViewModels;
using SDService.Database.Models.ViewModels.Container;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SDService.Database.Implements
{
    public class MissionInfoStorage : IMissionInfoStorage
    {
        private readonly SdDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<MissionInfoStorage> _logger;

        public MissionInfoStorage(
            SdDbContext context,
            IMapper mapper,
            ILogger<MissionInfoStorage> logger
        )
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<(bool IsSuccess, int FakeContainerId, int ContractClientId, int ContractId)> CreateFakeDataAsync(
            FullDataModelForCreateFakeMissionInfoContract createContract
        )
        {
            using (var tran = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var fakeContainerType = CreateFakeContainerType(createContract);
                    await _context.ContainerTypes.AddAsync(fakeContainerType);

                    var fakeStand = CreateFakeStand(createContract);
                    await _context.Stands.AddAsync(fakeStand);

                    var contractClientId = createContract.ContractClientId;
                    ContractClient fakeContractClient = default;
                    if (!contractClientId.HasValue)
                    {
                        fakeContractClient = CreateFakeContractClient(createContract);
                        await _context.ContractClients.AddAsync(fakeContractClient);
                    }

                    await _context.SaveChangesAsync();

                    contractClientId ??= fakeContractClient.Id;

                    var fakeContainer = CreateFakeContainer(createContract, fakeContainerType.Id, fakeStand.Id);
                    await _context.Containers.AddAsync(fakeContainer);

                    var contractId = createContract.ContractId;
                    Contract fakeContract = default;
                    if (!contractId.HasValue)
                    {
                        fakeContract = CreateFakeContract(createContract, contractClientId.Value);
                        await _context.Contracts.AddAsync(fakeContract);
                    }

                    await _context.SaveChangesAsync();

                    contractId ??= fakeContract.Id;

                    var fakeContainer2Schedule = CreateFakeContainer2Schedule(createContract, fakeContainer.Id, contractId.Value);
                    await _context.Container2Schedules.AddAsync(fakeContainer2Schedule);
                    await _context.SaveChangesAsync();

                    await tran.CommitAsync();

                    return (true, fakeContainer.Id, contractClientId.Value, contractId.Value);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error while creating fake missionInfo data");
                    await tran.RollbackAsync();
                    return (false, default, default, default);
                }
            }
        }

        public async Task<IList<InfoForMissionModel>> GetFilteredContainersForFilterMissionsAsync(
            IList<int> contractClientIds = default,
            IList<int> standIds = default,
            IList<int> containerIds = default,
            IList<int> containerTypeIds = default,
            IList<int> contractIds = default,
            DateTime? date = default
        )
        {
            var result = await (
                from container in _context.Containers
                join containerType in _context.ContainerTypes
                    on container.ContainerTypeId equals containerType.Id
                join stand in _context.Stands
                    on container.ContainerStandId equals stand.Id
                join container2Schedule in _context.Container2Schedules
                    on container.Id equals container2Schedule.ContainerId into subContainer2Schedules
                from subContainer2Schedule in subContainer2Schedules.DefaultIfEmpty()
                join contract in _context.Contracts
                    on subContainer2Schedule.ContractId equals contract.Id into subContracts
                from subContract in subContracts.DefaultIfEmpty()
                join contractClient in _context.ContractClients
                    on subContract.ContractClientId equals contractClient.Id into subContractsClients
                from subContractClient in subContractsClients.DefaultIfEmpty()
                where 1 == 1
                    && container.IsDeleted == false
                    && stand.IsDeleted == false
                    && containerType.IsDeleted == false
                    && (1 == 0
                        || subContainer2Schedule == default
                        || subContainer2Schedule.IsDeleted == false
                    )
                    && (1 == 0
                        || subContract == default
                        || subContract.IsDeleted == false
                    )
                    && (1 == 0
                        || subContractClient == default
                        || subContractClient.IsDeleted == false
                    )
                select new
                {
                    Container = container,
                    ContractClientInfo =
                        new { ContractClientId = (int?)subContractClient.Id, ContractClientName = subContractClient.ClientName },
                    Contract = subContract,
                    Stand = stand,
                    ContainerType = containerType
                })
                .WhereIf(containerIds != default && containerIds.Count > 0, x => containerIds.Contains(x.Container.Id))
                .WhereIf(containerTypeIds != default && containerTypeIds.Count > 0, x => containerTypeIds.Contains(x.ContainerType.Id))
                .WhereIf(standIds != default && standIds.Count > 0, x => x.Stand != default && standIds.Contains(x.Stand.Id))
                .WhereIf(contractClientIds != default && contractClientIds.Count > 0,
                    x => x.ContractClientInfo.ContractClientId != default & contractClientIds.Contains(x.ContractClientInfo.ContractClientId.Value)
                )
                .WhereIf(contractIds != default && contractIds.Count > 0, x => x.Contract != default && contractIds.Contains(x.Contract.Id))
                .WhereIf(date.HasValue, x => x.Contract != default && x.Contract.StartDate <= date.Value.Date &&
                    (x.Contract.IsAutorenewSchedules || !x.Contract.EndDate.HasValue || x.Contract.EndDate.Value >= date.Value.Date))
                .AsNoTracking()
                .ToListAsync();

            return result.Select(x =>
                CreateModel(
                    x.Container,
                    x.ContractClientInfo.ContractClientId,
                    x.ContractClientInfo.ContractClientName,
                    x.Stand,
                    x.ContainerType
                )
            ).ToList();
        }

        public async Task<(IList<ContractClient> ContractClients, IList<Contract> Contracts)> GetContractClientsWithContractsAsync(
            IList<int> contractClientIds,
            IList<int> contractIds
        )
        {
            var contractClients = await _context.ContractClients
                .Where(x => x.IsDeleted == false)
                .Where(x => contractClientIds.Contains(x.Id))
                .ToListAsync();

            var contracts = await _context.Contracts
                .Where(x => x.IsDeleted == false)
                .Where(x => contractIds.Contains(x.Id))
                .ToListAsync();

            return (contractClients, contracts);
        }

        public async Task<IList<(Stand Stand, decimal ContainerVolume)>> GetStandsForMissionInfoAsync(IList<int> containerIds)
        {
            var stands = await (
                from stand in _context.Stands
                join container in _context.Containers
                    on stand.Id equals container.ContainerStandId
                join containerType in _context.ContainerTypes
                    on container.ContainerTypeId equals containerType.Id
                where 1 == 1
                    && stand.IsDeleted == false
                    && container.IsDeleted == false
                    && containerType.IsDeleted == false
                    && containerIds.Contains(container.Id)
                select new { Stand = stand, ContainerType = containerType.Volume }
            )
            .ToListAsync();

            return stands.DistinctBy(x => x.Stand.Id).Select(x => (x.Stand, x.ContainerType)).ToList();
        }

        private ContainerType CreateFakeContainerType(FullDataModelForCreateFakeMissionInfoContract fakeMissionInfoContract)
        {
            return new ContainerType
            {
                Name = fakeMissionInfoContract.ContainerTypeName,
                Volume = fakeMissionInfoContract.CustomVolume,
                EntityType = (byte)MissionInfoEntityType.FakeForMissionInfo
            };
        }

        private Stand CreateFakeStand(
            FullDataModelForCreateFakeMissionInfoContract fakeMissionInfoContract
        )
        {
            return new Stand
            {
                Address = fakeMissionInfoContract.StandAddress,
                CoordinatesJson = new StandCoordinatesModel
                {
                    Coordinates = new List<Coordinates>
                    {
                        new Coordinates
                        {
                            Latitude = fakeMissionInfoContract.Latitude,
                            Longitude = fakeMissionInfoContract.Longitude
                        }
                    },
                    CoordinatesComment = fakeMissionInfoContract.CoordinatesComment
                }.SafeSerialize(),
                EntityType = (byte)MissionInfoEntityType.FakeForMissionInfo,
                CreatedUserId = fakeMissionInfoContract.UserId,
                CreatedUserName = fakeMissionInfoContract.UserName
            };
        }

        private Container CreateFakeContainer(
            FullDataModelForCreateFakeMissionInfoContract fakeMissionInfoContract,
            int fakeContainerType,
            int fakeStandId
        )
        {
            return new Container
            {
                Name = fakeMissionInfoContract.ContainerName,
                ContainerTypeId = fakeContainerType,
                ContainerStandId = fakeStandId,
                EntityType = (byte)MissionInfoEntityType.FakeForMissionInfo,
                CreatedUserId = fakeMissionInfoContract.UserId,
                CreatedUserName = fakeMissionInfoContract.UserName
            };
        }

        private ContractClient CreateFakeContractClient(
            FullDataModelForCreateFakeMissionInfoContract fakeMissionInfoContract
        )
        {
            return new ContractClient
            {
                ClientName = fakeMissionInfoContract.ContractClientName,
                EntityType = (byte)MissionInfoEntityType.FakeForMissionInfo
            };
        }

        private Contract CreateFakeContract(
            FullDataModelForCreateFakeMissionInfoContract fakeMissionInfoContract,
            int contractClientId
        )
        {
            return new Contract
            {
                ContractNumber = fakeMissionInfoContract.ContractNumber,
                ContractClientId = contractClientId,
                EntityType = (byte)MissionInfoEntityType.FakeForMissionInfo,
                CreatedUserId = fakeMissionInfoContract.UserId,
                CreatedUserName = fakeMissionInfoContract.UserName
            };
        }

        private Container2Schedule CreateFakeContainer2Schedule(
            FullDataModelForCreateFakeMissionInfoContract fakeMissionInfoContract,
            int fakeContainerId,
            int contractId
        )
        {
            return new Container2Schedule
            {
                ContainerId = fakeContainerId,
                ContractId = contractId,
                WorkTimeFrom = TimeSpan.Parse("00:00"),
                WorkTimeTo = TimeSpan.Parse("23:59")
            };
        }

        private InfoForMissionModel CreateModel(
            Container container,
            int? contractClientId,
            string contractClientName,
            Stand stand,
            ContainerType containerType
        )
        {
            return new InfoForMissionModel
            {
                ContainerId = container.Id,
                ContractClientId = contractClientId,
                ContractClientName = contractClientName,
                StandInfo = _mapper.Map<InfoForMissionStandModel>(stand),
                ContainerName = container.Name,
                TrashType = (TrashType)container.TrashType,
                ContainerTypeId = containerType.Id,
                ContainerTypeName = containerType.Name,
                Volume = containerType.Volume
            };
        }
    }
}
