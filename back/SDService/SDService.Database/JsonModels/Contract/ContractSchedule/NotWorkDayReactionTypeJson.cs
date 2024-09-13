namespace SDService.Database.JsonModels.Contract.ContractSchedule
{
    public enum NotWorkDayReactionTypeJson
    {
        /// <summary> Неизвестно </summary>
        Unknown = 0,

        /// <summary> Ближайший рабочий день вперед </summary>
        NextWorkDay = 1,

        /// <summary> Ближайший рабочий день назад </summary>
        PrevWorkDat = 2,

        /// <summary> Ближайший рабочий </summary>
        NearestWorkingDay = 3,

        /// <summary> Работаем в праздник или выходной </summary>
        Work = 4,

        /// <summary> Просто не работаем </summary>
        Ignore = 5
    }
}
