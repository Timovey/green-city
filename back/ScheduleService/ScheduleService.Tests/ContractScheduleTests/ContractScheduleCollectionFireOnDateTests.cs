using Moq;
using ScheduleService.Contracts.DataContracts.ContractSchedule;
using ScheduleService.Contracts.DataContracts.ContractSchedule.Mission;
using ScheduleService.Core.BusinessLogic;
using ScheduleService.Core.Services.ContractSchedule;
using ScheduleService.Core.Services.DateRange;
using ScheduleService.Database.Interfaces;
using ScheduleService.Tests.Common;
using ScheduleService.Tests.DataProvider;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace ScheduleService.Tests.ContractScheduleTests
{
    public class ContractScheduleCollectionFireOnDateTests : ScheduleTestsBase
    {
        private readonly Mock<IRedDatesStorage> _mockRedDatesStorage;
        private readonly Mock<IWeekendWorkDatesStorage> _mockWeekendWorkDatesStorage;

        public ContractScheduleCollectionFireOnDateTests()
        {
            _mockRedDatesStorage = new Mock<IRedDatesStorage>();
            _mockWeekendWorkDatesStorage = new Mock<IWeekendWorkDatesStorage>();
        }

        [Fact]
        public async Task IsContractScheduleCollectionFireOnDate__WhenInputDataScheduleDatesWithOneSlot__ThenTrueReturned()
        {
            _mockRedDatesStorage.Setup(x => x.GetActualRedDatesAsync()).Returns(Task.FromResult(RedDatesDataProvider.GetEmptyDates()));
            _mockWeekendWorkDatesStorage.Setup(x => x.GetActualWeekendWorkDatesAsync()).Returns(Task.FromResult(WeekendWorkDatesDataProvider.GetEmptyDates()));

            var contractScheduleCollection = new List<SSContractScheduleCollectionModel>
            {
                new SSContractScheduleCollectionModel
                {
                    ContractSchedule = new SSContractScheduleModel
                    {
                        ScheduleDates = new List<DateTime> { DateTime.Parse("2023-01-06") },
                        ExcludeDates = null,
                        StepInitialDateSchedule = null,
                        Crons = null,
                        ExcludeCrons = null,
                        ContractScheduleSettings = null
                    },
                    CreateDate = DateTime.UtcNow,
                    StartDate = DateTime.Parse("2023-01-05"),
                    EndDate = DateTime.Parse("2023-01-07"),
                }
            };

            var isFireRequest = new GetFireInfoOnDateContractScheduleCollectionContract
            {
                NotSlicedContractScheduleCollection = contractScheduleCollection,
                Date = DateTime.Parse("2023-01-06")
            };

            var contractScheduleLogicPerforemer = GetCommonLogic();

            var actual = await contractScheduleLogicPerforemer.IsContractScheduleCollectionFireOnDateAsync(isFireRequest);
            AssertSuccessNotNull(actual);

            var expected = true;
            Assert.Equal(expected, actual.Data);
        }

        [Fact]
        public async Task IsContractScheduleCollectionFireOnDate__WhenInputDataInExcludeDatesWithOneSlot__ThenFalseReturned()
        {
            _mockRedDatesStorage.Setup(x => x.GetActualRedDatesAsync()).Returns(Task.FromResult(RedDatesDataProvider.GetEmptyDates()));
            _mockWeekendWorkDatesStorage.Setup(x => x.GetActualWeekendWorkDatesAsync()).Returns(Task.FromResult(WeekendWorkDatesDataProvider.GetEmptyDates()));

            var contractScheduleCollection = new List<SSContractScheduleCollectionModel>
            {
                new SSContractScheduleCollectionModel
                {
                    ContractSchedule = new SSContractScheduleModel
                    {
                        ScheduleDates = null,
                        ExcludeDates = new List<DateTime> { DateTime.UtcNow },
                        StepInitialDateSchedule = null,
                        Crons = null,
                        ExcludeCrons = null,
                        ContractScheduleSettings = null
                    },
                    CreateDate = DateTime.UtcNow,
                    StartDate = DateTime.UtcNow.AddDays(-1),
                    EndDate = DateTime.UtcNow.AddDays(1),
                }
            };

            var isFireRequest = new GetFireInfoOnDateContractScheduleCollectionContract
            {
                NotSlicedContractScheduleCollection = contractScheduleCollection,
                Date = DateTime.UtcNow
            };

            var contractScheduleLogicPerforemer = GetCommonLogic();

            var actual = await contractScheduleLogicPerforemer.IsContractScheduleCollectionFireOnDateAsync(isFireRequest);
            AssertSuccessNotNull(actual);

            var expected = false;
            Assert.Equal(expected, actual.Data);
        }

        [Fact]
        public async Task IsContractScheduleCollectionFireOnDate__WhenInputDataInExcludeAndIncludeDatesWithOneSlot__ThenFalseReturned()
        {
            _mockRedDatesStorage.Setup(x => x.GetActualRedDatesAsync()).Returns(Task.FromResult(RedDatesDataProvider.GetEmptyDates()));
            _mockWeekendWorkDatesStorage.Setup(x => x.GetActualWeekendWorkDatesAsync()).Returns(Task.FromResult(WeekendWorkDatesDataProvider.GetEmptyDates()));

            var contractScheduleCollection = new List<SSContractScheduleCollectionModel>
            {
                new SSContractScheduleCollectionModel
                {
                    ContractSchedule = new SSContractScheduleModel
                    {
                        ScheduleDates = new List<DateTime> { DateTime.UtcNow },
                        ExcludeDates = new List<DateTime> { DateTime.UtcNow },
                        StepInitialDateSchedule = null,
                        Crons = null,
                        ExcludeCrons = null,
                        ContractScheduleSettings = null
                    },
                    CreateDate = DateTime.UtcNow,
                    StartDate = DateTime.UtcNow.AddDays(-1),
                    EndDate = DateTime.UtcNow.AddDays(1),
                }
            };

            var isFireRequest = new GetFireInfoOnDateContractScheduleCollectionContract
            {
                NotSlicedContractScheduleCollection = contractScheduleCollection,
                Date = DateTime.UtcNow
            };

            var contractScheduleLogicPerforemer = GetCommonLogic();

            var actual = await contractScheduleLogicPerforemer.IsContractScheduleCollectionFireOnDateAsync(isFireRequest);
            AssertSuccessNotNull(actual);

            var expected = false;
            Assert.Equal(expected, actual.Data);
        }

        [Fact]
        public async Task IsContractScheduleCollectionFireOnDate__WhenInputDataInExcludeAndIncludeDatesAndInUrgentDatesWithOneSlot__ThenTrueReturned()
        {
            _mockRedDatesStorage.Setup(x => x.GetActualRedDatesAsync()).Returns(Task.FromResult(RedDatesDataProvider.GetEmptyDates()));
            _mockWeekendWorkDatesStorage.Setup(x => x.GetActualWeekendWorkDatesAsync()).Returns(Task.FromResult(WeekendWorkDatesDataProvider.GetEmptyDates()));

            var contractScheduleCollection = new List<SSContractScheduleCollectionModel>
            {
                new SSContractScheduleCollectionModel
                {
                    ContractSchedule = new SSContractScheduleModel
                    {
                        ScheduleDates = new List<DateTime> { DateTime.UtcNow },
                        ExcludeDates = new List<DateTime> { DateTime.UtcNow },
                        StepInitialDateSchedule = null,
                        Crons = null,
                        ExcludeCrons = null,
                        ContractScheduleSettings = null
                    },
                    CreateDate = DateTime.UtcNow,
                    StartDate = DateTime.UtcNow.AddDays(-1),
                    EndDate = DateTime.UtcNow.AddDays(1),
                }
            };

            var isFireRequest = new GetFireInfoOnDateContractScheduleCollectionContract
            {
                NotSlicedContractScheduleCollection = contractScheduleCollection,
                Date = DateTime.UtcNow,
                UrgentDates = new List<DateTime> { DateTime.UtcNow }
            };

            var contractScheduleLogicPerforemer = GetCommonLogic();

            var actual = await contractScheduleLogicPerforemer.IsContractScheduleCollectionFireOnDateAsync(isFireRequest);
            AssertSuccessNotNull(actual);

            var expected = true;
            Assert.Equal(expected, actual.Data);
        }

        [Fact]
        public async Task IsContractScheduleCollectionFireOnDate__WhenInputDateInExcludeAndIncludeDatesAndInUrgentAndUrgentCancelDatesWithOneSlot__ThenFalseReturned()
        {
            _mockRedDatesStorage.Setup(x => x.GetActualRedDatesAsync()).Returns(Task.FromResult(RedDatesDataProvider.GetEmptyDates()));
            _mockWeekendWorkDatesStorage.Setup(x => x.GetActualWeekendWorkDatesAsync()).Returns(Task.FromResult(WeekendWorkDatesDataProvider.GetEmptyDates()));

            var contractScheduleCollection = new List<SSContractScheduleCollectionModel>
            {
                new SSContractScheduleCollectionModel
                {
                    ContractSchedule = new SSContractScheduleModel
                    {
                        ScheduleDates = new List<DateTime> { DateTime.UtcNow },
                        ExcludeDates = new List<DateTime> { DateTime.UtcNow },
                        StepInitialDateSchedule = null,
                        Crons = null,
                        ExcludeCrons = null,
                        ContractScheduleSettings = null
                    },
                    CreateDate = DateTime.UtcNow,
                    StartDate = DateTime.UtcNow.AddDays(-1),
                    EndDate = DateTime.UtcNow.AddDays(1),
                }
            };

            var isFireRequest = new GetFireInfoOnDateContractScheduleCollectionContract
            {
                NotSlicedContractScheduleCollection = contractScheduleCollection,
                Date = DateTime.UtcNow,
                UrgentDates = new List<DateTime> { DateTime.UtcNow },
                UrgentCancelDates = new List<DateTime> { DateTime.UtcNow }
            };

            var contractScheduleLogicPerforemer = GetCommonLogic();

            var actual = await contractScheduleLogicPerforemer.IsContractScheduleCollectionFireOnDateAsync(isFireRequest);
            AssertSuccessNotNull(actual);

            var expected = false;
            Assert.Equal(expected, actual.Data);
        }

        [Fact]
        public async Task IsContractScheduleCollectionFireOnDate__WhenInputDateInExcludeCronsWithOneSlot__ThenFalseReturned()
        {
            _mockRedDatesStorage.Setup(x => x.GetActualRedDatesAsync()).Returns(Task.FromResult(RedDatesDataProvider.GetEmptyDates()));
            _mockWeekendWorkDatesStorage.Setup(x => x.GetActualWeekendWorkDatesAsync()).Returns(Task.FromResult(WeekendWorkDatesDataProvider.GetEmptyDates()));

            var contractScheduleCollection = new List<SSContractScheduleCollectionModel>
            {
                new SSContractScheduleCollectionModel
                {
                    ContractSchedule = new SSContractScheduleModel
                    {
                        ScheduleDates = null,
                        ExcludeDates = null,
                        StepInitialDateSchedule = null,
                        Crons = null,
                        ExcludeCrons = null,
                        ContractScheduleSettings = null
                    },
                    CreateDate = DateTime.UtcNow,
                    StartDate = DateTime.UtcNow.AddDays(-1),
                    EndDate = DateTime.UtcNow.AddDays(1),
                }
            };

            var isFireRequest = new GetFireInfoOnDateContractScheduleCollectionContract
            {
                NotSlicedContractScheduleCollection = contractScheduleCollection,
                Date = DateTime.UtcNow,
                UrgentDates = new List<DateTime> { DateTime.UtcNow },
                UrgentCancelDates = new List<DateTime> { DateTime.UtcNow }
            };

            var contractScheduleLogicPerforemer = GetCommonLogic();

            var actual = await contractScheduleLogicPerforemer.IsContractScheduleCollectionFireOnDateAsync(isFireRequest);
            AssertSuccessNotNull(actual);

            var expected = false;
            Assert.Equal(expected, actual.Data);
        }

        [Fact]
        public async Task IsContractScheduleCollectionFireOnDate__WhenInputDateInExcludeDatesAndNotInUrgentDatesWithOneSlot__ThenFalseReturned()
        {
            _mockRedDatesStorage.Setup(x => x.GetActualRedDatesAsync()).Returns(Task.FromResult(RedDatesDataProvider.GetEmptyDates()));
            _mockWeekendWorkDatesStorage.Setup(x => x.GetActualWeekendWorkDatesAsync()).Returns(Task.FromResult(WeekendWorkDatesDataProvider.GetEmptyDates()));

            var contractScheduleCollection = new List<SSContractScheduleCollectionModel>
            {
                new SSContractScheduleCollectionModel
                {
                    ContractSchedule = new SSContractScheduleModel
                    {
                        ScheduleDates = null,
                        ExcludeDates = new List<DateTime> { DateTime.UtcNow },
                        StepInitialDateSchedule = null,
                        Crons = null,
                        ExcludeCrons = null,
                        ContractScheduleSettings = null
                    },
                    CreateDate = DateTime.UtcNow,
                    StartDate = DateTime.UtcNow.AddDays(-1),
                    EndDate = DateTime.UtcNow.AddDays(1),
                }
            };

            var isFireRequest = new GetFireInfoOnDateContractScheduleCollectionContract
            {
                NotSlicedContractScheduleCollection = contractScheduleCollection,
                Date = DateTime.UtcNow,
                UrgentDates = new List<DateTime> { DateTime.UtcNow.AddDays(-2) }
            };

            var contractScheduleLogicPerforemer = GetCommonLogic();

            var actual = await contractScheduleLogicPerforemer.IsContractScheduleCollectionFireOnDateAsync(isFireRequest);
            AssertSuccessNotNull(actual);

            var expected = false;
            Assert.Equal(expected, actual.Data);
        }

        [Fact]
        public async Task IsContractScheduleCollectionFireOnDate__WhenInputDateNotInSlotsWithThreeSlots__ThenFalseReturned()
        {
            _mockRedDatesStorage.Setup(x => x.GetActualRedDatesAsync()).Returns(Task.FromResult(RedDatesDataProvider.GetEmptyDates()));
            _mockWeekendWorkDatesStorage.Setup(x => x.GetActualWeekendWorkDatesAsync()).Returns(Task.FromResult(WeekendWorkDatesDataProvider.GetEmptyDates()));

            var contractScheduleCollection = new List<SSContractScheduleCollectionModel>
            {
                new SSContractScheduleCollectionModel
                {
                    ContractSchedule = new SSContractScheduleModel
                    {
                        ScheduleDates = new List<DateTime> { DateTime.UtcNow },
                        ExcludeDates = null,
                        StepInitialDateSchedule = null,
                        Crons = null,
                        ExcludeCrons = null,
                        ContractScheduleSettings = null
                    },
                    CreateDate = DateTime.UtcNow,
                    StartDate = DateTime.UtcNow.AddDays(-8),
                    EndDate = DateTime.UtcNow.AddDays(-7),
                },
                new SSContractScheduleCollectionModel
                {
                    ContractSchedule = new SSContractScheduleModel
                    {
                        ScheduleDates = new List<DateTime> { DateTime.UtcNow },
                        ExcludeDates = null,
                        StepInitialDateSchedule = null,
                        Crons = null,
                        ExcludeCrons = null,
                        ContractScheduleSettings = null
                    },
                    CreateDate = DateTime.UtcNow,
                    StartDate = DateTime.UtcNow.AddDays(-6),
                    EndDate = DateTime.UtcNow.AddDays(-5),
                },
                new SSContractScheduleCollectionModel
                {
                    ContractSchedule = new SSContractScheduleModel
                    {
                        ScheduleDates = new List<DateTime> { DateTime.UtcNow },
                        ExcludeDates = null,
                        StepInitialDateSchedule = null,
                        Crons = null,
                        ExcludeCrons = null,
                        ContractScheduleSettings = null
                    },
                    CreateDate = DateTime.UtcNow,
                    StartDate = DateTime.UtcNow.AddDays(-4),
                    EndDate = DateTime.UtcNow.AddDays(-3),
                }
            };

            var isFireRequest = new GetFireInfoOnDateContractScheduleCollectionContract
            {
                NotSlicedContractScheduleCollection = contractScheduleCollection,
                Date = DateTime.UtcNow,
                UrgentDates = new List<DateTime> { DateTime.UtcNow.AddDays(-2) }
            };

            var contractScheduleLogicPerforemer = GetCommonLogic();

            var actual = await contractScheduleLogicPerforemer.IsContractScheduleCollectionFireOnDateAsync(isFireRequest);
            AssertSuccessNotNull(actual);

            var expected = false;
            Assert.Equal(expected, actual.Data);
        }

        [Fact]
        public async Task IsContractScheduleCollectionFireOnDate__WhenInputDateInStepInitialDateAndNotInExcludeCronsWithOneSlot__ThenTrueReturned()
        {
            _mockRedDatesStorage.Setup(x => x.GetActualRedDatesAsync()).Returns(Task.FromResult(RedDatesDataProvider.GetEmptyDates()));
            _mockWeekendWorkDatesStorage.Setup(x => x.GetActualWeekendWorkDatesAsync()).Returns(Task.FromResult(WeekendWorkDatesDataProvider.GetEmptyDates()));

            var contractScheduleCollection = new List<SSContractScheduleCollectionModel>
            {
                new SSContractScheduleCollectionModel
                {
                    ContractSchedule = new SSContractScheduleModel
                    {
                        ScheduleDates = null,
                        ExcludeDates = null,
                        StepInitialDateSchedule = new SSStepInitialDateSchedule{ InitialDate = DateTime.Parse("2022-12-14"), StepValue = 2 },
                        Crons = null,
                        ExcludeCrons = new List<string> { "0, 0, 0, 1,2 * ? *" },
                        ContractScheduleSettings = null
                    },
                    CreateDate = DateTime.UtcNow,
                    StartDate = DateTime.Parse("2022-12-14"),
                    EndDate = DateTime.Parse("2022-12-18")
                }
            };

            var isFireRequest = new GetFireInfoOnDateContractScheduleCollectionContract
            {
                NotSlicedContractScheduleCollection = contractScheduleCollection,
                Date = DateTime.Parse("2022-12-16")
            };

            var contractScheduleLogicPerforemer = GetCommonLogic();

            var actual = await contractScheduleLogicPerforemer.IsContractScheduleCollectionFireOnDateAsync(isFireRequest);
            AssertSuccessNotNull(actual);

            var expected = true;
            Assert.Equal(expected, actual.Data);
        }

        [Fact]
        public async Task IsContractScheduleCollectionFireOnDate__WhenInputDateInCronWithTwoBorderdSlots__ThenTrueReturned()
        {
            _mockRedDatesStorage.Setup(x => x.GetActualRedDatesAsync()).Returns(Task.FromResult(RedDatesDataProvider.GetEmptyDates()));
            _mockWeekendWorkDatesStorage.Setup(x => x.GetActualWeekendWorkDatesAsync()).Returns(Task.FromResult(WeekendWorkDatesDataProvider.GetEmptyDates()));

            var contractScheduleCollection = new List<SSContractScheduleCollectionModel>
            {
                new SSContractScheduleCollectionModel
                {
                    ContractSchedule = new SSContractScheduleModel
                    {
                        ScheduleDates = null,
                        ExcludeDates = null,
                        StepInitialDateSchedule = null,
                        Crons = new List<string> { "0, 0, 0, 1,2,15 * ? *" },
                        ExcludeCrons = null,
                        ContractScheduleSettings = null
                    },
                    CreateDate = DateTime.UtcNow.AddSeconds(-2),
                    StartDate = DateTime.Parse("2022-12-14"),
                    EndDate = DateTime.Parse("2022-12-16")
                },
                new SSContractScheduleCollectionModel //Этот слот должен быть приоритетнее так как более новый
                {
                    ContractSchedule = new SSContractScheduleModel
                    {
                        ScheduleDates = null,
                        ExcludeDates = null,
                        StepInitialDateSchedule = null,
                        Crons = new List<string> { "0, 0, 0, 1,2,16 * ? *" },
                        ExcludeCrons = null,
                        ContractScheduleSettings = null
                    },
                    CreateDate = DateTime.UtcNow.AddSeconds(-1),
                    StartDate = DateTime.Parse("2022-12-16"),
                    EndDate = DateTime.Parse("2022-12-20")
                }
            };

            var isFireRequest = new GetFireInfoOnDateContractScheduleCollectionContract
            {
                NotSlicedContractScheduleCollection = contractScheduleCollection,
                Date = DateTime.Parse("2022-12-16")
            };

            var contractScheduleLogicPerforemer = GetCommonLogic();

            var actual = await contractScheduleLogicPerforemer.IsContractScheduleCollectionFireOnDateAsync(isFireRequest);
            AssertSuccessNotNull(actual);

            var expected = true;
            Assert.Equal(expected, actual.Data);
        }

        [Fact]
        public async Task IsContractScheduleCollectionFireOnDate__When_Input_Date_In_Cron_And_InputDate_Is_RedDateWith_NotWorkSetting__ThenFalseReturned()
        {
            _mockRedDatesStorage.Setup(x => x.GetActualRedDatesAsync()).Returns(Task.FromResult(RedDatesDataProvider.GetRedDatesDefaultSet()));
            _mockWeekendWorkDatesStorage.Setup(x => x.GetActualWeekendWorkDatesAsync()).Returns(Task.FromResult(WeekendWorkDatesDataProvider.GetEmptyDates()));

            var contractScheduleCollection = new List<SSContractScheduleCollectionModel>
            {
                new SSContractScheduleCollectionModel
                {
                    ContractSchedule = new SSContractScheduleModel
                    {
                        ScheduleDates = null,
                        ExcludeDates = null,
                        StepInitialDateSchedule = null,
                        Crons = new List<string> { "0, 0, 0, 1,2,13 * ? *" },
                        ExcludeCrons = null,
                        ContractScheduleSettings = new SSContractScheduleSettings
                        {
                            RedDateReactionType = SSNotWorkDayReactionType.NearestWorkingDay
                        }
                    },
                    CreateDate = DateTime.UtcNow,
                    StartDate = DateTime.Parse("2022-06-11"),
                    EndDate = DateTime.Parse("2022-09-25")
                }
            };

            var isFireRequest = new GetFireInfoOnDateContractScheduleCollectionContract
            {
                NotSlicedContractScheduleCollection = contractScheduleCollection,
                Date = DateTime.Parse("2022-06-13")
            };

            var contractScheduleLogicPerforemer = GetCommonLogic();

            var actual = await contractScheduleLogicPerforemer.IsContractScheduleCollectionFireOnDateAsync(isFireRequest);
            AssertSuccessNotNull(actual);

            var expected = false;
            Assert.Equal(expected, actual.Data);
        }

        [Fact]
        public async Task IsContractScheduleCollectionFireOnDate__When_Input_Date_In_Cron_And_InputDate_Is_WeekendWorkDate_NotWorkSetting__ThenTrueReturned()
        {
            _mockRedDatesStorage.Setup(x => x.GetActualRedDatesAsync()).Returns(Task.FromResult(RedDatesDataProvider.GetEmptyDates()));
            _mockWeekendWorkDatesStorage.Setup(x => x.GetActualWeekendWorkDatesAsync()).Returns(Task.FromResult(WeekendWorkDatesDataProvider.GetWeekendWorkDatesDefaultSet()));

            var contractScheduleCollection = new List<SSContractScheduleCollectionModel>
            {
                new SSContractScheduleCollectionModel
                {
                    ContractSchedule = new SSContractScheduleModel
                    {
                        ScheduleDates = null,
                        ExcludeDates = null,
                        StepInitialDateSchedule = null,
                        Crons = new List<string> { "0, 0, 0, 1,2,18 * ? *" },
                        ExcludeCrons = null,
                        ContractScheduleSettings = new SSContractScheduleSettings
                        {
                            EnableWeekendWorkDates = true,
                            WeekendReactionType = SSNotWorkDayReactionType.NearestWorkingDay
                        }
                    },
                    CreateDate = DateTime.UtcNow,
                    StartDate = DateTime.Parse("2022-09-14"),
                    EndDate = DateTime.Parse("2022-09-25")
                }
            };

            var isFireRequest = new GetFireInfoOnDateContractScheduleCollectionContract
            {
                NotSlicedContractScheduleCollection = contractScheduleCollection,
                Date = DateTime.Parse("2022-09-18") //Воскресенье
            };

            var contractScheduleLogicPerforemer = GetCommonLogic();

            var actual = await contractScheduleLogicPerforemer.IsContractScheduleCollectionFireOnDateAsync(isFireRequest);
            AssertSuccessNotNull(actual);

            var expected = true;
            Assert.Equal(expected, actual.Data);
        }

        [Fact]
        public async Task IsContractScheduleCollectionFireOnDate__When_Input_Date_In_Cron_And_InputDate_Is_Weekend_NotWorkSetting__ThenFalseReturned()
        {
            _mockRedDatesStorage.Setup(x => x.GetActualRedDatesAsync()).Returns(Task.FromResult(RedDatesDataProvider.GetEmptyDates()));
            _mockWeekendWorkDatesStorage.Setup(x => x.GetActualWeekendWorkDatesAsync()).Returns(Task.FromResult(WeekendWorkDatesDataProvider.GetEmptyDates()));

            var contractScheduleCollection = new List<SSContractScheduleCollectionModel>
            {
                new SSContractScheduleCollectionModel
                {
                    ContractSchedule = new SSContractScheduleModel
                    {
                        ScheduleDates = null,
                        ExcludeDates = null,
                        StepInitialDateSchedule = null,
                        Crons = new List<string> { "0, 0, 0, 1,2,17 * ? *" },
                        ExcludeCrons = null,
                        ContractScheduleSettings = new SSContractScheduleSettings
                        {
                            WeekendReactionType = SSNotWorkDayReactionType.NearestWorkingDay
                        }
                    },
                    CreateDate = DateTime.UtcNow,
                    StartDate = DateTime.Parse("2022-09-14"),
                    EndDate = DateTime.Parse("2022-09-25")
                }
            };


            var isFireRequest = new GetFireInfoOnDateContractScheduleCollectionContract
            {
                NotSlicedContractScheduleCollection = contractScheduleCollection,
                Date = DateTime.Parse("2022-09-17") //Суббота
            };

            var contractScheduleLogicPerforemer = GetCommonLogic();

            var actual = await contractScheduleLogicPerforemer.IsContractScheduleCollectionFireOnDateAsync(isFireRequest);
            AssertSuccessNotNull(actual);

            var expected = false;
            Assert.Equal(expected, actual.Data);
        }

        [Fact]
        public async Task IsContractScheduleCollectionFireOnDate__When_Input_Date_In_Cron_And_InputDate_Is_Weekend_With_WorkSetting__ThenTrueReturned()
        {
            _mockRedDatesStorage.Setup(x => x.GetActualRedDatesAsync()).Returns(Task.FromResult(RedDatesDataProvider.GetEmptyDates()));
            _mockWeekendWorkDatesStorage.Setup(x => x.GetActualWeekendWorkDatesAsync()).Returns(Task.FromResult(WeekendWorkDatesDataProvider.GetEmptyDates()));

            var contractScheduleCollection = new List<SSContractScheduleCollectionModel>
            {
                new SSContractScheduleCollectionModel
                {
                    ContractSchedule = new SSContractScheduleModel
                    {
                        ScheduleDates = null,
                        ExcludeDates = null,
                        StepInitialDateSchedule = null,
                        Crons = new List<string> { "0, 0, 0, 1,2,17 * ? *" },
                        ExcludeCrons = null,
                        ContractScheduleSettings = new SSContractScheduleSettings
                        {
                            WeekendReactionType = SSNotWorkDayReactionType.Work
                        }
                    },
                    CreateDate = DateTime.UtcNow,
                    StartDate = DateTime.Parse("2022-09-14"),
                    EndDate = DateTime.Parse("2022-09-25")
                }
            };


            var isFireRequest = new GetFireInfoOnDateContractScheduleCollectionContract
            {
                NotSlicedContractScheduleCollection = contractScheduleCollection,
                Date = DateTime.Parse("2022-09-17") //Суббота
            };

            var contractScheduleLogicPerforemer = GetCommonLogic();

            var actual = await contractScheduleLogicPerforemer.IsContractScheduleCollectionFireOnDateAsync(isFireRequest);
            AssertSuccessNotNull(actual);

            var expected = true;
            Assert.Equal(expected, actual.Data);
        }

        private CronScheduleLogic GetCommonLogic()
        {
            var contractScheduleLogic = new ContractScheduleService(_mockRedDatesStorage.Object, _mockWeekendWorkDatesStorage.Object);
            var slicer = new DateRangeSlicer();
            return new CronScheduleLogic(contractScheduleLogic, slicer);
        }
    }
}
