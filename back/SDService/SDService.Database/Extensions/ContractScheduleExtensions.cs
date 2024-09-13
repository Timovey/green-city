using GreenCity.CommonUtils.CommonExtensions;
using SDContracts.DataContracts.Contract.ScheduleModels;
using SDContracts.ViewModels.ContractSchedule;
using SDService.Database.JsonModels.Contract.ContractSchedule;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SDService.Database.Extensions
{
    internal static class ContractScheduleExtensions
    {
        private static readonly string _timeFormat = "dd:MM:yyyy";

        #region [ ToJsonContractSchedule ]

        public static IEnumerable<ContractScheduleCollectionJsonModel> ToJsonContractScheduleCollectionBulk(
            this IList<ContractScheduleCollectionModel> contractScheduleCollection
        )
        {
            if (contractScheduleCollection == null || contractScheduleCollection.Count == 0)
            {
                yield break;
            }

            foreach (var el in contractScheduleCollection)
            {
                yield return el.ToJsonContractScheduleCollection();
            }
        }

        public static ContractScheduleCollectionJsonModel ToJsonContractScheduleCollection(this ContractScheduleCollectionModel contractScheduleCollectionModel)
        {
            if (contractScheduleCollectionModel == null)
            {
                return null;
            }

            var dateTo = contractScheduleCollectionModel.ScheduleDateTo ?? System.DateTime.MaxValue;
            var result = new ContractScheduleCollectionJsonModel
            {
                ContractSchedule = contractScheduleCollectionModel.ContractSchedule.ToJsonContractSchedule(),
                CreateDate = contractScheduleCollectionModel.CreateDate.ToString(), //тут важны секунды
                ScheduleDateFrom = contractScheduleCollectionModel.ScheduleDateFrom.ToString(_timeFormat),
                ScheduleDateTo = dateTo.ToString(_timeFormat)
            };

            return result;
        }

        private static ContractScheduleJsonModel ToJsonContractSchedule(this ContractScheduleModel contractScheduleModel)
        {
            if (contractScheduleModel == null)
            {
                return null;
            }

            var result = new ContractScheduleJsonModel
            {
                StepInitialDateSchedule = contractScheduleModel.StepInitialDateSchedule.ToJsonStepInitialDate(),
                ScheduleDates = contractScheduleModel.ScheduleDates.Select(x => x.ToString(_timeFormat)).ToList(),
                ExcludeDates = contractScheduleModel.ExcludeDates.Select(x => x.ToString(_timeFormat)).ToList(),
                Crons = contractScheduleModel.Crons,
                ExcludeCrons = contractScheduleModel.ExcludeCrons,
                ContractScheduleSettings = contractScheduleModel.ContractScheduleSettings.ToJsonContractScheduleSettings()
            };

            return result;
        }

        private static StepInitialDateJsonModel ToJsonStepInitialDate(this StepInitialDateScheduleModel stepInitialDateSchedule)
        {
            if (stepInitialDateSchedule == null)
            {
                return null;
            }

            var result = new StepInitialDateJsonModel
            {
                InitialDate = stepInitialDateSchedule.InitialDate.ToString(_timeFormat),
                StepValue = stepInitialDateSchedule.StepValue
            };

            return result;
        }

        private static ContractScheduleSettingsJsonModel ToJsonContractScheduleSettings(this ContractScheduleSettings contractScheduleSettings)
        {
            if (contractScheduleSettings == null)
            {
                return null;
            }

            var result = new ContractScheduleSettingsJsonModel
            {
                EnableWeekendWorkDates = contractScheduleSettings.EnableWeekendWorkDates,
                RedDateReactionType = (NotWorkDayReactionTypeJson)contractScheduleSettings.RedDateReactionType,
                WeekendReactionType = (NotWorkDayReactionTypeJson)contractScheduleSettings.WeekendReactionType
            };

            return result;
        }

        #endregion

        #region [ ToContractScheduleViewModel ]

        public static IEnumerable<ContractScheduleCollectionViewModel> ToContractScheduleCollection(
            this string contractScheduleCollectionJsonString
        )
        {
            if (contractScheduleCollectionJsonString.IsNullOrEmpty())
            {
                return new List<ContractScheduleCollectionViewModel>();
            }

            return contractScheduleCollectionJsonString
                .SafeDeserialize<IList<ContractScheduleCollectionJsonModel>>(null)
                .ToContractScheduleCollectionBulk();
        }

        public static IEnumerable<ContractScheduleCollectionViewModel> ToContractScheduleCollectionBulk(
            this IList<ContractScheduleCollectionJsonModel> contractScheduleCollectionJson
        )
        {
            if (contractScheduleCollectionJson == null || contractScheduleCollectionJson.Count == 0)
            {
                yield break;
            }

            foreach (var el in contractScheduleCollectionJson)
            {
                yield return el.ToContractScheduleCollection();
            }
        }

        public static ContractScheduleCollectionViewModel ToContractScheduleCollection(this ContractScheduleCollectionJsonModel contractScheduleCollectionJsonModel)
        {
            if (contractScheduleCollectionJsonModel == null)
            {
                return null;
            }

            var result = new ContractScheduleCollectionViewModel
            {
                ContractSchedule = contractScheduleCollectionJsonModel.ContractSchedule.ToContractSchedule(),
                CreateDate = DateTime.Parse(contractScheduleCollectionJsonModel.CreateDate),
                ScheduleDateFrom = DateTime.ParseExact(contractScheduleCollectionJsonModel.ScheduleDateFrom, _timeFormat, null),
                ScheduleDateTo = DateTime.ParseExact(contractScheduleCollectionJsonModel.ScheduleDateTo, _timeFormat, null)
            };

            return result;
        }

        private static ContractScheduleViewModel ToContractSchedule(this ContractScheduleJsonModel contractScheduleJsonModel)
        {
            if (contractScheduleJsonModel == null)
            {
                return null;
            }

            var result = new ContractScheduleViewModel
            {
                StepInitialDateSchedule = contractScheduleJsonModel.StepInitialDateSchedule.ToStepInitialDate(),
                ScheduleDates = contractScheduleJsonModel.ScheduleDates.Select(x => DateTime.ParseExact(x, _timeFormat, null)).ToList(),
                ExcludeDates = contractScheduleJsonModel.ExcludeDates.Select(x => DateTime.ParseExact(x, _timeFormat, null)).ToList(),
                Crons = contractScheduleJsonModel.Crons,
                ExcludeCrons = contractScheduleJsonModel.ExcludeCrons,
                ContractScheduleSettings = contractScheduleJsonModel.ContractScheduleSettings.ToContractScheduleSettings()
            };

            return result;
        }

        private static StepInitialDateScheduleViewModel ToStepInitialDate(this StepInitialDateJsonModel stepInitialDateScheduleJsonModel)
        {
            if (stepInitialDateScheduleJsonModel == null)
            {
                return null;
            }

            var result = new StepInitialDateScheduleViewModel
            {
                InitialDate = DateTime.ParseExact(stepInitialDateScheduleJsonModel.InitialDate, _timeFormat, null),
                StepValue = stepInitialDateScheduleJsonModel.StepValue
            };

            return result;
        }

        private static ContractScheduleSettingsViewModel ToContractScheduleSettings(this ContractScheduleSettingsJsonModel contractScheduleSettingsJson)
        {
            if (contractScheduleSettingsJson == null)
            {
                return null;
            }

            var result = new ContractScheduleSettingsViewModel
            {
                EnableWeekendWorkDates = contractScheduleSettingsJson.EnableWeekendWorkDates,
                RedDateReactionType = (NotWorkDayReactionType)contractScheduleSettingsJson.RedDateReactionType,
                WeekendReactionType = (NotWorkDayReactionType)contractScheduleSettingsJson.WeekendReactionType
            };

            return result;
        }

        #endregion
    }
}
