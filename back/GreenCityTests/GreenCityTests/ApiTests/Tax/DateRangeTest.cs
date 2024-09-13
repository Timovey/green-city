using AuthContracts.DataContracts;
using AuthContracts.Interfaces;
using GreenCityTests.GenerateData;
using GreenCityTests.Heplers;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using TransportContracts.DataContracts.Driver;
using TransportContracts.DataContracts.Enums;
using TransportContracts.DataContracts.Transport;
using TransportContracts.DataContracts.TransportShedule;
using TransportContracts.DataContracts.TransportWorkShift;
using TransportContracts.Interfaces;
using TransportContracts.ViewModels;
using Xunit;

namespace GreenCityTests.ApiTests.Tax
{
    public class DateRangeTest : BaseApiTests
    {
        private readonly ITransportApi _transportApi;
        private readonly IDriverApi _driverApi;
        private readonly IAuthApi _authApi;
        private List<int> driverIds;
        private List<int> transportIds;
        public DateRangeTest() {
            var refitSettings = new RefitSettings
            {
                ContentSerializer = new SystemTextJsonContentSerializer(
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }
             )};
            _transportApi = RestService.For<ITransportApi>(Settings.TaxBaseUrl, refitSettings);
            _driverApi = RestService.For<IDriverApi>(Settings.TaxBaseUrl, refitSettings);
            _authApi = RestService.For<IAuthApi>(Settings.AuthBaseUrl, refitSettings);

        }

        [Fact]
        private async Task DateRangeTests__CheckInitialData()
        {
            try
            {
                var firstUserOrgs = await _authApi.GetOrgsContractAsync(new GetOrgsContract
                {
                    IsFilterByUserId = true,
                    UserId = 1
                });
                if (firstUserOrgs == null || !firstUserOrgs.Success)
                {
                    throw new Exception("Ошибка сервиса авторизации");
                }
                else if (firstUserOrgs.Success && (firstUserOrgs.Data.Count != 1 
                    || firstUserOrgs.Data.FirstOrDefault() == null
                    || firstUserOrgs.Data.FirstOrDefault().OrganizationId != 1))
                {
                    throw new Exception("У первого пользователя должена быть одна организация с id = 1");
                }

                var secondUserOrgs = await _authApi.GetOrgsContractAsync(new GetOrgsContract
                {
                    IsFilterByUserId = true,
                    UserId = 2
                });
                if (secondUserOrgs == null || !secondUserOrgs.Success)
                {
                    throw new Exception("Ошибка сервиса авторизации");
                }
                else if (secondUserOrgs.Success && (secondUserOrgs.Data.Count != 1
                    || secondUserOrgs.Data.FirstOrDefault() == null
                    || secondUserOrgs.Data.FirstOrDefault().OrganizationId != 2))
                {
                    throw new Exception("У первого пользователя должена быть одна организация с id = 2");
                }
                Assert.True(true);
            }
            catch (Exception ex)
            {
                Assert.True(false, ex.Message);
            }
        }

