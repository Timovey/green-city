using DispatcherService.Database.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GreenCity.Db.Services.History.Interface;
using DispatcherService.Contracts.DataContracts.Brigade;
using DispatcherService.Database.Models;
using Microsoft.EntityFrameworkCore;
using DispatcherService.Contracts.Filters;
using GreenCity.Db.Extensions;

namespace DispatcherService.Database.Implements
{
    public class BrigadeStorage : IBrigadeStorage
    {
        private readonly DispatcherDbContext _context;
        private readonly IFillHistoryService _fillHistoryService;

        public BrigadeStorage(
            DispatcherDbContext context,
            IFillHistoryService historyService
        )
        {
            _fillHistoryService = historyService;
            _context = context;
        }

        public async Task<(bool IsSuccess, int AddedBrigadeId)> CreateBrigadeAsync(CreateBrigadeContract contract)
        {
            using (var tran = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var dbBrigade = CreateBrigadeModel(contract.Date, contract.WorkShiftId);
                    await _context.Brigades.AddAsync(dbBrigade);

                    await _fillHistoryService.FillHistoryAsync<Brigade, BrigadeHistory>(
                        new List<Brigade> { dbBrigade },
                        _context,
                        contract.UserId,
                        contract.UserName,
                        true
                    );

                    var dbBrigade2Routes = CreateBrigade2RouteModel(dbBrigade.Id, contract.RouteIds);
                    await _context.Brigade2Routes.AddRangeAsync(dbBrigade2Routes);

                    await _fillHistoryService.FillHistoryAsync<Brigade2Route, Brigade2RouteHistory>(
                        dbBrigade2Routes.ToList(),
                        _context,
                        contract.UserId,
                        contract.UserName,
                        false
                    );

                    var dbBrigade2AuxWorker = CreateBrigade2AuxWorkerModel(dbBrigade.Id, contract.AuxWorkerIds);
                    await _context.Brigade2AuxWorkers.AddRangeAsync(dbBrigade2AuxWorker);

                    await _fillHistoryService.FillHistoryAsync<Brigade2AuxWorker, Brigade2AuxWorkerHistory>(
                        dbBrigade2AuxWorker.ToList(),
                        _context,
                        contract.UserId,
                        contract.UserName,
                        true
                    );

                    await tran.CommitAsync();
                    return (true, dbBrigade.Id);
                }
                catch (Exception)
                {
                    await tran.RollbackAsync();
                    return (false, default);
                }
            }
        }

        public async Task<IList<Brigade2Route>> GetBrigade2RoutesByRouteIdsAsync(
            IList<int> routeIds
        )
        {
            var result = await _context.Brigade2Routes
                .Where(x => x.IsDeleted == false)
                .Where(x => routeIds.Contains(x.RouteId))
                .AsNoTracking()
                .ToListAsync();

            return result;
        }

        public async Task<IList<(Brigade Brigade, IList<AuxiliaryWorker> AuxWorkers, IList<int> RouteIds)>> GetCommonFilteredBrigadesAsync(
            CommonBrigadesFilterModel filter
        )
        {
            var result = await (
                from brigade in _context.Brigades
                join brigade2Route in _context.Brigade2Routes
                    on brigade.Id equals brigade2Route.BrigadeId
                join brigade2AuxWorker in _context.Brigade2AuxWorkers
                    on brigade.Id equals brigade2AuxWorker.BrigadeId
                join auxWorker in _context.AuxiliaryWorkers
                    on brigade2AuxWorker.AuxWorkerId equals auxWorker.Id
                where 1 == 1
                    && brigade.IsDeleted == false
                    && brigade2Route.IsDeleted == false
                    && brigade2AuxWorker.IsDeleted == false
                    && auxWorker.IsDeleted == false
                    && brigade.Date.Date == filter.Date.Date
                select new
                {
                    Brigade = brigade,
                    Brigate2Route = brigade2Route,
                    AuxWorker = auxWorker
                })
                .WhereIf(filter.RouteIds != default && filter.RouteIds.Count > 0,
                    x => filter.RouteIds.Contains(x.Brigate2Route.RouteId)
                )
                .WhereIf(filter.WorkShiftIds != default && filter.WorkShiftIds.Count > 0,
                    x => filter.WorkShiftIds.Contains(x.Brigade.WorkShiftId)
                )
                .AsNoTracking()
                .ToListAsync();

            Func<IEnumerable<int>, IList<int>> getIList = (routeIds) => routeIds.ToList();
            Func<IEnumerable<AuxiliaryWorker>, IList<AuxiliaryWorker>> getIListAuxiliaryWorker = (auxWorkersList) => auxWorkersList.ToList();

            return result
                .GroupBy(x => x.Brigade.Id)
                .Select(x => (
                        Brigade: x.Select(xx => xx.Brigade).FirstOrDefault(),
                        AuxWorkers: getIListAuxiliaryWorker(x.Select(xx => xx.AuxWorker)),
                        RouteIds: getIList(x.Select(xx => xx.Brigate2Route.RouteId))
                    )
                )
                .ToList();
        }

