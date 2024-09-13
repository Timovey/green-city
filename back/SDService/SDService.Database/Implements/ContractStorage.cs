using AutoMapper;
using GreenCity.CommonUtils.CommonExtensions;
using GreenCity.Db.Services;
using GreenCity.Db.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SDContracts.DataContracts.Contract;
using SDContracts.Filter;
using SDContracts.SpecificModels.ContainerStand;
using SDContracts.ViewModels;
using SDService.Database.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SDContracts.Enums;
using SDService.Database.Models.DbModels.ContractSchedule;
using SDService.Database.Models.DbModels;
using SDContracts.Filter.DropDown;

namespace SDService.Database.Implements
{
    public class ContractStorage : IContractStorage
    {
        private readonly SdDbContext _context;
        private readonly ILogger<ContractStorage> _logger;
        private readonly FillHistoryService _fillHistoryService;
        private readonly IMapper _mapper;

        public ContractStorage(
            SdDbContext context,
            ILogger<ContractStorage> logger,
            FillHistoryService fillHistoryService,
            IMapper mapper
        )
        {
            _context = context;
            _logger = logger;
            _fillHistoryService = fillHistoryService;
            _mapper = mapper;
        }

        public async Task<int> CreateContractAsync(CreateContractContract createContract)
        {
            var contract = _mapper.Map<Contract>(createContract);
            var inserted = new List<Container2Schedule>();
            var changedContainers = new List<Container>();
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    //!_! Создаем контракт
                    var createResult = await _context.Contracts.AddAsync(contract);
                    await _context.SaveChangesAsync();

                    //!_! Привязываем контейнеры
                    foreach (var el in createContract.AttachedContainers)
                    {
                        var c2Sched = ToContainer2ScheduleModel(el, el.ContainerId, contract.Id);
                        await _context.Container2Schedules.AddAsync(c2Sched);

                        inserted.Add(c2Sched);
                    }

                    await _context.SaveChangesAsync();

                    //!_! Выставляем добавленным контейнерам статус нахождения в контракте
                    var toUpdateContract = _context.Containers
                        .Where(x => 1 == 1
                            && x.IsDeleted == false
                            && inserted.Select(x => x.ContainerId).Contains(x.Id));

                    await toUpdateContract.ForEachAsync(x => { x.IsInContract = true; changedContainers.Add(x); });
                    await _context.SaveChangesAsync();

                    //!_! Заполняем историю изменений
                    await FillContractOperationHistoryAsync(
                        createContract.UserId,
                        createContract.UserName,
                        new List<Contract> { contract },
                        inserted,
                        changedContainers.Distinct().ToList()
                    );

                    await transaction.CommitAsync();

                    return contract.Id;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error while creating contract");
                    await transaction.RollbackAsync();
                    return -1;
                }
            }
        }

        public async Task<bool> DeleteContractsAsync(DeleteContractContract deleteContract)
        {
            var deletedContracts = new List<Contract>();
            var deletedContainer2Schedules = new List<Container2Schedule>();
            var changedContainers = new List<Container>();
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    //!_! Удаляем контракты
                    var contracts = _context.Contracts
                       .Where(x => x.IsDeleted == false && deleteContract.ContractIds.Contains(x.Id));

                    foreach (var elem in contracts)
                    {
                        elem.IsDeleted = true;
                        deletedContracts.Add(elem);
                    }

                    //!_! Удаляем привязки
                    var container2Schedule = _context.Container2Schedules
                        .Where(x => x.IsDeleted == false &&
                            deleteContract.ContractIds.Contains(x.ContractId));

                    foreach (var elem in container2Schedule)
                    {
                        elem.IsDeleted = true;
                        deletedContainer2Schedules.Add(elem);
                    }

                    await _context.SaveChangesAsync();

                    //!_! Убираем флаг привязки контейнера к договору
                    var containersToUpdate = _context.Containers
                        .Where(x => 1 == 1
                            && x.IsDeleted == false
                            && deletedContainer2Schedules.Select(x => x.ContainerId).Contains(x.Id));

                    await containersToUpdate.ForEachAsync(x => { x.IsInContract = false; changedContainers.Add(x); });
                    await _context.SaveChangesAsync();

                    //!_! Заполняем историю изменений
                    await FillContractOperationHistoryAsync(
                        deleteContract.UserId,
                        deleteContract.UserName,
                        await contracts.ToListAsync(),
                        await container2Schedule.ToListAsync(),
                        changedContainers.Distinct().ToList()
                    );

                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error while creating contract");
                    await transaction.RollbackAsync();
                    return false;
                }
            }

            return true;
        }

        public async Task<IList<ContractViewModel>> GetContractsWithGroupedContainersAsync(
            IList<int> contractIds,
            int? createdUserId = null
        )
        {
            var resultContracts = await
                (from contract in _context.Contracts
                    .Where(x => x.IsDeleted == false)
                    .Where(x => x.EntityType == (byte)MissionInfoEntityType.Real)
                    .WhereIf(contractIds != default && contractIds.Count > 0, x => contractIds.Contains(x.Id))
                    .WhereIf(createdUserId.HasValue, x => x.CreatedUserId == createdUserId.Value)
                 from contractClient in _context.ContractClients
                     .Where(x =>
                         x.IsDeleted == false &&
                         x.Id == contract.ContractClientId)
                 from c2s in _context.Container2Schedules
                     .Where(x =>
                         x.IsDeleted == false &&
                         x.ContractId == contract.Id)
                 from container in _context.Containers
                     .Where(x => x.EntityType == (byte)MissionInfoEntityType.Real)
                     .Where(x =>
                         x.IsDeleted == false &&
                         x.Id == c2s.ContainerId)
                 from stand in _context.Stands
                     .Where(x => x.EntityType == (byte)MissionInfoEntityType.Real)
                     .Where(x =>
                         x.IsDeleted == false &&
                         x.Id == container.ContainerStandId)
                 select new
                 {
                     Contract = contract,
                     ContractClient = contractClient,
                     Container2Schedule = c2s,
                     Container = container,
                     Stand = stand
                 })
                .GroupBy(x => x.Contract.Id)
                .Select(x => new
                {
                    Contract = x.Select(x => x.Contract).FirstOrDefault(),
                    ContractClient = x.Select(x => x.ContractClient).FirstOrDefault(),
                    AttachedStands = x.Select(xx => new
                    {
                        Stand = xx.Stand,
                        Container = xx.Container,
                        Container2Schedule = xx.Container2Schedule
                    })
                    .GroupBy(xx => xx.Stand.Id)
                    .Select(xx => new
                    {
                        Stand = xx.Select(xxx => xxx.Stand).FirstOrDefault(),
                        Containers = xx.Select(xxx => new
                        {
                            Container = xxx.Container,
                            Container2Schedule = xxx.Container2Schedule
                        }),
                    })
                })
                .AsNoTracking()
                .ToListAsync();

            return resultContracts.Select(x =>
                ToContractViewModel(
                    contract: x.Contract,
                    contractClient: x.ContractClient,
                    stands: x.AttachedStands.Select(xx =>
                        ToContractStandViewModel(
                            stand: xx.Stand,
                            containers: xx.Containers.Select(xxx =>
                                ToContractContainerViewModel(
                                    container: xxx.Container,
                                    container2Schedule: xxx.Container2Schedule
                                )
                            ).ToList()
                        )
                    ).ToList()
                )
            ).ToList();
        }

        //Подумать про кэширование и падает если нету элементов
        public async Task<ContractViewModel> GetContractAsync(GetContractContract getContract)
        {
            var contractWithContractClient =
                await (from contract in _context.Contracts
                           .Where(x => x.EntityType == (byte)MissionInfoEntityType.Real)
                           .Where(x =>
                                x.IsDeleted == false &&
                                x.Id == getContract.ContractId)
                       from contractClient in _context.ContractClients
                           .Where(x =>
                               x.IsDeleted == false &&
                               x.Id == contract.ContractClientId)
                           .DefaultIfEmpty()
                       select new
                       {
                           Contract = contract,
                           ContractClient = contractClient
                       })
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (contractWithContractClient?.Contract == default)
                return null;

            var containers = await
                (from container in _context.Containers
                    .Where(x => x.EntityType == (byte)MissionInfoEntityType.Real)
                    .Where(x => x.IsDeleted == false)
                 from container2Schedule in _context.Container2Schedules
                     .Where(x =>
                         x.IsDeleted == false &&
                         x.ContainerId == container.Id &&
                         x.ContractId == getContract.ContractId)
                 select new
                 {
                     Container = container,
                     Container2Schedule = container2Schedule
                 })
                 .Distinct()
                 .AsNoTracking()
                 .ToListAsync();

            var containerIds = containers
                    .Where(xx => xx.Container.ContainerStandId.HasValue)
                    .Select(xx => xx.Container.ContainerStandId.Value)
                    .Distinct();

            var stands = await _context.Stands
                .Where(x => x.EntityType == (byte)MissionInfoEntityType.Real)
                .Where(x => containerIds.Contains(x.Id))
                .AsNoTracking()
                .ToListAsync();


            var groupedContainers = from stand in stands
                                    join container in containers
                                        on stand.Id equals container.Container.ContainerStandId into grouped
                                    select new
                                    {
                                        Stand = stand,
                                        Containers = grouped
                                    };

            var result = ToContractViewModel(
                contract: contractWithContractClient.Contract,
                contractClient: contractWithContractClient.ContractClient,
                stands: groupedContainers.Select(x =>
                    ToContractStandViewModel(
                        stand: x.Stand,
                        containers: x.Containers.Select(xx =>
                            ToContractContainerViewModel(
                                container: xx.Container,
                                container2Schedule: xx.Container2Schedule
                            )
                        ).ToList()
                    )
                ).ToList()
            );

            return result;
        }

        public async Task<IList<ContractViewModel>> GetContractsWithoutFilterAsync(ContractFilterModel filter)
        {
            var contractWithContractClient =
                await (from contract in _context.Contracts
                           .Where(x => x.EntityType == (byte)MissionInfoEntityType.Real)
                           .Where(x =>
                                x.IsDeleted == false)
                       from contractClient in _context.ContractClients
                           .Where(x =>
                               x.IsDeleted == false &&
                               x.Id == contract.ContractClientId)
                           .DefaultIfEmpty()
                       select new
                       {
                           Contract = contract,
                           ContractClient = contractClient
                       })
                .WhereIf(filter.IsFilterByCreatedUser, x => x.Contract.CreatedUserId == filter.UserId)
                .OrderByDescending(x => x.Contract.Id)
                .AsNoTracking()
                .Paging(filter.ItemsOnPage, filter.Page)
                .ToListAsync();

            var result = contractWithContractClient.Select(x => ToContractViewModel(x.Contract, x.ContractClient, null)).ToList();

            return result;
        }

        public async Task<IList<ContractViewModel>> GetFilteredContractsAsync(ContractFilterModel filter)
        {
            var filterContractIds = new List<int>();
            if (filter.ContractIds != default && filter.ContractIds.Count > 0)
            {
                filterContractIds.AddRange(filter.ContractIds);
            }

            if (filter.AttachedStands != default && filter.AttachedStands.Count > 0)
            {
                var idsByStands = await _context.Container2Schedules
                    .Where(x =>
                        x.IsDeleted == false)
                    .Join(_context.Containers
                            .Where(x => x.EntityType == (byte)MissionInfoEntityType.Real)
                            .Where(x =>
                                x.IsDeleted == false &&
                                x.ContainerStandId.HasValue),
                        c2s => c2s.ContainerId,
                        container => container.Id,
                        (c2s, container) =>
                        new
                        {
                            StandId = container.ContainerStandId,
                            ContractId = c2s.ContractId
                        }
                    )
                    .Where(x => filter.AttachedStands.Contains(x.StandId.Value))
                    .AsNoTracking()
                    .Select(x => x.ContractId)
                    .Distinct()
                    .ToListAsync();

                filterContractIds.AddRange(idsByStands);
            }

            if (filter.AttachedContainers != default && filter.AttachedContainers.Count > 0)
            {
                var idsByContainers = await _context.Container2Schedules
                    .Where(x =>
                        x.IsDeleted == false &&
                        filter.AttachedContainers.Contains(x.ContainerId))
                    .AsNoTracking()
                    .Select(x => x.ContractId)
                    .Distinct()
                    .ToListAsync();

                filterContractIds.AddRange(idsByContainers);
            }

            filterContractIds = filterContractIds.Distinct().ToList();

            var contractWithContractClient =
                await (from contract in _context.Contracts
                           .Where(x => x.EntityType == (byte)MissionInfoEntityType.Real)
                           .Where(x =>
                                x.IsDeleted == false)
                       from contractClient in _context.ContractClients
                           .Where(x =>
                               x.IsDeleted == false &&
                               x.Id == contract.ContractClientId)
                           .DefaultIfEmpty()
                       select new
                       {
                           Contract = contract,
                           ContractClient = contractClient
                       })
                .WhereIf(filter.IsFilterByCreatedUser, x => x.Contract.CreatedUserId == filter.UserId)
                .WhereIf(!filter.ContractNumber.IsNullOrEmpty(), x => x.Contract.ContractNumber.Contains(filter.ContractNumber))
                .WhereIf(filter.ContractClientIds != null && filter.ContractClientIds.Count > 0, x =>
                    x.ContractClient != null &&
                    filter.ContractClientIds.Contains(x.ContractClient.Id))
                .WhereIf(filterContractIds.Count > 0, x => filterContractIds.Contains(x.Contract.Id))
                .WhereIf(!filter.SendDocsType.IsNullOrEmpty(), x => x.Contract.SendDocsType.Contains(filter.SendDocsType))
                .WhereIf(filter.ContractStatus.HasValue, x => x.Contract.ContractStatus == filter.ContractStatus.Value)
                .WhereIf(filter.IsValidated.HasValue, x => x.Contract.IsValidated == filter.IsValidated.Value)
                .OrderByWithSortingModel(filter.Sorting, "Contract")
                .Paging(filter.ItemsOnPage, filter.Page)
                .AsNoTracking()
                .ToListAsync();

            var result = contractWithContractClient.Select(x => ToContractViewModel(x.Contract, x.ContractClient, null)).ToList();

            return result;
        }

        public async Task<bool> UpdateContractAsync(UpdateContractContract updateContract)
        {
            var inserted = new List<Container2Schedule>();
            var deleted = new List<Container2Schedule>();
            var updated = new List<Container2Schedule>();
            var changedContainers = new List<Container>();
            Contract updatedContract = null;
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    //!_! Ищем договор
                    updatedContract = await _context.Contracts
                        .FirstOrDefaultAsync(x => x.Id == updateContract.ContractId);

                    if (updatedContract == null)
                    {
                        await transaction.RollbackAsync();
                        return false;
                    }

                    //!_! Обновляем договор
                    _mapper.Map(updateContract, updatedContract);

                    //!_! Удаляем привязки контейнеров которые не пришли на апдейт
                    var container2ScheduleDeleted = _context.Container2Schedules
                        .Where(x => x.IsDeleted == false &&
                            x.ContractId == updateContract.ContractId &&
                            (updateContract.AttachedContainers == default ||
                            updateContract.AttachedContainers.Count == 0 &&
                            !updateContract.AttachedContainers.Select(xx => xx.ContainerId).Contains(x.ContainerId))
                        );

                    await container2ScheduleDeleted.ForEachAsync(x =>
                    {
                        x.IsDeleted = true;
                        deleted.Add(x);
                    });

                    await _context.SaveChangesAsync();

                    //!_! Добавляем новые привязки контейнеров
                    var container2ScheduleNewIds = updateContract.AttachedContainers
                        .Select(x => x.ContainerId)
                        .Except(
                            _context.Container2Schedules
                            .Where(x => x.IsDeleted == false &&
                                x.ContractId == updateContract.ContractId)
                            .Select(x => x.ContainerId)
                        );

                    foreach (var el in updateContract.AttachedContainers.Where(x => container2ScheduleNewIds.Contains(x.ContainerId)))
                    {
                        var c2Sched = ToContainer2ScheduleModel(el, el.ContainerId, updateContract.ContractId);
                        await _context.Container2Schedules.AddAsync(c2Sched);

                        inserted.Add(c2Sched);
                    }

                    await _context.SaveChangesAsync();

                    //!_! Обновляем старые привязки контейнеров
                    var container2ScheduleUpdate = _context.Container2Schedules
                        .Where(x =>
                            x.IsDeleted == false &&
                            !deleted.Select(xx => xx.ContainerId).Contains(x.ContainerId));

                    foreach (var el in container2ScheduleUpdate)
                    {
                        var toUpdateObj = updateContract.AttachedContainers.FirstOrDefault(x => x.ContainerId == el.ContainerId);

                        if (toUpdateObj != default)
                        {
                            _mapper.Map(toUpdateObj, el);
                            updated.Add(el);
                        }
                    }

                    await _context.SaveChangesAsync();

                    //!_! Обновляем статус удаленных и добавленных контейнеров
                    var deletedContainers = _context.Containers
                        .Where(x => deleted.Select(x => x.ContainerId).Contains(x.Id));
                    await deletedContainers.ForEachAsync(x => { x.IsInContract = false; changedContainers.Add(x); });

                    var insertedContainers = _context.Containers
                        .Where(x => inserted.Select(x => x.ContainerId).Contains(x.Id));
                    await insertedContainers.ForEachAsync(x => { x.IsInContract = true; changedContainers.Add(x); });

                    await _context.SaveChangesAsync();

                    //!_! Заполняем историю изменений
                    var container2ScheduleUnionChanges = deleted
                        .Union(inserted)
                        .Union(updated)
                        .Distinct()
                        .ToList();

                    await FillContractOperationHistoryAsync(
                        updateContract.UserId,
                        updateContract.UserName,
                        contracts: new List<Contract> { updatedContract },
                        c2S: container2ScheduleUnionChanges,
                        containers: changedContainers.Distinct().ToList()
                    );

                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error while updating contract with id {updateContract.ContractId}");
                    await transaction.RollbackAsync();
                    return false;
                }
            }

            return true;
        }

        public async Task<bool> BulkUpdateContractAsync(BulkUpdateContractContract bulkUpdateContract)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var contractsToUpdate = _context.Contracts
                        .Where(x => 1 == 1
                            && x.IsDeleted == false
                            && bulkUpdateContract.ContractIds.Contains(x.Id));

                    foreach (var el in contractsToUpdate)
                    {
                        if (bulkUpdateContract.StartDate.HasValue)
                            el.StartDate = bulkUpdateContract.StartDate.Value;

                        if (bulkUpdateContract.EndDate.HasValue)
                            el.EndDate = bulkUpdateContract.EndDate.Value;

                        if (bulkUpdateContract.ContractStatus.HasValue)
                            el.ContractStatus = bulkUpdateContract.ContractStatus.Value;

                        if (bulkUpdateContract.IsValidated.HasValue)
                            el.IsValidated = bulkUpdateContract.IsValidated.Value;
                    }

                    await _context.SaveChangesAsync();

                    await _fillHistoryService.FillHistoryAsync<Contract, ContractHistory>(
                        await contractsToUpdate.ToListAsync(),
                        _context,
                        bulkUpdateContract.UserId,
                        bulkUpdateContract.UserName
                    );

                    await transaction.CommitAsync();

                    return true;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error while bulk updating contracts with ids {string.Join(',', bulkUpdateContract.ContractIds)}");
                    await transaction.RollbackAsync();
                    return false;
                }
            }
        }

        public async Task<IList<Contract>> GetDropDownContractsAsync(GetDropDownContractsFilterModel filter)
        {
            var result = await (
                from contract in _context.Contracts
                join contractClient in _context.ContractClients
                    on contract.ContractClientId equals contractClient.Id into subContractClients
                from subContractClient in subContractClients.DefaultIfEmpty()
                where 1 == 1
                    && contract.IsDeleted == false
                    && (1 == 0
                        || subContractClient == default
                        || subContractClient.IsDeleted == false
                    )
                select new { Contract = contract, ContractClient = subContractClient }
                )
                .WhereIf(filter.EntityTypes != default && filter.EntityTypes.Count > 0,
                    x => filter.EntityTypes.Contains((MissionInfoEntityType)x.Contract.EntityType)
                )
                .WhereIf(!filter.ContractNumber.IsNullOrEmpty(),
                    x => x.Contract.ContractNumber.Contains(filter.ContractNumber)
                )
                .WhereIf(filter.ContractClientIds != default && filter.ContractClientIds.Count > 0,
                    x => x.ContractClient != default && filter.ContractClientIds.Contains(x.ContractClient.Id)
                )
                .AsNoTracking()
                .ToListAsync();

            return result.DistinctBy(x => x.Contract.Id).Select(x => x.Contract).ToList();
        }

        private Container2Schedule ToContainer2ScheduleModel(CreateContainer2ScheduleContract c2SContract, int containerId, int contractId)
        {
            var result = _mapper.Map<Container2Schedule>(c2SContract);
            result.ContainerId = containerId;
            result.ContractId = contractId;

            return result;
        }

        private ContractViewModel ToContractViewModel(
            Contract contract,
            ContractClient contractClient,
            IList<ContractStandViewModel> stands
        )
        {
            var result = _mapper.Map<ContractViewModel>(contract);
            result.ContractClient = _mapper.Map<ContractContractClientViewModel>(contractClient);
            result.AttachedStands = stands;
            return result;
        }

        private ContractStandViewModel ToContractStandViewModel(Stand stand, IList<ContractContainerViewModel> containers)
        {
            var result = _mapper.Map<ContractStandViewModel>(stand);
            result.Coordinates = stand.CoordinatesJson.SafeDeserialize<StandCoordinatesModel>();
            result.AttachedContainers = containers;

            return result;
        }

        private ContractContainerViewModel ToContractContainerViewModel(
            Container container,
            Container2Schedule container2Schedule
        )
        {
            var result = _mapper.Map<ContractContainerViewModel>(container);

            if (container2Schedule != default)
            {
                result.Container2Schedule = _mapper.Map<Container2ScheduleViewModel>(container2Schedule);
            }

            return result;
        }

        private async Task FillContractOperationHistoryAsync(
            int changeUserId,
            string changeUserName,
            IList<Contract> contracts = null,
            IList<Container2Schedule> c2S = null,
            IList<Container> containers = null
        )
        {
            if (c2S != default && c2S.Count > 0)
                await _fillHistoryService.FillHistoryAsync<Container2Schedule, Container2ScheduleHistory>(
                    c2S,
                    _context,
                    changeUserId,
                    changeUserName
                );

            if (contracts != default && contracts.Count > 0)
                await _fillHistoryService.FillHistoryAsync<Contract, ContractHistory>(
                     contracts,
                    _context,
                    changeUserId,
                    changeUserName
                );

            if (containers != default && containers.Count > 0)
                await _fillHistoryService.FillHistoryAsync<Container, ContainerHistory>(
                     containers,
                    _context,
                    changeUserId,
                    changeUserName
                );
        }
    }
}
