using DispatcherService.Contracts.DataContracts.TravelSheet;
using DispatcherService.Contracts.Filters;
using DispatcherService.Database.Implements;
using DispatcherService.Database.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DispatcherService.Database.Interfaces
{
    public interface ITravelSheetStorage
    {
        Task<int> GetLastNumberPerOrgByOrgIdAsync(int orgId);

        Task<IList<TravelSheet>> GetCommonFilteredTravelSheetsAsync(
            CommonTravelSheetFilterModel filter
        );

        Task<int> CreateTravelSheetAsync(
            TravelSheet travelSheet,
            int createdUserId = 0,
            string createdUserName = nameof(TravelSheetStorage)
        );

        Task<bool> DeleteTravelSheetsAsync(
            IList<int> travelSheetIds,
            int deletedUserId = 0,
            string deletedUserName = nameof(TravelSheetStorage)
        );

        Task FixTravelSheetDriveTimesAsync(FixTravelSheetDriveTimesContract contract);

        Task<IList<TravelSheet>> GetTravelSheetByIds(IList<int> travelSheetIds);
    }
}
