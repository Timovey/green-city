namespace SDContracts.Enums
{
    public enum MissionInfoEntityType : byte
    {
        /// <summary> Настоящая сущность доступная для отображения пользователю </summary>
        Real = 0,

        /// <summary> Ненастоящая заглушечная сущность для миссий </summary>
        FakeForMissionInfo = 1
    }
}
