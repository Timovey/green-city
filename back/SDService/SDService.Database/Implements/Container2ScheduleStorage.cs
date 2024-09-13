using AutoMapper;
using GreenCity.CommonUtils.CommonExtensions;
using GreenCity.Db.Extensions;
using Microsoft.EntityFrameworkCore;
using SDContracts.Enums;
using SDContracts.ViewModels;
using SDService.Database.Extensions;
using SDService.Database.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SDService.Database.Implements
{
    public class Container2ScheduleStorage : IContainer2ScheduleStorage
    {
        private readonly SdDbContext _context;

        public Container2ScheduleStorage(
            SdDbContext context
        )
        {
            _context = context;
        }

        public async Task<IList<Container2ScheduleForMissionsViewModel>> GetFilteredContainer2ScheduleForMissionsAsync(
            DateTime? date = default,
            IList<int> contractClientIds = default,
            IList<int> contractIds = default,
            MissionInfoEntityType? entityType = MissionInfoEntityType.Real
        )
        {
            var result = await (
                from c2S in _context.Container2Schedules
                join contract in _context.Contracts
                    on c2S.ContractId equals contract.Id
                join contractClient in _context.ContractClients
                    on contract.ContractClientId equals contractClient.Id
                join container in _context.Containers
                    on c2S.ContainerId equals container.Id
                where 1 == 1
                    && c2S.IsDeleted == false
                    && contract.IsDeleted == false
                    && contractClient.IsDeleted == false
                    && container.IsDeleted == false
                select new
                {
                    Container = container,
                    Contract = contract,
                    ContractClientId = contractClient.Id,
                    ContractScheduleCollection = c2S.ContractScheduleJson
                })
                .WhereIf(date.HasValue,
                    x => x.Contract.StartDate <= date.Value.Date
                    && (
                        x.Contract.IsAutorenewSchedules ||
                        !x.Contract.EndDate.HasValue ||
                        x.Contract.EndDate.Value >= date.Value.Date
                    )
                )
                .WhereIf(contractClientIds != default && contractClientIds.Count > 0, x => contractClientIds.Contains(x.ContractClientId))
                .WhereIf(contractIds != default && contractIds.Count > 0, x => x.Contract != default && contractIds.Contains(x.Contract.Id))
                .WhereIf(entityType.HasValue, x => x.Contract.EntityType == (byte)entityType.Value)
                .WhereIf(entityType.HasValue, x => x.Container.EntityType == (byte)entityType.Value)
                .AsNoTracking()
                .ToListAsync();

            return result
                .Where(x => !x.ContractScheduleCollection.IsNullOrEmpty())
                .Select(x => (
                    ContainerId: x.Container.Id,
                    ContractId: x.Contract.Id,
                    ContractClientId: x.ContractClientId,
                    ContractScheduleCollection: x.ContractScheduleCollection.ToContractScheduleCollection()
                ))
                .DistinctBy(x => (x.ContainerId, x.ContractId, x.ContractClientId))
                .Select(x => new Container2ScheduleForMissionsViewModel
                {
                    ContainerId = x.ContainerId,
                    ContractId = x.ContractId,
                    ContractClientId = x.ContractClientId,
                    ContractScheduleCollection = x.ContractScheduleCollection.ToList()
                })
                .ToList();
        }
    }
}
