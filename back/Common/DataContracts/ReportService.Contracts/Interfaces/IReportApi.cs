using GreenCity.Http;
using Refit;
using ReportService.Contracts.DataContracts;
using System.Threading.Tasks;
using ReportService.Contracts.DataContracts.DoneTasksList;

namespace ReportService.Contracts.Interfaces
{
    public interface IReportApi
    {
        [Post("/Report/GenerateTravelSheetsReport")]
        Task<CommonHttpResponse<ResponseFileContract>> GenerateTravelSheetsReport(
            [Body] CreateTravelSheetsReportContract contract);

        [Post("/Report/GenerateShiftTaskReport")]
        Task<CommonHttpResponse<ResponseFileContract>> GenerateShiftTaskReport(
            [Body] CreateShiftTaskReportContract contract);

        [Post("/Report/GenerateDoneTaskListReport")]
        Task<CommonHttpResponse<ResponseFileContract>> GenerateDoneTaskListReportAsync(
            [Body] DoneTaskListContract contract);
    }
}