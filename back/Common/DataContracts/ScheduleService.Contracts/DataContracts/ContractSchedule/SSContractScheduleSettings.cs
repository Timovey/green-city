namespace ScheduleService.Contracts.DataContracts.ContractSchedule
{
    public class SSContractScheduleSettings
    {
        /// <summary> Учитывать ли рабочие дни по выходным </summary>
        public bool EnableWeekendWorkDates { get; set; } = true;

        /// <summary> Что делать в случае выпадения графика на праздник </summary>
        public SSNotWorkDayReactionType RedDateReactionType { get; set; }

        /// <summary> Что делать в случае выпадения графика на выходной </summary>
        public SSNotWorkDayReactionType WeekendReactionType { get; set; }
    }
}
