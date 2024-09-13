using ExternalApi.Interface.Models.Transport.Driver;
using ExternalApi.Interface.Models.Transport.Transport;
using ExternalApi.Interface.Models.Transport.Driver2Transport;
using ExternalApi.Interface.Models.Transport.DriverVacation;
using ExternalApi.Interface.Models.Transport.RepairCard;
using ExternalApi.Interface.Models.Transport.TransportShedule;
using ExternalApi.Interface.Models.Transport.Rent;
using ExternalApi.Interface.Models.Transport.TransportWorkShift;
using GreenCity.Http;
using System.Threading.Tasks;
using ExternalApi.Interface.Models.Transport.TransportDutyShift;

namespace ExternalApiService.Operations.Interface
{
    public interface ITransportOperations
    {
        #region Transport
        Task<CommonHttpResponse<ExtTransportViewModel[]>> GetFilteredTransportsAsync(ExtTransportFilterRequest filter);

        Task<CommonHttpResponse<ExtTransportViewModel[]>> GetTransportsAsync(ExtGetTransportRequest request);

        Task<CommonHttpResponse<ExtTransportViewModel>> CreateTransportAsync(ExtCreateTransportRequest request);

        Task<CommonHttpResponse<ExtTransportViewModel>> UpdateTransportsAsync(ExtUpdateTransportRequest request);

        Task<CommonHttpResponse> DeleteTransportsAsync(ExtDeleteTransportRequest request);
        #endregion

        #region Driver
        Task<CommonHttpResponse<ExtDriverViewModel[]>> GetFilteredDriversAsync(ExtDriverFilterRequest filter);

        Task<CommonHttpResponse<ExtDriverViewModel[]>> GetDriversAsync(ExtGetDriverRequest request);

        Task<CommonHttpResponse<ExtDriverViewModel>> CreateDriverAsync(ExtCreateDriverRequest request);

        Task<CommonHttpResponse<ExtDriverViewModel>> UpdateDriversAsync(ExtUpdateDriverRequest request);

        Task<CommonHttpResponse> DeleteDriversAsync(ExtDeleteDriverRequest request);
        #endregion

        #region D2T
        Task<CommonHttpResponse<ExtDriver2TransportViewModel[]>> GetFilteredDrivers2TransportAsync(ExtDriver2TransportFilterRequest filter);

        Task<CommonHttpResponse<ExtDriver2TransportViewModel[]>> GetDrivers2TransportAsync(ExtGetDriver2TransportRequest request);

        Task<CommonHttpResponse<ExtDriver2TransportViewModel>> CreateDriver2TransportAsync(ExtCreateDriver2TransportRequest request);

        Task<CommonHttpResponse<ExtDriver2TransportViewModel>> UpdateDriver2TransportAsync(ExtUpdateDriver2TransportRequest request);

        Task<CommonHttpResponse> DeleteDrivers2TransportAsync(ExtDeleteDriver2TransportRequest request);
        #endregion

        #region DriverVacation
        Task<CommonHttpResponse<ExtDriverVacationViewModel[]>> GetFilteredDriverVacationsAsync(ExtDriverVacationFilterRequest filter);

        Task<CommonHttpResponse<ExtDriverVacationViewModel[]>> GetDriverVacationsAsync(ExtGetDriverVacationRequest request);

        Task<CommonHttpResponse<ExtDriverVacationViewModel>> CreateDriverVacationAsync(ExtCreateDriverVacationRequest request);

        Task<CommonHttpResponse<ExtDriverVacationViewModel>> UpdateDriverVacationsAsync(ExtUpdateDriverVacationRequest request);

        Task<CommonHttpResponse> DeleteDriverVacationsAsync(ExtDeleteDriverVacationRequest request);
        #endregion

        #region RepairCard
        Task<CommonHttpResponse<ExtRepairCardViewModel[]>> GetFilteredRepairCardsAsync(ExtRepairCardFilterRequest filter);

        Task<CommonHttpResponse<ExtRepairCardViewModel[]>> GetRepairCardsAsync(ExtGetRepairCardRequest request);

        Task<CommonHttpResponse<ExtRepairCardViewModel>> CreateRepairCardAsync(ExtCreateRepairCardRequest request);

        Task<CommonHttpResponse<ExtRepairCardViewModel>> UpdateRepairCardsAsync(ExtUpdateRepairCardRequest request);

        Task<CommonHttpResponse> DeleteRepairCardsAsync(ExtDeleteRepairCardRequest request);
        #endregion

        #region TransportShedule
        Task<CommonHttpResponse<ExtTransportSheduleViewModel[]>> GetFilteredTransportShedulesAsync(ExtTransportSheduleFilterRequest filter);

        Task<CommonHttpResponse<ExtTransportSheduleViewModel[]>> GetTransportShedulesAsync(ExtGetTransportSheduleRequest request);

        Task<CommonHttpResponse<ExtTransportSheduleViewModel>> CreateTransportSheduleAsync(ExtCreateTransportSheduleRequest request);

        Task<CommonHttpResponse<ExtTransportSheduleViewModel>> UpdateTransportShedulesAsync(ExtUpdateTransportSheduleRequest request);

        Task<CommonHttpResponse> DeleteTransportShedulesAsync(ExtDeleteTransportSheduleRequest request);

        Task<CommonHttpResponse> ActualizeWorkShits(ExtActualizeWorkShitsRequest contract);

        Task<CommonHttpResponse<ExtTransportWorkShiftViewModel[]>> GetActualWorkShits(ExtGetActualWorkShitsRequest request);

        Task<CommonHttpResponse<ExtTransportWorkShiftDropdownViewModel[]>> GetWorkShiftsDropdownAsync(ExtGetWorkShitsDropdownRequest request);
        
        Task<CommonHttpResponse<ExtDictinaryDateRangeViewModel>> GetTransportSheduleDateRangeAsync(ExtGetTransportSheduleDateRangeRequest contract);
        #endregion

        #region Rent
        Task<CommonHttpResponse<ExtRentViewModel[]>> GetFilteredRentsAsync(ExtRentFilterRequest filter);

        Task<CommonHttpResponse<ExtRentViewModel[]>> GetRentsAsync(ExtGetRentRequest request);

        Task<CommonHttpResponse<ExtRentViewModel>> CreateRentAsync(ExtCreateRentRequest request);

        Task<CommonHttpResponse> DeleteRentsAsync(ExtDeleteRentRequest request);
        #endregion

        #region TransportDutyShift
        Task<CommonHttpResponse<ExtTransportDutyShiftViewModel[]>> GetFilteredTransportDutyShiftsAsync(ExtTransportDutyShiftFilterRequest filter);

        Task<CommonHttpResponse<ExtTransportDutyShiftViewModel[]>> GetTransportDutyShiftsAsync(ExtGetTransportDutyShiftRequest request);

        Task<CommonHttpResponse<ExtTransportDutyShiftViewModel>> CreateTransportDutyShiftAsync(ExtCreateTransportDutyShiftRequest request);

        Task<CommonHttpResponse> DeleteTransportDutyShiftsAsync(ExtDeleteTransportDutyShiftRequest request);
        #endregion
    }
}
