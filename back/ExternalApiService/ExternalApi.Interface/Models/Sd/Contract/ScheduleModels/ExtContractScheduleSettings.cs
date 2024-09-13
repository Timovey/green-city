namespace ExternalApi.Interface.Models.Sd.Contract.ScheduleModels
{
    public class ExtContractScheduleSettings
    {
        /// <summary> Учитывать ли рабочие дни по выходным </summary>
        public bool EnableWeekendWorkDates { get; set; } = true;

        /// <summary> Что делать в случае выпадения графика на праздник </summary>
        public ExtNotWorkDayReactionType RedDateReactionType { get; set; }

        /// <summary> Что делать в случае выпадения графика на выходной </summary>
        public ExtNotWorkDayReactionType WeekendReactionType { get; set; }
    }
}
