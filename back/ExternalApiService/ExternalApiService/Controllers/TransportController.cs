using ExternalApi.Interface.Models.Transport.Driver;
using ExternalApi.Interface.Models.Transport.Transport;
using ExternalApi.Interface.Models.Transport.Driver2Transport;
using ExternalApi.Interface.Models.Transport.DriverVacation;
using ExternalApi.Interface.Models.Transport.RepairCard;
using ExternalApi.Interface.Models.Transport.TransportShedule;
using ExternalApi.Interface.Models.Transport.TransportDutyShift;
using ExternalApiService.Operations.Interface;
using GreenCity.Http;
using GreenCity.Http.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using ExternalApi.Interface.Models.Transport.TransportWorkShift;
using ExternalApi.Interface.Models.Transport.Rent;
using AuthContracts.Attributres;
using AuthContracts.Constants;

namespace ExternalApiService.Controllers
{
    [AuthRoles(AuthRolesConstants.ADMIN, AuthRolesConstants.TAX, AuthRolesConstants.LOGIST)]
    [ApiController]
    [Route("api/[controller]/[action]")]
    [ModelStateValidationActionFilter]
    public class TransportController : Controller
    {
        private readonly ILogger<TransportController> _logger;
        private readonly ITransportOperations _transportOperations;

        public TransportController(
            ITransportOperations transportOperations,
            ILogger<TransportController> logger
        )
        {
            _transportOperations = transportOperations;
            _logger = logger;
        }

        #region Transport

        [HttpPost]
        [AuthRoles(AuthRolesConstants.ADMIN, AuthRolesConstants.TAX)]
        public Task<CommonHttpResponse<ExtTransportViewModel[]>> GetFilteredTransport(ExtTransportFilterRequest filter)
        {
            return _transportOperations.GetFilteredTransportsAsync(filter);
        }

        [HttpPost]
        [AuthRoles(AuthRolesConstants.ADMIN, AuthRolesConstants.TAX)]
        public Task<CommonHttpResponse<ExtTransportViewModel[]>> GetTransport(ExtGetTransportRequest request)
        {
            return _transportOperations.GetTransportsAsync(request);
        }

        [HttpPost]
        [AuthRoles(AuthRolesConstants.ADMIN, AuthRolesConstants.TAX)]
        public Task<CommonHttpResponse<ExtTransportViewModel>> CreateTransport(ExtCreateTransportRequest request)
        {
            return _transportOperations.CreateTransportAsync(request);
        }

        [HttpPost]
        [AuthRoles(AuthRolesConstants.ADMIN, AuthRolesConstants.TAX)]
        public Task<CommonHttpResponse<ExtTransportViewModel>> UpdateTransport(ExtUpdateTransportRequest request)
        {
            return _transportOperations.UpdateTransportsAsync(request);
        }

        [HttpPost]
        [AuthRoles(AuthRolesConstants.ADMIN, AuthRolesConstants.TAX)]
        public Task<CommonHttpResponse> DeleteTransport(ExtDeleteTransportRequest request)
        {
            return _transportOperations.DeleteTransportsAsync(request);
        }

        #endregion

        #region Driver

        [HttpPost]
        [AuthRoles(AuthRolesConstants.ADMIN, AuthRolesConstants.TAX)]
        public Task<CommonHttpResponse<ExtDriverViewModel[]>> GetFilteredDriver(ExtDriverFilterRequest filter)
        {
            return _transportOperations.GetFilteredDriversAsync(filter);
        }

        [HttpPost]
        [AuthRoles(AuthRolesConstants.ADMIN, AuthRolesConstants.TAX)]
        public Task<CommonHttpResponse<ExtDriverViewModel[]>> GetDriver(ExtGetDriverRequest request)
        {
            return _transportOperations.GetDriversAsync(request);
        }

        [HttpPost]
        [AuthRoles(AuthRolesConstants.ADMIN, AuthRolesConstants.TAX)]
        public Task<CommonHttpResponse<ExtDriverViewModel>> CreateDriver(ExtCreateDriverRequest request)
        {
            return _transportOperations.CreateDriverAsync(request);
        }

