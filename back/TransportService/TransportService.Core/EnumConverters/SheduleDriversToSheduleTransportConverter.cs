using TransportContracts.DataContracts.Enums;

namespace TransportService.Core.EnumConverters
{
    internal static class SheduleDriversToSheduleTransportConverter
    {
        internal static TransportWorkDateType CompareAndChangeTransportSheduleType(
            this TransportWorkDateType transportWorkDateType,
            DateTime currentDate,
            Dictionary<int, DriverWorkDateType> driversShedules,
            IList<DateTime> redDates, IList<DateTime> weekendWorkDates, IList<DateTime> transportDutyShifts)
        => transportWorkDateType switch
        {
            TransportWorkDateType.WorkFiveTwo => CompareFiveTwo(driversShedules, currentDate, redDates, weekendWorkDates, transportDutyShifts),
            TransportWorkDateType.WorkThreeThree => CompareThreeThree(driversShedules),
            TransportWorkDateType.WorkSixThree => CompareSixThree(driversShedules),
            _ => transportWorkDateType
        };

        private static TransportWorkDateType CompareFiveTwo(Dictionary<int, DriverWorkDateType> driversShedules,
            DateTime currintDate, IList<DateTime> redDates, IList<DateTime> weekendWorkDates, IList<DateTime> transportDutyShifts)
        {
            //если дежурная смена - то работает в любом случае
            if (transportDutyShifts.Contains(currintDate))
            {
                return TransportWorkDateType.DutyShift;
            }
            //сначала проверяем, что сегодня выходной
            if (redDates.Contains(currintDate) && !weekendWorkDates.Contains(currintDate))
            {
                return TransportWorkDateType.Holidays;
            }
            //если выходные
            //и нет дежурных смен и рабочих дат
            if(!transportDutyShifts.Contains(currintDate) && !weekendWorkDates.Contains(currintDate) && (
                currintDate.DayOfWeek == DayOfWeek.Saturday
                || currintDate.DayOfWeek == DayOfWeek.Sunday))
            {
                return TransportWorkDateType.Holidays;
            }
            foreach (var driverSh in driversShedules.Values)
            {
                if(driverSh == DriverWorkDateType.WorkFiveTwo)
                {
                    return TransportWorkDateType.WorkFiveTwo;
                }
            }
            return TransportWorkDateType.NotDrivers;
        }

        private static TransportWorkDateType CompareThreeThree(Dictionary<int, DriverWorkDateType> driversShedules)
        {
            foreach (var driverSh in driversShedules.Values)
            {
                if (driverSh == DriverWorkDateType.WorkThreeThree)
                {
                    return TransportWorkDateType.WorkThreeThree;
                }
            }
            return TransportWorkDateType.NotDrivers;
        }

        private static TransportWorkDateType CompareSixThree(Dictionary<int, DriverWorkDateType> driversShedules)
        {
            //проверяем по флагам, что на этот день есть все водилы
            //если будет найден только 1 водила, транспорт все равно работает по этому графику
            bool isFirstWorkShift = false;
            bool isSecondWorkShift = false;
            foreach (var driverSh in driversShedules.Values)
            {
                if (driverSh == DriverWorkDateType.WorkFirst)
                {
                    isFirstWorkShift = true;
                }
                if (driverSh == DriverWorkDateType.WorkSecond)
                {
                    isSecondWorkShift = true;
                }
            }
            if(isFirstWorkShift || isSecondWorkShift)
            {
                return TransportWorkDateType.WorkSixThree;
            }
            return TransportWorkDateType.NotDrivers;
        }
    }
}
