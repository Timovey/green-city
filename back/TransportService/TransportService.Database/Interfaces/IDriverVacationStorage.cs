using GreenCity.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using TransportContracts.DataContracts.DriverVacation;
using TransportContracts.Filters;
using TransportContracts.ViewModels;

namespace TransportService.Database.Interfaces
{
    public interface IDriverVacationStorage
    {
        Task<DriverVacationViewModel> CreateDriverVacationAsync(CreateDriverVacationContract createContract);

        Task<DriverVacationViewModel> UpdateDriverVacationsAsync(UpdateDriverVacationContract updateContract);

        Task<IList<DriverVacationViewModel>> GetFilteredDriverVacationsAsync(DriverVacationFilterModel filter);

        Task<IList<DriverVacationViewModel>> GetDriverVacationsWithoutFilterAsync(CommonHttpFilter filter);

        Task<IList<DriverVacationViewModel>> GetDriverVacationsAsync(GetDriverVacationContract getContract);

        Task<bool> DeleteDriverVacationsAsync(DeleteDriverVacationContract deleteContract);
    }
}
