using TransportContracts.DataContracts.Enums;

namespace TransportService.Core.EnumConverters
{
    internal static class ErrorsConverter
    {
        internal static DateWorkedDriverType FillErrors(
            this TransportWorkDateType transportWorkDateType,
            Dictionary<int, DriverWorkDateType> driversShedules)
        => transportWorkDateType switch
        {
            TransportWorkDateType.Unknown => DateWorkedDriverType.NotWork,
            TransportWorkDateType.WorkFiveTwo => ComputeDrivers(driversShedules, DriverWorkDateType.WorkFiveTwo, 1),
            TransportWorkDateType.WorkThreeThree => ComputeDrivers(driversShedules, DriverWorkDateType.WorkThreeThree, 1),
            TransportWorkDateType.WorkSixThree => ComputeDriversSixThree(driversShedules),
            TransportWorkDateType.NotShedule => DateWorkedDriverType.NotShedule,
            TransportWorkDateType.RepairFirst => ComputeRepairWorkShift(driversShedules, 1),
            TransportWorkDateType.RepairSecond => ComputeRepairWorkShift(driversShedules, 2),
            TransportWorkDateType.Rent => DateWorkedDriverType.Rent,
            TransportWorkDateType.NotDrivers => DateWorkedDriverType.NotDrivers,
            TransportWorkDateType.RepairAll => DateWorkedDriverType.NotWork,
            TransportWorkDateType.Holidays => DateWorkedDriverType.NotWork,
            TransportWorkDateType.TO => DateWorkedDriverType.NotWork,
            _ => DateWorkedDriverType.NotWork
        };
        private static DateWorkedDriverType ComputeDrivers(Dictionary<int, DriverWorkDateType> driversShedules,
            DriverWorkDateType compareWorkDate,
            int targetCountDrivers = 1)
        {
            int driversCount = 0;
            foreach(var driver in driversShedules.Values)
            {
                if(driver == compareWorkDate)
                {
                    driversCount++;
                }
            }
            if(driversCount == 0)
            {
                return DateWorkedDriverType.NotDrivers;
            }
            else if(driversCount < targetCountDrivers)
            {
                return DateWorkedDriverType.WorkFew;
            }
            else if(driversCount == targetCountDrivers)
            {
                return DateWorkedDriverType.Work;
            }
            else
            {
                return DateWorkedDriverType.WorkMany;
            }
        }

        private static DateWorkedDriverType ComputeDriversSixThree(Dictionary<int, DriverWorkDateType> driversShedules)
        {
            int driversFirstCount = 0;
            int driversSecondCount = 0;
            foreach (var driver in driversShedules.Values)
            {
                if (driver == DriverWorkDateType.WorkFirst)
                {
                    driversFirstCount++;
                }
                else if (driver == DriverWorkDateType.WorkSecond)
                {
                    driversSecondCount++;
                }
            }
            //если не нашлось рабочих на обе смены, то непорядок
            if (driversFirstCount + driversSecondCount == 0)
            {
                return DateWorkedDriverType.NotDrivers;
            }
            //если рабочий только один
            else if (driversFirstCount + driversSecondCount == 1)
            {
                return DateWorkedDriverType.WorkFew;
            }
            //если рабочий в каждую смену только один
            else if (driversFirstCount == 1 && driversSecondCount == 1)
            {
                return DateWorkedDriverType.Work;
            }
            else
            {
                return DateWorkedDriverType.WorkMany;

            }
        }

        private static DateWorkedDriverType ComputeRepairWorkShift(Dictionary<int, DriverWorkDateType> driversShedules, int workShift = 1)
        {

            int driversFirstCount = 0;
            int driversSecondCount = 0;
            foreach (var driver in driversShedules.Values)
            {
                if (driver == DriverWorkDateType.WorkFirst)
                {
                    driversFirstCount++;
                }
                else if (driver == DriverWorkDateType.WorkSecond)
                {
                    driversSecondCount++;
                }
            }
            //если не нашлось рабочих на обе смены, то непорядок

            if(driversFirstCount + driversSecondCount == 0)
            {
                return DateWorkedDriverType.WorkFew;
            }
            //если рабочий в свободную смену только один, а в смену ремонта никого нет
            else if ((workShift == 1 && driversFirstCount == 0 && driversSecondCount == 1) 
                || (workShift == 2 && driversSecondCount == 0 && driversFirstCount == 1))
            {
                return DateWorkedDriverType.Work;
            }
            else
            {
                return DateWorkedDriverType.WorkMany;

            }
        }
    }
}
