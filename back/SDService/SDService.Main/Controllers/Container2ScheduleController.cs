using GreenCity.Http;
using GreenCity.Http.Attributes;
using Microsoft.AspNetCore.Mvc;
using SDContracts.Filter;
using SDContracts.Interfaces;
using SDContracts.ViewModels;
using SDService.Core.BusinessLogic;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SDService.Main.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    [ModelStateValidationActionFilter]
    public class Container2ScheduleController : Controller, IContainer2ScheduleApi
    {
        private readonly Container2ScheduleLogic _container2ScheduleLogic;

        public Container2ScheduleController(
            Container2ScheduleLogic container2ScheduleLogic
        )
        {
            _container2ScheduleLogic = container2ScheduleLogic;
        }

        [HttpPost]
        public Task<CommonHttpResponse<IList<Container2ScheduleForMissionsViewModel>>> GetFilteredContainer2SchedulesForMissionsAsync(Container2ScheduleFilterModel filter)
        {
            return _container2ScheduleLogic.GetFilteredContainer2SchedulesForMissionsAsync(filter);
        }
    }
}
