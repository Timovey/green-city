using AutoMapper;
using GreenCity.CommonUtils.CommonExtensions;
using GreenCity.Http;
using GreenCity.Http.Helpers;
using ScheduleService.Contracts.DataContracts.RedDates;
using ScheduleService.Contracts.DataContracts.WeekendWorkDates;
using ScheduleService.Contracts.ViewModels.RedDates;
using ScheduleService.Contracts.ViewModels.WeekendWorkDates;
using ScheduleService.Database.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace ScheduleService.Core.BusinessLogic
{
    public class DatesLogic
    {
        private readonly IRedDatesStorage _redDatesStorage;
        private readonly IWeekendWorkDatesStorage _weekendWorkDatesStorage;
        private readonly IMapper _mapper;

        public DatesLogic(
            IRedDatesStorage redDatesStorage,
            IWeekendWorkDatesStorage weekendWorkDatesStorage,
            IMapper mapper
        )
        {
            _redDatesStorage = redDatesStorage;
            _weekendWorkDatesStorage = weekendWorkDatesStorage;
            _mapper = mapper;
        }

        public async Task<CommonHttpResponse<int>> CreateRedDateAsync(CreateRedDateContract contract)
        {
            if (contract == null)
                return CommonHttpHelper.BuildErrorResponse<int>(initialError: "Пустой запрос");

            try
            {
                var result = await _redDatesStorage.CreateRedDateAsync(contract);

                return CommonHttpHelper.BuildSuccessResponse(result, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CommonHttpHelper.BuildErrorResponse<int>(
                    HttpStatusCode.InternalServerError,
                    ex.ToExceptionDetails(),
                    $"Ошибка выполнения метода {nameof(CreateRedDateAsync)} ReqId : {contract.RequestId}");
            }
        }

        public async Task<CommonHttpResponse<IList<RedDateViewModel>>> GetActualRedDatesAsync()
        {
            try
            {
                var result = await _redDatesStorage.GetActualRedDatesAsync();
                return CommonHttpHelper.BuildSuccessResponse(result, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CommonHttpHelper.BuildErrorResponse<IList<RedDateViewModel>>(
                    HttpStatusCode.InternalServerError,
                    ex.ToExceptionDetails(),
                    $"Ошибка выполнения метода {nameof(GetActualRedDatesAsync)}");
            }
        }

        public async Task<CommonHttpResponse<int>> CreateWeekendWorkDateAsync(CreateWeekendWorkDateContract contract)
        {
            if (contract == null)
                return CommonHttpHelper.BuildErrorResponse<int>(initialError: "Пустой запрос");

            try
            {
                var result = await _weekendWorkDatesStorage.CreateWeekendWorkDateAsync(contract);

                return CommonHttpHelper.BuildSuccessResponse(result, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CommonHttpHelper.BuildErrorResponse<int>(
                    HttpStatusCode.InternalServerError,
                    ex.ToExceptionDetails(),
                    $"Ошибка выполнения метода {nameof(CreateWeekendWorkDateAsync)} ReqId : {contract.RequestId}");
            }
        }

        public async Task<CommonHttpResponse<IList<WeekendWorkDateViewModel>>> GetActualWeekendWorkDatesAsync()
        {
            try
            {
                var result = await _weekendWorkDatesStorage.GetActualWeekendWorkDatesAsync();
                return CommonHttpHelper.BuildSuccessResponse(result, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CommonHttpHelper.BuildErrorResponse<IList<WeekendWorkDateViewModel>>(
                    HttpStatusCode.InternalServerError,
                    ex.ToExceptionDetails(),
                    $"Ошибка выполнения метода {nameof(GetActualWeekendWorkDatesAsync)}");
            }
        }
    }
}