        private async Task GenerateTestData()
        {
            driverIds = new List<int>();
            transportIds = new List<int>();

            var drivers = GenerateTaxContracts.GetTestsDrivers();
            foreach (var driver in drivers)
            {
                var response = await _driverApi.CreateDriverAsync(driver);
                driverIds.Add(response.Data.DriverId);
            }
            var transports = GenerateTaxContracts.GetTestsTransports();
            foreach (var transport in transports)
            {
                var response = await _transportApi.CreateTransportAsync(transport);
                transportIds.Add(response.Data.Id);
            }

            var driver2Transports = GenerateTaxContracts.GetTestsDriver2Transports(driverIds, transportIds);
            foreach (var driver2Transport in driver2Transports)
            {
                var response = await _transportApi.CreateDriver2TransportAsync(driver2Transport);
                if (!response.Success)
                {
                    throw new Exception(response.Errors.FirstOrDefault());
                }
            }

            var transportShedules = GenerateTaxContracts.GetTestsTransportShedules(transportIds);
            foreach (var transportShedule in transportShedules)
            {
                var response = await _transportApi.CreateTransportSheduleAsync(transportShedule);
                if (!response.Success)
                {
                    throw new Exception(response.Errors.FirstOrDefault());
                }
            }

            var driverVacations = GenerateTaxContracts.GetTestsDriverVacations(driverIds);
            foreach (var driverVacation in driverVacations)
            {
                var response = await _driverApi.CreateDriverVacationAsync(driverVacation);
                if (!response.Success)
                {
                    throw new Exception(response.Errors.FirstOrDefault());
                }
            }

            var rents = GenerateTaxContracts.GetTestsRents(transportIds);
            foreach (var rent in rents)
            {
                var response = await _transportApi.CreateRentAsync(rent);
                if (!response.Success)
                {
                    throw new Exception(response.Errors.FirstOrDefault());
                }
            }

            var dutyShifts = GenerateTaxContracts.GetTestsDutyShifts(transportIds);
            foreach (var dutyShift in dutyShifts)
            {
                var response = await _transportApi.CreateTransportDutyShiftAsync(dutyShift);
                if (!response.Success)
                {
                    throw new Exception(response.Errors.FirstOrDefault());
                }
            }

            var transportRepairs = GenerateTaxContracts.GetTestsTransportRepars(transportIds);
            foreach (var transportRepair in transportRepairs)
            {
                var response = await _transportApi.CreateRepairCardAsync(transportRepair);
                if (!response.Success)
                {
                    throw new Exception(response.Errors.FirstOrDefault());
                }
            }
           
        }
        private async Task RemoveData()
        {
            var driverRes = await _driverApi.DeleteDriverAsync(new DeleteDriverContract
            {
                DriverIds = driverIds,
            });
            var transportRes = await _transportApi.DeleteTransportAsync(new DeleteTransportContract
            {
                TransportIds = transportIds,
            });
            if (!driverRes.Success) {
                    
                throw new Exception(driverRes.Errors.FirstOrDefault());
            }
            if (!transportRes.Success)
            {
                throw new Exception(transportRes.Errors.FirstOrDefault());
            }
           
        }

        [Fact]
        public async Task DateRangeTests__GetFirstTrasnportDateRangeOrg1()
        {
            try
            {
                await GenerateTestData();
                var contract = new GetTransportSheduleDateRangeContract
                {
                    DateFrom = DateTime.Parse("2023-03-01"),
                    DateTo = DateTime.Parse("2023-03-07"),
                    TransportId = transportIds[0],
                    UserId = 1,
                };
                var dateRange = await _transportApi.GetTransportSheduleDateRangeAsync(contract);

                var driversShedules = CheckDateRangeData.GetFirstTrasnportDateRangeOrg1__DriverShedules(driverIds);

                bool isDriverCorrect = CheckEqualsDriverDates(driversShedules,
                    dateRange.Data.DatesDrivers.Values.ToList());

                var transportShedules = CheckDateRangeData.GetFirstTrasnportDateRangeOrg1__TransportShedules();
                bool isTransportCorrect = CheckEqualsTransportDates(transportShedules,
                    dateRange.Data.DatesTransport.Values.ToList());

                Assert.Equal(isDriverCorrect && isTransportCorrect, true);
            }
            catch(Exception ex)
            {
                Assert.True(false, ex.Message);
            }
            finally
            {
                await RemoveData();
            }
        }

        [Fact]
        public async Task DateRangeTests__GetFirstTrasnportDateRangeOrg2()
        {
            try
            {
                await GenerateTestData();

                var contract = new GetTransportSheduleDateRangeContract
                {
                    DateFrom = DateTime.Parse("2023-03-01"),
                    DateTo = DateTime.Parse("2023-03-07"),
                    TransportId = transportIds[0],
                    UserId = 2,
                };
                var dateRange = await _transportApi.GetTransportSheduleDateRangeAsync(contract);

                var driversShedules = CheckDateRangeData.GetFirstTrasnportDateRangeOrg2__DriverShedules(driverIds);


                bool isDriverCorrect = CheckEqualsDriverDates(driversShedules,
                    dateRange.Data.DatesDrivers.Values.ToList());

                var transportShedules = CheckDateRangeData.GetFirstTrasnportDateRangeOrg2__TransportShedules();
                bool isTransportCorrect = CheckEqualsTransportDates(transportShedules,
                    dateRange.Data.DatesTransport.Values.ToList());

                Assert.Equal(isDriverCorrect && isTransportCorrect, true);
            }
            catch (Exception ex)
            {
                Assert.True(false, ex.Message);
            }
            finally
            {
                await RemoveData();
            }
        }

