using SDContracts.DataContracts.Contract.ScheduleModels;

namespace SDContracts.ViewModels.ContractSchedule
{
    public class ContractScheduleSettingsViewModel
    {
        /// <summary> Учитывать ли рабочие дни по выходным </summary>
        public bool EnableWeekendWorkDates { get; set; } = true;

        /// <summary> Что делать в случае выпадения графика на праздник </summary>
        public NotWorkDayReactionType RedDateReactionType { get; set; }

        /// <summary> Что делать в случае выпадения графика на выходной </summary>
        public NotWorkDayReactionType WeekendReactionType { get; set; }
    }
}
