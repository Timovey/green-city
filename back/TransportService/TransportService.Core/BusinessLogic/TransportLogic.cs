using AutoMapper;
using GreenCity.CommonUtils.CommonExtensions;
using GreenCity.Http;
using GreenCity.Http.Helpers;
using System.Net;
using TransportContracts.DataContracts.Transport;
using TransportContracts.Filters;
using TransportContracts.ViewModels;
using TransportService.Database.Interfaces;

namespace TransportService.Core.BusinessLogic
{
    public class TransportLogic
    {
        private readonly ITransportStorage _transportStorage;
        private readonly IMapper _mapper;

        public TransportLogic(
            ITransportStorage transportStorage,
            IMapper mapper
        )
        {
            _transportStorage = transportStorage;
            _mapper = mapper;
        }

        public async Task<CommonHttpResponse<TransportViewModel>> CreateTransportAsync(CreateTransportContract contract)
        {
            if (contract == null)
                return CommonHttpHelper.BuildErrorResponse<TransportViewModel>(initialError: "Пустой запрос");

            try
            {
                var result = await _transportStorage.CreateTransportAsync(contract);

                return CommonHttpHelper.BuildSuccessResponse(result, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CommonHttpHelper.BuildErrorResponse<TransportViewModel>(
                    HttpStatusCode.InternalServerError,
                    ex.ToExceptionDetails(),
                    $"Ошибка выполнения метода {nameof(CreateTransportAsync)} ReqId : {contract.RequestId}");
            }
        }

        public async Task<CommonHttpResponse<TransportViewModel>> UpdateTransportsAsync(UpdateTransportContract contract)
        {
            if (contract?.TransportIds == null)
                return CommonHttpHelper.BuildErrorResponse<TransportViewModel>(initialError: "Пустой запрос");

            if (contract.TransportIds.Count == 0)
            {
                return CommonHttpHelper.BuildErrorResponse<TransportViewModel>(initialError: "Не выбраны обновляемые сущности");
            }
            try
            {
                var result = await _transportStorage.UpdateTransportsAsync(contract);
                return CommonHttpHelper.BuildSuccessResponse(result, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CommonHttpHelper.BuildErrorResponse<TransportViewModel>(
                    HttpStatusCode.InternalServerError,
                    ex.ToExceptionDetails(),
                    $"Ошибка выполнения метода {nameof(UpdateTransportsAsync)}");
            }
        }

        public async Task<CommonHttpResponse<IList<TransportViewModel>>> GetTransportsAsync(
            GetTransportContract contract
        )
        {
            if (contract?.TransportIds == null)
                return CommonHttpHelper.BuildErrorResponse<IList<TransportViewModel>>(initialError: "Пустой запрос");

            try
            {
                var transport = await _transportStorage.GetTransportsAsync(contract);
                if (transport == null)
                {
                    if (contract.ReturnEmptyResultIfNotFound)
                    {
                        return CommonHttpHelper.BuildSuccessResponse<IList<TransportViewModel>>(new List<TransportViewModel>(), HttpStatusCode.OK);
                    }

                    return CommonHttpHelper.BuildErrorResponse<IList<TransportViewModel>>(HttpStatusCode.NotFound, null, "Запись не найдена");
                }

                return CommonHttpHelper.BuildSuccessResponse(transport, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CommonHttpHelper.BuildErrorResponse<IList<TransportViewModel>>(
                    HttpStatusCode.InternalServerError,
                    ex.ToExceptionDetails(),
                    $"Ошибка выполнения метода {nameof(GetTransportsAsync)}");
            }
        }

        public async Task<CommonHttpResponse<IList<TransportViewModel>>> GetFilteredTransportsAsync(
            TransportFilterModel filter
        )
        {
            if (filter == null)
                return CommonHttpHelper.BuildErrorResponse<IList<TransportViewModel>>(initialError: "Пустой запрос");

            try
            {
                var result = filter.IsFilter ? await _transportStorage.GetFilteredTransportsAsync(filter) :
                    await _transportStorage.GetFilteredTransportsAsync(filter);

                return CommonHttpHelper.BuildSuccessResponse(result, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CommonHttpHelper.BuildErrorResponse<IList<TransportViewModel>>(
                    HttpStatusCode.InternalServerError,
                    ex.ToExceptionDetails(),
                    $"Ошибка выполнения метода {nameof(GetFilteredTransportsAsync)}");
            }
        }

        public async Task<CommonHttpResponse> DeleteTransportsAsync(DeleteTransportContract contract)
        {
            if (contract?.TransportIds == null)
                return CommonHttpHelper.BuildErrorResponse(initialError: "Пустой запрос");

            try
            {
                if (await _transportStorage.DeleteTransportsAsync(contract))
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
                    $"Ошибка выполнения метода {nameof(DeleteTransportsAsync)}");
            }
        }
    }
}