        [Fact]
        public async Task DateRangeTests__GetSecondTrasnportDateRange()
        {
            try
            {
                await GenerateTestData();

                var contract = new GetTransportSheduleDateRangeContract
                {
                    DateFrom = DateTime.Parse("2023-03-01"),
                    DateTo = DateTime.Parse("2023-03-07"),
                    TransportId = transportIds[1],
                    UserId = 2,
                };
                var dateRange = await _transportApi.GetTransportSheduleDateRangeAsync(contract);

                var driversShedules = CheckDateRangeData.GetSecondTrasnportDateRange__DriverShedules(driverIds);


                bool isDriverCorrect = CheckEqualsDriverDates(driversShedules,
                    dateRange.Data.DatesDrivers.Values.ToList());

                var transportShedules = CheckDateRangeData.GetSecondTrasnportDateRange__TransportShedules();

                bool isTransportCorrect = CheckEqualsTransportDates(transportShedules,
                    dateRange.Data.DatesTransport.Values.ToList());

                Assert.Equal(isDriverCorrect && isTransportCorrect, true);
            }
            catch (Exception ex)
            {
                Assert.True(false, ex.Message);
            }
            finally
            {
                await RemoveData();
            }
        }

        [Fact]
        public async Task DateRangeTests__GetFirstTrasnportAllWorkshifts()
        {
            try
            {
                await GenerateTestData();
                var dates = CheckDateRangeData.GetWorkshiftsDates();
                var workshifts = new Dictionary<DateTime,IList<TransportWorkShiftViewModel>>();
                foreach(var date in dates)
                {
                    var actualizeContract = new ActualizeWorkShitsContract
                    {
                        DateTime = date,
                        UserId = 1,
                    };
                    var actualizeRes = await _transportApi.ActualizeWorkShits(actualizeContract);
                    if(!actualizeRes.Success)
                    {
                        throw new Exception(actualizeRes.Errors.FirstOrDefault());
                    }
                    var workshiftContract = new GetActualWorkShitsContract
                    {
                        IsFilterByUser = false,
                        UserId = 1,
                        DateTime = date
                    };
                    var workshiftRes = await _transportApi.GetActualWorkShitsAsync(workshiftContract);
                    if (!workshiftRes.Success)
                    {
                        throw new Exception(actualizeRes.Errors.FirstOrDefault());
                    }
                    workshifts.Add(date, workshiftRes.Data);
                }

                var checkedWorkshifts = CheckDateRangeData.GetFirstTrasnportAllWorkshifts(transportIds);
                var isWorkshiftCorrect = CheckEqualsWorkshifts(workshifts, checkedWorkshifts);

                Assert.True(isWorkshiftCorrect);
            }
            catch (Exception ex)
            {
                Assert.True(false, ex.Message);
            }
            finally
            {
                await RemoveData();
            }
        }

