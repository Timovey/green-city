using ScheduleService.Contracts.DataContracts.WeekendWorkDates;
using ScheduleService.Contracts.ViewModels.WeekendWorkDates;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ScheduleService.Database.Interfaces
{
    public interface IWeekendWorkDatesStorage
    {
        Task<int> CreateWeekendWorkDateAsync(CreateWeekendWorkDateContract createContract);

        Task<IList<WeekendWorkDateViewModel>> GetActualWeekendWorkDatesAsync();
    }
}
