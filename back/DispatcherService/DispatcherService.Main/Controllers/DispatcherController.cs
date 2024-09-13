using DispatcherService.Contracts.DataContracts.AuxiliaryWorker;
using DispatcherService.Contracts.DataContracts.Brigade;
using DispatcherService.Contracts.Filters;
using DispatcherService.Contracts.Interfaces;
using DispatcherService.Contracts.ViewModels;
using DispatcherService.Core.BusinessLogic;
using DispatcherService.Core.BusinessLogic.Interface;
using GreenCity.Http;
using GreenCity.Http.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DispatcherService.Main.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    [ModelStateValidationActionFilter]
    public class DispatcherController : ControllerBase, IDispatcherApi
    {
        private readonly AuxiliaryWorkerLogic _auxiliaryWorkerLogic;
        private readonly IBrigadeLogic _brigadeLogic;
        private readonly ILogger<DispatcherController> _logger;

        public DispatcherController(
            ILogger<DispatcherController> logger,
            AuxiliaryWorkerLogic auxiliaryWorkerLogic,
            IBrigadeLogic brigadeLogic
        )
        {
            _logger = logger;
            _auxiliaryWorkerLogic = auxiliaryWorkerLogic;
            _brigadeLogic = brigadeLogic;
        }

        [HttpPost]
        public Task<CommonHttpResponse<IList<AuxiliaryWorkerViewModel>>> GetFilteredAuxiliaryWorkersAsync(AuxiliaryWorkerFilterModel filter)
        {
            return _auxiliaryWorkerLogic.GetFilteredAuxiliaryWorkersAsync(filter);
        }

        [HttpPost]
        public Task<CommonHttpResponse<AuxiliaryWorkerViewModel>> GetAuxiliaryWorkerAsync(GetAuxiliaryWorkerContract getContract)
        {
            return _auxiliaryWorkerLogic.GetAuxiliaryWorkerAsync(getContract);
        }

        [HttpPost]
        public Task<CommonHttpResponse<AuxiliaryWorkerViewModel>> CreateAuxiliaryWorkerAsync(CreateAuxiliaryWorkerContract createContract)
        {
            return _auxiliaryWorkerLogic.CreateAuxiliaryWorkerAsync(createContract);
        }

        [HttpPost]
        public Task<CommonHttpResponse<AuxiliaryWorkerViewModel>> UpdateAuxiliaryWorkerAsync(UpdateAuxiliaryWorkerContract updateContract)
        {
            return _auxiliaryWorkerLogic.UpdateAuxiliaryWorkerAsync(updateContract);
        }

        [HttpPost]
        public Task<CommonHttpResponse> DeleteAuxiliaryWorkerAsync(DeleteAuxiliaryWorkerContract deleteContract)
        {
            return _auxiliaryWorkerLogic.DeleteAuxiliaryWorkerAsync(deleteContract);
        }

        [HttpPost]
        public Task<CommonHttpResponse<int>> CreateBrigadeAsync(CreateBrigadeContract contract)
        {
            return _brigadeLogic.CreateBrigadeAsync(contract);
        }

        [HttpPost]
        public Task<CommonHttpResponse<IList<CommonBrigadeViewModel>>> GetFilteredBrigadesAsync(
            CommonBrigadesFilterModel filter
        )
        {
            return _brigadeLogic.GetFilteredBrigadesAsync(filter);
        }

        [HttpPost]
        public Task<CommonHttpResponse> DeleteBrigadesAsync(DeleteBrigadesContract contract)
        {
            return _brigadeLogic.DeleteBrigadesAsync(contract);
        }

        [HttpPost]
        public Task<CommonHttpResponse<IList<AvailableRoutesForCreateBrigadeViewModel>>> GetAvailableRoutesForCreateBrigadeAsync(
            GetAvailableRoutesForCreateBrigadeContract contract
        )
        {
            return _brigadeLogic.GetAvailableRoutesForCreateBrigadeAsync(contract);
        }
    }
}
