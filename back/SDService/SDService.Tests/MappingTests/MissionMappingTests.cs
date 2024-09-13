using AutoMapper;
using ScheduleService.Contracts.DataContracts.ContractSchedule;
using ScheduleService.Contracts.DataContracts.ContractSchedule.Mission;
using SDContracts.DataContracts.Contract.ScheduleModels;
using SDContracts.ViewModels;
using SDContracts.ViewModels.ContractSchedule;
using SDContracts.ViewModels.ContractSchedule.Mission;
using SDService.Database.MappingProfile;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SDService.Tests.MappingTests
{
    public class MissionMappingTests
    {
        private readonly MapperConfiguration _mapperConfiguration;
        private readonly IMapper _mapper;

        public MissionMappingTests()
        {
            _mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            _mapper = _mapperConfiguration.CreateMapper();
        }

        [Fact]
        public void MissionMappingTests__ConvertContainer2ScheduleViewModel_to_Container2ScheduleInfoMissionViewModel__ThenEqualCheckSuccess()
        {
            var dest = DataProvider.GetDefaultContainer2ScheduleViewModel();

            var expected = new Container2ScheduleInfoMissionViewModel
            {
                ScheduleComment = "schedule_comment_1",
                WorkTimeFrom = TimeSpan.Parse("10:10"),//DateTime.Parse("2024-12-12"),
                WorkTimeTo = TimeSpan.Parse("10:15"), //DateTime.Parse("2024-12-15"),
                IsInRegistry = true,
                InRegistryFromDate = DateTime.Parse("2001-12-12")
            };

            var actualResult = _mapper.Map<Container2ScheduleInfoMissionViewModel>(dest);

            Assert.True(actualResult.IsEqual(expected));
        }

        [Fact]
        public void MissionMappingTests__ConvertContractContainerViewModel_to_Container2ScheduleInfoMissionViewModel__ThenEqualCheckSuccess()
        {
            var dest = new ContractContainerViewModel
            {
                ContainerId = 1,
                Name = "Name_1",
                ContainerTypeId = 2,
                TrashType = SDContracts.Enums.TrashType.KGO,
                Container2Schedule = DataProvider.GetDefaultContainer2ScheduleViewModel(),
                IsInContract = true
            };

            var expected = new ContractContainerInfoMissionViewModel
            {
                ContainerId = 1,
                Name = "Name_1",
                ContainerTypeId = 2,
                TrashType = SDContracts.Enums.TrashType.KGO,
                Container2Schedule = new Container2ScheduleInfoMissionViewModel
                {
                    ScheduleComment = "schedule_comment_1",
                    WorkTimeFrom = TimeSpan.Parse("10:10"),//DateTime.Parse("2024-12-12"),
                    WorkTimeTo = TimeSpan.Parse("10:15"), //DateTime.Parse("2024-12-15"),
                    IsInRegistry = true,
                    InRegistryFromDate = DateTime.Parse("2001-12-12")
                }
            };

            var actualResult = _mapper.Map<ContractContainerInfoMissionViewModel>(dest);

            Assert.True(actualResult.IsEqual(expected));
        }

        [Fact]
        public void MissionMappingTests__ConvertListContractScheduleCollectionViewModel_to_ListSSContractScheduleCollectionModel__ThenEqualCheckSuccess()
        {
            var dest = new ContractScheduleCollectionViewModel[]
            {
                new ContractScheduleCollectionViewModel
                {
                    ContractSchedule = new ContractScheduleViewModel
                    {
                        StepInitialDateSchedule = new StepInitialDateScheduleViewModel
                        {
                            InitialDate = DateTime.Parse("2022-06-12"),
                            StepValue = 5
                        },
                        ScheduleDates = new List<DateTime>
                        {
                            DateTime.Parse("2022-12-13"),
                            DateTime.Parse("2022-12-14"),
                            DateTime.Parse("2022-12-15")
                        },
                        ExcludeDates = new List<DateTime>
                        {
                            DateTime.Parse("2022-12-15"),
                            DateTime.Parse("2022-06-16")
                        },
                        Crons = new List<string>
                        {
                            "Cron_1",
                            "Cron_2"
                        },
                        ExcludeCrons = new List<string>
                        {
                            "Exclude_cron_1",
                            "Exclude_cron_2",
                            "Exclude_cron_3"
                        },
                        ContractScheduleSettings = new ContractScheduleSettingsViewModel
                        {
                            EnableWeekendWorkDates = false,
                            RedDateReactionType = NotWorkDayReactionType.Ignore,
                            WeekendReactionType = NotWorkDayReactionType.PrevWorkDat
                        }
                    },
                    CreateDate = DateTime.Parse("2022-05-12"),
                    ScheduleDateFrom = DateTime.Parse("2022-06-11"),
                    ScheduleDateTo = DateTime.Parse("2022-07-15")
                },
                new ContractScheduleCollectionViewModel
                {
                    ContractSchedule = new ContractScheduleViewModel
                    {
                        StepInitialDateSchedule = new StepInitialDateScheduleViewModel
                        {
                            InitialDate = DateTime.Parse("2023-06-12"),
                            StepValue = 4
                        },
                        ScheduleDates = new List<DateTime>
                        {
                            DateTime.Parse("2023-12-13"),
                            DateTime.Parse("2023-12-14"),
                            DateTime.Parse("2023-12-15")
                        },
                        ExcludeDates = new List<DateTime>
                        {
                            DateTime.Parse("2023-12-15"),
                            DateTime.Parse("2023-06-16")
                        },
                        Crons = new List<string>
                        {
                            "Cron_10",
                            "Cron_20"
                        },
                        ExcludeCrons = new List<string>
                        {
                            "Exclude_cron_10",
                            "Exclude_cron_20",
                            "Exclude_cron_30"
                        },
                        ContractScheduleSettings = new ContractScheduleSettingsViewModel
                        {
                            EnableWeekendWorkDates = false,
                            RedDateReactionType = NotWorkDayReactionType.Ignore,
                            WeekendReactionType = NotWorkDayReactionType.PrevWorkDat
                        }
                    },
                    CreateDate = DateTime.Parse("2022-06-16"),
                    ScheduleDateFrom = DateTime.Parse("2022-06-11"),
                    ScheduleDateTo = DateTime.Parse("2022-07-15")
                }
            };

            var expected = new SSContractScheduleCollectionModel[]
            {
                new SSContractScheduleCollectionModel
                {
                    ContractSchedule = new SSContractScheduleModel
                    {
                        StepInitialDateSchedule = new SSStepInitialDateSchedule
                        {
                            InitialDate = DateTime.Parse("2022-06-12"),
                            StepValue = 5
                        },
                        ScheduleDates = new List<DateTime>
                        {
                            DateTime.Parse("2022-12-13"),
                            DateTime.Parse("2022-12-14"),
                            DateTime.Parse("2022-12-15")
                        },
                        ExcludeDates = new List<DateTime>
                        {
                            DateTime.Parse("2022-12-15"),
                            DateTime.Parse("2022-06-16")
                        },
                        Crons = new List<string>
                        {
                            "Cron_1",
                            "Cron_2"
                        },
                        ExcludeCrons = new List<string>
                        {
                            "Exclude_cron_1",
                            "Exclude_cron_2",
                            "Exclude_cron_3"
                        },
                        ContractScheduleSettings = new SSContractScheduleSettings
                        {
                            EnableWeekendWorkDates = false,
                            RedDateReactionType = SSNotWorkDayReactionType.Ignore,
                            WeekendReactionType = SSNotWorkDayReactionType.PrevWorkDat
                        }
                    },
                    CreateDate = DateTime.Parse("2022-05-12"),
                    StartDate = DateTime.Parse("2022-06-11"),
                    EndDate = DateTime.Parse("2022-07-15")
                },
                new SSContractScheduleCollectionModel
                {
                    ContractSchedule = new SSContractScheduleModel
                    {
                        StepInitialDateSchedule = new SSStepInitialDateSchedule
                        {
                            InitialDate = DateTime.Parse("2023-06-12"),
                            StepValue = 4
                        },
                        ScheduleDates = new List<DateTime>
                        {
                            DateTime.Parse("2023-12-13"),
                            DateTime.Parse("2023-12-14"),
                            DateTime.Parse("2023-12-15")
                        },
                        ExcludeDates = new List<DateTime>
                        {
                            DateTime.Parse("2023-12-15"),
                            DateTime.Parse("2023-06-16")
                        },
                        Crons = new List<string>
                        {
                            "Cron_10",
                            "Cron_20"
                        },
                        ExcludeCrons = new List<string>
                        {
                            "Exclude_cron_10",
                            "Exclude_cron_20",
                            "Exclude_cron_30"
                        },
                        ContractScheduleSettings = new SSContractScheduleSettings
                        {
                            EnableWeekendWorkDates = false,
                            RedDateReactionType = SSNotWorkDayReactionType.Ignore,
                            WeekendReactionType = SSNotWorkDayReactionType.PrevWorkDat
                        }
                    },
                    CreateDate = DateTime.Parse("2022-06-16"),
                    StartDate = DateTime.Parse("2022-06-11"),
                    EndDate = DateTime.Parse("2022-07-15")
                }
            };

            var actual = dest.Select(_mapper.Map<SSContractScheduleCollectionModel>).ToArray();

            Assert.True(actual.IsEqual(expected));
        }
    }
}
