using AutoMapper;
using GreenCity.CommonUtils.CommonExtensions;
using GreenCity.Http;
using GreenCity.Http.Helpers;
using System.Net;
using TransportContracts.DataContracts.Driver;
using TransportContracts.DataContracts.DriverVacation;
using TransportContracts.Filters;
using TransportContracts.SortModels;
using TransportContracts.ViewModels;
using TransportService.Core.Helpers;
using TransportService.Database.Interfaces;

namespace TransportService.Core.BusinessLogic
{
    public class DriverVacationLogic
    {
        private readonly IDriverVacationStorage _driverVacationStorage;
        private readonly IDriverStorage _driverStorage;
        private readonly IMapper _mapper;

        public DriverVacationLogic(
            IDriverVacationStorage driverVacationStorage,
            IDriverStorage driverStorage,
            IMapper mapper
        )
        {
            _driverVacationStorage = driverVacationStorage;
            _driverStorage = driverStorage;
            _mapper = mapper;
        }

        public async Task<CommonHttpResponse<DriverVacationViewModel>> CreateDriverVacationAsync(CreateDriverVacationContract contract)
        {
            if (contract == null)
                return CommonHttpHelper.BuildErrorResponse<DriverVacationViewModel>(initialError: "Пустой запрос");


            var originalDriver = await _driverStorage.GetDriversAsync(new GetDriverContract()
            { 
                DriverIds = new List<int> { contract.DriverId } 
            });
            //проверка на существование  водителя
            if (originalDriver == null || originalDriver.Count == 0)
            {
                return CommonHttpHelper.BuildErrorResponse<DriverVacationViewModel>(initialError: "Нет такого водителя ");
            }
            //нормализуем диапазон
            contract.DateRange.Normalize();

            //проверка на то, что дата начала раньше даты окончания
            if (contract.DateRange.EndDate.Value.Date < contract.DateRange.StartDate.Date)
            {
                return CommonHttpHelper.BuildErrorResponse<DriverVacationViewModel>(initialError:
                    "Дата окончания должна быть не меньше, чем дата начала");
            }
            if (contract.ReplacementDriverId.HasValue)
            {
                //проверка на то, что сменный водитель не числется на другой машине в этот период
                //!ВОЗМОЖНО следует сделать проверку на то,
                //что водитель не работает на другой машине как основной водитель
                var replaceDriverVacation = await _driverVacationStorage
                    .GetFilteredDriverVacationsAsync(
                        new DriverVacationFilterModel()
                        {
                            ReplacementDriverIds = new[] { contract.ReplacementDriverId.Value },
                            StartDateTo = contract.DateRange.EndDate,
                            EndDateFrom = contract.DateRange.StartDate,
                            Sorting = new DriverVacationSortModel(),
                        }); 
                if(replaceDriverVacation.Count() > 0)
                {
                    return CommonHttpHelper.BuildErrorResponse<DriverVacationViewModel>(initialError: "Такой водитель уже числется как сменный в данный период");
                }

                var replaceDriver = await _driverStorage.GetDriversAsync(new GetDriverContract()
                {
                    DriverIds = new List<int> { contract.ReplacementDriverId.Value }
                });
                //проверка на существование  водителя
                if (replaceDriver == null || replaceDriver.Count == 0)
                {
                    return CommonHttpHelper.BuildErrorResponse<DriverVacationViewModel>(initialError: "Нет такого сменного водителя ");
                }
                //проверка на соответствие прав водителей
                if (!DrivingSkillsHelper.IsCanDrive(originalDriverDrivingSkills: originalDriver.First().DrivingSkills,
                    replaceDriverDrivingSkills: replaceDriver.First().DrivingSkills))
                {
                    return CommonHttpHelper.BuildErrorResponse<DriverVacationViewModel>(initialError: "Сменный водитель не имеет такую же квалификацию, как основной");
                }
            }
            try
            {
                var result = await _driverVacationStorage.CreateDriverVacationAsync(contract);

                return CommonHttpHelper.BuildSuccessResponse(result, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CommonHttpHelper.BuildErrorResponse<DriverVacationViewModel>(
                    HttpStatusCode.InternalServerError,
                    ex.ToExceptionDetails(),
                    $"Ошибка выполнения метода {nameof(CreateDriverVacationAsync)} ReqId : {contract.RequestId}");
            }
        }

        public async Task<CommonHttpResponse<DriverVacationViewModel>> UpdateDriverVacationsAsync(UpdateDriverVacationContract contract)
        {
            if (contract == null)
                return CommonHttpHelper.BuildErrorResponse<DriverVacationViewModel>(initialError: "Пустой запрос");


            var originalDriver = await _driverStorage.GetDriversAsync(new GetDriverContract()
            {
                DriverIds = new List<int> { contract.DriverId }
            });
            //проверка на существование  водителя
            if (originalDriver == null || originalDriver.Count == 0)
            {
                return CommonHttpHelper.BuildErrorResponse<DriverVacationViewModel>(initialError: "Нет такого водителя ");
            }
            //нормализуем диапазон
            contract.DateRange.Normalize();

            //проверка на то, что дата начала раньше даты окончания
            if (contract.DateRange.EndDate.Value.Date < contract.DateRange.StartDate.Date)
            {
                return CommonHttpHelper.BuildErrorResponse<DriverVacationViewModel>(initialError:
                    "Дата окончания должна быть не меньше, чем дата начала");
            }
            if (contract.ReplacementDriverId.HasValue)
            {
                //проверка на то, что сменный водитель не числется на другой машине в этот период
                //!ВОЗМОЖНО следует сделать проверку на то, что водитель не работает на другой машине как основной водитель
                var replaceDriverVacation = await _driverVacationStorage
                    .GetFilteredDriverVacationsAsync(
                        new DriverVacationFilterModel()
                        {
                            ReplacementDriverIds = new[] { contract.ReplacementDriverId.Value },
                            StartDateTo = contract.DateRange.EndDate,
                            EndDateFrom = contract.DateRange.StartDate,
                            Sorting = new DriverVacationSortModel()
                        });
                if (replaceDriverVacation.Count() > 0)
                {
                    return CommonHttpHelper.BuildErrorResponse<DriverVacationViewModel>(initialError: "Такой водитель уже числется как сменный в данный период");
                }

                var replaceDriver = await _driverStorage.GetDriversAsync(new GetDriverContract()
                {
                    DriverIds = new List<int> { contract.ReplacementDriverId.Value }
                });
                //проверка на существование  водителя
                if (replaceDriver == null || replaceDriver.Count == 0)
                {
                    return CommonHttpHelper.BuildErrorResponse<DriverVacationViewModel>(initialError: "Нет такого сменного водителя ");
                }
                //проверка на соответствие прав водителей
                if (!DrivingSkillsHelper.IsCanDrive(originalDriverDrivingSkills: originalDriver.First().DrivingSkills,
                    replaceDriverDrivingSkills: replaceDriver.First().DrivingSkills))
                {
                    return CommonHttpHelper.BuildErrorResponse<DriverVacationViewModel>(initialError: "Сменный водитель не имеет такую же квалификацию, как основной");
                }
            }
            try
            {
                var result = await _driverVacationStorage.UpdateDriverVacationsAsync(contract);
                return CommonHttpHelper.BuildSuccessResponse(result, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CommonHttpHelper.BuildErrorResponse<DriverVacationViewModel>(
                    HttpStatusCode.InternalServerError,
                    ex.ToExceptionDetails(),
                    $"Ошибка выполнения метода {nameof(UpdateDriverVacationsAsync)}");
            }
        }

        public async Task<CommonHttpResponse<IList<DriverVacationViewModel>>> GetDriverVacationsAsync(
            GetDriverVacationContract contract
        )
        {
            if (contract?.DriverVacationIds == null)
                return CommonHttpHelper.BuildErrorResponse<IList<DriverVacationViewModel>>(initialError: "Пустой запрос");

            try
            {
                var driver = await _driverVacationStorage.GetDriverVacationsAsync(contract);
                if (driver == null)
                    return CommonHttpHelper.BuildErrorResponse<IList<DriverVacationViewModel>>(HttpStatusCode.NotFound, null, "Запись не найдена");

                return CommonHttpHelper.BuildSuccessResponse(driver, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CommonHttpHelper.BuildErrorResponse<IList<DriverVacationViewModel>>(
                    HttpStatusCode.InternalServerError,
                    ex.ToExceptionDetails(),
                    $"Ошибка выполнения метода {nameof(GetDriverVacationsAsync)}");
            }
        }

        public async Task<CommonHttpResponse<IList<DriverVacationViewModel>>> GetFilteredDriverVacationsAsync(
            DriverVacationFilterModel filter
        )
        {
            if (filter == null)
                return CommonHttpHelper.BuildErrorResponse<IList<DriverVacationViewModel>>(initialError: "Пустой запрос");

            try
            {
                var result = filter.IsFilter ? await _driverVacationStorage.GetFilteredDriverVacationsAsync(filter) :
                    await _driverVacationStorage.GetFilteredDriverVacationsAsync(filter);

                return CommonHttpHelper.BuildSuccessResponse(result, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CommonHttpHelper.BuildErrorResponse<IList<DriverVacationViewModel>>(
                    HttpStatusCode.InternalServerError,
                    ex.ToExceptionDetails(),
                    $"Ошибка выполнения метода {nameof(GetFilteredDriverVacationsAsync)}");
            }
        }

        public async Task<CommonHttpResponse> DeleteDriverVacationsAsync(DeleteDriverVacationContract contract)
        {
            if (contract?.DriverVacationIds == null)
                return CommonHttpHelper.BuildErrorResponse(initialError: "Пустой запрос");

            try
            {
                if (await _driverVacationStorage.DeleteDriverVacationsAsync(contract))
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
                    $"Ошибка выполнения метода {nameof(DeleteDriverVacationsAsync)}");
            }
        }
    }
}
