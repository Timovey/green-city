using AutoMapper;
using ExternalApi.Interface.Models.Transport.Driver;
using ExternalApi.Interface.Models.Transport.Transport;
using ExternalApi.Interface.Models.Transport.Driver2Transport;
using ExternalApi.Interface.Models.Transport.DriverVacation;
using ExternalApi.Interface.Models.Transport.RepairCard;
using ExternalApi.Interface.Models.Transport.TransportShedule;
using ExternalApi.Interface.Models.Transport.Rent;
using ExternalApi.Interface.Models.Transport.TransportDutyShift;
using ExternalApiService.Operations.Interface;
using GreenCity.Http;
using GreenCity.Http.Helpers;
using System.Linq;
using System.Threading.Tasks;
using TransportContracts.DataContracts.Driver;
using TransportContracts.DataContracts.Transport;
using TransportContracts.DataContracts.Driver2Transport;
using TransportContracts.DataContracts.DriverVacation;
using TransportContracts.DataContracts.TransportShedule;
using TransportContracts.DataContracts.TransportDutyShift;
using TransportContracts.Filters;
using TransportContracts.Interfaces;
using TransportContracts.DataContracts.RepairCard;
using ExternalApi.Interface.Models.Transport.TransportWorkShift;
using TransportContracts.DataContracts.TransportWorkShift;
using TransportContracts.DataContracts.Rent;

namespace ExternalApiService.Operations
{
    public class TransportOperations : ITransportOperations
    {
        private readonly ITransportApi _transportApi;
        private readonly IDriverApi _driverApi;
        private readonly IMapper _mapper;

        public TransportOperations(
            ITransportApi transportApi,
            IDriverApi driverApi,
            IMapper mapper
        )
        {
            _transportApi = transportApi;
            _driverApi = driverApi;
            _mapper = mapper;
        }

        #region Transport

        public async Task<CommonHttpResponse<ExtTransportViewModel[]>> GetFilteredTransportsAsync(ExtTransportFilterRequest filter)
        {
            if (filter == null)
                return CommonHttpHelper.BuildErrorResponse<ExtTransportViewModel[]>(initialError: "Пустой запрос");

            var transportFilter = _mapper.Map<TransportFilterModel>(filter);
            var result = await _transportApi.GetFilteredTransportAsync(transportFilter);

            if (result.Success)
            {
                return CommonHttpHelper.BuildSuccessResponse(
                    result.Data.Select(_mapper.Map<ExtTransportViewModel>).ToArray(),
                    System.Net.HttpStatusCode.OK);
            }

            return CommonHttpHelper.BuildErrorResponse<ExtTransportViewModel[]>(result.StatusCode, result.Errors.ToList());
        }

        public async Task<CommonHttpResponse<ExtTransportViewModel[]>> GetTransportsAsync(ExtGetTransportRequest request)
        {
            if (request == null)
                return CommonHttpHelper.BuildErrorResponse<ExtTransportViewModel[]>(initialError: "Пустой запрос");

            var getTransport = _mapper.Map<GetTransportContract>(request);
            var result = await _transportApi.GetTransportAsync(getTransport);

            if (result.Success)
            {
                return CommonHttpHelper.BuildSuccessResponse(
                    result.Data.Select(_mapper.Map<ExtTransportViewModel>).ToArray(),
                    System.Net.HttpStatusCode.OK);
            }

            return CommonHttpHelper.BuildErrorResponse<ExtTransportViewModel[]>(result.StatusCode, result.Errors.ToList());
        }

        public async Task<CommonHttpResponse<ExtTransportViewModel>> CreateTransportAsync(ExtCreateTransportRequest request)
        {
            if (request == null)
                return CommonHttpHelper.BuildErrorResponse<ExtTransportViewModel>(initialError: "Пустой запрос");

            var createTransport = _mapper.Map<CreateTransportContract>(request);
            var result = await _transportApi.CreateTransportAsync(createTransport);

            if (result.Success)
            {
                return CommonHttpHelper.BuildSuccessResponse(
                    _mapper.Map<ExtTransportViewModel>(result.Data),
                    System.Net.HttpStatusCode.OK);
            }

            return CommonHttpHelper.BuildErrorResponse<ExtTransportViewModel>(result.StatusCode, result.Errors.ToList());
        }

