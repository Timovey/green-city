using GreenCity.Http;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;
using TransportContracts.DataContracts.Transport;
using TransportContracts.DataContracts.Driver2Transport;
using TransportContracts.Filters;
using TransportContracts.ViewModels;
using TransportContracts.DataContracts.TransportShedule;
using TransportContracts.DataContracts.RepairCard;
using TransportContracts.DataContracts.TransportWorkShift;
using TransportContracts.DataContracts.Rent;
using TransportContracts.DataContracts.TransportDutyShift;

namespace TransportContracts.Interfaces
{
    public interface ITransportApi
    {
        #region Transport
        [Post("/Transport/GetFilteredTransport")]
        Task<CommonHttpResponse<IList<TransportViewModel>>> GetFilteredTransportAsync(
           [Body] TransportFilterModel filter
        );

        [Post("/Transport/GetTransport")]
        Task<CommonHttpResponse<IList<TransportViewModel>>> GetTransportAsync(
            [Body] GetTransportContract getContract
        );

        [Post("/Transport/CreateTransport")]
        Task<CommonHttpResponse<TransportViewModel>> CreateTransportAsync(
            [Body] CreateTransportContract createContract
        );

        [Post("/Transport/UpdateTransport")]
        Task<CommonHttpResponse<TransportViewModel>> UpdateTransportAsync(
            [Body] UpdateTransportContract updateContract
        );

        [Post("/Transport/DeleteTransport")]
        Task<CommonHttpResponse> DeleteTransportAsync(
            [Body] DeleteTransportContract deleteContract
        );

        #endregion

        #region Driver2Transport
        [Post("/Transport/GetFilteredDrivers2Transport")]
        Task<CommonHttpResponse<IList<Driver2TransportViewModel>>> GetFilteredDrivers2TransportAsync(
          [Body] Driver2TransportFilterModel filter
       );

        [Post("/Transport/GetDrivers2Transport")]
        Task<CommonHttpResponse<IList<Driver2TransportViewModel>>> GetDrivers2TransportAsync(
            [Body] GetDriver2TransportContract getContract
        );

        [Post("/Transport/CreateDriver2Transport")]
        Task<CommonHttpResponse<Driver2TransportViewModel>> CreateDriver2TransportAsync(
            [Body] CreateDriver2TransportContract createContract
        );

        [Post("/Transport/UpdateDriver2Transport")]
        Task<CommonHttpResponse<Driver2TransportViewModel>> UpdateDriver2TransportAsync(
            [Body] UpdateDriver2TransportContract updateContract
        );

        [Post("/Transport/DeleteDriver2Transport")]
        Task<CommonHttpResponse> DeleteDriver2TransportAsync(
            [Body] DeleteDriver2TransportContract deleteContract
        );
        #endregion

        #region TransportShedule
        [Post("/Transport/GetActualWorkShits")]
        Task<CommonHttpResponse<IList<TransportWorkShiftViewModel>>> GetActualWorkShitsAsync(
           [Body] GetActualWorkShitsContract contract
        );

        [Post("/Transport/GetWorkShiftsDropdown")]
        Task<CommonHttpResponse<IList<TransportWorkShiftDropdownViewModel>>> GetWorkShiftsDropdownAsync(
          [Body] GetWorkShitsDropdownContract contract
        );

        [Post("/Transport/GetWorkShiftsByIds")]
        Task<CommonHttpResponse<IList<TransportWorkShiftViewModel>>> GetWorkShiftsByIdsAsync(
           [Body] GetWorkShiftsByIdsContract contract
        );

        [Post("/Transport/ChangeWorkshiftRouteStatus")]
        Task<CommonHttpResponse> ChangeWorkshiftRouteStatusAsync(
          [Body] ChangeWorkshiftRouteStatusContract contract
        );

        [Post("/Transport/GetWorkShiftsForDoneTaskList")]
        Task<CommonHttpResponse<IList<TransportWorkShiftViewModel>>> GetWorkShiftsForDoneTaskListAsync(
            [Body] GetWorkShiftsForDoneTaskListContract contract
        );

        [Post("/Transport/ActualizeWorkShits")]
        Task<CommonHttpResponse> ActualizeWorkShits(
            [Body] ActualizeWorkShitsContract contract
        );
        
