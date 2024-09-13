using GreenCity.Http;
using Refit;
using SDContracts.Filter;
using SDContracts.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SDContracts.Interfaces
{
    public interface IContainer2ScheduleApi
    {
        [Post("/Container2Schedule/GetFilteredContainer2SchedulesForMissions")]
        Task<CommonHttpResponse<IList<Container2ScheduleForMissionsViewModel>>> GetFilteredContainer2SchedulesForMissionsAsync(
            [Body] Container2ScheduleFilterModel filter
        );       
    }
}
