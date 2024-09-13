using AutoMapper;
using GreenCity.CommonInfrastructure.CommonExtensions;
using GreenCity.CommonInfrastructure.Helpers;
using GreenCity.CommonInfrastructure.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TransportContracts.DataContracts.Transport;
using TransportContracts.Filters;
using TransportContracts.ViewModels;
using TransportService.Core.Extensions;
using TransportService.Database.Interfaces;

namespace TransportService.Core.BusinessLogic
{
    public class TransportLogic
    {
        private readonly ITransportStorage _transportStorage;
        private readonly IMapper _mapper;

        public TransportLogic(ITransportStorage transportStorage,
            IMapper mapper)
        {
            _transportStorage = transportStorage;
            _mapper = mapper;
        }

        public async Task<CommonHttpResponse> CreateTransport(CreateTransportContract contract)
        {
            try
            {
                var errors = contract.Validate();
                if (errors.Any())
                {
                    return CommonHttpHelper.BuildErrorResponse(HttpStatusCode.NoContent, errors, $"Неверные данные. ReqId {contract.RequestId}");
                }

                await _transportStorage.CreateTransport(contract);

                return new CommonHttpResponse
                {
                    Success = true
                };
            }
            catch (Exception ex)
            {
                return CommonHttpHelper.BuildErrorResponse(
                    HttpStatusCode.InternalServerError,
                    ex.ToExceptionDetails(),
                    $"Ошибка выполнения метода {nameof(CreateTransport)} ReqId : {contract.RequestId}");
            }
        }

        public async Task<CommonHttpResponse> UpdateTransport(UpdateTransportContract contract)
        {
            try
            {
                var errors = contract.Validate();
                if (errors.Count > 0)
                {
                    return CommonHttpHelper.BuildErrorResponse(HttpStatusCode.NoContent, errors, "Неверные данные");
                }

                if (await _transportStorage.UpdateTransport(contract))
                {
                    return new CommonHttpResponse
                    {
                        Success = true
                    };
                }

                return CommonHttpHelper.BuildErrorResponse(HttpStatusCode.NotFound, null, "Запись не найдена");
            }
            catch (Exception ex)
            {
                return CommonHttpHelper.BuildErrorResponse(
                    HttpStatusCode.InternalServerError,
                    ex.ToExceptionDetails(),
                    $"Ошибка выполнения метода {nameof(UpdateTransport)}");
            }
        }

        public async Task<CommonHttpResponse> DeleteTransport(DeleteTransportContract contract)
        {
            try
            {
                if (await _transportStorage.DeleteTransport(contract.TransportId))
                {
                    return new CommonHttpResponse
                    {
                        Success = true
                    };
                }

                return CommonHttpHelper.BuildErrorResponse(HttpStatusCode.NotFound, null, "Запись не найдена");
            }
            catch (Exception ex)
            {
                return CommonHttpHelper.BuildErrorResponse(
                    HttpStatusCode.InternalServerError,
                    ex.ToExceptionDetails(),
                    $"Ошибка выполнения метода {nameof(DeleteTransport)}");
            }
        }

        public async Task<CommonHttpResponse<TransportViewModel>> GetTransport(GetTransportContract contract)
        {
            try
            {
                var transport = await _transportStorage.GetTransport(contract.TransportId);
                if (transport == null)
                    return CommonHttpHelper.BuildErrorResponse<TransportViewModel>(HttpStatusCode.NotFound, null, "Запись не найдена");

                return new CommonHttpResponse<TransportViewModel>
                {
                    Success = true,
                    Data = transport
                };
            }
            catch (Exception ex)
            {
                return CommonHttpHelper.BuildErrorResponse<TransportViewModel>(
                    HttpStatusCode.InternalServerError,
                    ex.ToExceptionDetails(),
                    $"Ошибка выполнения метода {nameof(GetTransport)}");
            }
        }

        public async Task<CommonHttpResponse<List<TransportViewModel>>> GetFilteredTransport(TransportFilterModel filter)
        {
            try
            {
                var result = await _transportStorage.GetFilteredList(filter);
                return new CommonHttpResponse<List<TransportViewModel>>
                {
                    Success = true,
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return CommonHttpHelper.BuildErrorResponse<List<TransportViewModel>>(
                    HttpStatusCode.InternalServerError,
                    ex.ToExceptionDetails(),
                    $"Ошибка выполнения метода {nameof(GetTransport)}");
            }
        }
    }
}