        public async Task<CommonHttpResponse<ExtTransportViewModel>> UpdateTransportsAsync(ExtUpdateTransportRequest request)
        {
            if (request == null)
                return CommonHttpHelper.BuildErrorResponse<ExtTransportViewModel>(initialError: "Пустой запрос");

            var updateTransport = _mapper.Map<UpdateTransportContract>(request);
            var result = await _transportApi.UpdateTransportAsync(updateTransport);

            if (result.Success)
            {
                return CommonHttpHelper.BuildSuccessResponse(
                    _mapper.Map<ExtTransportViewModel>(result.Data),
                    System.Net.HttpStatusCode.OK);
            }

            return CommonHttpHelper.BuildErrorResponse<ExtTransportViewModel>(result.StatusCode, result.Errors.ToList());
        }

        public async Task<CommonHttpResponse> DeleteTransportsAsync(ExtDeleteTransportRequest request)
        {
            if (request == null)
                return CommonHttpHelper.BuildErrorResponse(initialError: "Пустой запрос");

            var deleteTransport = _mapper.Map<DeleteTransportContract>(request);
            var result = await _transportApi.DeleteTransportAsync(deleteTransport);

            if (result.Success)
            {
                return CommonHttpHelper.BuildSuccessResponse();
            }

            return CommonHttpHelper.BuildErrorResponse(result.StatusCode, result.Errors.ToList());
        }

        #endregion

        #region Driver

        public async Task<CommonHttpResponse<ExtDriverViewModel[]>> GetFilteredDriversAsync(ExtDriverFilterRequest filter)
        {
            if (filter == null)
                return CommonHttpHelper.BuildErrorResponse<ExtDriverViewModel[]>(initialError: "Пустой запрос");

            var transportFilter = _mapper.Map<DriverFilterModel>(filter);
            var result = await _driverApi.GetFilteredDriverAsync(transportFilter);

            if (result.Success)
            {
                return CommonHttpHelper.BuildSuccessResponse(
                    result.Data.Select(_mapper.Map<ExtDriverViewModel>).ToArray(),
                    System.Net.HttpStatusCode.OK);
            }

            return CommonHttpHelper.BuildErrorResponse<ExtDriverViewModel[]>(result.StatusCode, result.Errors.ToList());
        }

        public async Task<CommonHttpResponse<ExtDriverViewModel[]>> GetDriversAsync(ExtGetDriverRequest request)
        {
            if (request == null)
                return CommonHttpHelper.BuildErrorResponse<ExtDriverViewModel[]>(initialError: "Пустой запрос");

            var getDriver = _mapper.Map<GetDriverContract>(request);
            var result = await _driverApi.GetDriverAsync(getDriver);

            if (result.Success)
            {
                return CommonHttpHelper.BuildSuccessResponse(
                    result.Data.Select(_mapper.Map<ExtDriverViewModel>).ToArray(),
                    System.Net.HttpStatusCode.OK);
            }

            return CommonHttpHelper.BuildErrorResponse<ExtDriverViewModel[]>(result.StatusCode, result.Errors.ToList());
        }

        public async Task<CommonHttpResponse<ExtDriverViewModel>> CreateDriverAsync(ExtCreateDriverRequest request)
        {
            if (request == null)
                return CommonHttpHelper.BuildErrorResponse<ExtDriverViewModel>(initialError: "Пустой запрос");

            var createDriver = _mapper.Map<CreateDriverContract>(request);
            var result = await _driverApi.CreateDriverAsync(createDriver);

            if (result.Success)
            {
                return CommonHttpHelper.BuildSuccessResponse(
                     _mapper.Map<ExtDriverViewModel>(result.Data),
                     System.Net.HttpStatusCode.OK);
            }

            return CommonHttpHelper.BuildErrorResponse<ExtDriverViewModel>(result.StatusCode, result.Errors.ToList());
        }

        public async Task<CommonHttpResponse<ExtDriverViewModel>> UpdateDriversAsync(ExtUpdateDriverRequest request)
        {
            if (request == null)
                return CommonHttpHelper.BuildErrorResponse<ExtDriverViewModel>(initialError: "Пустой запрос");

            var updateDriver = _mapper.Map<UpdateDriverContract>(request);
            var result = await _driverApi.UpdateDriverAsync(updateDriver);

            if (result.Success)
            {
                return CommonHttpHelper.BuildSuccessResponse(
                    _mapper.Map<ExtDriverViewModel>(result.Data),
                    System.Net.HttpStatusCode.OK);
            }

            return CommonHttpHelper.BuildErrorResponse<ExtDriverViewModel>(result.StatusCode, result.Errors.ToList());
        }

