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
    public class ContractStepScheduleTests : ScheduleTestsBase
    {
        private readonly Mock<IRedDatesStorage> _mockRedDatesStorage;
        private readonly Mock<IWeekendWorkDatesStorage> _mockWeekendWorkDatesStorage;

        public ContractStepScheduleTests()
        {
            _mockRedDatesStorage = new Mock<IRedDatesStorage>();
            _mockWeekendWorkDatesStorage = new Mock<IWeekendWorkDatesStorage>();
        }

        [Fact]
        public async Task ContractStepScheduleTests__IfDateRangeFrom30DaysMonth__And__StepSkip31stDay__ThenCorrectDatesReturned()
        {
            _mockRedDatesStorage.Setup(x => x.GetActualRedDatesAsync()).Returns(Task.FromResult(RedDatesDataProvider.GetEmptyDates()));
            _mockWeekendWorkDatesStorage.Setup(x => x.GetActualWeekendWorkDatesAsync()).Returns(Task.FromResult(WeekendWorkDatesDataProvider.GetEmptyDates()));

            var contractSchedule = new SSContractScheduleModel
            {
                ScheduleDates = null,
                ExcludeDates = null,
                StepInitialDateSchedule = new SSStepInitialDateSchedule { InitialDate = DateTime.Parse("2022-09-19"), StepValue = 2 },
                Crons = null,
                ExcludeCrons = null,
                ContractScheduleSettings = new SSContractScheduleSettings
                {
                    EnableWeekendWorkDates = false,
                    RedDateReactionType = SSNotWorkDayReactionType.Work,
                    WeekendReactionType = SSNotWorkDayReactionType.Work
                }
            };

            var scheduleRequest = new GetContractScheduleContract
            {
                Schedule = contractSchedule,
                ExamplesCount = 10,
                FromDate = DateTime.Parse("2022-09-01"),
                ToDate = DateTime.Parse("2022-10-15")
            };

            var contractScheduleLogic = new ContractScheduleService(_mockRedDatesStorage.Object, _mockWeekendWorkDatesStorage.Object);
            var slicer = new DateRangeSlicer();
            var contractScheduleLogicPerforemer = new CronScheduleLogic(contractScheduleLogic, slicer);

            var expected = new List<DateTime>
            {
                DateTime.Parse("2022-09-19"),
                DateTime.Parse("2022-09-21"),
                DateTime.Parse("2022-09-23"),
                DateTime.Parse("2022-09-25"),
                DateTime.Parse("2022-09-27"),
                DateTime.Parse("2022-09-29"),
                DateTime.Parse("2022-10-01"),
                DateTime.Parse("2022-10-03"),
                DateTime.Parse("2022-10-05"),
                DateTime.Parse("2022-10-07")
            };

            var actual = await contractScheduleLogicPerforemer.GetNextFireContractScheduleDatesAsync(scheduleRequest);

            AssertSuccessNotEmpty(actual);
            Assert.Equal(expected, actual.Data);
        }

        [Fact]
        public async Task ContractStepScheduleTests__WithStepSchedule__And__IgnoreWeekendsWithRedDates__ThenCorrectDatesReturned()
        {
            _mockRedDatesStorage.Setup(x => x.GetActualRedDatesAsync()).Returns(Task.FromResult(RedDatesDataProvider.GetEmptyDates()));
            _mockWeekendWorkDatesStorage.Setup(x => x.GetActualWeekendWorkDatesAsync()).Returns(Task.FromResult(WeekendWorkDatesDataProvider.GetEmptyDates()));

            var contractSchedule = new SSContractScheduleModel
            {
                ScheduleDates = null,
                ExcludeDates = null,
                StepInitialDateSchedule = new SSStepInitialDateSchedule { InitialDate = DateTime.Parse("2022-09-19"), StepValue = 2 },
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
                FromDate = DateTime.Parse("2022-09-01"),
                ToDate = DateTime.Parse("2022-10-15")
            };

            var contractScheduleLogic = new ContractScheduleService(_mockRedDatesStorage.Object, _mockWeekendWorkDatesStorage.Object);
            var slicer = new DateRangeSlicer();
            var contractScheduleLogicPerforemer = new CronScheduleLogic(contractScheduleLogic, slicer);

            var expected = new List<DateTime>
            {
                DateTime.Parse("2022-09-19"),
                DateTime.Parse("2022-09-21"),
                DateTime.Parse("2022-09-23"),
                DateTime.Parse("2022-09-27"),
                DateTime.Parse("2022-09-29"),
                DateTime.Parse("2022-10-03"),
                DateTime.Parse("2022-10-05"),
                DateTime.Parse("2022-10-07"),
                DateTime.Parse("2022-10-11"),
                DateTime.Parse("2022-10-13")
            };

            var actual = await contractScheduleLogicPerforemer.GetNextFireContractScheduleDatesAsync(scheduleRequest);

            AssertSuccessNotEmpty(actual);
            Assert.Equal(expected, actual.Data);
        }

        [Fact]
        public async Task ContractStepScheduleTests__WhenNextFireDateTooFar__ThenEmptyResponseReturned()
        {
            _mockRedDatesStorage.Setup(x => x.GetActualRedDatesAsync()).Returns(Task.FromResult(RedDatesDataProvider.GetEmptyDates()));
            _mockWeekendWorkDatesStorage.Setup(x => x.GetActualWeekendWorkDatesAsync()).Returns(Task.FromResult(WeekendWorkDatesDataProvider.GetEmptyDates()));

            var contractSchedule = new SSContractScheduleModel
            {
                ScheduleDates = null,
                ExcludeDates = null,
                StepInitialDateSchedule = new SSStepInitialDateSchedule { InitialDate = DateTime.Parse("2071-09-19"), StepValue = 2000 },
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
                FromDate = DateTime.Parse("2022-09-01"),
                ToDate = DateTime.Parse("2078-10-15")
            };

            var contractScheduleLogic = new ContractScheduleService(_mockRedDatesStorage.Object, _mockWeekendWorkDatesStorage.Object);
            var slicer = new DateRangeSlicer();
            var contractScheduleLogicPerforemer = new CronScheduleLogic(contractScheduleLogic, slicer);

            var actual = await contractScheduleLogicPerforemer.GetNextFireContractScheduleDatesAsync(scheduleRequest);

            AssertSuccessNotNull(actual);
            Assert.Empty(actual.Data);
        }

        [Fact]
        public async Task ContractStepScheduleTests__WhenStepScheduleWithSimpleCrons__ThenEmptyResponseReturned()
        {
            //_mockRedDatesStorage.Setup(x => x.GetActualRedDatesAsync()).Returns(Task.FromResult(RedDatesDataProvider.GetEmptyDates()));
            //_mockWeekendWorkDatesStorage.Setup(x => x.GetActualWeekendWorkDatesAsync()).Returns(Task.FromResult(WeekendWorkDatesDataProvider.GetEmptyDates()));

            //var contractSchedule = new SSContractScheduleModel
            //{
            //    ScheduleDates = new List<DateTime> { DateTime.Parse("2022-09-02") },
            //    ExcludeDates = new List<DateTime> { DateTime.Parse("2022-09-01") },
            //    StepInitialDateSchedule = new SSStepInitialDateSchedule { InitialDate = DateTime.Parse("2022-09-01"), StepValue = 4 },
            //    Crons = null,
            //    ExcludeCrons = null,
            //    ContractScheduleSettings = new SSContractScheduleSettings { EnableWeekendWorkDates = false, NotWorkDayReactionType = SSNotWorkDayReactionType.Work }
            //};

            //var scheduleRequest = new GetContractScheduleContract
            //{
            //    Schedule = contractSchedule,
            //    ExamplesCount = 10,
            //    FromDate = DateTime.Parse("2022-09-01"),
            //    ToDate = DateTime.Parse("2022-10-15")
            //};

            //var contractScheduleLogicPerforemer = new CronScheduleLogic(_mockRedDatesStorage.Object, _mockWeekendWorkDatesStorage.Object);

            //var actual = await contractScheduleLogicPerforemer.GetNextFireContractScheduleDatesAsync(scheduleRequest);

            //AssertSuccessNotNull(actual);
            //Assert.Empty(actual.Data);
        }
    }
}
