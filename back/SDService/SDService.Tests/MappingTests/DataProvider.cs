using SDContracts.DataContracts.Contract.ScheduleModels;
using SDContracts.ViewModels;
using SDContracts.ViewModels.ContractSchedule;
using System;
using System.Collections.Generic;

namespace SDService.Tests.MappingTests
{
    internal class DataProvider
    {
        public static IList<ContractScheduleCollectionViewModel> GetDefaultContractScheduleCollectionCollection()
        {
            return new List<ContractScheduleCollectionViewModel>
            {
                new ContractScheduleCollectionViewModel
                {
                    ContractSchedule = new ContractScheduleViewModel
                    {
                        StepInitialDateSchedule = new StepInitialDateScheduleViewModel
                        {
                            StepValue = 10,
                            InitialDate = DateTime.Parse("2021-12-12")
                        },
                        ScheduleDates = new List<DateTime>
                        {
                            DateTime.Parse("2021-12-12"),
                            DateTime.Parse("2021-12-13"),
                            DateTime.Parse("2021-12-14")
                        },
                        ExcludeDates = new List<DateTime>
                        {
                            DateTime.Parse("2020-12-12"),
                            DateTime.Parse("2020-12-13"),
                            DateTime.Parse("2020-12-14")
                        },
                        Crons = new List<string>
                        {
                            "Cron_1",
                            "Cron_2",
                            "Cron_3"
                        },
                        ExcludeCrons = new List<string>
                        {
                            "Exclude_Cron_1",
                            "Exclude_Cron_2",
                            "Exclude_Cron_3"
                        },
                        ContractScheduleSettings = new ContractScheduleSettingsViewModel
                        {
                            EnableWeekendWorkDates = true,
                            RedDateReactionType = NotWorkDayReactionType.PrevWorkDat,
                            WeekendReactionType = NotWorkDayReactionType.Ignore
                        }
                    },
                    ScheduleDateFrom = DateTime.Parse("2022-12-12"),
                    ScheduleDateTo = DateTime.Parse("2022-12-13")
                },
                new ContractScheduleCollectionViewModel
                {
                    ContractSchedule = new ContractScheduleViewModel
                    {
                        StepInitialDateSchedule = new StepInitialDateScheduleViewModel
                        {
                            StepValue = 10,
                            InitialDate = DateTime.Parse("2022-12-12")
                        },
                        ScheduleDates = new List<DateTime>
                        {
                            DateTime.Parse("2022-12-12"),
                            DateTime.Parse("2022-12-13"),
                            DateTime.Parse("2022-12-14")
                        },
                        ExcludeDates = new List<DateTime>
                        {
                            DateTime.Parse("2021-12-12"),
                            DateTime.Parse("2021-12-13"),
                            DateTime.Parse("2021-12-14")
                        },
                        Crons = new List<string>
                        {
                            "Cron_1_1",
                            "Cron_2_1",
                            "Cron_3_1"
                        },
                        ExcludeCrons = new List<string>
                        {
                            "Exclude_Cron_1_1",
                            "Exclude_Cron_2_1",
                            "Exclude_Cron_3_1"
                        },
                        ContractScheduleSettings = new ContractScheduleSettingsViewModel
                        {
                            EnableWeekendWorkDates = false,
                            RedDateReactionType = NotWorkDayReactionType.Ignore,
                            WeekendReactionType = NotWorkDayReactionType.Work
                        }
                    },
                    ScheduleDateFrom = DateTime.Parse("2023-12-12"),
                    ScheduleDateTo = DateTime.Parse("2023-12-13")
                }
            };
        }

        public static Container2ScheduleViewModel GetDefaultContainer2ScheduleViewModel()
        {
            return new Container2ScheduleViewModel
            {
                ContractScheduleCollection = GetDefaultContractScheduleCollectionCollection(),
                ScheduleComment = "schedule_comment_1",
                WorkTimeFrom = TimeSpan.Parse("10:10"),//DateTime.Parse("2024-12-12"),
                WorkTimeTo = TimeSpan.Parse("10:15"), //DateTime.Parse("2024-12-15"),
                IsInRegistry = true,
                InRegistryFromDate = DateTime.Parse("2001-12-12")
            };
        }

        public static ContractContainerViewModel GetDefaultContractContainerViewModel()
        {
            return new ContractContainerViewModel
            {
                ContainerId = 1,
                Name = "Name_1",
                ContainerTypeId = 2,
                TrashType = SDContracts.Enums.TrashType.KGO,
                Container2Schedule = GetDefaultContainer2ScheduleViewModel(),
                IsInContract = true
            };
        }
    }
}