        public async Task<CommonHttpResponse> DeleteDriversAsync(ExtDeleteDriverRequest request)
        {
            if (request == null)
                return CommonHttpHelper.BuildErrorResponse(initialError: "Пустой запрос");

            var deleteDriver = _mapper.Map<DeleteDriverContract>(request);
            var result = await _driverApi.DeleteDriverAsync(deleteDriver);

            if (result.Success)
            {
                return CommonHttpHelper.BuildSuccessResponse();
            }

            return CommonHttpHelper.BuildErrorResponse(result.StatusCode, result.Errors.ToList());
        }

        #endregion

        #region Driver2Transport

        public async Task<CommonHttpResponse<ExtDriver2TransportViewModel[]>> GetFilteredDrivers2TransportAsync(ExtDriver2TransportFilterRequest filter)
        {
            if (filter == null)
                return CommonHttpHelper.BuildErrorResponse<ExtDriver2TransportViewModel[]>(initialError: "Пустой запрос");

            var driver2TransportFilter = _mapper.Map<Driver2TransportFilterModel>(filter);
            var result = await _transportApi.GetFilteredDrivers2TransportAsync(driver2TransportFilter);

            if (result.Success)
            {
                return CommonHttpHelper.BuildSuccessResponse(
                    result.Data.Select(_mapper.Map<ExtDriver2TransportViewModel>).ToArray(),
                    System.Net.HttpStatusCode.OK);
            }

            return CommonHttpHelper.BuildErrorResponse<ExtDriver2TransportViewModel[]>(result.StatusCode, result.Errors.ToList());
        }

        public async Task<CommonHttpResponse<ExtDriver2TransportViewModel[]>> GetDrivers2TransportAsync(ExtGetDriver2TransportRequest request)
        {
            if (request == null)
                return CommonHttpHelper.BuildErrorResponse<ExtDriver2TransportViewModel[]>(initialError: "Пустой запрос");

            var getDriver2Transport = _mapper.Map<GetDriver2TransportContract>(request);
            var result = await _transportApi.GetDrivers2TransportAsync(getDriver2Transport);

            if (result.Success)
            {
                return CommonHttpHelper.BuildSuccessResponse(
                    result.Data.Select(_mapper.Map<ExtDriver2TransportViewModel>).ToArray(),
                    System.Net.HttpStatusCode.OK);
            }

            return CommonHttpHelper.BuildErrorResponse<ExtDriver2TransportViewModel[]>(result.StatusCode, result.Errors.ToList());
        }

        public async Task<CommonHttpResponse<ExtDriver2TransportViewModel>> CreateDriver2TransportAsync(ExtCreateDriver2TransportRequest request)
        {
            if (request == null)
                return CommonHttpHelper.BuildErrorResponse<ExtDriver2TransportViewModel>(initialError: "Пустой запрос");

            var createDriver2Transport = _mapper.Map<CreateDriver2TransportContract>(request);
            var result = await _transportApi.CreateDriver2TransportAsync(createDriver2Transport);

            if (result.Success)
            {
                return CommonHttpHelper.BuildSuccessResponse(
                    _mapper.Map<ExtDriver2TransportViewModel>(result.Data),
                    System.Net.HttpStatusCode.OK);
            }

            return CommonHttpHelper.BuildErrorResponse<ExtDriver2TransportViewModel>(result.StatusCode, result.Errors.ToList());
        }

        public async Task<CommonHttpResponse<ExtDriver2TransportViewModel>> UpdateDriver2TransportAsync(ExtUpdateDriver2TransportRequest request)
        {
            if (request == null)
                return CommonHttpHelper.BuildErrorResponse<ExtDriver2TransportViewModel>(initialError: "Пустой запрос");

            var updateDriver2Transport = _mapper.Map<UpdateDriver2TransportContract>(request);
            var result = await _transportApi.UpdateDriver2TransportAsync(updateDriver2Transport);

            if (result.Success)
            {
                return CommonHttpHelper.BuildSuccessResponse(
                   _mapper.Map<ExtDriver2TransportViewModel>(result.Data),
                   System.Net.HttpStatusCode.OK);
            }

            return CommonHttpHelper.BuildErrorResponse<ExtDriver2TransportViewModel>(result.StatusCode, result.Errors.ToList());
        }

        public async Task<CommonHttpResponse> DeleteDrivers2TransportAsync(ExtDeleteDriver2TransportRequest request)
        {
            if (request == null)
                return CommonHttpHelper.BuildErrorResponse(initialError: "Пустой запрос");

            var deleteDriver2Transport = _mapper.Map<DeleteDriver2TransportContract>(request);
            var result = await _transportApi.DeleteDriver2TransportAsync(deleteDriver2Transport);

            if (result.Success)
            {
                return CommonHttpHelper.BuildSuccessResponse();
            }

            return CommonHttpHelper.BuildErrorResponse(result.StatusCode, result.Errors.ToList());
        }