        [HttpPost]
        [AuthRoles(AuthRolesConstants.ADMIN, AuthRolesConstants.TAX)]
        public Task<CommonHttpResponse<ExtDriverViewModel>> UpdateDriver(ExtUpdateDriverRequest request)
        {
            return _transportOperations.UpdateDriversAsync(request);
        }

        [HttpPost]
        [AuthRoles(AuthRolesConstants.ADMIN, AuthRolesConstants.TAX)]
        public Task<CommonHttpResponse> DeleteDriver(ExtDeleteDriverRequest request)
        {
            return _transportOperations.DeleteDriversAsync(request);
        }

        #endregion

        #region Driver2Transport

        [HttpPost]
        [AuthRoles(AuthRolesConstants.ADMIN, AuthRolesConstants.TAX)]
        public Task<CommonHttpResponse<ExtDriver2TransportViewModel[]>> GetFilteredDrivers2Transport(ExtDriver2TransportFilterRequest filter)
        {
            return _transportOperations.GetFilteredDrivers2TransportAsync(filter);
        }

        [HttpPost]
        [AuthRoles(AuthRolesConstants.ADMIN, AuthRolesConstants.TAX)]
        public Task<CommonHttpResponse<ExtDriver2TransportViewModel[]>> GetDrivers2Transport(ExtGetDriver2TransportRequest request)
        {
            return _transportOperations.GetDrivers2TransportAsync(request);
        }

        [HttpPost]
        [AuthRoles(AuthRolesConstants.ADMIN, AuthRolesConstants.TAX)]
        public Task<CommonHttpResponse<ExtDriver2TransportViewModel>> CreateDriver2Transport(ExtCreateDriver2TransportRequest request)
        {
            return _transportOperations.CreateDriver2TransportAsync(request);
        }

        [HttpPost]
        [AuthRoles(AuthRolesConstants.ADMIN, AuthRolesConstants.TAX)]
        public Task<CommonHttpResponse<ExtDriver2TransportViewModel>> UpdateDriver2Transport(ExtUpdateDriver2TransportRequest request)
        {
            return _transportOperations.UpdateDriver2TransportAsync(request);
        }

        [HttpPost]
        [AuthRoles(AuthRolesConstants.ADMIN, AuthRolesConstants.TAX)]
        public Task<CommonHttpResponse> DeleteDriver2Transport(ExtDeleteDriver2TransportRequest request)
        {
            return _transportOperations.DeleteDrivers2TransportAsync(request);
        }

        #endregion

        #region DriverVacation

        [HttpPost]
        [AuthRoles(AuthRolesConstants.ADMIN, AuthRolesConstants.TAX)]
        public Task<CommonHttpResponse<ExtDriverVacationViewModel[]>> GetFilteredDriverVacation(ExtDriverVacationFilterRequest filter)
        {
            return _transportOperations.GetFilteredDriverVacationsAsync(filter);
        }

        [HttpPost]
        [AuthRoles(AuthRolesConstants.ADMIN, AuthRolesConstants.TAX)]
        public Task<CommonHttpResponse<ExtDriverVacationViewModel[]>> GetDriverVacation(ExtGetDriverVacationRequest request)
        {
            return _transportOperations.GetDriverVacationsAsync(request);
        }

        [HttpPost]
        [AuthRoles(AuthRolesConstants.ADMIN, AuthRolesConstants.TAX)]
        public Task<CommonHttpResponse<ExtDriverVacationViewModel>> CreateDriverVacation(ExtCreateDriverVacationRequest request)
        {
            return _transportOperations.CreateDriverVacationAsync(request);
        }

        [HttpPost]
        [AuthRoles(AuthRolesConstants.ADMIN, AuthRolesConstants.TAX)]
        public Task<CommonHttpResponse<ExtDriverVacationViewModel>> UpdateDriverVacation(ExtUpdateDriverVacationRequest request)
        {
            return _transportOperations.UpdateDriverVacationsAsync(request);
        }

