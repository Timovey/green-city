using GreenCity.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using TransportContracts.DataContracts.RepairCard;
using TransportContracts.Filters;
using TransportContracts.ViewModels;

namespace TransportService.Database.Interfaces
{
    public interface IRepairCardStorage
    {
        Task<RepairCardViewModel> CreateRepairCardAsync(CreateRepairCardContract createContract);

        Task<bool> DeleteRepairCardsAsync(DeleteRepairCardContract deleteContract);

        Task<IList<RepairCardViewModel>> GetRepairCardsWithoutFilterAsync(CommonHttpFilter filter);

        Task<IList<RepairCardViewModel>> GetFilteredRepairCardsAsync(RepairCardFilterModel filter);

        Task<IList<RepairCardViewModel>> GetRepairCardsAsync(GetRepairCardContract getContract);

        Task<RepairCardViewModel> UpdateRepairCardsAsync(UpdateRepairCardContract updateContract);
    }
}