        #endregion

        #region DriverVacation

        public async Task<CommonHttpResponse<ExtDriverVacationViewModel[]>> GetFilteredDriverVacationsAsync(ExtDriverVacationFilterRequest filter)
        {
            if (filter == null)
                return CommonHttpHelper.BuildErrorResponse<ExtDriverVacationViewModel[]>(initialError: "Пустой запрос");

            var transportFilter = _mapper.Map<DriverVacationFilterModel>(filter);
            var result = await _driverApi.GetFilteredDriverVacationAsync(transportFilter);

            if (result.Success)
            {
                return CommonHttpHelper.BuildSuccessResponse(
                    result.Data.Select(_mapper.Map<ExtDriverVacationViewModel>).ToArray(),
                    System.Net.HttpStatusCode.OK);
            }

            return CommonHttpHelper.BuildErrorResponse<ExtDriverVacationViewModel[]>(result.StatusCode, result.Errors.ToList());
        }

        public async Task<CommonHttpResponse<ExtDriverVacationViewModel[]>> GetDriverVacationsAsync(ExtGetDriverVacationRequest request)
        {
            if (request == null)
                return CommonHttpHelper.BuildErrorResponse<ExtDriverVacationViewModel[]>(initialError: "Пустой запрос");

            var getDriverVacation = _mapper.Map<GetDriverVacationContract>(request);
            var result = await _driverApi.GetDriverVacationAsync(getDriverVacation);

            if (result.Success)
            {
                return CommonHttpHelper.BuildSuccessResponse(
                    result.Data.Select(_mapper.Map<ExtDriverVacationViewModel>).ToArray(),
                    System.Net.HttpStatusCode.OK);
            }

            return CommonHttpHelper.BuildErrorResponse<ExtDriverVacationViewModel[]>(result.StatusCode, result.Errors.ToList());
        }

        public async Task<CommonHttpResponse<ExtDriverVacationViewModel>> CreateDriverVacationAsync(ExtCreateDriverVacationRequest request)
        {
            if (request == null)
                return CommonHttpHelper.BuildErrorResponse<ExtDriverVacationViewModel>(initialError: "Пустой запрос");

            var createDriverVacation = _mapper.Map<CreateDriverVacationContract>(request);
            var result = await _driverApi.CreateDriverVacationAsync(createDriverVacation);

            if (result.Success)
            {
                return CommonHttpHelper.BuildSuccessResponse(
                  _mapper.Map<ExtDriverVacationViewModel>(result.Data),
                  System.Net.HttpStatusCode.OK);
            }

            return CommonHttpHelper.BuildErrorResponse<ExtDriverVacationViewModel>(result.StatusCode, result.Errors.ToList());
        }

        public async Task<CommonHttpResponse<ExtDriverVacationViewModel>> UpdateDriverVacationsAsync(ExtUpdateDriverVacationRequest request)
        {
            if (request == null)
                return CommonHttpHelper.BuildErrorResponse<ExtDriverVacationViewModel>(initialError: "Пустой запрос");

            var updateDriverVacation = _mapper.Map<UpdateDriverVacationContract>(request);
            var result = await _driverApi.UpdateDriverVacationAsync(updateDriverVacation);

            if (result.Success)
            {
                return CommonHttpHelper.BuildSuccessResponse(
                    _mapper.Map<ExtDriverVacationViewModel>(result.Data),
                    System.Net.HttpStatusCode.OK);
            }

            return CommonHttpHelper.BuildErrorResponse<ExtDriverVacationViewModel>(result.StatusCode, result.Errors.ToList());
        }

        public async Task<CommonHttpResponse> DeleteDriverVacationsAsync(ExtDeleteDriverVacationRequest request)
        {
            if (request == null)
                return CommonHttpHelper.BuildErrorResponse(initialError: "Пустой запрос");

            var deleteDriverVacation = _mapper.Map<DeleteDriverVacationContract>(request);
            var result = await _driverApi.DeleteDriverVacationAsync(deleteDriverVacation);

            if (result.Success)
            {
                return CommonHttpHelper.BuildSuccessResponse();
            }

            return CommonHttpHelper.BuildErrorResponse(result.StatusCode, result.Errors.ToList());
        }
        #endregion

        #region RepairCard

