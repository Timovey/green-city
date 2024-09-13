namespace SDContracts.DataContracts.Contract.ScheduleModels
{
    public class ContractScheduleSettings
    {
        /// <summary> Учитывать ли рабочие дни по выходным </summary>
        public bool EnableWeekendWorkDates { get; set; } = true;

        /// <summary> Что делать в случае выпадения графика на праздник </summary>
        public NotWorkDayReactionType RedDateReactionType { get; set; }

        /// <summary> Что делать в случае выпадения графика на выходной </summary>
        public NotWorkDayReactionType WeekendReactionType { get; set; }
    }
}
