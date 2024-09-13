using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using DispatcherService.Contracts.Interfaces;
using ExternalApi.Interface.Models.Report;
using ExternalApiService.Operations.Interface;
using GreenCity.Http;
using GreenCity.Http.Helpers;
using MissionService.Contracts.Interface;
using ReportService.Contracts.DataContracts;
using ReportService.Contracts.DataContracts.DoneTasksList;
using ReportService.Contracts.Interfaces;

namespace ExternalApiService.Operations
{
    public class ReportOperations : IReportOperations
    {
        private readonly ITravelSheetApi _travelSheetApi;
        private readonly IShiftTaskApi _shiftTaskApi;
        private readonly IDoneTaskListApi _doneTaskListApi;
        private readonly IMissionApi _missionApi;
        private readonly IReportApi _reportApi;
        private readonly IMapper _mapper;

        public ReportOperations(
            ITravelSheetApi travelSheetApi,
            IShiftTaskApi shiftTaskApi,
            IDoneTaskListApi doneTaskListApi,
            IMissionApi missionApi,
            IReportApi reportApi,
            IMapper mapper
        )
        {
            _travelSheetApi = travelSheetApi;
            _shiftTaskApi = shiftTaskApi;
            _doneTaskListApi = doneTaskListApi;
            _missionApi = missionApi;
            _reportApi = reportApi;
            _mapper = mapper;
        }

        public async Task<CommonHttpResponse<ResponseFileContract>> GetTravelSheetFileAsync(
            ExtGetTravelSheetFileRequest request
        )
        {
            if (request == null)
                return CommonHttpHelper.BuildErrorResponse<ResponseFileContract>(initialError: "Пустой запрос");

            var getTravelSheetResult = await _travelSheetApi.GetTravelSheetModelForPrintAsync(request.Id);
            if (!getTravelSheetResult.Success)
            {
                return CommonHttpHelper.BuildErrorResponse<ResponseFileContract>(
                    initialError: "Ошибка при получении данных на печать",
                    extErrors: getTravelSheetResult.Errors?.ToList()
                );
            }

            var getTravelSheetContract = _mapper.Map<CreateTravelSheetsReportContract>(getTravelSheetResult.Data);
            var getTravelSheetReportResult = await _reportApi.GenerateTravelSheetsReport(getTravelSheetContract);
            if (!getTravelSheetReportResult.Success)
            {
                return CommonHttpHelper.BuildErrorResponse<ResponseFileContract>(
                    initialError: "Ошибка при генерации файла",
                    extErrors: getTravelSheetReportResult.Errors?.ToList()
                );
            }

            return CommonHttpHelper.BuildSuccessResponse(getTravelSheetReportResult.Data, HttpStatusCode.OK);
        }

        public async Task<CommonHttpResponse<ResponseFileContract>> GetShiftTaskFileAsync(
            ExtGetShiftTaskFileRequest request
        )
        {
            if (request == null)
                return CommonHttpHelper.BuildErrorResponse<ResponseFileContract>(initialError: "Пустой запрос");

            var getShiftTaskResult = await _shiftTaskApi.GetShiftTaskModelForPrintAsync(request.Id);
            if (!getShiftTaskResult.Success)
            {
                return CommonHttpHelper.BuildErrorResponse<ResponseFileContract>(
                    initialError: "Ошибка при получении данных на печать",
                    extErrors: getShiftTaskResult.Errors?.ToList()
                );
            }

            var getShiftTaskContract = _mapper.Map<CreateShiftTaskReportContract>(getShiftTaskResult.Data);
            var getShiftTaskReportResult = await _reportApi.GenerateShiftTaskReport(getShiftTaskContract);
            if (!getShiftTaskReportResult.Success)
            {
                return CommonHttpHelper.BuildErrorResponse<ResponseFileContract>(
                    initialError: "Ошибка при генерации файла",
                    extErrors: getShiftTaskReportResult.Errors?.ToList()
                );
            }

            return CommonHttpHelper.BuildSuccessResponse(getShiftTaskReportResult.Data, HttpStatusCode.OK);
        }

        public async Task<CommonHttpResponse<ResponseFileContract>> GetDoneTaskListFileAsync(
            ExtGetDoneTaskListFileRequest request
        )
        {
            if (request == null)
                return CommonHttpHelper.BuildErrorResponse<ResponseFileContract>(initialError: "Пустой запрос");

            var getDoneTaskListResult =
                await _doneTaskListApi.GetDoneTaskListModelForPrintAsync(request.DateFrom, request.DateTo,
                    request.CarIds);
            if (!getDoneTaskListResult.Success)
                return CommonHttpHelper.BuildErrorResponse<ResponseFileContract>(
                    initialError: "Ошибка при получении данных на печать",
                    extErrors: getDoneTaskListResult.Errors?.ToList()
                );

            var getDoneTaskListContract = _mapper.Map<DoneTaskListContract>(getDoneTaskListResult.Data);
            var getDoneTaskListReportResult = await _reportApi.GenerateDoneTaskListReportAsync(getDoneTaskListContract);
            if (!getDoneTaskListReportResult.Success)
                return CommonHttpHelper.BuildErrorResponse<ResponseFileContract>(
                    initialError: "Ошибка при генерации файла",
                    extErrors: getDoneTaskListReportResult.Errors?.ToList()
                );

            return CommonHttpHelper.BuildSuccessResponse(getDoneTaskListReportResult.Data, HttpStatusCode.OK);
        }
    }
}
