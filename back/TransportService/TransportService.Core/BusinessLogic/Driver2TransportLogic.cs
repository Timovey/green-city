using AutoMapper;
using GreenCity.CommonUtils.CommonExtensions;
using GreenCity.Http;
using GreenCity.Http.Helpers;
using System.Net;
using TransportContracts.DataContracts.Driver2Transport;
using TransportContracts.DataContracts.Transport;
using TransportContracts.DataContracts.Driver;
using TransportContracts.Filters;
using TransportContracts.ViewModels;
using TransportService.Database.Interfaces;
using TransportService.Core.Helpers;

namespace TransportService.Core.BusinessLogic
{
    public class Driver2TransportLogic
    {
        private readonly IDriver2TransportStorage _driver2TransportStorage;
        private readonly IDriverStorage _driverStorage;
        private readonly ITransportStorage _transportStorage;
        private readonly IMapper _mapper;

        public Driver2TransportLogic(
            IDriver2TransportStorage driver2TransportStorage,
            IDriverStorage driverStorage,
            ITransportStorage transportStorage,
            IMapper mapper
        )
        {
            _driver2TransportStorage = driver2TransportStorage;
            _driverStorage = driverStorage;
            _transportStorage = transportStorage;
            _mapper = mapper;
        }

        public async Task<CommonHttpResponse<Driver2TransportViewModel>> CreateDriver2TransportAsync(CreateDriver2TransportContract contract)
        {
            if (contract == null)
                return CommonHttpHelper.BuildErrorResponse<Driver2TransportViewModel>(initialError: "Пустой запрос");

            try
            {
                var transport = await _transportStorage.GetTransportsAsync(new GetTransportContract() 
                    { TransportIds = new List<int> { contract.TransportId} }
                );
                var driver = await _driverStorage.GetDriversAsync(new GetDriverContract()
                    { DriverIds = new List<int> { contract.DriverId } }
                );
                //проверки на существование транспорта и водителя
                if (driver == null || transport == null || driver.Count == 0 || transport.Count == 0)
                {
                    return CommonHttpHelper.BuildErrorResponse<Driver2TransportViewModel>(initialError: "Нет такого водителя или транспорта");
                }
                //проверки на совместимость водителя и данного транспорта
                if(!driver.First().DrivingSkills.IsCompatible(transport.First().TransportType))
                {
                    return CommonHttpHelper.BuildErrorResponse<Driver2TransportViewModel>(initialError: "Данный водитель не может водить этот транспорт");
                }
                //нормализуем диапазон
                contract.DateRange.Normalize();
                //проверка на то, что дата начала раньше даты окончания
                if (contract.DateRange.EndDate.Value.Date < contract.DateRange.StartDate.Date)
                {
                    return CommonHttpHelper.BuildErrorResponse<Driver2TransportViewModel>(initialError:
                        "Дата окончания должна быть не меньше, чем дата начала");
                }

                var result = await _driver2TransportStorage.CreateDriver2TransportAsync(contract);

                return CommonHttpHelper.BuildSuccessResponse(result, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CommonHttpHelper.BuildErrorResponse<Driver2TransportViewModel>(
                    HttpStatusCode.InternalServerError,
                    ex.ToExceptionDetails(),
                    $"Ошибка выполнения метода {nameof(CreateDriver2TransportAsync)} ReqId : {contract.RequestId}");
            }
        }

        public async Task<CommonHttpResponse<Driver2TransportViewModel>> UpdateDriver2TransportAsync(UpdateDriver2TransportContract contract)
        {
            if (contract == null)
                return CommonHttpHelper.BuildErrorResponse<Driver2TransportViewModel>(initialError: "Пустой запрос");

            try
            {
                var transport = await _transportStorage.GetTransportsAsync(new GetTransportContract()
                { TransportIds = new List<int> { contract.TransportId } }
                 );
                var driver = await _driverStorage.GetDriversAsync(new GetDriverContract()
                { DriverIds = new List<int> { contract.DriverId } }
                );
                //проверки на существование транспорта и водителя
                if (driver == null || transport == null || driver.Count == 0 || transport.Count == 0)
                {
                    return CommonHttpHelper.BuildErrorResponse<Driver2TransportViewModel>(initialError: "Нет такого водителя или транспорта");
                }
                //проверки на совместимость водителя и данного транспорта
                if (!driver.First().DrivingSkills.IsCompatible(transport.First().TransportType))
                {
                    return CommonHttpHelper.BuildErrorResponse<Driver2TransportViewModel>(initialError: "Данный водитель не может водить этот транспорт");
                }
                //нормализуем диапазон
                contract.DateRange.Normalize();
                //проверка на то, что дата начала раньше даты окончания
                if (contract.DateRange.EndDate.Value.Date < contract.DateRange.StartDate.Date)
                {
                    return CommonHttpHelper.BuildErrorResponse<Driver2TransportViewModel>(initialError:
                        "Дата окончания должна быть не меньше, чем дата начала");
                }

                var result = await _driver2TransportStorage.UpdateDriver2TransportAsync(contract);
                return CommonHttpHelper.BuildSuccessResponse(result, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CommonHttpHelper.BuildErrorResponse<Driver2TransportViewModel>(
                    HttpStatusCode.InternalServerError,
                    ex.ToExceptionDetails(),
                    $"Ошибка выполнения метода {nameof(UpdateDriver2TransportAsync)}");
            }
        }

        public async Task<CommonHttpResponse<IList<Driver2TransportViewModel>>> GetDrivers2TransportAsync(
            GetDriver2TransportContract contract
        )
        {
            if (contract?.Driver2TransportIds == null)
                return CommonHttpHelper.BuildErrorResponse<IList<Driver2TransportViewModel>>(initialError: "Пустой запрос");

            try
            {
                var driver2Transport = await _driver2TransportStorage.GetDrivers2TransportAsync(contract);
                if (driver2Transport == null)
                    return CommonHttpHelper.BuildErrorResponse<IList<Driver2TransportViewModel>>(HttpStatusCode.NotFound, null, "Запись не найдена");

                return CommonHttpHelper.BuildSuccessResponse(driver2Transport, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CommonHttpHelper.BuildErrorResponse< IList<Driver2TransportViewModel>>(
                    HttpStatusCode.InternalServerError,
                    ex.ToExceptionDetails(),
                    $"Ошибка выполнения метода {nameof(GetDrivers2TransportAsync)}");
            }
        }

        public async Task<CommonHttpResponse<IList<Driver2TransportViewModel>>> GetFilteredDrivers2TransportAsync(
            Driver2TransportFilterModel filter
        )
        {
            if (filter == null)
                return CommonHttpHelper.BuildErrorResponse<IList<Driver2TransportViewModel>>(initialError: "Пустой запрос");

            try
            {
                var result = filter.IsFilter ? await _driver2TransportStorage.GetFilteredDrivers2TransportAsync(filter) :
                    await _driver2TransportStorage.GetFilteredDrivers2TransportAsync(filter);

                return CommonHttpHelper.BuildSuccessResponse(result, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CommonHttpHelper.BuildErrorResponse<IList<Driver2TransportViewModel>>(
                    HttpStatusCode.InternalServerError,
                    ex.ToExceptionDetails(),
                    $"Ошибка выполнения метода {nameof(GetFilteredDrivers2TransportAsync)}");
            }
        }

        public async Task<CommonHttpResponse> DeleteDriver2TransportAsync(DeleteDriver2TransportContract contract)
        {
            if (contract?.Driver2TransportIds == null)
                return CommonHttpHelper.BuildErrorResponse(initialError: "Пустой запрос");

            try
            {
                if (await _driver2TransportStorage.DeleteDrivers2TransportAsync(contract))
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
                    $"Ошибка выполнения метода {nameof(DeleteDriver2TransportAsync)}");
            }
        }
    }
}
