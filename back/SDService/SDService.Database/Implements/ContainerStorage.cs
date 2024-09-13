using AutoMapper;
using GreenCity.CommonUtils.CommonExtensions;
using GreenCity.Db.Extensions;
using GreenCity.Db.Services;
using GreenCity.Http;
using Microsoft.EntityFrameworkCore;
using SDContracts.DataContracts.Container;
using SDContracts.Enums;
using SDContracts.Filter;
using SDContracts.Filter.DropDown;
using SDContracts.ViewModels;
using SDService.Database.Interfaces;
using SDService.Database.Models.DbModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SDService.Database.Implements
{
    public class ContainerStorage : IContainerStorage
    {
        private readonly SdDbContext _context;
        private readonly FillHistoryService _fillHistoryService;
        private readonly IMapper _mapper;

        public ContainerStorage(
            SdDbContext context,
            FillHistoryService historyService,
            IMapper mapper
        )
        {
            _context = context;
            _fillHistoryService = historyService;
            _mapper = mapper;
        }

        public async Task<int> CreateContainerAsync(CreateContainerContract createContract)
        {
            if (createContract == null)
                return -1;

            var container = _mapper.Map<Container>(createContract);

            await _context.Containers.AddAsync(container);
            await _context.SaveChangesAsync();

            await _fillHistoryService.FillHistoryAsync<Container, ContainerHistory>(
                new List<Container> { container },
                _context,
                createContract.UserId,
                createContract.UserName
            );

            return container.Id;
        }

        public async Task<bool> DeleteContainersAsync(DeleteContainerContract deleteContract)
        {
            if (deleteContract == null || deleteContract.ContainerIds == null || deleteContract.ContainerIds.Count < 1)
                return true;

            var elems = _context.Containers
                .Where(
                    x => 1 == 1
                    && x.IsDeleted == false
                    && deleteContract.ContainerIds.Contains(x.Id));

            foreach (var elem in elems)
            {
                elem.IsDeleted = true;
            }

            await _context.SaveChangesAsync();

            await _fillHistoryService.FillHistoryAsync<Container, ContainerHistory>(
                await elems.ToListAsync(),
                _context,
                deleteContract.UserId,
                deleteContract.UserName
            );

            return true;
        }

        public async Task<IList<ContainerViewModel>> GetContainersAsync(GetContainerContract getContract)
        {
            if (getContract.ContainerIds == null || getContract.ContainerIds.Count < 1)
                return null;

            var elems = await _context.Containers
                .Where(
                    x => x.IsDeleted == false &&
                    getContract.ContainerIds.Contains(x.Id))
                .ToListAsync();

            if (elems == null || elems.Count < 1)
            {
                return null;
            }

            return elems.Select(_mapper.Map<ContainerViewModel>).ToList();
        }

        public async Task<IList<ContainerViewModel>> GetContainersWithoutFilterAsync(CommonHttpFilter filter)
        {
            var data = await
                (from container in _context.Containers
                    .Where(x => x.IsDeleted == false)
                 from stand in _context.Stands
                     .Where(x =>
                         x.IsDeleted == false &&
                         x.Id == container.ContainerStandId)
                     .DefaultIfEmpty()
                 select new
                 {
                     Container = container,
                     StandAddress = stand.Address
                 })
                 .Where(x => x.Container.IsDeleted == false)
                 .Where(x => x.Container.EntityType == (byte)MissionInfoEntityType.Real)
                 .WhereIf(filter.IsFilterByCreatedUser, x => x.Container.CreatedUserId == filter.UserId)
                 .OrderByDescending(x => x.Container.Id)
                 .Paging(filter.ItemsOnPage, filter.Page)
                 .AsNoTracking()
                 .ToListAsync();

            return data.Select(x => CreateModel(x.Container, x.StandAddress)).ToList();
        }

        public async Task<IList<ContainerViewModel>> GetFilteredContainersAsync(ContainerFilterModel filter)
        {
            if (filter == null)
                return null;

            var data = await
                (from container in _context.Containers
                    .Where(x => x.IsDeleted == false)
                 from stand in _context.Stands
                    .Where(x =>
                        x.IsDeleted == false &&
                        x.Id == container.ContainerStandId)
                    .DefaultIfEmpty()
                 select new
                 {
                     Container = container,
                     StandAddress = stand.Address
                 })
                .Where(x => x.Container.IsDeleted == false)
                .Where(x => x.Container.EntityType == (byte)MissionInfoEntityType.Real)
                .WhereIf(filter.IsFilterByCreatedUser, x => x.Container.CreatedUserId == filter.UserId)
                .WhereIf(!filter.Name.IsNullOrEmpty(), x => x.Container.Name.Contains(filter.Name))
                .WhereIf(filter.ContainerTypeIds != null && filter.ContainerTypeIds.Count > 0, x => filter.ContainerTypeIds.Contains(x.Container.ContainerTypeId))
                .WhereIf(filter.TrashTypes != null && filter.TrashTypes.Count > 0, x => filter.TrashTypes.Contains(x.Container.TrashType))
                .WhereIf(filter.IsInContract.HasValue, x => x.Container.IsInContract == filter.IsInContract.Value)
                .WhereIf(filter.IsValidated.HasValue, x => x.Container.IsValidated == filter.IsValidated.Value)
                .WhereIf(filter.ContainerStandIds != null && filter.ContainerStandIds.Count > 0,
                    x => x.Container.ContainerStandId.HasValue && filter.ContainerStandIds.Contains(x.Container.ContainerStandId.Value))
                .OrderByWithSortingModel(filter.Sorting, "Container")
                .Paging(filter.ItemsOnPage, filter.Page)
                .AsNoTracking()
                .ToListAsync();

            return data.Select(x => CreateModel(x.Container, x.StandAddress)).ToList();
        }

        public async Task<bool> UpdateContainersAsync(UpdateContainerContract updateContract)
        {
            if (updateContract == null || updateContract.ContainerIds.Count < 1)
                return true;

            var container = _mapper.Map<Container>(updateContract);

            var elems = _context.Containers
                .Where(
                    x => x.IsDeleted == false &&
                    updateContract.ContainerIds.Contains(x.Id));

            foreach (var elem in elems)
            {
                container.Id = elem.Id;
                _mapper.Map(container, elem);
            }

            await _context.SaveChangesAsync();

            await _fillHistoryService.FillHistoryAsync<Container, ContainerHistory>(
                 await elems.ToListAsync(),
                 _context,
                 updateContract.UserId,
                 updateContract.UserName
            );

            return true;
        }

        public async Task<IList<ContainerTypeViewModel>> GetActualContainerTypesAsync()
        {
            var result = await _context.ContainerTypes
                .Where(x => x.IsDeleted == false)
                .Where(x => x.EntityType == (byte)MissionInfoEntityType.Real)
                .AsNoTracking()
                .ToListAsync();

            return result.Select(_mapper.Map<ContainerTypeViewModel>).ToList();
        }

        public async Task<IList<ContainerTypeViewModel>> GetContainerTypesByIdsAsync(IList<int> containerTypeIds)
        {
            var result = await _context.ContainerTypes
                .Where(x => x.IsDeleted == false)
                .Where(x => x.EntityType == (byte)MissionInfoEntityType.Real)
                .WhereIf(containerTypeIds != default && containerTypeIds.Count > 0, x => containerTypeIds.Contains(x.Id))
                .AsNoTracking()
                .ToListAsync();

            return result.Select(_mapper.Map<ContainerTypeViewModel>).ToList();
        }

        public async Task<IList<(int StandId, int ContainerId)>> GetFilteredContainerIdsForRouteGroupedByStandsAsync(
            GetFilteredContainerIdsForRouteContract contract
        )
        {
            var result = await (
                from container in _context.Containers
                join containerType in _context.ContainerTypes
                    on container.ContainerTypeId equals containerType.Id
                join stand in _context.Stands
                    on container.ContainerStandId equals stand.Id
                where 1 == 1
                    && container.IsDeleted == false
                    && container.ContainerStandId.HasValue
                    && containerType.IsDeleted == false
                    && stand.IsDeleted == false
                select container
                )
                .WhereIf(contract.StandIds != default && contract.StandIds.Count > 0,
                    x => contract.StandIds.Contains(x.ContainerStandId.Value)
                )
                .WhereIf(contract.ContainerTypeIds != default && contract.ContainerTypeIds.Count > 0,
                    x => contract.ContainerTypeIds.Contains(x.ContainerTypeId) ||
                    x.EntityType == (byte)MissionInfoEntityType.FakeForMissionInfo
                )
                .ToListAsync();

            return result.Select(x => (x.ContainerStandId.Value, x.Id)).Distinct().ToList();
        }

        public async Task<IList<(Container Container, ContainerType ContainerType)>> GetDropDownContainersAsync(GetDropDownContainersFilterModel filter)
        {
            var result = await (
                from container in _context.Containers
                join containerType in _context.ContainerTypes
                    on container.ContainerTypeId equals containerType.Id
                join stand in _context.Stands
                    on container.ContainerStandId equals stand.Id into subStands
                from subStand in subStands.DefaultIfEmpty()
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
                    && container.EntityType == (byte)MissionInfoEntityType.Real
                    && (1 == 0
                        || subStand == default
                        || subStand.IsDeleted == false
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
                select new { Container = container, ContainerType = containerType, Stand = subStand, Contract = subContract, ContractClient = subContractClient })
                .AsNoTracking()
                .WhereIf(!filter.Name.IsNullOrEmpty(), x => x.Container.Name.Contains(filter.Name))
                .WhereIf(filter.StandIds != default && filter.StandIds.Count > 0,
                    x => x.Stand != default && filter.StandIds.Contains(x.Stand.Id)
                )
                .WhereIf(filter.ContractClientIds != default && filter.ContractClientIds.Count > 0,
                    x => x.ContractClient != default && filter.ContractClientIds.Contains(x.ContractClient.Id)
                )
                .WhereIf(filter.ContractIds != default && filter.ContractIds.Count > 0,
                    x => x.Contract != default && filter.ContractIds.Contains(x.Contract.Id)
                )
                .ToListAsync();

            return result.DistinctBy(x => x.Container.Id).Select(x => (x.Container, x.ContainerType)).ToList();
        }

        private ContainerViewModel CreateModel(Container container, string standAddress)
        {
            var result = _mapper.Map<ContainerViewModel>(container);
            result.ContainerStandAddress = standAddress;

            return result;
        }
    }
}

