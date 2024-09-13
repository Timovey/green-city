using System.Threading.Tasks;
using ExternalApi.Interface.Models.Report;
using GreenCity.Http;
using ReportService.Contracts.DataContracts;

namespace ExternalApiService.Operations.Interface
{
    public interface IReportOperations
    {
        Task<CommonHttpResponse<ResponseFileContract>> GetTravelSheetFileAsync(
            ExtGetTravelSheetFileRequest request
        );

        Task<CommonHttpResponse<ResponseFileContract>> GetShiftTaskFileAsync(
            ExtGetShiftTaskFileRequest request
        );
        
        Task<CommonHttpResponse<ResponseFileContract>> GetDoneTaskListFileAsync(
            ExtGetDoneTaskListFileRequest request
        );
    }
}
