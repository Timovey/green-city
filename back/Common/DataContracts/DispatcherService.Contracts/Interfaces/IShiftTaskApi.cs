using DispatcherService.Contracts.DataContracts.ShiftTask;
using DispatcherService.Contracts.DataContracts.ShiftTask.Report;
using DispatcherService.Contracts.Filters;
using DispatcherService.Contracts.ViewModels;
using GreenCity.Http;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DispatcherService.Contracts.Interfaces
{
    public interface IShiftTaskApi
    {
        /// <summary>
        /// Создания СЗ
        /// </summary>
        /// <param name="contract"></param>
        /// <returns></returns>
        [Post("/ShiftTask/CreateShiftTask")]
        Task<CommonHttpResponse<CreateShiftTaskResponse>> CreateShiftTaskAsync(
           [Body] CreateShiftTaskContract contract
        );

        /// <summary>
        /// Получение фильтрованного списка СЗ - можно использовать и для получения по ИД
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [Post("/ShiftTask/GetCommonFilteredShiftTasks")]
        Task<CommonHttpResponse<IList<CommonShiftTaskViewModel>>> GetCommonFilteredShiftTasksAsync(
           [Body] CommonShiftTasksFilterModel filter
        );

        /// <summary>
        /// Удаление СЗ
        /// </summary>
        /// <param name="contract"></param>
        /// <returns></returns>
        [Post("/ShiftTask/DeleteShiftTasks")]
        Task<CommonHttpResponse> DeleteShiftTasksAsync(
           [Body] DeleteShiftTasksContract contract
        );

        /// <summary>
        /// Получения доступных для создания бригады рейсов
        /// </summary>
        /// <param name="contract"></param>
        /// <returns></returns>
        [Post("/ShiftTask/GetAvailableRouteForCreateShiftTask")]
        Task<CommonHttpResponse<IList<AvailableRouteForCreateShiftTaskViewModel>>> GetAvailableRouteForCreateShiftTaskAsync(
           [Body] GetAvailableRoutesForCreateShiftTaskContract contract
        );

        /// <summary>
        /// Получить модель СЗ на печать
        /// </summary>
        /// <param name="shiftTaskId"></param>
        /// <returns></returns>
        [Post("/ShiftTask/GetShiftTaskModelForPrint")]
        Task<CommonHttpResponse<ShiftTaskReportModel>> GetShiftTaskModelForPrintAsync(
            int shiftTaskId
        );

        /// <summary>
        /// Получить выпадающий список организаций с фильтром по экипажам
        /// </summary>
        /// <param name="contract"></param>
        /// <returns></returns>
        [Post("/ShiftTask/GetOrgsDropDownForCreateShiftTask")]
        Task<CommonHttpResponse<IList<OrgsDropDownForCreateShiftTaskViewModel>>> GetOrgsDropDownForCreateShiftTaskAsync(
            GetOrgsDropDownForCreateShiftTaskContract contract
        );
    }
}
