using DispatcherService.Contracts.DataContracts.TravelSheet;
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
    public class TravelSheetStorage : ITravelSheetStorage
    {
        private readonly DispatcherDbContext _context;
        private readonly IFillHistoryService _fillHistoryService;

        public TravelSheetStorage(
            DispatcherDbContext context,
            IFillHistoryService historyService
        )
        {
            _fillHistoryService = historyService;
            _context = context;
        }

        public async Task<int> GetLastNumberPerOrgByOrgIdAsync(int orgId)
        {
            var result = await _context.TravelSheets
                .Where(x => x.IsDeleted == false)
                .Where(x => x.OrgId == orgId)
                .OrderByDescending(x => x.NumberPerOrg)
                .Select(x => x.NumberPerOrg)
                .FirstOrDefaultAsync();

            return result;
        }

        public async Task<IList<TravelSheet>> GetCommonFilteredTravelSheetsAsync(
            CommonTravelSheetFilterModel filter
        )
        {
            var result = await _context.TravelSheets
                .Where(x => x.IsDeleted == false)
                .WhereIf(filter.Date.HasValue, x => x.Date.Date == filter.Date.Value.Date)
                .WhereIf(filter.DateFrom.HasValue, x => x.Date.Date >= filter.DateFrom.Value.Date)
                .WhereIf(filter.DateTo.HasValue, x => x.Date.Date <= filter.DateTo.Value.Date)
                .WhereIf(!filter.NumberToShow.IsNullOrEmpty(), x => x.NumberToShow != default && x.NumberToShow.Contains(filter.NumberToShow))
                .WhereIf(filter.DriverIds != default && filter.DriverIds.Count > 0, x => filter.DriverIds.Contains(x.DriverId))
                .WhereIf(filter.OrgIds != default && filter.OrgIds.Count > 0, x => filter.OrgIds.Contains(x.OrgId))
                .WhereIf(filter.CarIds != default && filter.CarIds.Count > 0, x => filter.CarIds.Contains(x.CarId))
                .WhereIf(filter.TravelSheetIds != default && filter.TravelSheetIds.Count > 0, x => filter.TravelSheetIds.Contains(x.Id))
                .WhereIf(filter.WithNotEmptyStartDriveTime.HasValue && filter.WithNotEmptyStartDriveTime.Value, x => x.StartDriveTime != default)
                .WhereIf(filter.WithNotEmptyStartDriveTime.HasValue && !filter.WithNotEmptyStartDriveTime.Value,
                    x => x.StartDriveTime == default || x.StartDriveTime == string.Empty
                )
                .ToListAsync();

            return result;
        }

        public async Task<int> CreateTravelSheetAsync(
            TravelSheet travelSheet,
            int createdUserId = 0,
            string createdUserName = nameof(TravelSheetStorage)
        )
        {
            await _context.TravelSheets.AddAsync(travelSheet);
            await _fillHistoryService.FillHistoryAsync<TravelSheet, TravelSheetHistory>(
                new List<TravelSheet> { travelSheet },
                _context,
                createdUserId,
                createdUserName,
                true
            );

            return travelSheet.Id;
        }

        public async Task<bool> DeleteTravelSheetsAsync(
            IList<int> travelSheetIds,
            int deletedUserId = 0,
            string deletedUserName = nameof(TravelSheetStorage)
        )
        {
            var deletedTravelSheets = new List<TravelSheet>();
            var deletedShiftTasks = new List<ShiftTask>();
            var deletedShiftTask2Routes = new List<ShiftTask2Route>();

            var toDeleteTravelSheets = await _context.TravelSheets
                .Where(x => x.IsDeleted == false)
                .Where(x => travelSheetIds.Contains(x.Id))
                .ToListAsync();

            //!_! Удалять так же будем и связанные сменные задания
            var toDeleteShiftTasks = await (
                from shiftTask in _context.ShiftTasks
                join shiftTask2Route in _context.ShiftTask2Routes
                    on shiftTask.Id equals shiftTask2Route.ShiftTaskId
                where 1 == 1
                    && shiftTask.IsDeleted == false
                    && shiftTask2Route.IsDeleted == false
                    && travelSheetIds.Contains(shiftTask.TravelSheetId)
                select new { ShiftTask = shiftTask, ShiftTask2Route = shiftTask2Route })
                .ToListAsync();


            using (var tran = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    foreach (var travelSheet in toDeleteTravelSheets)
                    {
                        travelSheet.IsDeleted = true;
                        deletedTravelSheets.Add(travelSheet);
                    }

                    foreach (var shiftTask in toDeleteShiftTasks.Select(x => x.ShiftTask).DistinctBy(x => x.Id))
                    {
                        shiftTask.IsDeleted = true;
                        deletedShiftTasks.Add(shiftTask);
                    }

                    foreach (var shiftTask2Route in toDeleteShiftTasks.Select(x => x.ShiftTask2Route).DistinctBy(x => x.Id))
                    {
                        shiftTask2Route.IsDeleted = true;
                        deletedShiftTask2Routes.Add(shiftTask2Route);
                    }

                    await _fillHistoryService.FillHistoryAsync<TravelSheet, TravelSheetHistory>(
                        deletedTravelSheets,
                        _context,
                        deletedUserId,
                        deletedUserName,
                        true
                    );

                    await _fillHistoryService.FillHistoryAsync<ShiftTask, ShiftTaskHistory>(
                       deletedShiftTasks,
                       _context,
                       deletedUserId,
                       deletedUserName,
                       false
                   );

                    await _fillHistoryService.FillHistoryAsync<ShiftTask2Route, ShiftTask2RouteHistory>(
                       deletedShiftTask2Routes,
                       _context,
                       deletedUserId,
                       deletedUserName,
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

        public async Task FixTravelSheetDriveTimesAsync(FixTravelSheetDriveTimesContract contract)
        {
            var updatedTravelSheet = await _context.TravelSheets
                .Where(x => x.IsDeleted == false)
                .Where(x => x.Id == contract.TravelSheetId)
                .FirstOrDefaultAsync();

            updatedTravelSheet.StartDriveTime = contract.StartDriveTime;
            updatedTravelSheet.FinishDriveTime = contract.FinishDriveTime;

            await _fillHistoryService.FillHistoryAsync<TravelSheet, TravelSheetHistory>(
                new List<TravelSheet> { updatedTravelSheet },
                _context,
                contract.UserId,
                contract.UserName,
                true
            );
        }

        public async Task<IList<TravelSheet>> GetTravelSheetByIds(IList<int> travelSheetIds)
        {
            return await _context.TravelSheets
                .Where(x => x.IsDeleted == false)
                .Where(x => travelSheetIds.Contains(x.Id))
                .ToListAsync();
        }
    }
}
