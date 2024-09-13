using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DispatcherService.Contracts.DataContracts.DoneTaskList.Report;
using GreenCity.Http;
using Refit;

namespace DispatcherService.Contracts.Interfaces
{
    public interface IDoneTaskListApi
    {
        /// <summary>
        /// Получить модель журнала на печать по ИД
        /// </summary>
        /// <param name="dateFrom">Дата начала</param>
        /// <param name="dateFrom">Дата окончания</param>
        /// /// <param name="dateFrom">ИД машин</param>
        /// <returns></returns>
        [Post("/DoneTaskList/GetDoneTaskListModelForPrint")]
        Task<CommonHttpResponse<DoneTaskListReportModel>> GetDoneTaskListModelForPrintAsync(
            DateTime dateFrom, DateTime dateTo, IList<int> carIds
        );
    }
}