using ExternalApi.Interface.Models.Report;
using ExternalApiService.Operations.Interface;
using GreenCity.Http;
using GreenCity.Http.Attributes;
using Microsoft.AspNetCore.Mvc;
using ReportService.Contracts.DataContracts;
using System;
using System.Threading.Tasks;

namespace ExternalApiService.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [ModelStateValidationActionFilter]
    public class ReportController : Controller
    {
        private readonly IReportOperations _reportOperations;

        public ReportController(IReportOperations reportOperations)
        {
            _reportOperations = reportOperations;
        }

        [HttpPost]
        public async Task<IActionResult> GetTravelSheetFileAsync(ExtGetTravelSheetFileRequest request)
        {
            var fileResult = await _reportOperations.GetTravelSheetFileAsync(request);

            return GetActionResultFromFileResponse(fileResult);
        }

        [HttpPost]
        public async Task<IActionResult> GetShiftTaskFileAsync(ExtGetShiftTaskFileRequest request)
        {
            var fileResult = await _reportOperations.GetShiftTaskFileAsync(request);

            return GetActionResultFromFileResponse(fileResult);
        }

        [HttpPost]
        public async Task<IActionResult> GetDoneTaskListFileAsync(ExtGetDoneTaskListFileRequest request)
        {
            var fileResult = await _reportOperations.GetDoneTaskListFileAsync(request);

            return GetActionResultFromFileResponse(fileResult);
        }

        private IActionResult GetActionResultFromFileResponse(CommonHttpResponse<ResponseFileContract> fileResult)
        {
            if (fileResult.Success)
            {
                return File(fileResult.Data.Buffer, fileResult.Data.Extension, fileResult.Data.Filename);
            }

            foreach (var error in fileResult.Errors)
            {
                ModelState.AddModelError(Guid.NewGuid().ToString(), error);
            }

            return BadRequest(ModelState.Values);
        }
    }
}
