namespace SDService.Database.JsonModels.Contract.ContractSchedule
{
    public class ContractScheduleSettingsJsonModel
    {
        /// <summary> Учитывать ли рабочие дни по выходным </summary>
        public bool EnableWeekendWorkDates { get; set; } = true;

        /// <summary> Что делать в случае выпадения графика на праздник </summary>
        public NotWorkDayReactionTypeJson RedDateReactionType { get; set; }

        /// <summary> Что делать в случае выпадения графика на выходной </summary>
        public NotWorkDayReactionTypeJson WeekendReactionType { get; set; }
    }
}
