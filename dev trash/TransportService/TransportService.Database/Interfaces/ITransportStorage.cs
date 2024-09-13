using System.Collections.Generic;
using System.Threading.Tasks;
using TransportContracts.DataContracts.Transport;
using TransportContracts.Filters;
using TransportContracts.ViewModels;

namespace TransportService.Database.Interfaces
{
    public interface ITransportStorage
    {
        Task CreateTransport(CreateTransportContract transport);

        Task<bool> UpdateTransport(UpdateTransportContract transport);

        Task<bool> DeleteTransport(int transportId);

        Task<TransportViewModel> GetTransport(int transportId);

        Task<List<TransportViewModel>> GetFilteredList(TransportFilterModel filter);
    }
}
