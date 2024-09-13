using TransportContracts.DataContracts.Enums;

namespace TransportService.Core.EnumConverters
{
    internal static class VacationTypeConverter
    {
        internal static TransportWorkDateType ToTransportRepairType(this RepairType repairType, WorkShift repairCardWorkShift)
        {
            //сначала смотрим на тип карточки - ремонт это или ТО, или вообще неизвестно
            TransportWorkDateType res = TransportWorkDateType.Unknown;
            switch (repairType)
            {
                case RepairType.Repair:
                    res = TransportWorkDateType.RepairAll;
                    break;
                case RepairType.TO:
                    res = TransportWorkDateType.TO;
                    break;
                default:
                    res = TransportWorkDateType.Unknown;
                    break;
            }

            //затем смотрим на смену
            //!ВАЖНО необходимость проверять смену ремонта при создании сущности ремонта
            //если только это ремонт, график 6/3
            switch (repairCardWorkShift)
            {
                case WorkShift.AllDay:
                    res = TransportWorkDateType.RepairAll;
                    break;
                case WorkShift.First:
                    res = TransportWorkDateType.RepairFirst;
                    break;
                case WorkShift.Second:
                    res = TransportWorkDateType.RepairSecond;
                    break;
                default:
                    break;
            }
            return res;
        }

        internal static DriverWorkDateType ToDriverVacationType(this DriverVacationType driverVacationType)
         => driverVacationType switch
         {
             DriverVacationType.Unknown => DriverWorkDateType.Unknown,
             DriverVacationType.Vacation => DriverWorkDateType.Vacation,
             DriverVacationType.SickLeave => DriverWorkDateType.SickLeave,
             DriverVacationType.NotCome => DriverWorkDateType.NotCome,
             _ => DriverWorkDateType.Unknown
         };
    }
}
