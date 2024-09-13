using ScheduleService.Contracts.DataContracts.ContractSchedule;
using ScheduleService.Contracts.DataContracts.ContractSchedule.Mission;
using SDContracts.ViewModels.ContractSchedule.Mission;
using System.Collections.Generic;
using System.Linq;

namespace SDService.Tests.MappingTests
{
    public static class EqualMappingTestExtensions
    {
        public static bool IsEqual(this Container2ScheduleInfoMissionViewModel dest, Container2ScheduleInfoMissionViewModel source)
        {
            if (!dest.ScheduleComment.Equals(source.ScheduleComment))
                return false;

            if (dest.WorkTimeFrom != source.WorkTimeFrom)
                return false;

            if (dest.WorkTimeTo != source.WorkTimeTo)
                return false;

            if (dest.IsInRegistry != source.IsInRegistry)
                return false;

            if (dest.InRegistryFromDate != source.InRegistryFromDate)
                return false;

            return true;
        }

        public static bool IsEqual(this ContractContainerInfoMissionViewModel dest, ContractContainerInfoMissionViewModel source)
        {
            if (dest.ContainerId != source.ContainerId)
                return false;

            if (!dest.Name.Equals(source.Name))
                return false;

            if (dest.ContainerTypeId != source.ContainerTypeId)
                return false;

            if (dest.TrashType != source.TrashType)
                return false;

            if (!dest.Container2Schedule.IsEqual(source.Container2Schedule))
                return false;

            return true;
        }

        public static bool IsEqual(this SSStepInitialDateSchedule dest, SSStepInitialDateSchedule source)
        {
            if (dest.InitialDate != source.InitialDate)
                return false;

            if (dest.StepValue != source.StepValue)
                return false;

            return true;
        }

        public static bool IsEqual(this SSContractScheduleSettings dest, SSContractScheduleSettings source)
        {
            if (dest.EnableWeekendWorkDates != source.EnableWeekendWorkDates)
                return false;

            if (dest.RedDateReactionType != source.RedDateReactionType)
                return false;

            if (dest.WeekendReactionType != source.WeekendReactionType)
                return false;

            return true;
        }

        public static bool IsEqual(this SSContractScheduleModel dest, SSContractScheduleModel source)
        {
            if (!dest.StepInitialDateSchedule.IsEqual(source.StepInitialDateSchedule))
                return false;

            if (!dest.ContractScheduleSettings.IsEqual(source.ContractScheduleSettings))
                return false;

            foreach (var cron in dest.Crons)
            {
                if (source.Crons.Where(x => x.Equals(cron)).FirstOrDefault() == default)
                {
                    return false;
                }
            }

            foreach (var cron in dest.ExcludeCrons)
            {
                if (source.ExcludeCrons.Where(x => x.Equals(cron)).FirstOrDefault() == default)
                {
                    return false;
                }
            }

            foreach (var date in dest.ScheduleDates)
            {
                if (source.ScheduleDates.Where(x => x == date).FirstOrDefault() == default)
                {
                    return false;
                }
            }

            foreach (var date in dest.ExcludeDates)
            {
                if (source.ExcludeDates.Where(x => x == date).FirstOrDefault() == default)
                {
                    return false;
                }
            }

            return true;
        }

        public static bool IsEqual(this SSContractScheduleCollectionModel dest, SSContractScheduleCollectionModel source)
        {
            if (dest.CreateDate != source.CreateDate)
                return false;

            if (dest.StartDate != source.StartDate)
                return false;

            if (dest.EndDate != source.EndDate)
                return false;

            if (!dest.ContractSchedule.IsEqual(source.ContractSchedule))
                return false;

            return true;
        }

        public static bool IsEqual(this IList<SSContractScheduleCollectionModel> dest, IList<SSContractScheduleCollectionModel> source)
        {
            foreach (var el in dest)
            {
                if (source.Where(x => x.IsEqual(el)).FirstOrDefault() == default)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
