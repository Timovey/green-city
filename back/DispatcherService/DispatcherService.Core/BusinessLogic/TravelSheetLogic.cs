using AuthContracts.DataContracts;
using AuthContracts.Interfaces;
using AuthContracts.ViewModels;
using DispatcherService.Contracts.DataContracts.TravelSheet;
using DispatcherService.Contracts.Filters;
using DispatcherService.Contracts.ViewModels;
using DispatcherService.Core.BusinessLogic.Interface;
using DispatcherService.Core.Extensions;
using DispatcherService.Database.Interfaces;
using DispatcherService.Database.Models;
using GreenCity.Http;
using GreenCity.Http.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TransportContracts.DataContracts.Driver;
using TransportContracts.DataContracts.Transport;
using TransportContracts.Interfaces;
using TransportContracts.ViewModels;

namespace DispatcherService.Core.BusinessLogic
{
    public class TravelSheetLogic : ITravelSheetLogic
    {
        private readonly ITravelSheetStorage _travelSheetStorage;
        private readonly IAuthApi _authApi;
        private readonly ITransportApi _transportApi;
        private readonly IDriverApi _driverApi;

        public TravelSheetLogic(
            ITravelSheetStorage travelSheetStorage,
            IAuthApi authApi,
            ITransportApi transportApi,
            IDriverApi driverApi
        )
        {
            _travelSheetStorage = travelSheetStorage;
            _authApi = authApi;
            _transportApi = transportApi;
            _driverApi = driverApi;
        }

        public async Task<CommonHttpResponse<int>> CreateTravelSheetAsync(CreateTravelSheetContract contract)
        {
            if (contract == default)
                return CommonHttpHelper.BuildErrorResponse<int>(initialError: "Пустой запрос");

            var lastNumberPerOrg = await _travelSheetStorage.GetLastNumberPerOrgByOrgIdAsync(contract.OrgId);

            var createResult = await _travelSheetStorage.CreateTravelSheetAsync(
                CreateTravelSheetModel(contract, ++lastNumberPerOrg),
                contract.UserId,
                contract.UserName
            );

            return CommonHttpHelper.BuildSuccessResponse(createResult, HttpStatusCode.OK);
        }

        public async Task<CommonHttpResponse> FixTravelSheetDriveTimesAsync(
            FixTravelSheetDriveTimesContract contract
        )
        {
            if (contract == default)
                return CommonHttpHelper.BuildErrorResponse(initialError: "Пустой запрос");

            await _travelSheetStorage.FixTravelSheetDriveTimesAsync(contract);

            return CommonHttpHelper.BuildSuccessResponse();
        }

        public async Task<CommonHttpResponse<IList<CommonTravelSheetViewModel>>> GetCommonFilteredTravelSheetsAsync(
            CommonTravelSheetFilterModel filter
        )
        {
            if (filter == default)
                return CommonHttpHelper.BuildErrorResponse<IList<CommonTravelSheetViewModel>>(initialError: "Пустой запрос");

            var travelSheets = await _travelSheetStorage.GetCommonFilteredTravelSheetsAsync(filter);

            if (travelSheets == default)
                return CommonHttpHelper.BuildErrorResponse<IList<CommonTravelSheetViewModel>>(initialError: "Ошибка при получении данных из бд");

            //!_! Если ничего не нашли, то отдаем пустой список
            if (travelSheets.Count == 0)
            {
                return CommonHttpHelper.BuildSuccessResponse<IList<CommonTravelSheetViewModel>>(new List<CommonTravelSheetViewModel>(), HttpStatusCode.OK);
            }

            var allOrgsInfo = await _authApi.GetOrgsContractAsync(new GetOrgsContract { IsFilterByUserId = false });

            if (!allOrgsInfo.Success || allOrgsInfo.Data == default)
            {
                return CommonHttpHelper.BuildErrorResponse<IList<CommonTravelSheetViewModel>>(
                    initialError: "Ошибка при получении организаций",
                    extErrors: allOrgsInfo.Errors?.ToList()
                );
            }

            var transportInfo = await _transportApi.GetTransportAsync(new GetTransportContract
            {
                TransportIds = travelSheets.Select(x => x.CarId).Distinct().ToList(),
                ReturnEmptyResultIfNotFound = true
            });

            if (!transportInfo.Success || transportInfo.Data == default)
            {
                return CommonHttpHelper.BuildErrorResponse<IList<CommonTravelSheetViewModel>>(
                    initialError: "Ошибка при получении машин",
                    extErrors: transportInfo.Errors?.ToList()
                );
            }

            var driverInfo = await _driverApi.GetDriverAsync(new GetDriverContract
            {
                DriverIds = travelSheets.Select(x => x.DriverId).Distinct().ToList(),
                ReturnEmptyResultIfNotFound = true
            });

            if (!driverInfo.Success || driverInfo.Data == default)
            {
                return CommonHttpHelper.BuildErrorResponse<IList<CommonTravelSheetViewModel>>(
                    initialError: "Ошибка при получении водителей",
                    extErrors: driverInfo.Errors?.ToList()
                );
            }

            var result = CreateCommonTravelSheetViewModels(
                travelSheets,
                allOrgsInfo.Data.GroupBy(x => x.OrganizationId).ToDictionary(x => x.Key, value => value.FirstOrDefault()),
                transportInfo.Data.GroupBy(x => x.Id).ToDictionary(x => x.Key, value => value.FirstOrDefault()),
                driverInfo.Data.GroupBy(x => x.DriverId).ToDictionary(x => x.Key, value => value.FirstOrDefault())
            );

            return CommonHttpHelper.BuildSuccessResponse<IList<CommonTravelSheetViewModel>>(result.ToList(), HttpStatusCode.OK);
        }

