using GreenCity.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using TransportContracts.DataContracts.Rent;
using TransportContracts.Filters;
using TransportContracts.ViewModels;

namespace TransportService.Database.Interfaces
{
    public interface IRentStorage
    {
        Task<RentViewModel> CreateRentAsync(CreateRentContract createContract);

        Task<bool> DeleteRentsAsync(DeleteRentContract deleteContract);

        Task<IList<RentViewModel>> GetRentsWithoutFilterAsync(CommonHttpFilter filter);

        Task<IList<RentViewModel>> GetFilteredRentsAsync(RentFilterModel filter);

        Task<IList<RentViewModel>> GetRentsAsync(GetRentContract getContract);
    }
}