        [Post("/Transport/GetTransportSheduleDateRange")]
        Task<CommonHttpResponse<DictinaryDateRangeViewModel>> GetTransportSheduleDateRangeAsync(
           [Body] GetTransportSheduleDateRangeContract contract
        );

        [Post("/Transport/GetFilteredTransportShedule")]
        Task<CommonHttpResponse<IList<TransportSheduleViewModel>>> GetFilteredTransportSheduleAsync(
           [Body] TransportSheduleFilterModel filter
        );

        [Post("/Transport/GetTransportShedule")]
        Task<CommonHttpResponse<IList<TransportSheduleViewModel>>> GetTransportSheduleAsync(
            [Body] GetTransportSheduleContract getContract
        );

        [Post("/Transport/CreateTransportShedule")]
        Task<CommonHttpResponse<TransportSheduleViewModel>> CreateTransportSheduleAsync(
            [Body] CreateTransportSheduleContract createContract
        );

        [Post("/Transport/UpdateTransportShedule")]
        Task<CommonHttpResponse<TransportSheduleViewModel>> UpdateTransportSheduleAsync(
            [Body] UpdateTransportSheduleContract updateContract
        );

        [Post("/Transport/DeleteTransportShedule")]
        Task<CommonHttpResponse> DeleteTransportSheduleAsync(
            [Body] DeleteTransportSheduleContract deleteContract
        );
        #endregion

        #region RepairCard
        [Post("/Transport/GetFilteredRepairCard")]
        Task<CommonHttpResponse<IList<RepairCardViewModel>>> GetFilteredRepairCardAsync(
           [Body] RepairCardFilterModel filter
        );

        [Post("/Transport/GetRepairCard")]
        Task<CommonHttpResponse<IList<RepairCardViewModel>>> GetRepairCardAsync(
            [Body] GetRepairCardContract getContract
        );

        [Post("/Transport/CreateRepairCard")]
        Task<CommonHttpResponse<RepairCardViewModel>> CreateRepairCardAsync(
            [Body] CreateRepairCardContract createContract
        );

        [Post("/Transport/UpdateRepairCard")]
        Task<CommonHttpResponse<RepairCardViewModel>> UpdateRepairCardAsync(
            [Body] UpdateRepairCardContract updateContract
        );

        [Post("/Transport/DeleteRepairCard")]
        Task<CommonHttpResponse> DeleteRepairCardAsync(
            [Body] DeleteRepairCardContract deleteContract
        );
        #endregion

        #region Rent
        [Post("/Transport/GetFilteredRent")]
        Task<CommonHttpResponse<IList<RentViewModel>>> GetFilteredRentAsync(
           [Body] RentFilterModel filter
        );

        [Post("/Transport/GetRent")]
        Task<CommonHttpResponse<IList<RentViewModel>>> GetRentAsync(
            [Body] GetRentContract getContract
        );

        [Post("/Transport/CreateRent")]
        Task<CommonHttpResponse<RentViewModel>> CreateRentAsync(
            [Body] CreateRentContract createContract
        );

        [Post("/Transport/DeleteRent")]
        Task<CommonHttpResponse> DeleteRentAsync(
            [Body] DeleteRentContract deleteContract
        );
        #endregion

        #region TransportDutyShift
        [Post("/Transport/GetFilteredTransportDutyShift")]
        Task<CommonHttpResponse<IList<TransportDutyShiftViewModel>>> GetFilteredTransportDutyShiftAsync(
           [Body] TransportDutyShiftFilterModel filter
        );

        [Post("/Transport/GetTransportDutyShift")]
        Task<CommonHttpResponse<IList<TransportDutyShiftViewModel>>> GetTransportDutyShiftAsync(
            [Body] GetTransportDutyShiftContract getContract
        );

        [Post("/Transport/CreateTransportDutyShift")]
        Task<CommonHttpResponse<TransportDutyShiftViewModel>> CreateTransportDutyShiftAsync(
            [Body] CreateTransportDutyShiftContract createContract
        );

        [Post("/Transport/DeleteTransportDutyShift")]
        Task<CommonHttpResponse> DeleteTransportDutyShiftAsync(
            [Body] DeleteTransportDutyShiftContract deleteContract
        );
        #endregion
    }
}
