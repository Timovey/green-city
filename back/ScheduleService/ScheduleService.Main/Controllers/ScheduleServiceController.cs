using GreenCity.Http;
using GreenCity.Http.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ScheduleService.Contracts.DataContracts.ContractSchedule;
using ScheduleService.Contracts.DataContracts.ContractSchedule.Mission;
using ScheduleService.Contracts.DataContracts.DateRange;
using ScheduleService.Contracts.DataContracts.RedDates;
using ScheduleService.Contracts.DataContracts.WeekendWorkDates;
using ScheduleService.Contracts.Interfaces;
using ScheduleService.Contracts.ViewModels;
using ScheduleService.Contracts.ViewModels.RedDates;
using ScheduleService.Contracts.ViewModels.WeekendWorkDates;
using ScheduleService.Core.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ScheduleService.Main.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    [ModelStateValidationActionFilter]
    public class ScheduleServiceController : Controller, IScheduleServiceApi
    {
        private readonly ILogger<ScheduleServiceController> _logger;
        private readonly DatesLogic _datesLogic;
        private readonly CronScheduleLogic _cronScheduleLogic;
        private readonly TimeRangeLogic _timeRangeLogic;

        public ScheduleServiceController(
            ILogger<ScheduleServiceController> logger,
            DatesLogic datesLogic,
            CronScheduleLogic cronScheduleLogic,
            TimeRangeLogic timeRangeLogic
        )
        {
            _logger = logger;
            _datesLogic = datesLogic;
            _cronScheduleLogic = cronScheduleLogic;
            _timeRangeLogic = timeRangeLogic;
        }

        [HttpPost]
        public Task<CommonHttpResponse<int>> CreateRedDateAsync(CreateRedDateContract contract)
        {
            return _datesLogic.CreateRedDateAsync(contract);
        }

        [HttpPost]
        public Task<CommonHttpResponse<IList<RedDateViewModel>>> GetActualRedDatesAsync()
        {
            return _datesLogic.GetActualRedDatesAsync();
        }

        [HttpPost]
        public Task<CommonHttpResponse<int>> CreateWeekendWorkDateAsync(CreateWeekendWorkDateContract contract)
        {
            return _datesLogic.CreateWeekendWorkDateAsync(contract);
        }

        [HttpPost]
        public Task<CommonHttpResponse<IList<WeekendWorkDateViewModel>>> GetActualWeekendWorkDatesAsync()
        {
            return _datesLogic.GetActualWeekendWorkDatesAsync();
        }

        [HttpPost]
        public Task<CommonHttpResponse<IList<DateTime>>> GetNextFireContractScheduleDatesAsync(GetContractScheduleContract contract)
        {
            return _cronScheduleLogic.GetNextFireContractScheduleDatesAsync(contract);
        }

        [HttpPost]
        public Task<CommonHttpResponse<IList<DateRangeViewModel>>> GetSlisedDateRangeAsync(GetDateRangeContract contract)
        {
            return _timeRangeLogic.GetSlicedDateRangeAsync(contract);
        }

        [HttpPost]
        public Task<CommonHttpResponse<bool>> IsContractScheduleFireOnDateAsync(GetFireInfoOnDateContractScheduleCollectionContract contract)
        {
            return _cronScheduleLogic.IsContractScheduleCollectionFireOnDateAsync(contract);
        }
    }
}
