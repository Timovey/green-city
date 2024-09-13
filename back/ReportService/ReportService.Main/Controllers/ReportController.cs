using GreenCity.Http;
using Microsoft.AspNetCore.Mvc;
using ReportService.Contracts.DataContracts;
using ReportService.Contracts.DataContracts.DoneTasksList;
using ReportService.Contracts.Interfaces;
using ReportService.Core.BusinessLogic;
using System.Threading.Tasks;

namespace ReportService.Main.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class ReportController : ControllerBase, IReportApi
    {
        private readonly ReportWordLogic _reportWordLogic;
        private readonly ReportExcelLogic _reportExcelLogic;
        public ReportController(ReportExcelLogic reportExcelLogic, ReportWordLogic reportWordLogic) { 
            _reportWordLogic = reportWordLogic;
            _reportExcelLogic = reportExcelLogic;
        }

        [HttpPost]
        public Task<CommonHttpResponse<ResponseFileContract>> GenerateDoneTaskListReportAsync(DoneTaskListContract contract)
        {
            return _reportExcelLogic.GenerateDoneTaskListReportAsync(contract);
        }

        [HttpPost]
        public Task<CommonHttpResponse<ResponseFileContract>> GenerateShiftTaskReport(CreateShiftTaskReportContract contract)
        {
            return _reportExcelLogic.GenerateShiftTaskReportAsync(contract);
        }

        [HttpPost]
        public Task<CommonHttpResponse<ResponseFileContract>> GenerateTravelSheetsReport(CreateTravelSheetsReportContract contract)
        {
            return _reportWordLogic.GenerateTravelSheetsReportAsync(contract);
        }
    }
}
