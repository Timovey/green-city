using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DispatcherService.Contracts.DataContracts.DoneTaskList.Report;
using GreenCity.Http;

namespace DispatcherService.Core.BusinessLogic.Interface;

public interface IDoneTaskListLogic
{
    Task<CommonHttpResponse<DoneTaskListReportModel>> GetDoneTaskListModelForPrintAsync(DateTime dateFrom,
        DateTime dateTo, IList<int> carIds
    );
}