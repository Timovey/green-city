using ExternalApi.Interface.Models.Common.Cron;
using GreenCity.Http;
using System.Threading.Tasks;

namespace ExternalApiService.Operations.Interface
{
    public interface IScheduleOperations
    {
        Task<CommonHttpResponse<ExtGetCronNextFireDatesResult>> GetNextFireContractScheduleDatesAsync(ExtGetCronNextFireDatesRequest request);

        Task<CommonHttpResponse<ExtGetCronCollectionNextFireDatesResult>> GetNextFireContractScheduleCollectionDatesAsync(ExtGetCronCollectionNextFireDatesRequest request);
    }
}
