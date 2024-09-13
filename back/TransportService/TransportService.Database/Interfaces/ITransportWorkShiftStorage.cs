using System.Collections.Generic;
using System.Threading.Tasks;
using TransportContracts.DataContracts.TransportWorkShift;
using TransportContracts.ViewModels;

namespace TransportService.Database.Interfaces
{
    public interface ITransportWorkShiftStorage
    {
        Task<int> CreateTransportWorkShiftAsync(CreateTransportWorkShiftContract createContract);

        Task<bool> DeleteAllTransportWorkShiftAsync();

        Task<bool> DeleteOnDateTransportWorkShiftAsync(ActualizeWorkShitsContract contract);

        Task<bool> DeleteTransportWorkShiftsAsync(DeleteTransportWorkShiftsContract contract);

        Task<bool> ChangeWorkshiftRouteStatusAsync(ChangeWorkshiftRouteStatusContract contract);

        Task<IList<TransportWorkShiftViewModel>> GetTransportWorkShiftsAsync(GetActualWorkShitsContract contract);

        Task<IList<TransportWorkShiftDropdownViewModel>> GetTransportWorkShiftsDropdownAsync(
            GetWorkShitsDropdownContract contract);

        Task<IList<TransportWorkShiftViewModel>> GetWorkShiftByIdsAsync(
            GetWorkShiftsByIdsContract contract,
            IList<int> orgIds = default
        );

        Task<IList<TransportWorkShiftViewModel>> GetWorkShiftsForDoneTaskListAsync(
            GetWorkShiftsForDoneTaskListContract contract);
    }
}