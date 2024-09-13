using GreenCity.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using TransportContracts.DataContracts.Transport;
using TransportContracts.Filters;
using TransportContracts.ViewModels;

namespace TransportService.Database.Interfaces
{
    public interface ITransportStorage
    {
        Task<TransportViewModel> CreateTransportAsync(CreateTransportContract createContract);

        Task<TransportViewModel> UpdateTransportsAsync(UpdateTransportContract updateContract);

        Task<IList<TransportViewModel>> GetFilteredTransportsAsync(TransportFilterModel filter);

        Task<IList<TransportViewModel>> GetTransportsWithoutFilterAsync(CommonHttpFilter filter);

        Task<IList<TransportViewModel>> GetTransportsAsync(GetTransportContract getContract);

        Task<bool> DeleteTransportsAsync(DeleteTransportContract deleteContract);
    }
}
