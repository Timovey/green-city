using Moq;
using ScheduleService.Contracts.DataContracts.ContractSchedule;
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
    public class ContractScheduleTests : ScheduleTestsBase
    {
        private readonly Mock<IRedDatesStorage> _mockRedDatesStorage;
        private readonly Mock<IWeekendWorkDatesStorage> _mockWeekendWorkDatesStorage;

        public ContractScheduleTests()
        {
            _mockRedDatesStorage = new Mock<IRedDatesStorage>();
            _mockWeekendWorkDatesStorage = new Mock<IWeekendWorkDatesStorage>();
        }

        [Fact]
        public async Task ContractScheduleTest__WhenNoOneFieldFilled__ThenErrorReturnedAsync()
        {
            _mockRedDatesStorage.Setup(x => x.GetActualRedDatesAsync()).Returns(Task.FromResult(RedDatesDataProvider.GetEmptyDates()));
            _mockWeekendWorkDatesStorage.Setup(x => x.GetActualWeekendWorkDatesAsync()).Returns(Task.FromResult(WeekendWorkDatesDataProvider.GetEmptyDates()));

            var contractSchedule = new SSContractScheduleModel
            {
                ScheduleDates = null,
                ExcludeDates = null,
                StepInitialDateSchedule = null,
                Crons = null,
                ExcludeCrons = null,
                ContractScheduleSettings = null
            };

            var scheduleRequest = new GetContractScheduleContract
            {
                Schedule = contractSchedule,
                ExamplesCount = 10,
                FromDate = DateTime.Parse("2022-01-01"),
                ToDate = default
            };

            var contractScheduleLogic = new ContractScheduleService(_mockRedDatesStorage.Object,_mockWeekendWorkDatesStorage.Object);
            var slicer = new DateRangeSlicer();
            var contractScheduleLogicPerforemer = new CronScheduleLogic(contractScheduleLogic, slicer);

            var actual = await contractScheduleLogicPerforemer.GetNextFireContractScheduleDatesAsync(scheduleRequest);
            AssertFailure(actual);
        }

        [Fact]
        public async Task ContractScheduleTest__WithOnlyScheduleDatesFilled__ThenSuccessCorrectDatesReturnedAsync()
        {
            _mockRedDatesStorage.Setup(x => x.GetActualRedDatesAsync()).Returns(Task.FromResult(RedDatesDataProvider.GetEmptyDates()));
            _mockWeekendWorkDatesStorage.Setup(x => x.GetActualWeekendWorkDatesAsync()).Returns(Task.FromResult(WeekendWorkDatesDataProvider.GetEmptyDates()));

            var contractSchedule = new SSContractScheduleModel
            {
                ScheduleDates = new List<DateTime> { DateTime.Parse("2022-02-03"), DateTime.Parse("2022-03-10") },
                ExcludeDates = null,
                StepInitialDateSchedule = null,
                Crons = null,
                ExcludeCrons = null,
                ContractScheduleSettings = new SSContractScheduleSettings
                {
                    EnableWeekendWorkDates = false,
                    RedDateReactionType = SSNotWorkDayReactionType.Ignore,
                    WeekendReactionType = SSNotWorkDayReactionType.Ignore
                }
            };

            var scheduleRequest = new GetContractScheduleContract
            {
                Schedule = contractSchedule,
                ExamplesCount = 10,
                FromDate = DateTime.Parse("2022-01-01"),
                ToDate = default
            };

            var contractScheduleLogic = new ContractScheduleService(_mockRedDatesStorage.Object, _mockWeekendWorkDatesStorage.Object);
            var slicer = new DateRangeSlicer();
            var contractScheduleLogicPerforemer = new CronScheduleLogic(contractScheduleLogic, slicer);

            var expected = new List<DateTime> { DateTime.Parse("2022-02-03"), DateTime.Parse("2022-03-10") };
            var actual = await contractScheduleLogicPerforemer.GetNextFireContractScheduleDatesAsync(scheduleRequest);

            AssertSuccessNotEmpty(actual);
            Assert.Equal(expected, actual.Data);
        }

        [Fact]
        public async Task ContractScheduleTest__WhenEqualScheduleDatesAndExcludeDates__ThenEmptyDatesReturnedAsync()
        {
            _mockRedDatesStorage.Setup(x => x.GetActualRedDatesAsync()).Returns(Task.FromResult(RedDatesDataProvider.GetEmptyDates()));
            _mockWeekendWorkDatesStorage.Setup(x => x.GetActualWeekendWorkDatesAsync()).Returns(Task.FromResult(WeekendWorkDatesDataProvider.GetEmptyDates()));

            var contractSchedule = new SSContractScheduleModel
            {
                ScheduleDates = new List<DateTime> { DateTime.Parse("2022-02-03"), DateTime.Parse("2022-03-10") },
                ExcludeDates = new List<DateTime> { DateTime.Parse("2022-02-03"), DateTime.Parse("2022-03-10") },
                StepInitialDateSchedule = null,
                Crons = null,
                ExcludeCrons = null,
                ContractScheduleSettings = new SSContractScheduleSettings
                {
                    EnableWeekendWorkDates = false,
                    RedDateReactionType = SSNotWorkDayReactionType.Ignore,
                    WeekendReactionType = SSNotWorkDayReactionType.Ignore
                }
            };

            var scheduleRequest = new GetContractScheduleContract
            {
                Schedule = contractSchedule,
                ExamplesCount = 10,
                FromDate = DateTime.Parse("2022-01-01"),
                ToDate = default
            };

            var contractScheduleLogic = new ContractScheduleService(_mockRedDatesStorage.Object, _mockWeekendWorkDatesStorage.Object);
            var slicer = new DateRangeSlicer();
            var contractScheduleLogicPerforemer = new CronScheduleLogic(contractScheduleLogic, slicer);

            var expected = new List<DateTime> { DateTime.Parse("2022-02-03"), DateTime.Parse("2022-03-10") };
            var actual = await contractScheduleLogicPerforemer.GetNextFireContractScheduleDatesAsync(scheduleRequest);

            AssertSuccessEmpty(actual);
        }

        [Fact]
        public async Task ContractScheduleTest__WithSimpleCrons__ThenSuccessCorrectDatesReturnedAsync()
        {
            _mockRedDatesStorage.Setup(x => x.GetActualRedDatesAsync()).Returns(Task.FromResult(RedDatesDataProvider.GetEmptyDates()));
            _mockWeekendWorkDatesStorage.Setup(x => x.GetActualWeekendWorkDatesAsync()).Returns(Task.FromResult(WeekendWorkDatesDataProvider.GetEmptyDates()));

            //Каждый третий понедельник месяца + каждую пятницу + 11 и 20 число и не по вторникам + пропустить 19 сентября и вывести 7 сентября
            var contractSchedule = new SSContractScheduleModel
            {
                ScheduleDates = new List<DateTime> { DateTime.Parse("2022-09-07") },
                ExcludeDates = new List<DateTime> { DateTime.Parse("2022-09-19") },
                StepInitialDateSchedule = null,
                Crons = new List<string> { "0 0 0 ? * 2#3 *", "0 0 0 ? * FRI *", "0 0 0 11,20 * ? *" },
                ExcludeCrons = new List<string> { "0 0 0 ? * TUE *" },
                ContractScheduleSettings = new SSContractScheduleSettings
                {
                    EnableWeekendWorkDates = false,
                    RedDateReactionType = SSNotWorkDayReactionType.Ignore,
                    WeekendReactionType = SSNotWorkDayReactionType.Ignore
                }
            };

            var scheduleRequest = new GetContractScheduleContract
            {
                Schedule = contractSchedule,
                ExamplesCount = 7,
                FromDate = DateTime.Parse("2022-09-01"),
                ToDate = DateTime.Parse("2022-11-30")
            };

            var contractScheduleLogic = new ContractScheduleService(_mockRedDatesStorage.Object, _mockWeekendWorkDatesStorage.Object);
            var slicer = new DateRangeSlicer();
            var contractScheduleLogicPerforemer = new CronScheduleLogic(contractScheduleLogic, slicer);

            var expected = new List<DateTime>
            {
                DateTime.Parse("2022-09-02"),
                DateTime.Parse("2022-09-07"),
                DateTime.Parse("2022-09-09"),
                DateTime.Parse("2022-09-16"),
                DateTime.Parse("2022-09-23"),
                DateTime.Parse("2022-09-30"),
                DateTime.Parse("2022-10-07")
            };

            var actual = await contractScheduleLogicPerforemer.GetNextFireContractScheduleDatesAsync(scheduleRequest);

            AssertSuccessNotEmpty(actual);
            Assert.Equal(expected, actual.Data);
        }

        [Fact]
        public async Task ContractScheduleTest__WhenInseretedOnlyIncludeAndExcludeDates__ThenSuccessCorrectDatesReturnedAsync()
        {
            _mockRedDatesStorage.Setup(x => x.GetActualRedDatesAsync()).Returns(Task.FromResult(RedDatesDataProvider.GetEmptyDates()));
            _mockWeekendWorkDatesStorage.Setup(x => x.GetActualWeekendWorkDatesAsync()).Returns(Task.FromResult(WeekendWorkDatesDataProvider.GetEmptyDates()));

            var contractSchedule = new SSContractScheduleModel
            {
                ScheduleDates = new List<DateTime> { DateTime.Parse("2022-09-07"), DateTime.Parse("2022-09-17"), DateTime.Parse("2022-09-29") },
                ExcludeDates = new List<DateTime> { DateTime.Parse("2022-09-07") },
                StepInitialDateSchedule = null,
                Crons = null,
                ExcludeCrons = null,
                ContractScheduleSettings = new SSContractScheduleSettings
                {
                    EnableWeekendWorkDates = false,
                    RedDateReactionType = SSNotWorkDayReactionType.Ignore,
                    WeekendReactionType = SSNotWorkDayReactionType.Ignore
                }
            };

            var scheduleRequest = new GetContractScheduleContract
            {
                Schedule = contractSchedule,
                ExamplesCount = 7,
                FromDate = DateTime.Parse("2022-09-01"),
                ToDate = null
            };

            var contractScheduleLogic = new ContractScheduleService(_mockRedDatesStorage.Object, _mockWeekendWorkDatesStorage.Object);
            var slicer = new DateRangeSlicer();
            var contractScheduleLogicPerforemer = new CronScheduleLogic(contractScheduleLogic, slicer);

            var expected = new List<DateTime>
            {
                DateTime.Parse("2022-09-17"),
                DateTime.Parse("2022-09-29")
            };

            var actual = await contractScheduleLogicPerforemer.GetNextFireContractScheduleDatesAsync(scheduleRequest);

            AssertSuccessNotEmpty(actual);
            Assert.Equal(expected, actual.Data);
        }
    }
}
