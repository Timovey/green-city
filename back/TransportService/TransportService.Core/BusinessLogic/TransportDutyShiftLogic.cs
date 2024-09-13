using AutoMapper;
using GreenCity.CommonUtils.CommonExtensions;
using GreenCity.Http.Helpers;
using GreenCity.Http;
using System.Net;
using TransportContracts.DataContracts.TransportDutyShift;
using TransportContracts.DataContracts.Transport;
using TransportContracts.Filters;
using TransportContracts.ViewModels;
using TransportService.Database.Interfaces;
using TransportService.Core.Helpers;

namespace TransportService.Core.BusinessLogic
{
    public class TransportDutyShiftLogic
    {
        private readonly ITransportDutyShiftStorage _transportDutyShiftStorage;
        private readonly ITransportStorage _transportStorage;
        private readonly IMapper _mapper;

        public TransportDutyShiftLogic(
            ITransportDutyShiftStorage transportDutyShiftStorage,
            ITransportStorage transportStorage,
            IMapper mapper
        )
        {
            _transportDutyShiftStorage = transportDutyShiftStorage;
            _transportStorage = transportStorage;
            _mapper = mapper;
        }

        public async Task<CommonHttpResponse<TransportDutyShiftViewModel>> CreateTransportDutyShiftAsync(CreateTransportDutyShiftContract contract)
        {
            if (contract == null)
                return CommonHttpHelper.BuildErrorResponse<TransportDutyShiftViewModel>(initialError: "Пустой запрос");

            try
            {
                var transports = await _transportStorage.GetTransportsAsync(new GetTransportContract()
                {
                    TransportIds = new List<int> { contract.TransportId }
                });
                if (transports == null || transports.Count == 0)
                {
                    return CommonHttpHelper.BuildErrorResponse<TransportDutyShiftViewModel>(initialError: "Нет такого транспорта");
                }
                //нормализуем при вставке дату
                contract.Date = DateRangeHelper.Normalize(contract.Date);

                var result = await _transportDutyShiftStorage.CreateTransportDutyShiftAsync(contract);

                return CommonHttpHelper.BuildSuccessResponse(result, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CommonHttpHelper.BuildErrorResponse<TransportDutyShiftViewModel>(
                    HttpStatusCode.InternalServerError,
                    ex.ToExceptionDetails(),
                    $"Ошибка выполнения метода {nameof(CreateTransportDutyShiftAsync)} ReqId : {contract.RequestId}");
            }
        }

        public async Task<CommonHttpResponse<IList<TransportDutyShiftViewModel>>> GetTransportDutyShiftsAsync(
            GetTransportDutyShiftContract contract
        )
        {
            if (contract?.TransportDutyShiftIds == null)
                return CommonHttpHelper.BuildErrorResponse<IList<TransportDutyShiftViewModel>>(initialError: "Пустой запрос");

            try
            {
                var transport = await _transportDutyShiftStorage.GetTransportDutyShiftsAsync(contract);
                if (transport == null)
                    return CommonHttpHelper.BuildErrorResponse<IList<TransportDutyShiftViewModel>>(HttpStatusCode.NotFound, null, "Запись не найдена");

                return CommonHttpHelper.BuildSuccessResponse(transport, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CommonHttpHelper.BuildErrorResponse<IList<TransportDutyShiftViewModel>>(
                    HttpStatusCode.InternalServerError,
                    ex.ToExceptionDetails(),
                    $"Ошибка выполнения метода {nameof(GetTransportDutyShiftsAsync)}");
            }
        }

        public async Task<CommonHttpResponse<IList<TransportDutyShiftViewModel>>> GetFilteredTransportDutyShiftsAsync(
            TransportDutyShiftFilterModel filter
        )
        {
            if (filter == null)
                return CommonHttpHelper.BuildErrorResponse<IList<TransportDutyShiftViewModel>>(initialError: "Пустой запрос");

            try
            {
                var result = filter.IsFilter ? await _transportDutyShiftStorage.GetFilteredTransportDutyShiftsAsync(filter) :
                    await _transportDutyShiftStorage.GetFilteredTransportDutyShiftsAsync(filter);

                return CommonHttpHelper.BuildSuccessResponse(result, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CommonHttpHelper.BuildErrorResponse<IList<TransportDutyShiftViewModel>>(
                    HttpStatusCode.InternalServerError,
                    ex.ToExceptionDetails(),
                    $"Ошибка выполнения метода {nameof(GetFilteredTransportDutyShiftsAsync)}");
            }
        }

        public async Task<CommonHttpResponse> DeleteTransportDutyShiftsAsync(DeleteTransportDutyShiftContract contract)
        {
            if (contract?.TransportDutyShiftIds == null)
                return CommonHttpHelper.BuildErrorResponse(initialError: "Пустой запрос");

            try
            {
                if (await _transportDutyShiftStorage.DeleteTransportDutyShiftsAsync(contract))
                {
                    return CommonHttpHelper.BuildSuccessResponse();
                }

                return CommonHttpHelper.BuildErrorResponse(HttpStatusCode.NotFound, null, "Запись не найдена");
            }
            catch (Exception ex)
            {
                return CommonHttpHelper.BuildErrorResponse(
                    HttpStatusCode.InternalServerError,
                    ex.ToExceptionDetails(),
                    $"Ошибка выполнения метода {nameof(DeleteTransportDutyShiftsAsync)}");
            }
        }
    }
}
