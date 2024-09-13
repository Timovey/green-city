using GreenCity.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using TransportContracts.DataContracts.TransportShedule;
using TransportContracts.Filters;
using TransportContracts.ViewModels;

namespace TransportService.Database.Interfaces
{
    public interface ITtansportSheduleStorage
    {
        Task<TransportSheduleViewModel> CreateTransportSheduleAsync(CreateTransportSheduleContract createContract);

        Task<TransportSheduleViewModel> UpdateTransportSheduleAsync(UpdateTransportSheduleContract updateContract);

        Task<TransportSheduleDateRangeViewModel> GetTransportSheduleDateRangeAsync(GetTransportSheduleDateRangeContract getContract);

        Task<IList<TransportSheduleViewModel>> GetFilteredTransportsSheduleAsync(TransportSheduleFilterModel filter);

        Task<IList<TransportSheduleViewModel>> GetTransportsSheduleWithoutFilterAsync(CommonHttpFilter filter);

        Task<IList<TransportSheduleViewModel>> GetTransportsSheduleAsync(GetTransportSheduleContract getContract);

        Task<bool> DeleteTransportsSheduleAsync(DeleteTransportSheduleContract deleteContract);
    }
}
