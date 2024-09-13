using DispatcherService.Contracts.DataContracts.ShiftTask;
using DispatcherService.Contracts.Filters;
using DispatcherService.Database.Interfaces;
using DispatcherService.Database.Models;
using GreenCity.CommonUtils.CommonExtensions;
using GreenCity.Db.Extensions;
using GreenCity.Db.Services.History.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DispatcherService.Database.Implements
{
    public class ShiftTaskStorage : IShiftTaskStorage
    {
        private readonly DispatcherDbContext _context;
        private readonly IFillHistoryService _fillHistoryService;

        public ShiftTaskStorage(
            DispatcherDbContext context,
            IFillHistoryService historyService
        )
        {
            _fillHistoryService = historyService;
            _context = context;
        }

        public async Task<(bool IsSuccess, int AddedShiftTaskId)> CreateShiftTaskAsync(
            CreateShiftTaskContract contract,
            int travelSheetId
        )
        {
            using (var tran = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var dbShiftTask = CreateShiftTaskModel(contract, travelSheetId);
                    await _context.ShiftTasks.AddAsync(dbShiftTask);

                    await _fillHistoryService.FillHistoryAsync<ShiftTask, ShiftTaskHistory>(
                        new List<ShiftTask> { dbShiftTask },
                        _context,
                        contract.UserId,
                        contract.UserName,
                        true
                    );

                    var dbShiftTask2Routes = CreateShiftTask2RouteModel(dbShiftTask.Id, contract.RouteIds);
                    await _context.ShiftTask2Routes.AddRangeAsync(dbShiftTask2Routes);

                    await _fillHistoryService.FillHistoryAsync<ShiftTask2Route, ShiftTask2RouteHistory>(
                        dbShiftTask2Routes.ToList(),
                        _context,
                        contract.UserId,
                        contract.UserName,
                        true
                    );

                    await tran.CommitAsync();
                    return (true, dbShiftTask.Id);
                }
                catch (Exception)
                {
                    await tran.RollbackAsync();
                    return (false, default);
                }
            }
        }

        public async Task<IList<(ShiftTask ShiftTask, IList<int> RouteIds)>> GetCommonFilteredShiftTasksAsync(
            CommonShiftTasksFilterModel filter
        )
        {
            var result = await (
                from shiftTask in _context.ShiftTasks
                join shiftTask2Route in _context.ShiftTask2Routes
                    on shiftTask.Id equals shiftTask2Route.ShiftTaskId
                join travelSheet in _context.TravelSheets
                    on shiftTask.TravelSheetId equals travelSheet.Id
                where 1 == 1
                    && shiftTask.IsDeleted == false
                    && shiftTask2Route.IsDeleted == false
                    && travelSheet.IsDeleted == false
                select new
                {
                    ShiftTask = shiftTask,
                    ShiftTask2Route = shiftTask2Route,
                    TravelSheet = travelSheet
                })
                .AsNoTracking()
                .WhereIf(filter.ShiftTaskIds == default || filter.ShiftTaskIds.Count == 0,
                    x => x.ShiftTask.Date.Date == filter.Date.Date
                )
                .WhereIf(filter.ShiftTaskIds != default && filter.ShiftTaskIds.Count > 0,
                    x => filter.ShiftTaskIds.Contains(x.ShiftTask.Id)
                )
                .WhereIf(filter.OrgIds != default && filter.OrgIds.Count > 0,
                    x => filter.OrgIds.Contains(x.ShiftTask.OrgId)
                )
                .WhereIf(filter.WorkShiftIds != default && filter.WorkShiftIds.Count > 0,
                    x => filter.WorkShiftIds.Contains(x.ShiftTask.WorkShiftId)
                )
                .WhereIf(filter.RouteIds != default && filter.RouteIds.Count > 0,
                    x => filter.RouteIds.Contains(x.ShiftTask2Route.RouteId)
                )
                .WhereIf(!filter.TravelSheetNumber.IsNullOrEmpty(),
                    x => x.TravelSheet.NumberToShow != default && x.TravelSheet.NumberToShow.Contains(filter.TravelSheetNumber)
                )
                .ToListAsync();

            Func<IEnumerable<int>, IList<int>> getIList = (routeIds) => routeIds.ToList();

            return result
                .GroupBy(x => x.ShiftTask.Id)
                .Select(x =>
                    (
                        ShiftTask: x.Select(xx => xx.ShiftTask).FirstOrDefault(),
                        RouteIds: getIList(x.Select(xx => xx.ShiftTask2Route.RouteId))
                    )
                )
                .ToList();
        }

        public async Task<bool> DeleteShiftTasksAsync(DeleteShiftTasksContract contract)
        {
            var deletedShiftTasks = new List<ShiftTask>();
            var deletedShiftTask2Routes = new List<ShiftTask2Route>();
            var deletedTravelSheets = new List<TravelSheet>();

            var toDeleteShiftTasks = await _context.ShiftTasks
                .Where(x => x.IsDeleted == false)
                .Where(x => contract.ShiftTaskIds.Contains(x.Id))
                .ToListAsync();

            var toDeleteShiftTask2Routes = await _context.ShiftTask2Routes
                .Where(x => x.IsDeleted == false)
                .Where(x => contract.ShiftTaskIds.Contains(x.ShiftTaskId))
                .ToListAsync();

            var toDeleteTravelSheets = await _context.TravelSheets
                .Where(x => x.IsDeleted == false)
                .Where(x => toDeleteShiftTasks.Select(xx => xx.TravelSheetId).Distinct().Contains(x.Id))
                .ToListAsync();            

            using (var tran = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    foreach (var shiftTask in toDeleteShiftTasks)
                    {
                        shiftTask.IsDeleted = true;
                        deletedShiftTasks.Add(shiftTask);
                    }

                    foreach (var shiftTask2Route in toDeleteShiftTask2Routes)
                    {
                        shiftTask2Route.IsDeleted = true;
                        deletedShiftTask2Routes.Add(shiftTask2Route);
                    }

                    foreach (var travelSheet in toDeleteTravelSheets)
                    {
                        travelSheet.IsDeleted = true;
                        deletedTravelSheets.Add(travelSheet);
                    }

                    await _fillHistoryService.FillHistoryAsync<ShiftTask, ShiftTaskHistory>(
                        deletedShiftTasks,
                        _context,
                        contract.UserId,
                        contract.UserName,
                        false
                    );

                    await _fillHistoryService.FillHistoryAsync<ShiftTask2Route, ShiftTask2RouteHistory>(
                        deletedShiftTask2Routes,
                        _context,
                        contract.UserId,
                        contract.UserName,
                        false
                    );

                    await _fillHistoryService.FillHistoryAsync<TravelSheet, TravelSheetHistory>(
                        deletedTravelSheets,
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

        public async Task<IList<int>> GetRouteIdsFromShiftTasksWithFilterAsync(
            DateTime shiftTaskDate
        )
        {
            var result = await (
                from shiftTask in _context.ShiftTasks
                join shiftTask2Route in _context.ShiftTask2Routes
                    on shiftTask.Id equals shiftTask2Route.ShiftTaskId
                where 1 == 1
                    && shiftTask.IsDeleted == false
                    && shiftTask2Route.IsDeleted == false
                    && shiftTask.Date.Date == shiftTaskDate.Date
                select shiftTask2Route.RouteId)
                .ToListAsync();

            return result.Distinct().ToList();
        }

        private ShiftTask CreateShiftTaskModel(
            CreateShiftTaskContract contract,
            int travelSheetId
        )
        {
            return new ShiftTask
            {
                Date = contract.Date.Date,
                WorkShiftId = contract.WorkShiftId,
                TravelSheetId = travelSheetId,
                OrgId = contract.OrgId
            };
        }

        private IEnumerable<ShiftTask2Route> CreateShiftTask2RouteModel(int shiftTaskId, IList<int> routeIds)
        {
            return routeIds.Select(x => new ShiftTask2Route { ShiftTaskId = shiftTaskId, RouteId = x });
        }
    }
}