        public async Task<CommonHttpResponse> DeleteTravelSheetsAsync(
            DeleteTravelSheetsContract contract
        )
        {
            if (contract == default)
                return CommonHttpHelper.BuildErrorResponse(initialError: "Пустой запрос");

            var deleteResult = await _travelSheetStorage.DeleteTravelSheetsAsync(
                contract.TravelSheetIds,
                contract.UserId,
                contract.UserName
            );

            if (!deleteResult)
            {
                return CommonHttpHelper.BuildErrorResponse(initialError: "Ошибка при получении удалении ПЛ в бд!");
            }

            return CommonHttpHelper.BuildSuccessResponse(deleteResult);
        }

        public async Task<CommonHttpResponse<TravelSheetsReportContract>> GetTravelSheetModelForPrintAsync(
            int travelSheetId
        )
        {
            var travelSheet = (await _travelSheetStorage.GetTravelSheetByIds(new List<int> { travelSheetId })).FirstOrDefault();

            if (travelSheet == default)
                return CommonHttpHelper.BuildErrorResponse<TravelSheetsReportContract>(initialError: "Ошибка при получении данных из бд");

            var allOrgsInfo = await _authApi.GetOrgsContractAsync(new GetOrgsContract { IsFilterByUserId = false });

            if (!allOrgsInfo.Success || allOrgsInfo.Data == default)
            {
                return CommonHttpHelper.BuildErrorResponse<TravelSheetsReportContract>(
                    initialError: "Ошибка при получении организаций",
                    extErrors: allOrgsInfo.Errors?.ToList()
                );
            }

            var transportInfo = await _transportApi.GetTransportAsync(new GetTransportContract
            {
                TransportIds = new List<int> { travelSheet.CarId }
            });

            if (!transportInfo.Success || transportInfo.Data == default || transportInfo.Data.Count != 1)
            {
                return CommonHttpHelper.BuildErrorResponse<TravelSheetsReportContract>(
                    initialError: "Ошибка при получении машин",
                    extErrors: transportInfo.Errors?.ToList()
                );
            }

            var driverInfo = await _driverApi.GetDriverAsync(new GetDriverContract
            {
                DriverIds = new List<int> { travelSheet.DriverId }
            });

            if (!driverInfo.Success || driverInfo.Data == default || driverInfo.Data.Count != 1)
            {
                return CommonHttpHelper.BuildErrorResponse<TravelSheetsReportContract>(
                    initialError: "Ошибка при получении водителей",
                    extErrors: driverInfo.Errors?.ToList()
                );
            }

            var result = GetTravelSheetsReportContract(
                travelSheet,
                allOrgsInfo.Data.Where(x => x.OrganizationId == travelSheet.OrgId).Single(),
                transportInfo.Data.Single(),
                driverInfo.Data.Single()
            );

            return CommonHttpHelper.BuildSuccessResponse(result, HttpStatusCode.OK);
        }

