using DispatcherService.Contracts.DataContracts.TravelSheet;
using DispatcherService.Contracts.Filters;
using DispatcherService.Contracts.ViewModels;
using GreenCity.Http;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DispatcherService.Contracts.Interfaces
{
    public interface ITravelSheetApi
    {
        /// <summary>
        /// Создание ПЛ
        /// </summary>
        /// <param name="contract"></param>
        /// <returns></returns>
        [Post("/TravelSheet/CreateTravelSheet")]
        Task<CommonHttpResponse<int>> CreateTravelSheetAsync(
           [Body] CreateTravelSheetContract contract
        );

        /// <summary>
        /// Получени фильтрованного списка ПЛ, также можно использовать для получения по ИД
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [Post("/TravelSheet/GetCommonFilteredTravelSheets")]
        Task<CommonHttpResponse<IList<CommonTravelSheetViewModel>>> GetCommonFilteredTravelSheetsAsync(
           [Body] CommonTravelSheetFilterModel filter
        );

        /// <summary>
        /// Удлаение ПЛ
        /// </summary>
        /// <param name="contract"></param>
        /// <returns></returns>
        [Post("/TravelSheet/DeleteTravelSheets")]
        Task<CommonHttpResponse> DeleteTravelSheetsAsync(
           [Body] DeleteTravelSheetsContract contract
        );

        /// <summary>
        /// Проставка времени выезда и заезда ПЛ
        /// </summary>
        /// <param name="contract"></param>
        /// <returns></returns>
        [Post("/TravelSheet/FixTravelSheetDriveTimes")]
        Task<CommonHttpResponse> FixTravelSheetDriveTimesAsync(
           [Body] FixTravelSheetDriveTimesContract contract
        );

        /// <summary>
        /// Получить модель ПЛ на печать по ИД ПЛ
        /// </summary>
        /// <param name="travelSheetId">ИД ПЛ</param>
        /// <returns></returns>
        [Post("/TravelSheet/GetTravelSheetModelForPrint")]
        Task<CommonHttpResponse<TravelSheetsReportContract>> GetTravelSheetModelForPrintAsync(
            int travelSheetId
        );

        /// <summary>
        /// Получить путевые листы в виде, который нужен фронту
        /// </summary>
        /// <param name="travelSheetIds"></param>
        /// <returns></returns>
        [Post("/TravelSheet/GetTravelSheetByIdsForFront")]
        Task<CommonHttpResponse<IList<TravelSheetForFrontViewModel>>> GetTravelSheetByIdsForFrontAsync(
            IList<int> travelSheetIds
        );
    }
}