        [HttpPost]
        [AuthRoles(AuthRolesConstants.ADMIN, AuthRolesConstants.TAX)]
        public Task<CommonHttpResponse> DeleteDriverVacation(ExtDeleteDriverVacationRequest request)
        {
            return _transportOperations.DeleteDriverVacationsAsync(request);
        }

        #endregion

        #region RepairCard

        [HttpPost]
        [AuthRoles(AuthRolesConstants.ADMIN, AuthRolesConstants.TAX)]
        public Task<CommonHttpResponse<ExtRepairCardViewModel[]>> GetFilteredRepairCard(ExtRepairCardFilterRequest filter)
        {
            return _transportOperations.GetFilteredRepairCardsAsync(filter);
        }

        [HttpPost]
        [AuthRoles(AuthRolesConstants.ADMIN, AuthRolesConstants.TAX)]
        public Task<CommonHttpResponse<ExtRepairCardViewModel[]>> GetRepairCard(ExtGetRepairCardRequest request)
        {
            return _transportOperations.GetRepairCardsAsync(request);
        }

        [HttpPost]
        [AuthRoles(AuthRolesConstants.ADMIN, AuthRolesConstants.TAX)]
        public Task<CommonHttpResponse<ExtRepairCardViewModel>> CreateRepairCard(ExtCreateRepairCardRequest request)
        {
            return _transportOperations.CreateRepairCardAsync(request);
        }

        [HttpPost]
        [AuthRoles(AuthRolesConstants.ADMIN, AuthRolesConstants.TAX)]
        public Task<CommonHttpResponse<ExtRepairCardViewModel>> UpdateRepairCard(ExtUpdateRepairCardRequest request)
        {
            return _transportOperations.UpdateRepairCardsAsync(request);
        }

        [HttpPost]
        [AuthRoles(AuthRolesConstants.ADMIN, AuthRolesConstants.TAX)]
        public Task<CommonHttpResponse> DeleteRepairCard(ExtDeleteRepairCardRequest request)
        {
            return _transportOperations.DeleteRepairCardsAsync(request);
        }

        #endregion

        #region TransportShedule

        [HttpPost]
        [AuthRoles(AuthRolesConstants.ADMIN, AuthRolesConstants.TAX)]
        public Task<CommonHttpResponse<ExtTransportSheduleViewModel[]>> GetFilteredTransportShedule(ExtTransportSheduleFilterRequest filter)
        {
            return _transportOperations.GetFilteredTransportShedulesAsync(filter);
        }

        [HttpPost]
        [AuthRoles(AuthRolesConstants.ADMIN, AuthRolesConstants.TAX)]
        public Task<CommonHttpResponse<ExtTransportSheduleViewModel[]>> GetTransportShedule(ExtGetTransportSheduleRequest request)
        {
            return _transportOperations.GetTransportShedulesAsync(request);
        }

        [HttpPost]
        [AuthRoles(AuthRolesConstants.ADMIN, AuthRolesConstants.TAX)]
        public Task<CommonHttpResponse<ExtTransportSheduleViewModel>> CreateTransportShedule(ExtCreateTransportSheduleRequest request)
        {
            return _transportOperations.CreateTransportSheduleAsync(request);
        }

        [HttpPost]
        [AuthRoles(AuthRolesConstants.ADMIN, AuthRolesConstants.TAX)]
        public Task<CommonHttpResponse<ExtTransportSheduleViewModel>> UpdateTransportShedule(ExtUpdateTransportSheduleRequest request)
        {
            return _transportOperations.UpdateTransportShedulesAsync(request);
        }

        [HttpPost]
        [AuthRoles(AuthRolesConstants.ADMIN, AuthRolesConstants.TAX)]
        public Task<CommonHttpResponse> DeleteTransportShedule(ExtDeleteTransportSheduleRequest request)
        {
            return _transportOperations.DeleteTransportShedulesAsync(request);
        }

        [HttpPost]
        [AuthRoles(AuthRolesConstants.ADMIN, AuthRolesConstants.TAX)]
        public Task<CommonHttpResponse> ActualizeWorkShits(ExtActualizeWorkShitsRequest request)
        {
            return _transportOperations.ActualizeWorkShits(request);
        }