        public async Task<CommonHttpResponse<IList<TravelSheetForFrontViewModel>>> GetTravelSheetByIdsForFrontAsync(IList<int> travelSheetIds)
        {
            if (travelSheetIds == default || travelSheetIds.Count == 0)
                return CommonHttpHelper.BuildErrorResponse<IList<TravelSheetForFrontViewModel>>(initialError: "Пустой запрос");

            var travelSheets = await _travelSheetStorage.GetTravelSheetByIds(travelSheetIds);

            if (travelSheets == default || travelSheets.Count == 0)
            {
                return CommonHttpHelper.BuildErrorResponse<IList<TravelSheetForFrontViewModel>>(
                    initialError: "Данные не найдены"
                );
            }

            var result = travelSheets.Select(x => new TravelSheetForFrontViewModel
            {
                Id = x.Id,
                Date = x.Date.Date,
                CarId = x.CarId,
                DriverId = x.DriverId,
                OrgId = x.OrgId,
                StartDriveTime = x.StartDriveTime,
                FinishDriveTime = x.FinishDriveTime
            }).ToList();

            return CommonHttpHelper.BuildSuccessResponse<IList<TravelSheetForFrontViewModel>>(result, HttpStatusCode.OK);
        }

        private TravelSheetsReportContract GetTravelSheetsReportContract(
            TravelSheet travelSheet,
            OrganizationApiViewModel orgInfo,
            TransportViewModel transportInfo,
            DriverViewModel driverInfo
        )
        {
            var dateInfo = travelSheet.Date.GetRussianDateInfo();

            return new TravelSheetsReportContract
            {
                DateDay = dateInfo.Day,
                DateMonth = dateInfo.Month,
                DateYear = dateInfo.Year,
                WayBillId = travelSheet.NumberToShow,
                OrganizationInfo = $"{orgInfo.Name}, {orgInfo.Address} тел: {orgInfo.Phone}, ОГРН: {orgInfo.Ogrn}",
                TransportName = transportInfo.Name,
                TransportNumber = transportInfo.RegNumber,
                DrivingLicenseNumber = driverInfo.DriversLicense,
                DriverFullName = $"{driverInfo.DriverSurname} {driverInfo.DriverName} {driverInfo.DriverPatronymic}",
                DriverShortName = $"{driverInfo.DriverSurname} {driverInfo.DriverName?.FirstOrDefault()} {driverInfo.DriverPatronymic?.FirstOrDefault()}"
            };
        }

        private IEnumerable<CommonTravelSheetViewModel> CreateCommonTravelSheetViewModels(
            IList<TravelSheet> travelSheets,
            IDictionary<int, OrganizationApiViewModel> orgDict,
            IDictionary<int, TransportViewModel> transportDict,
            IDictionary<int, DriverViewModel> driverDict
        )
        {
            foreach (var travelSheet in travelSheets)
            {
                if (orgDict.TryGetValue(travelSheet.OrgId, out var orgResult) &&
                    transportDict.TryGetValue(travelSheet.CarId, out var transportResult) &&
                    driverDict.TryGetValue(travelSheet.DriverId, out var driverResult))
                {
                    yield return new CommonTravelSheetViewModel
                    {
                        Id = travelSheet.Id,
                        NumberToShow = travelSheet.NumberToShow,
                        OrgInfo = orgResult.Name,
                        CarInfo = $"{transportResult.Name} {transportResult.RegNumber}",
                        DriverInfo = $"{driverResult.DriverSurname} {driverResult.DriverName} {driverResult.DriverPatronymic}",
                        StartDriveTime = travelSheet.StartDriveTime,
                        FinishDriveTime = travelSheet.FinishDriveTime
                    };
                }
            }
        }

        private TravelSheet CreateTravelSheetModel(CreateTravelSheetContract contract, int numberPerOrg)
        {
            return new TravelSheet
            {
                NumberToShow = numberPerOrg.ToString(),
                NumberPerOrg = numberPerOrg,
                Date = contract.Date.Date,
                CarId = contract.CarId,
                DriverId = contract.DriverId,
                OrgId = contract.OrgId
            };
        }
    }
}
