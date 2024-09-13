namespace MissionService.Contracts.Enums
{
    public enum MissionStandType : byte
    {
        Unknown = 0,

        /// <summary> Обычный с контграентами и договорами </summary>
        Default = 1,

        /// <summary> Жилой фонд </summary>
        HouseStock = 2
    }
}
