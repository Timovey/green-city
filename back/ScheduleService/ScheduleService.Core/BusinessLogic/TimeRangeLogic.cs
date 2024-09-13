using AutoMapper;
using GreenCity.CommonUtils.CommonExtensions;
using GreenCity.Http;
using GreenCity.Http.Helpers;
using ScheduleService.Contracts.DataContracts.ContractSchedule.Mission;
using ScheduleService.Contracts.DataContracts.DateRange;
using ScheduleService.Contracts.ViewModels;
using ScheduleService.Core.Services.DateRange;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ScheduleService.Core.BusinessLogic
{
    public class TimeRangeLogic
    {
        private readonly IMapper _mapper;
        private readonly IDateRangeSlicer _slicer;

        public TimeRangeLogic(IMapper mapper, IDateRangeSlicer slicer)
        {
            _mapper = mapper;
            _slicer = slicer;
        }

        public async Task<CommonHttpResponse<IList<DateRangeViewModel>>> GetSlicedDateRangeAsync(GetDateRangeContract contract)
        {
            try
            {
                var result = _slicer.GetSortedTimeSlots(contract.DateRanges)
                    .Select(x => _mapper.Map<DateRangeViewModel>(x))
                    .ToList();

                return CommonHttpHelper.BuildSuccessResponse<IList<DateRangeViewModel>>(result, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CommonHttpHelper.BuildErrorResponse<IList<DateRangeViewModel>> (
                    HttpStatusCode.InternalServerError,
                    ex.ToExceptionDetails(),
                    $"Ошибка выполнения метода {nameof(GetSlicedDateRangeAsync)} ReqId : {contract.RequestId}");
            }        
        }

        public async Task<CommonHttpResponse<IList<SSContractScheduleCollectionModel>>> SliceContractScheduleCollectionAsync(
            GetFireInfoOnDateContractScheduleCollectionContract contract
        )
        {
            try
            {
                var result = _slicer.GetSortedTimeSlots(contract.NotSlicedContractScheduleCollection).ToList();

                return CommonHttpHelper.BuildSuccessResponse<IList<SSContractScheduleCollectionModel>>(result, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CommonHttpHelper.BuildErrorResponse<IList<SSContractScheduleCollectionModel>>(
                    HttpStatusCode.InternalServerError,
                    ex.ToExceptionDetails(),
                    $"Ошибка выполнения метода {nameof(SliceContractScheduleCollectionAsync)} ReqId : {contract.RequestId}");
            }
        }
    }
}