        public async Task<CommonHttpResponse<ExtRepairCardViewModel[]>> GetFilteredRepairCardsAsync(ExtRepairCardFilterRequest filter)
        {
            if (filter == null)
                return CommonHttpHelper.BuildErrorResponse<ExtRepairCardViewModel[]>(initialError: "Пустой запрос");

            var repairCardFilter = _mapper.Map<RepairCardFilterModel>(filter);
            var result = await _transportApi.GetFilteredRepairCardAsync(repairCardFilter);

            if (result.Success)
            {
                return CommonHttpHelper.BuildSuccessResponse(
                    result.Data.Select(_mapper.Map<ExtRepairCardViewModel>).ToArray(),
                    System.Net.HttpStatusCode.OK);
            }

            return CommonHttpHelper.BuildErrorResponse<ExtRepairCardViewModel[]>(result.StatusCode, result.Errors.ToList());
        }

        public async Task<CommonHttpResponse<ExtRepairCardViewModel[]>> GetRepairCardsAsync(ExtGetRepairCardRequest request)
        {
            if (request == null)
                return CommonHttpHelper.BuildErrorResponse<ExtRepairCardViewModel[]>(initialError: "Пустой запрос");

            var getRepairCard = _mapper.Map<GetRepairCardContract>(request);
            var result = await _transportApi.GetRepairCardAsync(getRepairCard);

            if (result.Success)
            {
                return CommonHttpHelper.BuildSuccessResponse(
                    result.Data.Select(_mapper.Map<ExtRepairCardViewModel>).ToArray(),
                    System.Net.HttpStatusCode.OK);
            }

            return CommonHttpHelper.BuildErrorResponse<ExtRepairCardViewModel[]>(result.StatusCode, result.Errors.ToList());
        }

        public async Task<CommonHttpResponse<ExtRepairCardViewModel>> CreateRepairCardAsync(ExtCreateRepairCardRequest request)
        {
            if (request == null)
                return CommonHttpHelper.BuildErrorResponse<ExtRepairCardViewModel>(initialError: "Пустой запрос");

            var createRepairCard = _mapper.Map<CreateRepairCardContract>(request);
            var result = await _transportApi.CreateRepairCardAsync(createRepairCard);

            if (result.Success)
            {
                return CommonHttpHelper.BuildSuccessResponse(
                  _mapper.Map<ExtRepairCardViewModel>(result.Data),
                  System.Net.HttpStatusCode.OK);
            }

            return CommonHttpHelper.BuildErrorResponse<ExtRepairCardViewModel>(result.StatusCode, result.Errors.ToList());
        }

        public async Task<CommonHttpResponse<ExtRepairCardViewModel>> UpdateRepairCardsAsync(ExtUpdateRepairCardRequest request)
        {
            if (request == null)
                return CommonHttpHelper.BuildErrorResponse<ExtRepairCardViewModel>(initialError: "Пустой запрос");

            var updateRepairCard = _mapper.Map<UpdateRepairCardContract>(request);
            var result = await _transportApi.UpdateRepairCardAsync(updateRepairCard);

            if (result.Success)
            {
                return CommonHttpHelper.BuildSuccessResponse(
                  _mapper.Map<ExtRepairCardViewModel>(result.Data),
                  System.Net.HttpStatusCode.OK);
            }

            return CommonHttpHelper.BuildErrorResponse<ExtRepairCardViewModel>(result.StatusCode, result.Errors.ToList());
        }

        public async Task<CommonHttpResponse> DeleteRepairCardsAsync(ExtDeleteRepairCardRequest request)
        {
            if (request == null)
                return CommonHttpHelper.BuildErrorResponse(initialError: "Пустой запрос");

            var deleteRepairCard = _mapper.Map<DeleteRepairCardContract>(request);
            var result = await _transportApi.DeleteRepairCardAsync(deleteRepairCard);

            if (result.Success)
            {
                return CommonHttpHelper.BuildSuccessResponse();
            }

            return CommonHttpHelper.BuildErrorResponse(result.StatusCode, result.Errors.ToList());
        }

        #endregion

        #region TransportShedule

        public async Task<CommonHttpResponse<ExtTransportSheduleViewModel[]>> GetFilteredTransportShedulesAsync(ExtTransportSheduleFilterRequest filter)
        {
            if (filter == null)
                return CommonHttpHelper.BuildErrorResponse<ExtTransportSheduleViewModel[]>(initialError: "Пустой запрос");

            var repairCardFilter = _mapper.Map<TransportSheduleFilterModel>(filter);
            var result = await _transportApi.GetFilteredTransportSheduleAsync(repairCardFilter);

            if (result.Success)
            {
                return CommonHttpHelper.BuildSuccessResponse(
                    result.Data.Select(_mapper.Map<ExtTransportSheduleViewModel>).ToArray(),
                    System.Net.HttpStatusCode.OK);
            }

            return CommonHttpHelper.BuildErrorResponse<ExtTransportSheduleViewModel[]>(result.StatusCode, result.Errors.ToList());
        }

