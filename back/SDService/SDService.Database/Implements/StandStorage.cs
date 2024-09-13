using AutoMapper;
using GreenCity.CommonUtils.CommonExtensions;
using GreenCity.Db.Extensions;
using GreenCity.Db.Services;
using GreenCity.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SDContracts.DataContracts.Stand;
using SDContracts.Enums;
using SDContracts.Filter;
using SDContracts.Filter.DropDown;
using SDContracts.ViewModels.Container;
using SDContracts.ViewModels.Stand;
using SDService.Database.Interfaces;
using SDService.Database.Models.Contracts;
using SDService.Database.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SDService.Database.Implements
{
    public class StandStorage : IStandStorage
    {
        private readonly SdDbContext _context;
        private readonly FillHistoryService _fillHistoryService;
        private readonly ILogger<StandStorage> _logger;
        private readonly IMapper _mapper;

        public StandStorage(
            SdDbContext context,
            FillHistoryService fillHistoryService,
            IMapper mapper,
            ILogger<StandStorage> logger
        )
        {
            _context = context;
            _fillHistoryService = fillHistoryService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<int> CreateStandAsync(CreateStandContract createContract)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    //!_! Создаем кп
                    var stand = _mapper.Map<Stand>(createContract);
                    stand.CoordinatesJson = createContract.Coordinates.SafeSerialize();

                    var addedResult = await _context.AddAsync(stand);
                    await _context.SaveChangesAsync();

                    //!_! Обновляем контейнеры
                    if (createContract.AttachedContainerIds != null && createContract.AttachedContainerIds.Count > 0)
                    {
                        var containersToUpdate = _context.Containers
                            .Where(x => 1 == 1
                                && x.IsDeleted == false
                                && createContract.AttachedContainerIds.Contains(x.Id));

                        await containersToUpdate.ForEachAsync(x => x.ContainerStandId = stand.Id);
                        await _context.SaveChangesAsync();

                        var toUpdate = await containersToUpdate.Distinct().ToListAsync();

                        //!_! Добавляем историю по контейнерам
                        await _fillHistoryService.FillHistoryAsync<Container, ContainerHistory>(
                            toUpdate,
                            _context,
                            createContract.UserId,
                            createContract.UserName
                        );
                    }

                    //!_! Добавляем исторические записи
                    await _fillHistoryService.FillHistoryAsync<Stand, StandHistory>(
                        new List<Stand> { stand },
                        _context,
                        createContract.UserId,
                        createContract.UserName
                    );

                    await transaction.CommitAsync();

                    return addedResult.Entity.Id;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error while creating stand with address {createContract.Address}");
                    await transaction.RollbackAsync();
                    return -1;
                }
            }
        }

        //Надо отвязать связанные контейнеры
        public async Task<bool> DeleteStandsAsync(DeleteStandContract deleteContract)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    //!_! Удаляем сами площадки
                    var elems = _context.Stands
                        .Where(
                            x => 1 == 1
                            && x.IsDeleted == false
                            && deleteContract.StandIds.Contains(x.Id));

                    foreach (var elem in elems)
                    {
                        elem.IsDeleted = true;
                    }

                    await _context.SaveChangesAsync();

                    //!_! Отвязываем от удаленных площадок контейнеры
                    var deletingIds = elems.Select(x => x.Id);
                    var containersToFree = _context.Containers
                        .Where(x =>
                            x.IsDeleted == false &&
                            x.ContainerStandId.HasValue &&
                            deleteContract.StandIds.Contains(x.ContainerStandId.Value));

                    foreach (var el in containersToFree)
                    {
                        el.ContainerStandId = null;
                    }

                    await FillStandAndContainerHistory(
                        deleteContract.UserId,
                        deleteContract.UserName,
                        await elems.ToListAsync(),
                        await containersToFree.ToListAsync()
                    );

                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error while deleting stands with ids {string.Join(',', deleteContract.StandIds)}");
                    await transaction.RollbackAsync();
                    return false;
                }
            }

            return true;
        }

        public async Task<IList<StandViewModel>> GetStandsAsync(GetStandContract getContract)
        {
            var data = await (
                from stand in _context.Stands
                    .Where(x => x.IsDeleted == false)
                    .Where(x => x.EntityType == (byte)MissionInfoEntityType.Real)
                    .Where(x => getContract.StandIds.Contains(x.Id))
                from container in _context.Containers
                    .Where(x => x.IsDeleted == false)
                    .Where(x => x.ContainerStandId == stand.Id)
                    .DefaultIfEmpty()
                select new
                {
                    Stand = stand,
                    Container = container
                })
                .GroupBy(x => x.Stand.Id)
                .Select(x => new
                {
                    Stand = x.Select(xx => xx.Stand).FirstOrDefault(),
                    AttachedContainers = x.Where(x => x.Container != default).Select(xx => xx.Container).ToList() ?? new List<Container>()
                })
                //Сложная сортировка
                .AsNoTracking()
                .ToListAsync();

            return data.Select(x => CreateStandViewModel(x.Stand, x.AttachedContainers)).ToList();
        }

        public async Task<IList<StandViewModel>> GetStandsWithoutFilterAsync(CommonHttpFilter filter)
        {
            var data = await (
                from stand in _context.Stands
                    .Where(x => x.IsDeleted == false)
                    .Where(x => x.EntityType == (byte)MissionInfoEntityType.Real)
                    .WhereIf(filter.IsFilterByCreatedUser, x => x.CreatedUserId == filter.UserId)
                from container in _context.Containers
                    .Where(x => x.IsDeleted == false)
                    .Where(x => x.ContainerStandId == stand.Id)
                    .DefaultIfEmpty()
                select new
                {
                    Stand = stand,
                    Container = container
                })
                .GroupBy(x => x.Stand.Id)
                .Select(x => new
                {
                    Stand = x.Select(xx => xx.Stand).FirstOrDefault(),
                    AttachedContainers = x.Where(x => x.Container != default).Select(xx => xx.Container).ToList() ?? new List<Container>()
                })
                //Сложная сортировка
                .AsNoTracking()
                .Paging(filter.ItemsOnPage, filter.Page)
                .ToListAsync();

            return data.Select(x => CreateStandViewModel(x.Stand, x.AttachedContainers)).ToList();
        }

        public async Task<IList<StandViewModel>> GetFilteredStandsAsync(StandFilterModel filter)
        {
            if (filter == null)
                return null;

            var data = await (
                from stand in _context.Stands
                    .Where(x => x.IsDeleted == false)
                    .Where(x => x.EntityType == (byte)filter.MissionInfoEntityType)
                    .WhereIf(filter.IsFilterByCreatedUser, x => x.CreatedUserId == filter.UserId)
                    .WhereIf(filter.IsHighSecurityObject.HasValue, x => x.IsHighSecurityObject == filter.IsHighSecurityObject.Value)
                    .WhereIf(filter.HaveBase.HasValue, x => x.HaveBase == filter.HaveBase.Value)
                    .WhereIf(filter.HaveRoof.HasValue, x => x.HaveRoof == filter.HaveRoof.Value)
                    .WhereIf(filter.HaveFence.HasValue, x => x.HaveFence == filter.HaveFence.Value)
                    .WhereIf(!filter.Address.IsNullOrEmpty(), x => x.Address.Contains(filter.Address))
                    .WhereIf(!filter.DriveWay.IsNullOrEmpty(), x => x.DriveWay.Contains(filter.DriveWay))
                    .WhereIf(!filter.Contacts.IsNullOrEmpty(), x => x.Contacts.Contains(filter.Contacts))
                    .WhereIf(filter.StandTypes != null && filter.StandTypes.Count > 0, x => filter.StandTypes.Contains((StandType)x.StandType))
                    .WhereIf(filter.IsValidated.HasValue, x => x.IsValidated == filter.IsValidated.Value)
                from container in _context.Containers
                    .Where(x => x.IsDeleted == false)
                    .Where(x => x.ContainerStandId == stand.Id)
                    .DefaultIfEmpty()
                select new
                {
                    Stand = stand,
                    Container = container
                })
                .GroupBy(x => x.Stand.Id)
                .Select(x => new
                {
                    Stand = x.Select(xx => xx.Stand).FirstOrDefault(),
                    AttachedContainers = x.Where(x => x.Container != default).Select(xx => xx.Container).ToList() ?? new List<Container>()
                })
                //Сложная сортировка
                .AsNoTracking()
                .Paging(filter.ItemsOnPage, filter.Page)
                .ToListAsync();

            return data.Select(x => CreateStandViewModel(x.Stand, x.AttachedContainers)).ToList();
        }

        public async Task<IList<(Stand Stand, IList<(Container Container, ContainerType ContainerType)> AttachedContainers)>> GetFilteredStandsForRouteAsync(
            GetFilteredStandsForRouteFilter filter
        )
        {
            if (filter == null)
                return null;

            var dbData = await (
                from stand in _context.Stands
                join container in _context.Containers
                    on stand.Id equals container.ContainerStandId
                join containerType in _context.ContainerTypes
                    on container.ContainerTypeId equals containerType.Id
                where 1 == 1
                    && stand.IsDeleted == false
                    && container.IsDeleted == false
                    && container.ContainerStandId.HasValue == true
                    && containerType.IsDeleted == false
                select new
                {
                    Stand = stand,
                    Container = container,
                    ContainerType = containerType
                })
                .WhereIf(filter.IncludeStandIds != default && filter.IncludeStandIds.Count > 0, x => filter.IncludeStandIds.Contains(x.Stand.Id))
                .WhereIf(filter.ExcludeStandIds != default && filter.ExcludeStandIds.Count > 0, x => !filter.ExcludeStandIds.Contains(x.Stand.Id))
                .WhereIf(!filter.StandAddress.IsNullOrEmpty(), x => x.Stand.Address != default && x.Stand.Address.Contains(filter.StandAddress))
                .WhereIf(filter.IncludeContainerIds != default && filter.IncludeContainerIds.Count > 0, x => filter.IncludeContainerIds.Contains(x.Container.Id))
                .WhereIf(filter.ContainerTypeIds != default && filter.ContainerTypeIds.Count > 0,
                    x => filter.ContainerTypeIds.Contains(x.ContainerType.Id) ||
                    x.ContainerType.EntityType == (byte)MissionInfoEntityType.FakeForMissionInfo
                )
                .AsNoTracking()
                .ToListAsync();

            var data = dbData
                 .GroupBy(x => x.Stand.Id)
                 .Select(x => new
                 {
                     Stand = x.Select(xx => xx.Stand).FirstOrDefault(),
                     AttachedContainers = x.Where(x => x.Container != default && x.ContainerType != default)
                         .Select(xx => new
                         {
                             Container = xx.Container,
                             ContainerType = xx.ContainerType
                         })
                         .DistinctBy(xx => xx.Container.Id)
                         .ToList()
                 });

            return data.Select(x => CreateModel(x.Stand, x.AttachedContainers.Select(xx => CreateModel(xx.Container, xx.ContainerType)).ToList())).ToList();
        }

        //Надо перевязать контейнеры
        public async Task<bool> UpdateStandsAsync(UpdateStandContract updateContract)
        {
            List<Container> deleted = new List<Container>();
            List<Container> added = new List<Container>();
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    //!_! Ищем саму площадку
                    var stand = _mapper.Map<Stand>(updateContract);
                    stand.CoordinatesJson = updateContract.Coordinates.SafeSerialize();

                    var elem = await _context.Stands
                        .Where(x =>
                            x.IsDeleted == false &&
                            x.Id == updateContract.StandId)
                        .FirstOrDefaultAsync();

                    if (elem == default)
                    {
                        await transaction.CommitAsync();
                        return true;
                    }

                    //!_! Обновляем контейнерную площадку
                    _mapper.Map(stand, elem);
                    await _context.SaveChangesAsync();

                    //!_! Отвязываем контейнеры которые не пришли на обновление
                    var deletedContainers = _context.Containers
                        .Where(x =>
                            x.IsDeleted == false &&
                            x.ContainerStandId.HasValue &&
                            x.ContainerStandId.Value == updateContract.StandId &&
                            (updateContract.AttachedContainerIds == default ||
                            updateContract.AttachedContainerIds.Count == 0 ||
                            !updateContract.AttachedContainerIds.Contains(x.Id)));

                    foreach (var el in deletedContainers)
                    {
                        el.ContainerStandId = null;
                        deleted.Add(el);
                    }
                    await _context.SaveChangesAsync();

                    //!_! Добавляем новые привязки
                    var containersToAdd = _context.Containers
                        .Where(x =>
                            x.IsDeleted == false &&
                            x.ContainerStandId != updateContract.StandId &&
                            updateContract.AttachedContainerIds.Contains(x.Id));

                    foreach (var el in containersToAdd)
                    {
                        el.ContainerStandId = updateContract.StandId;
                        added.Add(el);
                    }
                    await _context.SaveChangesAsync();

                    //!_! Добавляем записи в историю SaveChanges в истории вызовется
                    var containerChanges = deleted
                        .Union(added)
                        .Distinct();

                    await FillStandAndContainerHistory(
                        updateContract.UserId,
                        updateContract.UserName,
                        new List<Stand> { elem },
                        containerChanges.ToList()
                    );

                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error while updating stand with id {updateContract.StandId}");
                    await transaction.RollbackAsync();
                    return false;
                }
            }
            return true;
        }

        public async Task<IList<Stand>> GetDropDownStandsAsync(GetDropDownStandsFilterModel filter)
        {
            var result = await (
                from stand in _context.Stands
                join container in _context.Containers
                    on stand.Id equals container.ContainerStandId into subContainers
                from subContainer in subContainers.DefaultIfEmpty()
                join container2Schedule in _context.Container2Schedules
                    on subContainer.Id equals container2Schedule.ContainerId into subContainer2Schedules
                from subContainer2Schedule in subContainer2Schedules.DefaultIfEmpty()
                join contract in _context.Contracts
                    on subContainer2Schedule.ContractId equals contract.Id into subContracts
                from subContract in subContracts.DefaultIfEmpty()
                join contractClient in _context.ContractClients
                    on subContract.ContractClientId equals contractClient.Id into subContractsClients
                from subContractClient in subContractsClients.DefaultIfEmpty()
                where 1 == 1
                    && stand.IsDeleted == false
                    && stand.EntityType == (byte)MissionInfoEntityType.Real
                    && (1 == 0
                        || subContainer == default
                        || subContainer.IsDeleted == false
                    )
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
                select new { Stand = stand, Contract = subContract, ContractClient = subContractClient }
                )
                .AsNoTracking()
                .WhereIf(!filter.Address.IsNullOrEmpty(), x => x.Stand.Address.Contains(filter.Address))
                .WhereIf(filter.ContractClientIds != default && filter.ContractClientIds.Count > 0,
                    x => x.ContractClient != default && filter.ContractClientIds.Contains(x.ContractClient.Id)
                )
                .WhereIf(filter.ContractIds != default && filter.ContractIds.Count > 0,
                    x => x.Contract != default && filter.ContractIds.Contains(x.Contract.Id)
                )
                .ToListAsync();

            return result.DistinctBy(x => x.Stand.Id).Select(x => x.Stand).ToList();
        }

        private StandViewModel CreateStandViewModel(Stand stand, IList<Container> containers)
        {
            if (stand == null)
                return null;

            var result = _mapper.Map<StandViewModel>(stand);
            if (containers != default && containers.Count > 0)
            {
                result.AttachedContainers = containers.Select(_mapper.Map<ContainerDropDownViewModel>).ToList();
            }

            return result;
        }

        private (Stand Stand, IList<(Container Container, ContainerType ContainerType)> AttachedContainers) CreateModel(
            Stand stand,
            IList<(Container Container, ContainerType ContainerType)> containers
        )
        {
            return (stand, containers);
        }

        private (Container Container, ContainerType ContainerType) CreateModel(Container container, ContainerType containerType)
        {
            return (container, containerType);
        }

        private async Task FillStandAndContainerHistory(
           int changeUserId,
           string changeUserName,
           IList<Stand> stands,
           IList<Container> containers
       )
        {
            await _fillHistoryService.FillHistoryAsync<Stand, StandHistory>(
                stands,
                _context,
                changeUserId,
                changeUserName
            );

            await _fillHistoryService.FillHistoryAsync<Container, ContainerHistory>(
                 containers,
                _context,
                changeUserId,
                changeUserName
            );
        }
    }
}
