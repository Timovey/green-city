using ScheduleService.Contracts.DataContracts.RedDates;
using ScheduleService.Contracts.ViewModels.RedDates;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ScheduleService.Database.Interfaces
{
    public interface IRedDatesStorage
    {
        Task<int> CreateRedDateAsync(CreateRedDateContract createContract);

        Task<IList<RedDateViewModel>> GetActualRedDatesAsync();
    }
}