        public async Task<CommonHttpResponse<ExtTransportSheduleViewModel[]>> GetTransportShedulesAsync(ExtGetTransportSheduleRequest request)
        {
            if (request == null)
                return CommonHttpHelper.BuildErrorResponse<ExtTransportSheduleViewModel[]>(initialError: "Пустой запрос");

            var getTransportShedule = _mapper.Map<GetTransportSheduleContract>(request);
            var result = await _transportApi.GetTransportSheduleAsync(getTransportShedule);

            if (result.Success)
            {
                return CommonHttpHelper.BuildSuccessResponse(
                    result.Data.Select(_mapper.Map<ExtTransportSheduleViewModel>).ToArray(),
                    System.Net.HttpStatusCode.OK);
            }

            return CommonHttpHelper.BuildErrorResponse<ExtTransportSheduleViewModel[]>(result.StatusCode, result.Errors.ToList());
        }

        public async Task<CommonHttpResponse<ExtTransportSheduleViewModel>> CreateTransportSheduleAsync(ExtCreateTransportSheduleRequest request)
        {
            if (request == null)
                return CommonHttpHelper.BuildErrorResponse<ExtTransportSheduleViewModel>(initialError: "Пустой запрос");

            var createTransportShedule = _mapper.Map<CreateTransportSheduleContract>(request);
            var result = await _transportApi.CreateTransportSheduleAsync(createTransportShedule);

            if (result.Success)
            {
                return CommonHttpHelper.BuildSuccessResponse(
                    _mapper.Map<ExtTransportSheduleViewModel>(result.Data),
                    System.Net.HttpStatusCode.OK);
            }

            return CommonHttpHelper.BuildErrorResponse<ExtTransportSheduleViewModel>(result.StatusCode, result.Errors.ToList());
        }

        public async Task<CommonHttpResponse<ExtTransportSheduleViewModel>> UpdateTransportShedulesAsync(ExtUpdateTransportSheduleRequest request)
        {
            if (request == null)
                return CommonHttpHelper.BuildErrorResponse<ExtTransportSheduleViewModel>(initialError: "Пустой запрос");

            var updateTransportShedule = _mapper.Map<UpdateTransportSheduleContract>(request);
            var result = await _transportApi.UpdateTransportSheduleAsync(updateTransportShedule);

            if (result.Success)
            {
                return CommonHttpHelper.BuildSuccessResponse(
                    _mapper.Map<ExtTransportSheduleViewModel>(result.Data),
                    System.Net.HttpStatusCode.OK);
            }

            return CommonHttpHelper.BuildErrorResponse<ExtTransportSheduleViewModel>(result.StatusCode, result.Errors.ToList());
        }

        public async Task<CommonHttpResponse> DeleteTransportShedulesAsync(ExtDeleteTransportSheduleRequest request)
        {
            if (request == null)
                return CommonHttpHelper.BuildErrorResponse(initialError: "Пустой запрос");

            var deleteTransportShedule = _mapper.Map<DeleteTransportSheduleContract>(request);
            var result = await _transportApi.DeleteTransportSheduleAsync(deleteTransportShedule);

            if (result.Success)
            {
                return CommonHttpHelper.BuildSuccessResponse();
            }

            return CommonHttpHelper.BuildErrorResponse(result.StatusCode, result.Errors.ToList());
        }

        public async Task<CommonHttpResponse> ActualizeWorkShits(ExtActualizeWorkShitsRequest request)
        {
            if (request == null)
                return CommonHttpHelper.BuildErrorResponse(initialError: "Пустой запрос");

            var actualizeWorkShits = _mapper.Map<ActualizeWorkShitsContract>(request);
            var result = await _transportApi.ActualizeWorkShits(actualizeWorkShits);

            if (result.Success)
            {
                return CommonHttpHelper.BuildSuccessResponse(
                    System.Net.HttpStatusCode.OK);
            }

            return CommonHttpHelper.BuildErrorResponse<ExtTransportSheduleViewModel[]>(result.StatusCode, result.Errors.ToList());
        }
        public async Task<CommonHttpResponse<ExtTransportWorkShiftViewModel[]>> GetActualWorkShits(ExtGetActualWorkShitsRequest request)
        {
            if (request == null)
                return CommonHttpHelper.BuildErrorResponse<ExtTransportWorkShiftViewModel[]>(initialError: "Пустой запрос");

            var getActualWorkShits = _mapper.Map<GetActualWorkShitsContract>(request);
            var result = await _transportApi.GetActualWorkShitsAsync(getActualWorkShits);

            if (result.Success)
            {
                return CommonHttpHelper.BuildSuccessResponse(
                     result.Data.Select(_mapper.Map<ExtTransportWorkShiftViewModel>).ToArray(),
                     System.Net.HttpStatusCode.OK);
            }

            return CommonHttpHelper.BuildErrorResponse<ExtTransportWorkShiftViewModel[]>(result.StatusCode, result.Errors.ToList());
        }

