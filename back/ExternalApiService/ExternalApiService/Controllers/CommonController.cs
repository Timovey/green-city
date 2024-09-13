using ExternalApi.Interface.Models.Common;
using ExternalApi.Interface.Models.Common.Cron;
using ExternalApiService.Operations.Interface;
using GreenCity.Http;
using GreenCity.Http.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ExternalApiService.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]/[action]")]
    [ModelStateValidationActionFilter]
    public class CommonController : Controller
    {
        private readonly IScheduleOperations _scheduleOperations;
        private readonly ICommonOperations _commonOperations;

        public CommonController(
            IScheduleOperations scheduleOperations,
            ICommonOperations commonOperations
        )
        {
            _scheduleOperations = scheduleOperations;
            _commonOperations = commonOperations;
        }

        [HttpPost]
        public Task<CommonHttpResponse<ExtGetCronNextFireDatesResult>> GetFireDatesByCronAsync(ExtGetCronNextFireDatesRequest request)
        {
            return _scheduleOperations.GetNextFireContractScheduleDatesAsync(request);
        }

        [HttpPost]
        public Task<CommonHttpResponse<ExtGetCronCollectionNextFireDatesResult>> GetFireDatesByCronCollectionAsync(ExtGetCronCollectionNextFireDatesRequest request)
        {
            return _scheduleOperations.GetNextFireContractScheduleCollectionDatesAsync(request);
        }

        [HttpPost]
        public Task<CommonHttpResponse> GenerateTestDataAsync(ExtGenerateTestDataRequest request)
        {
            return _commonOperations.GenerateTestDataAsync(request);
        }
    }
}
