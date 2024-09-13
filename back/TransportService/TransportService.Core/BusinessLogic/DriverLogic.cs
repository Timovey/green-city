using AutoMapper;
using GreenCity.CommonUtils.CommonExtensions;
using GreenCity.Http;
using GreenCity.Http.Helpers;
using System.Net;
using TransportContracts.DataContracts.Driver;
using TransportContracts.Filters;
using TransportContracts.ViewModels;
using TransportService.Database.Interfaces;

namespace TransportService.Core.BusinessLogic
{
    public class DriverLogic
    {
        private readonly IDriverStorage _driverStorage;
        private readonly IMapper _mapper;

        public DriverLogic(
            IDriverStorage driverStorage,
            IMapper mapper
        )
        {
            _driverStorage = driverStorage;
            _mapper = mapper;
        }

        public async Task<CommonHttpResponse<DriverViewModel>> CreateDriverAsync(CreateDriverContract contract)
        {
            if (contract == null)
                return CommonHttpHelper.BuildErrorResponse<DriverViewModel>(initialError: "Пустой запрос");

            try
            {
                var result = await _driverStorage.CreateDriverAsync(contract);

                return CommonHttpHelper.BuildSuccessResponse(result, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CommonHttpHelper.BuildErrorResponse<DriverViewModel>(
                    HttpStatusCode.InternalServerError,
                    ex.ToExceptionDetails(),
                    $"Ошибка выполнения метода {nameof(CreateDriverAsync)} ReqId : {contract.RequestId}");
            }
        }

        public async Task<CommonHttpResponse<DriverViewModel>> UpdateDriversAsync(UpdateDriverContract contract)
        {
            if (contract == null)
                return CommonHttpHelper.BuildErrorResponse<DriverViewModel>(initialError: "Пустой запрос");

            try
            {
                //если мы разуволили сотрудника - убираем дату увольнения
                if (!contract.IsFired)
                {
                    contract.FiredDate = null;
                }
                var result = await _driverStorage.UpdateDriversAsync(contract);
                return CommonHttpHelper.BuildSuccessResponse(result, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CommonHttpHelper.BuildErrorResponse<DriverViewModel>(
                    HttpStatusCode.InternalServerError,
                    ex.ToExceptionDetails(),
                    $"Ошибка выполнения метода {nameof(UpdateDriversAsync)}");
            }
        }

        public async Task<CommonHttpResponse<IList<DriverViewModel>>> GetDriversAsync(
            GetDriverContract contract
        )
        {
            if (contract?.DriverIds == null)
                return CommonHttpHelper.BuildErrorResponse<IList<DriverViewModel>>(initialError: "Пустой запрос");

            try
            {
                var driver = await _driverStorage.GetDriversAsync(contract);
                if (driver == null)
                {
                    if (contract.ReturnEmptyResultIfNotFound)
                    {
                        return CommonHttpHelper.BuildSuccessResponse<IList<DriverViewModel>>(new List<DriverViewModel>(), HttpStatusCode.OK);
                    }

                    return CommonHttpHelper.BuildErrorResponse<IList<DriverViewModel>>(HttpStatusCode.NotFound, null, "Запись не найдена");
                }

                return CommonHttpHelper.BuildSuccessResponse(driver, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CommonHttpHelper.BuildErrorResponse<IList<DriverViewModel>>(
                    HttpStatusCode.InternalServerError,
                    ex.ToExceptionDetails(),
                    $"Ошибка выполнения метода {nameof(GetDriversAsync)}");
            }
        }

        public async Task<CommonHttpResponse<IList<DriverViewModel>>> GetFilteredDriversAsync(
            DriverFilterModel filter
        )
        {
            if (filter == null)
                return CommonHttpHelper.BuildErrorResponse<IList<DriverViewModel>>(initialError: "Пустой запрос");

            try
            {
                var result = filter.IsFilter ? await _driverStorage.GetFilteredDriversAsync(filter) :
                    await _driverStorage.GetFilteredDriversAsync(filter);

                return CommonHttpHelper.BuildSuccessResponse(result, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CommonHttpHelper.BuildErrorResponse<IList<DriverViewModel>>(
                    HttpStatusCode.InternalServerError,
                    ex.ToExceptionDetails(),
                    $"Ошибка выполнения метода {nameof(GetFilteredDriversAsync)}");
            }
        }

        public async Task<CommonHttpResponse> DeleteDriversAsync(DeleteDriverContract contract)
        {
            if (contract?.DriverIds == null)
                return CommonHttpHelper.BuildErrorResponse(initialError: "Пустой запрос");

            try
            {
                if (await _driverStorage.DeleteDriversAsync(contract))
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
                    $"Ошибка выполнения метода {nameof(DeleteDriversAsync)}");
            }
        }
    }
}