        public async Task<CommonHttpResponse<ExtTransportWorkShiftDropdownViewModel[]>> GetWorkShiftsDropdownAsync(ExtGetWorkShitsDropdownRequest request)
        {
            if (request == null)
                return CommonHttpHelper.BuildErrorResponse<ExtTransportWorkShiftDropdownViewModel[]>(initialError: "Пустой запрос");

            var getWorkShitsDropdown = _mapper.Map<GetWorkShitsDropdownContract>(request);
            var result = await _transportApi.GetWorkShiftsDropdownAsync(getWorkShitsDropdown);

            if (result.Success)
            {
                return CommonHttpHelper.BuildSuccessResponse(
                     result.Data.Select(_mapper.Map<ExtTransportWorkShiftDropdownViewModel>).ToArray(),
                     System.Net.HttpStatusCode.OK);
            }

            return CommonHttpHelper.BuildErrorResponse<ExtTransportWorkShiftDropdownViewModel[]>(result.StatusCode, result.Errors.ToList());
        }

        public async Task<CommonHttpResponse<ExtDictinaryDateRangeViewModel>> GetTransportSheduleDateRangeAsync(ExtGetTransportSheduleDateRangeRequest request)
        {
            if (request == null)
                return CommonHttpHelper.BuildErrorResponse<ExtDictinaryDateRangeViewModel>(initialError: "Пустой запрос");

            var getTransportSheduleDateRange = _mapper.Map<GetTransportSheduleDateRangeContract>(request);
            var result = await _transportApi.GetTransportSheduleDateRangeAsync(getTransportSheduleDateRange);
 
            if (result.Success)
            {
                return CommonHttpHelper.BuildSuccessResponse(
                    _mapper.Map<ExtDictinaryDateRangeViewModel>(result.Data),
                    System.Net.HttpStatusCode.OK);
            }

            return CommonHttpHelper.BuildErrorResponse<ExtDictinaryDateRangeViewModel>(result.StatusCode, result.Errors.ToList());
        }

        #endregion

        #region Rent

        public async Task<CommonHttpResponse<ExtRentViewModel[]>> GetFilteredRentsAsync(ExtRentFilterRequest filter)
        {
            if (filter == null)
                return CommonHttpHelper.BuildErrorResponse<ExtRentViewModel[]>(initialError: "Пустой запрос");

            var rentFilter = _mapper.Map<RentFilterModel>(filter);
            var result = await _transportApi.GetFilteredRentAsync(rentFilter);

            if (result.Success)
            {
                return CommonHttpHelper.BuildSuccessResponse(
                    result.Data.Select(_mapper.Map<ExtRentViewModel>).ToArray(),
                    System.Net.HttpStatusCode.OK);
            }

            return CommonHttpHelper.BuildErrorResponse<ExtRentViewModel[]>(result.StatusCode, result.Errors.ToList());
        }

        public async Task<CommonHttpResponse<ExtRentViewModel[]>> GetRentsAsync(ExtGetRentRequest request)
        {
            if (request == null)
                return CommonHttpHelper.BuildErrorResponse<ExtRentViewModel[]>(initialError: "Пустой запрос");

            var getRent = _mapper.Map<GetRentContract>(request);
            var result = await _transportApi.GetRentAsync(getRent);

            if (result.Success)
            {
                return CommonHttpHelper.BuildSuccessResponse(
                    result.Data.Select(_mapper.Map<ExtRentViewModel>).ToArray(),
                    System.Net.HttpStatusCode.OK);
            }

            return CommonHttpHelper.BuildErrorResponse<ExtRentViewModel[]>(result.StatusCode, result.Errors.ToList());
        }

