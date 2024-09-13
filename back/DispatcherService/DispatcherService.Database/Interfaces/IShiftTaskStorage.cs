using DispatcherService.Contracts.DataContracts.ShiftTask;
using DispatcherService.Contracts.Filters;
using DispatcherService.Database.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DispatcherService.Database.Interfaces
{
    public interface IShiftTaskStorage
    {
        Task<(bool IsSuccess, int AddedShiftTaskId)> CreateShiftTaskAsync(
            CreateShiftTaskContract contract,
            int travelSheetId
        );

        Task<IList<(ShiftTask ShiftTask, IList<int> RouteIds)>> GetCommonFilteredShiftTasksAsync(
            CommonShiftTasksFilterModel filter
        );

        Task<bool> DeleteShiftTasksAsync(DeleteShiftTasksContract contract);

        Task<IList<int>> GetRouteIdsFromShiftTasksWithFilterAsync(
            DateTime shiftTaskDate
        );
    }
}
