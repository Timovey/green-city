using GreenCity.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using TransportContracts.DataContracts.Driver2Transport;
using TransportContracts.Filters;
using TransportContracts.ViewModels;

namespace TransportService.Database.Interfaces
{
    public interface IDriver2TransportStorage
    {
        Task<Driver2TransportViewModel> CreateDriver2TransportAsync(CreateDriver2TransportContract createContract);

        Task<Driver2TransportViewModel> UpdateDriver2TransportAsync(UpdateDriver2TransportContract updateContract);

        Task<IList<Driver2TransportViewModel>> GetFilteredDrivers2TransportAsync(Driver2TransportFilterModel filter);

        Task<IList<Driver2TransportViewModel>> GetDrivers2TransportWithoutFilterAsync(CommonHttpFilter filter);

        Task<IList<Driver2TransportViewModel>> GetDrivers2TransportAsync(GetDriver2TransportContract getContract);

        Task<bool> DeleteDrivers2TransportAsync(DeleteDriver2TransportContract deleteContract);
    }
}