        public async Task<CommonHttpResponse<ExtRentViewModel>> CreateRentAsync(ExtCreateRentRequest request)
        {
            if (request == null)
                return CommonHttpHelper.BuildErrorResponse<ExtRentViewModel>(initialError: "Пустой запрос");

            var createRent = _mapper.Map<CreateRentContract>(request);
            var result = await _transportApi.CreateRentAsync(createRent);

            if (result.Success)
            {
                return CommonHttpHelper.BuildSuccessResponse(
                  _mapper.Map<ExtRentViewModel>(result.Data),
                  System.Net.HttpStatusCode.OK);
            }

            return CommonHttpHelper.BuildErrorResponse<ExtRentViewModel>(result.StatusCode, result.Errors.ToList());
        }

        public async Task<CommonHttpResponse> DeleteRentsAsync(ExtDeleteRentRequest request)
        {
            if (request == null)
                return CommonHttpHelper.BuildErrorResponse(initialError: "Пустой запрос");

            var deleteRent = _mapper.Map<DeleteRentContract>(request);
            var result = await _transportApi.DeleteRentAsync(deleteRent);

            if (result.Success)
            {
                return CommonHttpHelper.BuildSuccessResponse();
            }

            return CommonHttpHelper.BuildErrorResponse(result.StatusCode, result.Errors.ToList());
        }

        #endregion

        #region TransportDutyShift

        public async Task<CommonHttpResponse<ExtTransportDutyShiftViewModel[]>> GetFilteredTransportDutyShiftsAsync(ExtTransportDutyShiftFilterRequest filter)
        {
            if (filter == null)
                return CommonHttpHelper.BuildErrorResponse<ExtTransportDutyShiftViewModel[]>(initialError: "Пустой запрос");

            var rentFilter = _mapper.Map<TransportDutyShiftFilterModel>(filter);
            var result = await _transportApi.GetFilteredTransportDutyShiftAsync(rentFilter);

            if (result.Success)
            {
                return CommonHttpHelper.BuildSuccessResponse(
                    result.Data.Select(_mapper.Map<ExtTransportDutyShiftViewModel>).ToArray(),
                    System.Net.HttpStatusCode.OK);
            }

            return CommonHttpHelper.BuildErrorResponse<ExtTransportDutyShiftViewModel[]>(result.StatusCode, result.Errors.ToList());
        }

        public async Task<CommonHttpResponse<ExtTransportDutyShiftViewModel[]>> GetTransportDutyShiftsAsync(ExtGetTransportDutyShiftRequest request)
        {
            if (request == null)
                return CommonHttpHelper.BuildErrorResponse<ExtTransportDutyShiftViewModel[]>(initialError: "Пустой запрос");

            var getTransportDutyShift = _mapper.Map<GetTransportDutyShiftContract>(request);
            var result = await _transportApi.GetTransportDutyShiftAsync(getTransportDutyShift);

            if (result.Success)
            {
                return CommonHttpHelper.BuildSuccessResponse(
                    result.Data.Select(_mapper.Map<ExtTransportDutyShiftViewModel>).ToArray(),
                    System.Net.HttpStatusCode.OK);
            }

            return CommonHttpHelper.BuildErrorResponse<ExtTransportDutyShiftViewModel[]>(result.StatusCode, result.Errors.ToList());
        }

        public async Task<CommonHttpResponse<ExtTransportDutyShiftViewModel>> CreateTransportDutyShiftAsync(ExtCreateTransportDutyShiftRequest request)
        {
            if (request == null)
                return CommonHttpHelper.BuildErrorResponse<ExtTransportDutyShiftViewModel>(initialError: "Пустой запрос");

            var createTransportDutyShift = _mapper.Map<CreateTransportDutyShiftContract>(request);
            var result = await _transportApi.CreateTransportDutyShiftAsync(createTransportDutyShift);

            if (result.Success)
            {
                return CommonHttpHelper.BuildSuccessResponse(
                  _mapper.Map<ExtTransportDutyShiftViewModel>(result.Data),
                  System.Net.HttpStatusCode.OK);
            }

            return CommonHttpHelper.BuildErrorResponse<ExtTransportDutyShiftViewModel>(result.StatusCode, result.Errors.ToList());
        }

        public async Task<CommonHttpResponse> DeleteTransportDutyShiftsAsync(ExtDeleteTransportDutyShiftRequest request)
        {
            if (request == null)
                return CommonHttpHelper.BuildErrorResponse(initialError: "Пустой запрос");

            var deleteTransportDutyShift = _mapper.Map<DeleteTransportDutyShiftContract>(request);
            var result = await _transportApi.DeleteTransportDutyShiftAsync(deleteTransportDutyShift);

            if (result.Success)
            {
                return CommonHttpHelper.BuildSuccessResponse();
            }

            return CommonHttpHelper.BuildErrorResponse(result.StatusCode, result.Errors.ToList());
        }

        #endregion
    }
}
