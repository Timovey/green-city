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
    public class MostUnusualRealContractSchedulesTests : ScheduleTestsBase
    {
        private readonly Mock<IRedDatesStorage> _mockRedDatesStorage;
        private readonly Mock<IWeekendWorkDatesStorage> _mockWeekendWorkDatesStorage;

        public MostUnusualRealContractSchedulesTests()
        {
            _mockRedDatesStorage = new Mock<IRedDatesStorage>();
            _mockWeekendWorkDatesStorage = new Mock<IWeekendWorkDatesStorage>();
        }

        [Fact]
        public async Task ContractScheduleTest__WithMostUnusualSchedulesNum1__ThenSuccessCorrectDatesReturnedAsync()
        {
            _mockRedDatesStorage.Setup(x => x.GetActualRedDatesAsync()).Returns(Task.FromResult(RedDatesDataProvider.GetEmptyDates()));
            _mockWeekendWorkDatesStorage.Setup(x => x.GetActualWeekendWorkDatesAsync()).Returns(Task.FromResult(WeekendWorkDatesDataProvider.GetEmptyDates()));

            //Каждое 15 и последнее число месяца без учета выходных и праздников
            var contractSchedule = new SSContractScheduleModel
            {
                ScheduleDates = null,
                ExcludeDates = null,
                StepInitialDateSchedule = null,
                Crons = new List<string> { "0 0 0 15 * ? *", "0 0 0 L * ? *" },
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
                ExamplesCount = 7,
                FromDate = DateTime.Parse("2022-09-01"),
                ToDate = DateTime.Parse("2023-11-30")
            };

            var contractScheduleLogic = new ContractScheduleService(_mockRedDatesStorage.Object, _mockWeekendWorkDatesStorage.Object);
            var slicer = new DateRangeSlicer();
            var contractScheduleLogicPerforemer = new CronScheduleLogic(contractScheduleLogic, slicer);

            var expected = new List<DateTime>
            {
                DateTime.Parse("2022-09-15"),
                DateTime.Parse("2022-09-30"),
                DateTime.Parse("2022-10-15"),
                DateTime.Parse("2022-10-31"),
                DateTime.Parse("2022-11-15"),
                DateTime.Parse("2022-11-30"),
                DateTime.Parse("2022-12-15")
            };

            var actual = await contractScheduleLogicPerforemer.GetNextFireContractScheduleDatesAsync(scheduleRequest);

            AssertSuccessNotEmpty(actual);
            Assert.Equal(expected, actual.Data);
        }

        [Fact]
        public async Task ContractScheduleTest__WithMostUnusualSchedulesNum2__ThenSuccessCorrectDatesReturnedAsync()
        {
            _mockRedDatesStorage.Setup(x => x.GetActualRedDatesAsync()).Returns(Task.FromResult(RedDatesDataProvider.GetRedDatesForMostUnusualScheduleNum2()));
            _mockWeekendWorkDatesStorage.Setup(x => x.GetActualWeekendWorkDatesAsync()).Returns(Task.FromResult(WeekendWorkDatesDataProvider.GetWeekendWorkDatesForMostUnusualScheduleNum2()));

            //Учитываются праздники в январе и один рабочий выходной день в октябре
            //1 15 число месяца кроме выходных и праздничных с переносом на первый рабочий день
            var contractSchedule = new SSContractScheduleModel
            {
                ScheduleDates = null,
                ExcludeDates = null,
                StepInitialDateSchedule = null,
                Crons = new List<string> { "0 0 0 1,15 * ? *" },
                ExcludeCrons = null,
                ContractScheduleSettings = new SSContractScheduleSettings
                {
                    EnableWeekendWorkDates = true,
                    RedDateReactionType = SSNotWorkDayReactionType.NextWorkDay,
                    WeekendReactionType = SSNotWorkDayReactionType.NextWorkDay
                }
            };

            var scheduleRequest = new GetContractScheduleContract
            {
                Schedule = contractSchedule,
                ExamplesCount = 11,
                FromDate = DateTime.Parse("2022-09-01"),
                ToDate = DateTime.Parse("2023-11-30")
            };

            var contractScheduleLogic = new ContractScheduleService(_mockRedDatesStorage.Object, _mockWeekendWorkDatesStorage.Object);
            var slicer = new DateRangeSlicer();
            var contractScheduleLogicPerforemer = new CronScheduleLogic(contractScheduleLogic, slicer);

            var expected = new List<DateTime>
            {
                DateTime.Parse("2022-09-01"),
                DateTime.Parse("2022-09-15"),
                DateTime.Parse("2022-10-04"),
                DateTime.Parse("2022-10-15"),
                DateTime.Parse("2022-11-01"),
                DateTime.Parse("2022-11-15"),
                DateTime.Parse("2022-12-01"),
                DateTime.Parse("2022-12-15"),
                DateTime.Parse("2023-01-09"),
                DateTime.Parse("2023-01-16"),
                DateTime.Parse("2023-02-01"),
            };

            var actual = await contractScheduleLogicPerforemer.GetNextFireContractScheduleDatesAsync(scheduleRequest);

            AssertSuccessNotEmpty(actual);
            Assert.Equal(expected, actual.Data);
        }

        [Fact]
        public async Task ContractScheduleTest__WithMostUnusualSchedulesNum3__ThenSuccessCorrectDatesReturnedAsync()
        {
            _mockRedDatesStorage.Setup(x => x.GetActualRedDatesAsync()).Returns(Task.FromResult(RedDatesDataProvider.GetEmptyDates()));
            _mockWeekendWorkDatesStorage.Setup(x => x.GetActualWeekendWorkDatesAsync()).Returns(Task.FromResult(WeekendWorkDatesDataProvider.GetEmptyDates()));

            //по пн еженедельно + последний чт месяца
            var contractSchedule = new SSContractScheduleModel
            {
                ScheduleDates = null,
                ExcludeDates = null,
                StepInitialDateSchedule = null,
                Crons = new List<string> { "0 0 0 ? * MON *", "0 0 0 ? * 5L *" },
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
                ToDate = DateTime.Parse("2023-11-30")
            };

            var contractScheduleLogic = new ContractScheduleService(_mockRedDatesStorage.Object, _mockWeekendWorkDatesStorage.Object);
            var slicer = new DateRangeSlicer();
            var contractScheduleLogicPerforemer = new CronScheduleLogic(contractScheduleLogic, slicer);

            var expected = new List<DateTime>
            {
                DateTime.Parse("2022-09-05"),
                DateTime.Parse("2022-09-12"),
                DateTime.Parse("2022-09-19"),
                DateTime.Parse("2022-09-26"),
                DateTime.Parse("2022-09-29"),
                DateTime.Parse("2022-10-03"),
                DateTime.Parse("2022-10-10"),
                DateTime.Parse("2022-10-17"),
                DateTime.Parse("2022-10-24"),
                DateTime.Parse("2022-10-27")
            };

            var actual = await contractScheduleLogicPerforemer.GetNextFireContractScheduleDatesAsync(scheduleRequest);

            AssertSuccessNotEmpty(actual);
            Assert.Equal(expected, actual.Data);
        }

        [Fact]
        public async Task ContractScheduleTest__WithMostUnusualSchedulesNum4__ThenSuccessCorrectDatesReturnedAsync()
        {
            _mockRedDatesStorage.Setup(x => x.GetActualRedDatesAsync()).Returns(Task.FromResult(RedDatesDataProvider.GetEmptyDates()));
            _mockWeekendWorkDatesStorage.Setup(x => x.GetActualWeekendWorkDatesAsync()).Returns(Task.FromResult(WeekendWorkDatesDataProvider.GetEmptyDates()));

            //по средам еженедельно + 1 и 3 суббота месяца
            var contractSchedule = new SSContractScheduleModel
            {
                ScheduleDates = null,
                ExcludeDates = null,
                StepInitialDateSchedule = null,
                Crons = new List<string> { "0 0 0 ? * WED *", "0 0 0 ? * 7#1 *", "0 0 0 ? * 7#3 *" },
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
                ToDate = DateTime.Parse("2023-11-30")
            };

            var contractScheduleLogic = new ContractScheduleService(_mockRedDatesStorage.Object, _mockWeekendWorkDatesStorage.Object);
            var slicer = new DateRangeSlicer();
            var contractScheduleLogicPerforemer = new CronScheduleLogic(contractScheduleLogic, slicer);

            var expected = new List<DateTime>
            {
                DateTime.Parse("2022-09-03"),
                DateTime.Parse("2022-09-07"),
                DateTime.Parse("2022-09-14"),
                DateTime.Parse("2022-09-17"),
                DateTime.Parse("2022-09-21"),
                DateTime.Parse("2022-09-28"),
                DateTime.Parse("2022-10-01"),
                DateTime.Parse("2022-10-05"),
                DateTime.Parse("2022-10-12"),
                DateTime.Parse("2022-10-15")
            };

            var actual = await contractScheduleLogicPerforemer.GetNextFireContractScheduleDatesAsync(scheduleRequest);

            AssertSuccessNotEmpty(actual);
            Assert.Equal(expected, actual.Data);
        }

        [Fact]
        public async Task ContractScheduleTest__WithMostUnusualSchedulesNum5__ThenSuccessCorrectDatesReturnedAsync()
        {
            _mockRedDatesStorage.Setup(x => x.GetActualRedDatesAsync()).Returns(Task.FromResult(RedDatesDataProvider.GetEmptyDates()));
            _mockWeekendWorkDatesStorage.Setup(x => x.GetActualWeekendWorkDatesAsync()).Returns(Task.FromResult(WeekendWorkDatesDataProvider.GetEmptyDates()));

            //1 и 2 поонедельник и 30 число любого месяца, но не по четвергам, если выпадает то просто не везем
            var contractSchedule = new SSContractScheduleModel
            {
                ScheduleDates = null,
                ExcludeDates = null,
                StepInitialDateSchedule = null,
                Crons = new List<string> { "0 0 0 ? * 2#1 *", "0 0 0 ? * 2#2 *", "0 0 0 30 * ? *" },
                ExcludeCrons = new List<string> { "0 0 0 ? * 5 *" }, //0 0 0 ? * 5 *
                ContractScheduleSettings = new SSContractScheduleSettings
                {
                    EnableWeekendWorkDates = true,
                    RedDateReactionType = SSNotWorkDayReactionType.Ignore,
                    WeekendReactionType = SSNotWorkDayReactionType.Ignore
                }
            };

            var scheduleRequest = new GetContractScheduleContract
            {
                Schedule = contractSchedule,
                ExamplesCount = 10,
                FromDate = DateTime.Parse("2023-03-15"),
                ToDate = DateTime.Parse("2023-05-30")
            };

            var contractScheduleLogic = new ContractScheduleService(_mockRedDatesStorage.Object, _mockWeekendWorkDatesStorage.Object);
            var slicer = new DateRangeSlicer();
            var contractScheduleLogicPerforemer = new CronScheduleLogic(contractScheduleLogic, slicer);

            var expected = new List<DateTime>
            {
                DateTime.Parse("2023-04-03"),
                DateTime.Parse("2023-04-10"),
                DateTime.Parse("2023-05-01"),
                DateTime.Parse("2023-05-08"),
                DateTime.Parse("2023-05-30")
            };

            var actual = await contractScheduleLogicPerforemer.GetNextFireContractScheduleDatesAsync(scheduleRequest);

            AssertSuccessNotEmpty(actual);
            Assert.Equal(expected, actual.Data);
        }
    }
}