        public async Task<bool> DeleteBrigadesAsync(DeleteBrigadesContract contract)
        {
            var deletedBrigades = new List<Brigade>();
            var deletedBrigade2Routes = new List<Brigade2Route>();
            var deletedBrigade2AuxWorkers = new List<Brigade2AuxWorker>();

            var toDeleteBrigades = await (
                from brigade in _context.Brigades
                join brigade2Route in _context.Brigade2Routes
                    on brigade.Id equals brigade2Route.BrigadeId
                join brigade2AuxWorker in _context.Brigade2AuxWorkers
                    on brigade.Id equals brigade2AuxWorker.BrigadeId
                where 1 == 1
                    && brigade.IsDeleted == false
                    && brigade2Route.IsDeleted == false
                    && brigade2AuxWorker.IsDeleted == false
                    && contract.BrigadeIds.Contains(brigade.Id)
                select new
                {
                    Brigade = brigade,
                    Brigade2Route = brigade2Route,
                    Brigade2AuxWorker = brigade2AuxWorker
                })
                .ToListAsync();

            using (var tran = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    foreach (var brigade in toDeleteBrigades.Select(x => x.Brigade).DistinctBy(x => x.Id))
                    {
                        brigade.IsDeleted = true;
                        deletedBrigades.Add(brigade);
                    }

                    foreach (var brigade2Route in toDeleteBrigades.Select(x => x.Brigade2Route).DistinctBy(x => x.Id))
                    {
                        brigade2Route.IsDeleted = true;
                        deletedBrigade2Routes.Add(brigade2Route);
                    }

                    foreach (var brigade2AuxWorker in toDeleteBrigades.Select(x => x.Brigade2AuxWorker).DistinctBy(x => x.Id))
                    {
                        brigade2AuxWorker.IsDeleted = true;
                        deletedBrigade2AuxWorkers.Add(brigade2AuxWorker);
                    }

                    await _fillHistoryService.FillHistoryAsync<Brigade, BrigadeHistory>(
                        deletedBrigades,
                        _context,
                        contract.UserId,
                        contract.UserName,
                        false
                    );

                    await _fillHistoryService.FillHistoryAsync<Brigade2Route, Brigade2RouteHistory>(
                        deletedBrigade2Routes,
                        _context,
                        contract.UserId,
                        contract.UserName,
                        false
                    );

                    await _fillHistoryService.FillHistoryAsync<Brigade2AuxWorker, Brigade2AuxWorkerHistory>(
                        deletedBrigade2AuxWorkers,
                        _context,
                        contract.UserId,
                        contract.UserName,
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

        public async Task<IList<int>> GetRouteIdsFromBrigadesWithFilterAsync(
            DateTime shiftTaskDate
        )
        {
            var result = await (
                from brigade in _context.Brigades
                join brigade2Route in _context.Brigade2Routes
                    on brigade.Id equals brigade2Route.BrigadeId
                where 1 == 1
                    && brigade.IsDeleted == false
                    && brigade2Route.IsDeleted == false
                    && brigade.Date.Date == shiftTaskDate.Date
                select brigade2Route.RouteId)
                .ToListAsync();

            return result.Distinct().ToList();
        }

        private Brigade CreateBrigadeModel(
            DateTime date,
            int workShiftId
        )
        {
            return new Brigade
            {
                Date = date.Date,
                WorkShiftId = workShiftId
            };
        }

        private IEnumerable<Brigade2Route> CreateBrigade2RouteModel(int brigadeId, IList<int> routeIds)
        {
            return routeIds.Select(x => new Brigade2Route { BrigadeId = brigadeId, RouteId = x });
        }

        private IEnumerable<Brigade2AuxWorker> CreateBrigade2AuxWorkerModel(int brigadeId, IList<int> auxWorkerIds)
        {
            return auxWorkerIds.Select(x => new Brigade2AuxWorker { BrigadeId = brigadeId, AuxWorkerId = x });
        }
    }
}
