using DispatcherService.Contracts.DataContracts.ShiftTask;
using DispatcherService.Contracts.DataContracts.ShiftTask.Report;
using DispatcherService.Contracts.Filters;
using DispatcherService.Contracts.Interfaces;
using DispatcherService.Contracts.ViewModels;
using DispatcherService.Core.BusinessLogic.Interface;
using GreenCity.Http;
using GreenCity.Http.Attributes;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DispatcherService.Main.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    [ModelStateValidationActionFilter]
    public class ShiftTaskController : Controller, IShiftTaskApi
    {
        private readonly IShiftTaskLogic _shiftTaskLogic;

        public ShiftTaskController(
            IShiftTaskLogic shiftTaskLogic
        )
        {
            _shiftTaskLogic = shiftTaskLogic;
        }

        [HttpPost]
        public Task<CommonHttpResponse<CreateShiftTaskResponse>> CreateShiftTaskAsync(
            CreateShiftTaskContract contract
        )
        {
            return _shiftTaskLogic.CreateShiftTaskAsync(contract);
        }

        [HttpPost]
        public Task<CommonHttpResponse<IList<CommonShiftTaskViewModel>>> GetCommonFilteredShiftTasksAsync(
            CommonShiftTasksFilterModel filter
        )
        {
            return _shiftTaskLogic.GetCommonFilteredShiftTasksAsync(filter);
        }

        [HttpPost]
        public Task<CommonHttpResponse> DeleteShiftTasksAsync(DeleteShiftTasksContract contract)
        {
            return _shiftTaskLogic.DeleteShiftTasksAsync(contract);
        }

        [HttpPost]
        public Task<CommonHttpResponse<IList<AvailableRouteForCreateShiftTaskViewModel>>> GetAvailableRouteForCreateShiftTaskAsync(
            GetAvailableRoutesForCreateShiftTaskContract contract
        )
        {
            return _shiftTaskLogic.GetAvailableRouteForCreateShiftTaskAsync(contract);
        }

        [HttpPost]
        public Task<CommonHttpResponse<ShiftTaskReportModel>> GetShiftTaskModelForPrintAsync(int shiftTaskId)
        {
            return _shiftTaskLogic.GetShiftTaskModelForPrintAsync(shiftTaskId);
        }

        [HttpPost]
        public Task<CommonHttpResponse<IList<OrgsDropDownForCreateShiftTaskViewModel>>> GetOrgsDropDownForCreateShiftTaskAsync(
            GetOrgsDropDownForCreateShiftTaskContract contract
        )
        {
            return _shiftTaskLogic.GetOrgsDropDownForCreateShiftTaskAsync(contract);
        }
    }
}
