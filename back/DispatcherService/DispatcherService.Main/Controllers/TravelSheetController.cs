using DispatcherService.Contracts.DataContracts.TravelSheet;
using DispatcherService.Contracts.Filters;
using DispatcherService.Contracts.Interfaces;
using DispatcherService.Contracts.ViewModels;
using DispatcherService.Core.BusinessLogic.Interface;
using GreenCity.Http;
using GreenCity.Http.Attributes;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DispatcherService.Main.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    [ModelStateValidationActionFilter]
    public class TravelSheetController : Controller, ITravelSheetApi
    {
        private readonly ITravelSheetLogic _travelSheetLogic;

        public TravelSheetController(
            ITravelSheetLogic travelSheetLogic
        )
        {
            _travelSheetLogic = travelSheetLogic;
        }

        [HttpPost]
        public Task<CommonHttpResponse<int>> CreateTravelSheetAsync(CreateTravelSheetContract contract)
        {
            return _travelSheetLogic.CreateTravelSheetAsync(contract);
        }

        [HttpPost]
        public Task<CommonHttpResponse<IList<CommonTravelSheetViewModel>>> GetCommonFilteredTravelSheetsAsync(
            CommonTravelSheetFilterModel filter
        )
        {
            return _travelSheetLogic.GetCommonFilteredTravelSheetsAsync(filter);
        }

        [HttpPost]
        public Task<CommonHttpResponse<IList<TravelSheetForFrontViewModel>>> GetTravelSheetByIdsForFrontAsync(
            IList<int> travelSheetIds
        )
        {
            return _travelSheetLogic.GetTravelSheetByIdsForFrontAsync(travelSheetIds);
        }

        [HttpPost]
        public Task<CommonHttpResponse> DeleteTravelSheetsAsync(
            DeleteTravelSheetsContract contract
        )
        {
            return _travelSheetLogic.DeleteTravelSheetsAsync(contract);
        }

        [HttpPost]
        public Task<CommonHttpResponse> FixTravelSheetDriveTimesAsync(
            FixTravelSheetDriveTimesContract contract
        )
        {
            return _travelSheetLogic.FixTravelSheetDriveTimesAsync(contract);
        }

        [HttpPost]
        public Task<CommonHttpResponse<TravelSheetsReportContract>> GetTravelSheetModelForPrintAsync(
            int travelSheetId
        )
        {
            return _travelSheetLogic.GetTravelSheetModelForPrintAsync(travelSheetId);
        }
    }
}
