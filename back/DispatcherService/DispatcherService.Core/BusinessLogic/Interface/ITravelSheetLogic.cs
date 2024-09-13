using DispatcherService.Contracts.DataContracts.TravelSheet;
using DispatcherService.Contracts.Filters;
using DispatcherService.Contracts.ViewModels;
using GreenCity.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DispatcherService.Core.BusinessLogic.Interface
{
    public interface ITravelSheetLogic
    {
        Task<CommonHttpResponse<int>> CreateTravelSheetAsync(CreateTravelSheetContract contract);

        Task<CommonHttpResponse<IList<CommonTravelSheetViewModel>>> GetCommonFilteredTravelSheetsAsync(
            CommonTravelSheetFilterModel filter
        );

        Task<CommonHttpResponse> DeleteTravelSheetsAsync(
            DeleteTravelSheetsContract contract
        );

        Task<CommonHttpResponse> FixTravelSheetDriveTimesAsync(
            FixTravelSheetDriveTimesContract contract
        );

        Task<CommonHttpResponse<TravelSheetsReportContract>> GetTravelSheetModelForPrintAsync(
            int travelSheetId
        );

        Task<CommonHttpResponse<IList<TravelSheetForFrontViewModel>>> GetTravelSheetByIdsForFrontAsync(IList<int> travelSheetIds);
    }
}
