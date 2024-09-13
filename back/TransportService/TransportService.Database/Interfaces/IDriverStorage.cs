using GreenCity.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using TransportContracts.DataContracts.Driver;
using TransportContracts.Filters;
using TransportContracts.ViewModels;

namespace TransportService.Database.Interfaces
{
    public interface IDriverStorage
    {
        Task<DriverViewModel> CreateDriverAsync(CreateDriverContract createContract);

        Task<DriverViewModel> UpdateDriversAsync(UpdateDriverContract updateContract);

        Task<IList<DriverViewModel>> GetFilteredDriversAsync(DriverFilterModel filter);

        Task<IList<DriverViewModel>> GetDriversWithoutFilterAsync(CommonHttpFilter filter);

        Task<IList<DriverViewModel>> GetDriversAsync(GetDriverContract getContract);

        Task<bool> DeleteDriversAsync(DeleteDriverContract deleteContract);
    }
}
