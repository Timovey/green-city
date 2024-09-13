using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DispatcherService.Contracts.DataContracts.DoneTaskList.Report;
using DispatcherService.Contracts.Interfaces;
using DispatcherService.Core.BusinessLogic.Interface;
using GreenCity.Http;
using GreenCity.Http.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace DispatcherService.Main.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    [ModelStateValidationActionFilter]
    public class DoneTaskListController : Controller, IDoneTaskListApi
    {
        private readonly IDoneTaskListLogic _doneTaskListLogic;

        public DoneTaskListController(
            IDoneTaskListLogic doneTaskListLogic
        )
        {
            _doneTaskListLogic = doneTaskListLogic;
        }

        [HttpPost]
        public Task<CommonHttpResponse<DoneTaskListReportModel>> GetDoneTaskListModelForPrintAsync(
            DateTime dateFrom,
            DateTime dateTo,
            IList<int> carIds
        )
        {
            return _doneTaskListLogic.GetDoneTaskListModelForPrintAsync(dateFrom, dateTo, carIds);
        }
    }
}