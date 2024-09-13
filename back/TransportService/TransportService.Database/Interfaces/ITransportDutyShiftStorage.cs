using GreenCity.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using TransportContracts.DataContracts.TransportDutyShift;
using TransportContracts.Filters;
using TransportContracts.ViewModels;

namespace TransportService.Database.Interfaces
{
    public interface ITransportDutyShiftStorage
    {
        Task<TransportDutyShiftViewModel> CreateTransportDutyShiftAsync(CreateTransportDutyShiftContract createContract);

        Task<bool> DeleteTransportDutyShiftsAsync(DeleteTransportDutyShiftContract deleteContract);

        Task<IList<TransportDutyShiftViewModel>> GetTransportDutyShiftsWithoutFilterAsync(CommonHttpFilter filter);

        Task<IList<TransportDutyShiftViewModel>> GetFilteredTransportDutyShiftsAsync(TransportDutyShiftFilterModel filter);

        Task<IList<TransportDutyShiftViewModel>> GetTransportDutyShiftsAsync(GetTransportDutyShiftContract getContract);
    }
}
