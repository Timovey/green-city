using GreenCity.Http;
using GreenCity.Http.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using TransportContracts.DataContracts.Transport;
using TransportContracts.DataContracts.RepairCard;
using TransportContracts.DataContracts.Driver2Transport;
using TransportContracts.Filters;
using TransportContracts.Interfaces;
using TransportContracts.ViewModels;
using TransportService.Core.BusinessLogic;
using TransportContracts.DataContracts.TransportShedule;
using TransportContracts.DataContracts.TransportWorkShift;
using TransportContracts.DataContracts.Rent;
using TransportContracts.DataContracts.TransportDutyShift;

namespace TransportService.Main.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    [ModelStateValidationActionFilter]
    public class TransportController : Controller, ITransportApi
    {
        private readonly Driver2TransportLogic _driver2TransportLogic;
        private readonly TransportLogic _transportLogic;
        private readonly TransportSheduleLogic _transportSheduleLogic;
        private readonly RepairCardLogic _repairCardLogic;
        private readonly TransportWorkShiftLogic _transportWorkShiftLogic;
        private readonly RentLogic _rentLogic;
        private readonly TransportDutyShiftLogic _transportDutyShiftLogic;
        private readonly ILogger<TransportController> _logger;

        public TransportController(
            ILogger<TransportController> logger,
            TransportLogic transportLogic,
            TransportSheduleLogic transportSheduleLogic,
            Driver2TransportLogic driver2TransportLogic,
            RepairCardLogic repairCardLogic,
            TransportWorkShiftLogic transportWorkShiftLogic,
            RentLogic rentLogic,
            TransportDutyShiftLogic transportDutyShiftLogic
        )
        {
            _logger = logger;
            _transportLogic = transportLogic;
            _transportSheduleLogic = transportSheduleLogic;
            _driver2TransportLogic = driver2TransportLogic;
            _repairCardLogic = repairCardLogic;
            _transportWorkShiftLogic = transportWorkShiftLogic;
            _rentLogic = rentLogic;
            _transportDutyShiftLogic = transportDutyShiftLogic;
        }
        #region Transport
        [HttpPost]
        public Task<CommonHttpResponse<IList<TransportViewModel>>> GetFilteredTransportAsync(TransportFilterModel filter)
        {
            return _transportLogic.GetFilteredTransportsAsync(filter);
        }

        [HttpPost]
        public Task<CommonHttpResponse<IList<TransportViewModel>>> GetTransportAsync(GetTransportContract getContract)
        {
            return _transportLogic.GetTransportsAsync(getContract);
        }

        [HttpPost]
        public Task<CommonHttpResponse<TransportViewModel>> CreateTransportAsync(CreateTransportContract createContract)
        {
            return _transportLogic.CreateTransportAsync(createContract);
        }

        [HttpPost]
        public Task<CommonHttpResponse<TransportViewModel>> UpdateTransportAsync(UpdateTransportContract updateContract)
        {
            return _transportLogic.UpdateTransportsAsync(updateContract);
        }

        [HttpPost]
        public Task<CommonHttpResponse> DeleteTransportAsync(DeleteTransportContract deleteContract)
        {
            return _transportLogic.DeleteTransportsAsync(deleteContract);
        }
        #endregion

        #region Driver2Transport
        [HttpPost]
        public Task<CommonHttpResponse<IList<Driver2TransportViewModel>>> GetFilteredDrivers2TransportAsync(Driver2TransportFilterModel filter)
        {
            return _driver2TransportLogic.GetFilteredDrivers2TransportAsync(filter);
        }

        [HttpPost]
        public Task<CommonHttpResponse<IList<Driver2TransportViewModel>>> GetDrivers2TransportAsync(GetDriver2TransportContract getContract)
        {
            return _driver2TransportLogic.GetDrivers2TransportAsync(getContract);
        }

        [HttpPost]
        public Task<CommonHttpResponse<Driver2TransportViewModel>> CreateDriver2TransportAsync(CreateDriver2TransportContract createContract)
        {
            return _driver2TransportLogic.CreateDriver2TransportAsync(createContract);
        }

        [HttpPost]
        public Task<CommonHttpResponse<Driver2TransportViewModel>> UpdateDriver2TransportAsync(UpdateDriver2TransportContract updateContract)
        {
            return _driver2TransportLogic.UpdateDriver2TransportAsync(updateContract);
        }

        [HttpPost]
        public Task<CommonHttpResponse> DeleteDriver2TransportAsync(DeleteDriver2TransportContract deleteContract)
        {
            return _driver2TransportLogic.DeleteDriver2TransportAsync(deleteContract);
        }
        #endregion

        #region TransportShedule
        [HttpPost]
        public Task<CommonHttpResponse<IList<TransportWorkShiftViewModel>>> GetActualWorkShitsAsync(
            GetActualWorkShitsContract contract)
        {
            return _transportWorkShiftLogic.GetWorkShiftsAsync(contract);
        }

        [HttpPost]
        public Task<CommonHttpResponse<IList<TransportWorkShiftDropdownViewModel>>> GetWorkShiftsDropdownAsync(
            GetWorkShitsDropdownContract contract)
        {
            return _transportWorkShiftLogic.GetWorkShiftsDropdownAsync(contract);
        }

        [HttpPost]
        public Task<CommonHttpResponse<IList<TransportWorkShiftViewModel>>> GetWorkShiftsByIdsAsync(
            GetWorkShiftsByIdsContract contract
        )
        {
            return _transportWorkShiftLogic.GetWorkShiftsByIdsAsync(contract);
        }

        [HttpPost]
        public Task<CommonHttpResponse> ChangeWorkshiftRouteStatusAsync(
           ChangeWorkshiftRouteStatusContract contract
        )
        {
            return _transportWorkShiftLogic.ChangeWorkshiftRouteStatusAsync(contract);
        }

        [HttpPost]
        public Task<CommonHttpResponse<IList<TransportWorkShiftViewModel>>> GetWorkShiftsForDoneTaskListAsync(
            GetWorkShiftsForDoneTaskListContract contract
        )
        {
            return _transportWorkShiftLogic.GetWorkShiftsForDoneTaskListAsync(contract);
        }

        [HttpPost]
        public Task<CommonHttpResponse> ActualizeWorkShits(ActualizeWorkShitsContract contract)
        {
            return _transportSheduleLogic.FillTransportNextWorkDateAsync(contract);
        }
        
        [HttpPost]
        public Task<CommonHttpResponse<DictinaryDateRangeViewModel>> GetTransportSheduleDateRangeAsync(GetTransportSheduleDateRangeContract contract)
        {
            return _transportSheduleLogic.GetTransportSheduleDateRangeAsync(contract);
        }

        [HttpPost]
        public Task<CommonHttpResponse<IList<TransportSheduleViewModel>>> GetFilteredTransportSheduleAsync(TransportSheduleFilterModel filter)
        {
            return _transportSheduleLogic.GetFilteredTransportShedulesAsync(filter);
        }

        [HttpPost]
        public Task<CommonHttpResponse<IList<TransportSheduleViewModel>>> GetTransportSheduleAsync(GetTransportSheduleContract getContract)
        {
            return _transportSheduleLogic.GetTransportShedulesAsync(getContract);
        }

        [HttpPost]
        public Task<CommonHttpResponse<TransportSheduleViewModel>> CreateTransportSheduleAsync(CreateTransportSheduleContract createContract)
        {
            return _transportSheduleLogic.CreateTransportSheduleAsync(createContract);
        }

        [HttpPost]
        public Task<CommonHttpResponse<TransportSheduleViewModel>> UpdateTransportSheduleAsync(UpdateTransportSheduleContract updateContract)
        {
            return _transportSheduleLogic.UpdateTransportShedulesAsync(updateContract);
        }

        [HttpPost]
        public Task<CommonHttpResponse> DeleteTransportSheduleAsync(DeleteTransportSheduleContract deleteContract)
        {
            return _transportSheduleLogic.DeleteTransportShedulesAsync(deleteContract);
        }

        #endregion

        #region RepairCard
        [HttpPost]
        public Task<CommonHttpResponse<IList<RepairCardViewModel>>> GetFilteredRepairCardAsync(RepairCardFilterModel filter)
        {
            return _repairCardLogic.GetFilteredRepairCardsAsync(filter);
        }

        [HttpPost]
        public Task<CommonHttpResponse<IList<RepairCardViewModel>>> GetRepairCardAsync(GetRepairCardContract getContract)
        {
            return _repairCardLogic.GetRepairCardsAsync(getContract);
        }

        [HttpPost]
        public Task<CommonHttpResponse<RepairCardViewModel>> CreateRepairCardAsync(CreateRepairCardContract createContract)
        {
            return _repairCardLogic.CreateRepairCardAsync(createContract);
        }

        [HttpPost]
        public Task<CommonHttpResponse<RepairCardViewModel>> UpdateRepairCardAsync(UpdateRepairCardContract updateContract)
        {
            return _repairCardLogic.UpdateRepairCardsAsync(updateContract);
        }

        [HttpPost]
        public Task<CommonHttpResponse> DeleteRepairCardAsync(DeleteRepairCardContract deleteContract)
        {
            return _repairCardLogic.DeleteRepairCardsAsync(deleteContract);
        }

        #endregion

        #region Rent
        [HttpPost]
        public Task<CommonHttpResponse<IList<RentViewModel>>> GetFilteredRentAsync(RentFilterModel filter)
        {
            return _rentLogic.GetFilteredRentsAsync(filter);
        }

        [HttpPost]
        public Task<CommonHttpResponse<IList<RentViewModel>>> GetRentAsync(GetRentContract getContract)
        {
            return _rentLogic.GetRentsAsync(getContract);
        }

        [HttpPost]
        public Task<CommonHttpResponse<RentViewModel>> CreateRentAsync(CreateRentContract createContract)
        {
            return _rentLogic.CreateRentAsync(createContract);
        }

        [HttpPost]
        public Task<CommonHttpResponse> DeleteRentAsync(DeleteRentContract deleteContract)
        {
            return _rentLogic.DeleteRentsAsync(deleteContract);
        }
        #endregion

        #region TransportDutyShift
        [HttpPost]
        public Task<CommonHttpResponse<IList<TransportDutyShiftViewModel>>> GetFilteredTransportDutyShiftAsync(TransportDutyShiftFilterModel filter)
        {
            return _transportDutyShiftLogic.GetFilteredTransportDutyShiftsAsync(filter);
        }

        [HttpPost]
        public Task<CommonHttpResponse<IList<TransportDutyShiftViewModel>>> GetTransportDutyShiftAsync(GetTransportDutyShiftContract getContract)
        {
            return _transportDutyShiftLogic.GetTransportDutyShiftsAsync(getContract);
        }

        [HttpPost]
        public Task<CommonHttpResponse<TransportDutyShiftViewModel>> CreateTransportDutyShiftAsync(CreateTransportDutyShiftContract createContract)
        {
            return _transportDutyShiftLogic.CreateTransportDutyShiftAsync(createContract);
        }

        [HttpPost]
        public Task<CommonHttpResponse> DeleteTransportDutyShiftAsync(DeleteTransportDutyShiftContract deleteContract)
        {
            return _transportDutyShiftLogic.DeleteTransportDutyShiftsAsync(deleteContract);
        }
        #endregion
    }
}
