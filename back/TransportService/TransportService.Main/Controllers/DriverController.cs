using GreenCity.Http;
using GreenCity.Http.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using TransportContracts.DataContracts.Driver;
using TransportContracts.DataContracts.DriverVacation;
using TransportContracts.Filters;
using TransportContracts.Interfaces;
using TransportContracts.ViewModels;
using TransportService.Core.BusinessLogic;

namespace TransportService.Main.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    [ModelStateValidationActionFilter]
    public class DriverController : Controller, IDriverApi
    {
        private readonly DriverLogic _driverLogic;
        private readonly DriverVacationLogic _driverVacationLogic;
        private readonly ILogger<DriverController> _logger;

        public DriverController(
            ILogger<DriverController> logger,
            DriverLogic driverLogic,
            DriverVacationLogic driverVacationLogic
        )
        {
            _logger = logger;
            _driverLogic = driverLogic;
            _driverVacationLogic = driverVacationLogic;
        }

        [HttpPost]
        public async Task<CommonHttpResponse<IList<DriverViewModel>>> GetFilteredDriverAsync(DriverFilterModel filter)
        {
            return await _driverLogic.GetFilteredDriversAsync(filter);
        }

        [HttpPost]
        public async Task<CommonHttpResponse<IList<DriverViewModel>>> GetDriverAsync(GetDriverContract getContract)
        {
            return await _driverLogic.GetDriversAsync(getContract);
        }

        [HttpPost]
        public async Task<CommonHttpResponse<DriverViewModel>> CreateDriverAsync(CreateDriverContract createContract)
        {
            return await _driverLogic.CreateDriverAsync(createContract);
        }

        [HttpPost]
        public async Task<CommonHttpResponse<DriverViewModel>> UpdateDriverAsync(UpdateDriverContract updateContract)
        {
            return await _driverLogic.UpdateDriversAsync(updateContract);
        }

        [HttpPost]
        public async Task<CommonHttpResponse> DeleteDriverAsync(DeleteDriverContract deleteContract)
        {
            return await _driverLogic.DeleteDriversAsync(deleteContract);
        }

        [HttpPost]
        public async Task<CommonHttpResponse<IList<DriverVacationViewModel>>> GetFilteredDriverVacationAsync(DriverVacationFilterModel filter)
        {
            return await _driverVacationLogic.GetFilteredDriverVacationsAsync(filter);
        }

        [HttpPost]
        public async Task<CommonHttpResponse<IList<DriverVacationViewModel>>> GetDriverVacationAsync(GetDriverVacationContract getContract)
        {
            return await _driverVacationLogic.GetDriverVacationsAsync(getContract);
        }

        [HttpPost]
        public async Task<CommonHttpResponse<DriverVacationViewModel>> CreateDriverVacationAsync(CreateDriverVacationContract createContract)
        {
            return await _driverVacationLogic.CreateDriverVacationAsync(createContract);
        }

        [HttpPost]
        public async Task<CommonHttpResponse<DriverVacationViewModel>> UpdateDriverVacationAsync(UpdateDriverVacationContract updateContract)
        {
            return await _driverVacationLogic.UpdateDriverVacationsAsync(updateContract);
        }

        [HttpPost]
        public async Task<CommonHttpResponse> DeleteDriverVacationAsync(DeleteDriverVacationContract deleteContract)
        {
            return await _driverVacationLogic.DeleteDriverVacationsAsync(deleteContract);
        }
    }
}
