using AutoMapper;
using GreenCity.CommonUtils.CommonExtensions;
using GreenCity.Http.Helpers;
using GreenCity.Http;
using System.Net;
using TransportContracts.DataContracts.Rent;
using TransportContracts.DataContracts.Transport;
using TransportContracts.Filters;
using TransportContracts.ViewModels;
using TransportService.Database.Interfaces;
using TransportService.Core.Helpers;

namespace TransportService.Core.BusinessLogic
{
    public class RentLogic
    {
        private readonly IRentStorage _rentStorage;
        private readonly ITransportStorage _transportStorage;
        private readonly IMapper _mapper;

        public RentLogic(
            IRentStorage rentStorage,
            ITransportStorage transportStorage,
            IMapper mapper
        )
        {
            _rentStorage = rentStorage;
            _transportStorage = transportStorage;
            _mapper = mapper;
        }

        public async Task<CommonHttpResponse<RentViewModel>> CreateRentAsync(CreateRentContract contract)
        {
            if (contract == null)
                return CommonHttpHelper.BuildErrorResponse<RentViewModel>(initialError: "Пустой запрос");

            try
            {
                var transports = await _transportStorage.GetTransportsAsync(new GetTransportContract()
                {
                    TransportIds = new List<int> { contract.TransportId }
                });
                if (transports == null || transports.Count == 0)
                {
                    return CommonHttpHelper.BuildErrorResponse<RentViewModel>(initialError: "Нет такого транспорта");
                }
                //нормализуем диапазон
                contract.DateRange.Normalize();

                //проверка на то, что дата начала раньше даты окончания
                if (contract.DateRange.EndDate.Value.Date < contract.DateRange.StartDate.Date)
                {
                    return CommonHttpHelper.BuildErrorResponse<RentViewModel>(initialError:
                        "Дата окончания должна быть не меньше, чем дата начала");
                }
                var result = await _rentStorage.CreateRentAsync(contract);

                return CommonHttpHelper.BuildSuccessResponse(result, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CommonHttpHelper.BuildErrorResponse<RentViewModel>(
                    HttpStatusCode.InternalServerError,
                    ex.ToExceptionDetails(),
                    $"Ошибка выполнения метода {nameof(CreateRentAsync)} ReqId : {contract.RequestId}");
            }
        }

        public async Task<CommonHttpResponse<IList<RentViewModel>>> GetRentsAsync(
            GetRentContract contract
        )
        {
            if (contract?.RentIds == null)
                return CommonHttpHelper.BuildErrorResponse<IList<RentViewModel>>(initialError: "Пустой запрос");

            try
            {
                var transport = await _rentStorage.GetRentsAsync(contract);
                if (transport == null)
                    return CommonHttpHelper.BuildErrorResponse<IList<RentViewModel>>(HttpStatusCode.NotFound, null, "Запись не найдена");

                return CommonHttpHelper.BuildSuccessResponse(transport, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CommonHttpHelper.BuildErrorResponse<IList<RentViewModel>>(
                    HttpStatusCode.InternalServerError,
                    ex.ToExceptionDetails(),
                    $"Ошибка выполнения метода {nameof(GetRentsAsync)}");
            }
        }

        public async Task<CommonHttpResponse<IList<RentViewModel>>> GetFilteredRentsAsync(
            RentFilterModel filter
        )
        {
            if (filter == null)
                return CommonHttpHelper.BuildErrorResponse<IList<RentViewModel>>(initialError: "Пустой запрос");

            try
            {
                var result = filter.IsFilter ? await _rentStorage.GetFilteredRentsAsync(filter) :
                    await _rentStorage.GetFilteredRentsAsync(filter);

                return CommonHttpHelper.BuildSuccessResponse(result, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CommonHttpHelper.BuildErrorResponse<IList<RentViewModel>>(
                    HttpStatusCode.InternalServerError,
                    ex.ToExceptionDetails(),
                    $"Ошибка выполнения метода {nameof(GetFilteredRentsAsync)}");
            }
        }

        public async Task<CommonHttpResponse> DeleteRentsAsync(DeleteRentContract contract)
        {
            if (contract?.RentIds == null)
                return CommonHttpHelper.BuildErrorResponse(initialError: "Пустой запрос");

            try
            {
                if (await _rentStorage.DeleteRentsAsync(contract))
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
                    $"Ошибка выполнения метода {nameof(DeleteRentsAsync)}");
            }
        }
    }
}
