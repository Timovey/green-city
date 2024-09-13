using GreenCity.CommonInfrastructure.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TransportContracts.DataContracts.Transport;
using TransportContracts.Filters;
using TransportContracts.ViewModels;
using TransportService.Core.BusinessLogic;

namespace TransportsService.Main.Controllers
{
    //[Authorize(Roles = "admin, tax")]
    [ApiController]
    [Route("[controller]/[action]")]
    public class TransportController : Controller
    {
        private readonly TransportLogic _transportLogic;
        private readonly ILogger<TransportController> _logger;

        public TransportController(
            TransportLogic transportLogic,
            ILogger<TransportController> logger)
        {
            _transportLogic = transportLogic;
            _logger = logger;
        }

        [HttpGet]
        public async Task<CommonHttpResponse<TransportViewModel>> GetTransport([FromQuery] GetTransportContract contract)
        {
            _logger.LogInformation($"ACCEPT: get transport reqId: {contract.RequestId}, {DateTime.UtcNow}");
            return await _transportLogic.GetTransport(contract);
        }

        [HttpGet]
        public async Task<CommonHttpResponse<List<TransportViewModel>>> GetFiltered([FromQuery] TransportFilterModel filter)
        {
            _logger.LogInformation($"ACCEPT: get filter transport reqId: {filter.RequestId}, {DateTime.UtcNow}");
            return await _transportLogic.GetFilteredTransport(filter);
        }

        [HttpPost]
        public async Task<CommonHttpResponse> CreateTransport(CreateTransportContract contract)
        {
            _logger.LogInformation($"ACCEPT: create transport reqId: {contract.RequestId}, {DateTime.UtcNow}");
            return await _transportLogic.CreateTransport(contract);
        }

        [HttpPut]
        public async Task<CommonHttpResponse> UpdateTransport(UpdateTransportContract contract)
        {
            _logger.LogInformation($"ACCEPT: update transport reqId: {contract.RequestId}, {DateTime.UtcNow}");
            return await _transportLogic.UpdateTransport(contract);
        }

        [HttpDelete]
        public async Task<CommonHttpResponse> DeleteTransport([FromQuery] DeleteTransportContract contract)
        {
            _logger.LogInformation($"ACCEPT: delete transport reqId: {contract.RequestId}, {DateTime.UtcNow}");
            return await _transportLogic.DeleteTransport(contract);
        }
    }
}
