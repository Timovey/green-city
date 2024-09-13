using ScheduleService.Contracts.DataContracts.ContractSchedule;
using ScheduleService.Contracts.DataContracts.ContractSchedule.Mission;
using SDContracts.ViewModels.ContractSchedule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissionService.Core.Converters
{
    internal static class ContractScheduleConverter
    {
        internal static SSContractScheduleCollectionModel ToSSContractScheduleCollectionModel(
            this ContractScheduleCollectionViewModel sdContractSchedule
        )
        {
            if (sdContractSchedule == default)
                return default;

            return new SSContractScheduleCollectionModel
            {
                CreateDate = sdContractSchedule.CreateDate,
                StartDate = (sdContractSchedule.ScheduleDateFrom ?? DateTime.MinValue).Date,
                EndDate = (sdContractSchedule.ScheduleDateTo ?? DateTime.MaxValue).Date,
                ContractSchedule = sdContractSchedule.ContractSchedule != default ? new SSContractScheduleModel
                {
                    ScheduleDates = sdContractSchedule.ContractSchedule.ScheduleDates.Select(x => x.Date).ToList(),
                    ExcludeDates = sdContractSchedule.ContractSchedule.ExcludeDates.Select(x => x.Date).ToList(),
                    Crons = sdContractSchedule.ContractSchedule.Crons,
                    ExcludeCrons = sdContractSchedule.ContractSchedule.ExcludeCrons,
                    StepInitialDateSchedule = sdContractSchedule.ContractSchedule.StepInitialDateSchedule != default ? new SSStepInitialDateSchedule
                    {
                        InitialDate = sdContractSchedule.ContractSchedule.StepInitialDateSchedule.InitialDate,
                        StepValue = sdContractSchedule.ContractSchedule.StepInitialDateSchedule.StepValue
                    } : default,
                    ContractScheduleSettings = sdContractSchedule.ContractSchedule.ContractScheduleSettings != default ? new SSContractScheduleSettings
                    {
                        EnableWeekendWorkDates = sdContractSchedule.ContractSchedule.ContractScheduleSettings.EnableWeekendWorkDates,
                        RedDateReactionType = (SSNotWorkDayReactionType)sdContractSchedule.ContractSchedule.ContractScheduleSettings.RedDateReactionType,
                        WeekendReactionType = (SSNotWorkDayReactionType)sdContractSchedule.ContractSchedule.ContractScheduleSettings.WeekendReactionType
                    } : default
                } : default
            };
        }
    }
}