        [HttpPost]
        [AuthRoles(AuthRolesConstants.ADMIN, AuthRolesConstants.TAX)]
        public Task<CommonHttpResponse<ExtTransportWorkShiftViewModel[]>> GetActualWorkShits(
            ExtGetActualWorkShitsRequest request)
        {
            return _transportOperations.GetActualWorkShits(request);
        }

        [HttpPost]
        [AuthRoles(AuthRolesConstants.ADMIN, AuthRolesConstants.TAX, AuthRolesConstants.LOGIST, AuthRolesConstants.DISPATCHER)]
        public Task<CommonHttpResponse<ExtTransportWorkShiftDropdownViewModel[]>> GetWorkShiftsDropdownAsync(
            ExtGetWorkShitsDropdownRequest request)
        {
            return _transportOperations.GetWorkShiftsDropdownAsync(request);
        }

        [HttpPost]
        [AuthRoles(AuthRolesConstants.ADMIN, AuthRolesConstants.TAX)]
        public Task<CommonHttpResponse<ExtDictinaryDateRangeViewModel>> GetTransportSheduleDateRangeAsync(
            ExtGetTransportSheduleDateRangeRequest request)
        {
            return _transportOperations.GetTransportSheduleDateRangeAsync(request);
        }

        #endregion

        #region Rent

        [HttpPost]
        [AuthRoles(AuthRolesConstants.ADMIN, AuthRolesConstants.TAX)]
        public Task<CommonHttpResponse<ExtRentViewModel[]>> GetFilteredRent(ExtRentFilterRequest filter)
        {
            return _transportOperations.GetFilteredRentsAsync(filter);
        }

        [HttpPost]
        [AuthRoles(AuthRolesConstants.ADMIN, AuthRolesConstants.TAX)]
        public Task<CommonHttpResponse<ExtRentViewModel[]>> GetRent(ExtGetRentRequest request)
        {
            return _transportOperations.GetRentsAsync(request);
        }

        [HttpPost]
        [AuthRoles(AuthRolesConstants.ADMIN, AuthRolesConstants.TAX)]
        public Task<CommonHttpResponse<ExtRentViewModel>> CreateRent(ExtCreateRentRequest request)
        {
            return _transportOperations.CreateRentAsync(request);
        }

        [HttpPost]
        [AuthRoles(AuthRolesConstants.ADMIN, AuthRolesConstants.TAX)]
        public Task<CommonHttpResponse> DeleteRent(ExtDeleteRentRequest request)
        {
            return _transportOperations.DeleteRentsAsync(request);
        }

        #endregion

        #region TransportDutyShift

        [HttpPost]
        [AuthRoles(AuthRolesConstants.ADMIN, AuthRolesConstants.TAX)]
        public Task<CommonHttpResponse<ExtTransportDutyShiftViewModel[]>> GetFilteredTransportDutyShift(ExtTransportDutyShiftFilterRequest filter)
        {
            return _transportOperations.GetFilteredTransportDutyShiftsAsync(filter);
        }

        [HttpPost]
        [AuthRoles(AuthRolesConstants.ADMIN, AuthRolesConstants.TAX)]
        public Task<CommonHttpResponse<ExtTransportDutyShiftViewModel[]>> GetTransportDutyShift(ExtGetTransportDutyShiftRequest request)
        {
            return _transportOperations.GetTransportDutyShiftsAsync(request);
        }

        [HttpPost]
        [AuthRoles(AuthRolesConstants.ADMIN, AuthRolesConstants.TAX)]
        public Task<CommonHttpResponse<ExtTransportDutyShiftViewModel>> CreateTransportDutyShift(ExtCreateTransportDutyShiftRequest request)
        {
            return _transportOperations.CreateTransportDutyShiftAsync(request);
        }

        [HttpPost]
        [AuthRoles(AuthRolesConstants.ADMIN, AuthRolesConstants.TAX)]
        public Task<CommonHttpResponse> DeleteTransportDutyShift(ExtDeleteTransportDutyShiftRequest request)
        {
            return _transportOperations.DeleteTransportDutyShiftsAsync(request);
        }

        #endregion
    }
}
