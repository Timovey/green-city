using AutoMapper;
using ExternalApi.Interface.Models.Common.Cron;
using ExternalApiService.Operations.Interface;
using GreenCity.Http;
using GreenCity.Http.Helpers;
using ScheduleService.Contracts.DataContracts.ContractSchedule;
using ScheduleService.Contracts.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace ExternalApiService.Operations
{
    public class ScheduleOperations : IScheduleOperations
    {
        private readonly IScheduleServiceApi _scheduleApi;
        private readonly IMapper _mapper;

        public ScheduleOperations(
            IScheduleServiceApi scheduleApi,
            IMapper mapper
        )
        {
            _scheduleApi = scheduleApi;
            _mapper = mapper;
        }

        public async Task<CommonHttpResponse<ExtGetCronNextFireDatesResult>> GetNextFireContractScheduleDatesAsync(ExtGetCronNextFireDatesRequest request)
        {
            if (request == null)
                return CommonHttpHelper.BuildErrorResponse<ExtGetCronNextFireDatesResult>(initialError: "Пустой запрос");

            var scheduleContract = _mapper.Map<GetContractScheduleContract>(request);
            var result = await _scheduleApi.GetNextFireContractScheduleDatesAsync(scheduleContract);

            if (result.Success)
            {
                return CommonHttpHelper.BuildSuccessResponse(
                    new ExtGetCronNextFireDatesResult { Dates = result.Data },
                    System.Net.HttpStatusCode.OK);
            }

            return CommonHttpHelper.BuildErrorResponse<ExtGetCronNextFireDatesResult>(result.StatusCode, result.Errors.ToList());
        }

        public async Task<CommonHttpResponse<ExtGetCronCollectionNextFireDatesResult>> GetNextFireContractScheduleCollectionDatesAsync(ExtGetCronCollectionNextFireDatesRequest request)
        {
            if (request == null)
                return CommonHttpHelper.BuildErrorResponse<ExtGetCronCollectionNextFireDatesResult>(initialError: "Пустой запрос");

            var scheduleContract = _mapper.Map<GetContractScheduleContract>(request);
            var result = await _scheduleApi.GetNextFireContractScheduleDatesAsync(scheduleContract);

            if (result.Success)
            {
                return CommonHttpHelper.BuildSuccessResponse(
                    new ExtGetCronCollectionNextFireDatesResult { Dates = result.Data },
                    System.Net.HttpStatusCode.OK);
            }

            return CommonHttpHelper.BuildErrorResponse<ExtGetCronCollectionNextFireDatesResult>(result.StatusCode, result.Errors.ToList());
        }
    }
}
