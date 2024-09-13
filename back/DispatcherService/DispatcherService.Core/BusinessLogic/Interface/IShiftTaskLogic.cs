using DispatcherService.Contracts.DataContracts.ShiftTask;
using DispatcherService.Contracts.DataContracts.ShiftTask.Report;
using DispatcherService.Contracts.Filters;
using DispatcherService.Contracts.ViewModels;
using GreenCity.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DispatcherService.Core.BusinessLogic.Interface
{
    public interface IShiftTaskLogic
    {
        Task<CommonHttpResponse<CreateShiftTaskResponse>> CreateShiftTaskAsync(
            CreateShiftTaskContract contract
        );

        Task<CommonHttpResponse<IList<CommonShiftTaskViewModel>>> GetCommonFilteredShiftTasksAsync(
            CommonShiftTasksFilterModel filter
        );

        Task<CommonHttpResponse> DeleteShiftTasksAsync(DeleteShiftTasksContract contract);

        Task<CommonHttpResponse<IList<AvailableRouteForCreateShiftTaskViewModel>>> GetAvailableRouteForCreateShiftTaskAsync(
            GetAvailableRoutesForCreateShiftTaskContract contract
        );

        Task<CommonHttpResponse<ShiftTaskReportModel>> GetShiftTaskModelForPrintAsync(int shiftTaskId);

        Task<CommonHttpResponse<IList<OrgsDropDownForCreateShiftTaskViewModel>>> GetOrgsDropDownForCreateShiftTaskAsync(
            GetOrgsDropDownForCreateShiftTaskContract contract
        );
    }
}