        [Fact]
        public async Task DateRangeTests__GetFirstTrasnportWorkshiftsOrg1()
        {
            try
            {
                await GenerateTestData();
                var dates = CheckDateRangeData.GetWorkshiftsDates();
                var workshifts = new Dictionary<DateTime, IList<TransportWorkShiftViewModel>>();
                foreach (var date in dates)
                {
                    var actualizeContract = new ActualizeWorkShitsContract
                    {
                        DateTime = date,
                        UserId = 1,
                    };
                    var actualizeRes = await _transportApi.ActualizeWorkShits(actualizeContract);
                    if (!actualizeRes.Success)
                    {
                        throw new Exception(actualizeRes.Errors.FirstOrDefault());
                    }
                    var workshiftContract = new GetActualWorkShitsContract
                    {
                        IsFilterByUser = true,
                        UserId = 1,
                        DateTime = date
                    };
                    var workshiftRes = await _transportApi.GetActualWorkShitsAsync(workshiftContract);
                    if (!workshiftRes.Success)
                    {
                        throw new Exception(actualizeRes.Errors.FirstOrDefault());
                    }
                    workshifts.Add(date, workshiftRes.Data);
                }

                var checkedWorkshifts = CheckDateRangeData.GetFirstTrasnportWorkshiftsOrg1(transportIds);
                var isWorkshiftCorrect = CheckEqualsWorkshifts(workshifts, checkedWorkshifts);

                Assert.True(isWorkshiftCorrect);
            }
            catch (Exception ex)
            {
                Assert.True(false, ex.Message);
            }
            finally
            {
                await RemoveData();
            }
        }

        [Fact]
        public async Task DateRangeTests__GetFirstTrasnportWorkshiftsOrg2()
        {
            try
            {
                await GenerateTestData();
                var dates = CheckDateRangeData.GetWorkshiftsDates();
                var workshifts = new Dictionary<DateTime, IList<TransportWorkShiftViewModel>>();
                foreach (var date in dates)
                {
                    var actualizeContract = new ActualizeWorkShitsContract
                    {
                        DateTime = date,
                        UserId = 2,
                    };
                    var actualizeRes = await _transportApi.ActualizeWorkShits(actualizeContract);
                    if (!actualizeRes.Success)
                    {
                        throw new Exception(actualizeRes.Errors.FirstOrDefault());
                    }
                    var workshiftContract = new GetActualWorkShitsContract
                    {
                        IsFilterByUser = true,
                        UserId = 2,
                        DateTime = date
                    };
                    var workshiftRes = await _transportApi.GetActualWorkShitsAsync(workshiftContract);
                    if (!workshiftRes.Success)
                    {
                        throw new Exception(actualizeRes.Errors.FirstOrDefault());
                    }
                    workshifts.Add(date, workshiftRes.Data);
                }

                var checkedWorkshifts = CheckDateRangeData.GetFirstTrasnportWorkshiftsOrg2(transportIds);
                var isWorkshiftCorrect = CheckEqualsWorkshifts(workshifts, checkedWorkshifts);

                Assert.True(isWorkshiftCorrect);
            }
            catch (Exception ex)
            {
                Assert.True(false, ex.Message);
            }
            finally
            {
                await RemoveData();
            }
        }


        private bool CheckEqualsWorkshifts(Dictionary<DateTime, IList<TransportWorkShiftViewModel>> workshifts, Dictionary<DateTime, IList<TransportWorkShiftViewModel>> originalWorkshifts)
        {
            foreach(var key in workshifts.Keys)
            {
                if (workshifts[key].Count == originalWorkshifts[key].Count)
                {
                    foreach(var transportShift in workshifts[key])
                    {
                        var originalShift = originalWorkshifts[key].Select(el =>
                            (el.TransportId, el.DriverWorkDateType));
                        bool isExist = originalShift.Any(el =>
                            el.TransportId == transportShift.TransportId &&
                            el.DriverWorkDateType == transportShift.DriverWorkDateType);
                        if(!isExist)
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        private bool CheckEqualsDriverDates(List<Dictionary<int, DriverWorkDateType>> driversShedules, List<Dictionary<int, DriverWorkDateType>> driversOriginalShedules)
        {
            for (int i = 0; i < driversOriginalShedules.Count; i++)
            {
                if (driversOriginalShedules[i].Count == driversShedules[i].Count)
                {
                    foreach (var d in driversOriginalShedules[i])
                    {
                        if (!(driversShedules[i].ContainsKey(d.Key) && driversShedules[i][d.Key] ==
                            d.Value))
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        private bool CheckEqualsTransportDates(List<TransportWorkDateType> transportShedules, List<TransportWorkDateType> transportOriginalShedules)
        {

            for (int i = 0; i < transportOriginalShedules.Count; i++)
            {
                if (transportShedules[i] != transportOriginalShedules[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
