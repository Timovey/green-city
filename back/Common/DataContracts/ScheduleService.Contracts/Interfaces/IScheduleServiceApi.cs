using GreenCity.Http;
using Refit;
using ScheduleService.Contracts.DataContracts.ContractSchedule;
using ScheduleService.Contracts.DataContracts.ContractSchedule.Mission;
using ScheduleService.Contracts.DataContracts.DateRange;
using ScheduleService.Contracts.DataContracts.RedDates;
using ScheduleService.Contracts.DataContracts.WeekendWorkDates;
using ScheduleService.Contracts.ViewModels;
using ScheduleService.Contracts.ViewModels.RedDates;
using ScheduleService.Contracts.ViewModels.WeekendWorkDates;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ScheduleService.Contracts.Interfaces
{
    public interface IScheduleServiceApi
    {
        [Post("/ScheduleService/CreateRedDate")]
        Task<CommonHttpResponse<int>> CreateRedDateAsync(
            [Body] CreateRedDateContract contract
        );

        [Post("/ScheduleService/GetActualRedDates")]
        Task<CommonHttpResponse<IList<RedDateViewModel>>> GetActualRedDatesAsync();

        [Post("/ScheduleService/CreateWeekendWorkDate")]
        Task<CommonHttpResponse<int>> CreateWeekendWorkDateAsync(
            [Body] CreateWeekendWorkDateContract contract
        );

        [Post("/ScheduleService/GetActualWeekendWorkDates")]
        Task<CommonHttpResponse<IList<WeekendWorkDateViewModel>>> GetActualWeekendWorkDatesAsync();

        [Post("/ScheduleService/GetNextFireContractScheduleDates")]
        Task<CommonHttpResponse<IList<DateTime>>> GetNextFireContractScheduleDatesAsync(
            GetContractScheduleContract contract
        );

        [Post("/ScheduleService/GetSlisedDateRange")]
        Task<CommonHttpResponse<IList<DateRangeViewModel>>> GetSlisedDateRangeAsync(
            [Body] GetDateRangeContract contract
        );

        [Post("/ScheduleService/IsContractScheduleFireOnDate")]
        Task<CommonHttpResponse<bool>> IsContractScheduleFireOnDateAsync(
           [Body] GetFireInfoOnDateContractScheduleCollectionContract contract
        );
    }
}
