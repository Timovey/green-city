using TransportContracts.DataContracts.Enums;

namespace TransportService.Core.EnumConverters
{
    internal static class WorkTypeConverter
    {
        internal static TransportWorkDateType ToTransportWorkType(this WorkScheduleType workScheduleType)
           => workScheduleType switch
           {
               WorkScheduleType.Unknown => TransportWorkDateType.NotShedule,
               WorkScheduleType.ThreeThree => TransportWorkDateType.WorkThreeThree,
               WorkScheduleType.SixThree => TransportWorkDateType.WorkSixThree,
               WorkScheduleType.FiveTwo => TransportWorkDateType.WorkFiveTwo,
               _ => TransportWorkDateType.NotShedule
           };

        internal static TransportWorkDateType ToTransportRentWorkType(this TransportWorkDateType transportWorkDateType, RentDateType rentDateType)
           => rentDateType switch
           {
               RentDateType.InRent => TransportWorkDateType.Rent,
               RentDateType.Work => transportWorkDateType,
               _ => TransportWorkDateType.Unknown
           };

        internal static DriverWorkDateType ToDriverWorkType(this TransportWorkDateType transportWorkDate, DateTime targetDate, 
            DateTime startDateWork, IList<DateTime> redDates, IList<DateTime> weekendWorkDates, IList<DateTime> transportDutyShifts, 
            WorkShift workShift, TransportWorkDateType origWorkDateType)
        {
            var res = DriverWorkDateType.Unknown;

            //если дата начала работы по расписанию меньше просчитываемой даты
            if(startDateWork > targetDate)
            {
                return res;
            }
            //проверка на то, что оригинальное расписание транспорта есть,а также что он чинится весь день
            if((origWorkDateType == TransportWorkDateType.WorkFiveTwo || origWorkDateType == TransportWorkDateType.WorkThreeThree ||
                origWorkDateType == TransportWorkDateType.WorkSixThree) && transportWorkDate == TransportWorkDateType.RepairAll)
            {
                switch(origWorkDateType)
                {
                    //в графиках 6/3 и 3/3 мы проверяем, как по оригинальному расписанию ведет себя водитель
                    //работает или отдыхает
                    //и если он работает в этот день - то он едет на ремонт
                    //в графике 5/2 нет смысла проверять т.к. водитель один и он точно едет на ремонт
                    case TransportWorkDateType.WorkFiveTwo:
                        res = DriverWorkDateType.ToRepair;
                        break;
                    //работает по 3/3
                    case TransportWorkDateType.WorkThreeThree:
                        res = ComputeThreeThree(targetDate, startDateWork);
                        break;
                    //работает по 6/3
                    case TransportWorkDateType.WorkSixThree:
                        res = ComputeSixThree(targetDate, startDateWork, workShift);
                        break;
                }
                if(res == DriverWorkDateType.NotWork)
                {
                    
                }
                else
                {
                    res = DriverWorkDateType.ToRepair;
                }
                return res;
            }
            switch (transportWorkDate)
            {
                //работает по 5/2
                case TransportWorkDateType.WorkFiveTwo:
                    res = ComputeFiveTwo(targetDate, startDateWork, redDates, weekendWorkDates, transportDutyShifts);
                    break;
                //работает по 3/3
                case TransportWorkDateType.WorkThreeThree:
                    res = ComputeThreeThree(targetDate, startDateWork);
                    break;
                //работает по 6/3
                case TransportWorkDateType.WorkSixThree:
                    res = ComputeSixThree(targetDate, startDateWork, workShift);
                    break;
                //чинится в первую смену
                case TransportWorkDateType.RepairFirst:
                    res = ComputeSixThree(targetDate, startDateWork, workShift, 1);
                    break;
                //чинится во вторую смену
                case TransportWorkDateType.RepairSecond:
                    res = ComputeSixThree(targetDate, startDateWork, workShift, 2);
                    break;
                //чинится весь день
                case TransportWorkDateType.RepairAll:
                    res = DriverWorkDateType.ToRepair;
                    break;
                //если нет расписания
                case TransportWorkDateType.NotShedule:
                    res = DriverWorkDateType.Unknown;
                    break;
                default:
                    break;
            }
            return res;
        }

        /// <summary>
        /// Метод высчитывает как будет работать водитель в эту дату по графику 5/2
        /// </summary>
        /// <param name="startDateWork"></param>
        /// <param name="targetDate"></param>
        /// <param name="redDates"></param>
        /// <param name="weekendWorkDates"></param>
        /// <returns></returns>
        private static DriverWorkDateType ComputeFiveTwo(DateTime targetDate, DateTime startDateWork, 
            IList<DateTime> redDates, IList<DateTime> weekendWorkDates, IList<DateTime> transportDutyShifts)
        {
            if(transportDutyShifts.Contains(targetDate))
            {
                return DriverWorkDateType.WorkFiveTwo;
            }
            else if (weekendWorkDates.Contains(targetDate))
            {
                return DriverWorkDateType.WorkFiveTwo;
            }
            else if(redDates.Contains(targetDate) && !weekendWorkDates.Contains(targetDate))
            {
                return DriverWorkDateType.NotWork;
            }
            else
            {
                if(targetDate.DayOfWeek != DayOfWeek.Sunday && targetDate.DayOfWeek != DayOfWeek.Saturday)
                {
                    return DriverWorkDateType.WorkFiveTwo;
                }
                return DriverWorkDateType.NotWork;
            }
        }

        /// <summary>
        /// Метод высчитывает как будет работать водитель в эту дату по графику 3/3
        /// </summary>
        /// <param name="targetDate"></param>
        /// <param name="startDateWork"></param>
        /// <param name="redDates"></param>
        /// <param name="weekendWorkDates"></param>
        /// <returns></returns>
        private static DriverWorkDateType ComputeThreeThree(DateTime targetDate, DateTime startDateWork)
        {
            int range = (targetDate - startDateWork).Days;
            if(range % 6 < 3)
            {
                return DriverWorkDateType.WorkThreeThree;
            }
            else
            {
                return DriverWorkDateType.NotWork;
            }
        }

        private static DriverWorkDateType ComputeSixThree(DateTime targetDate, DateTime startDateWork, WorkShift workShift, int repairWorkShift = 0)
        {
            int range = (targetDate - startDateWork).Days;
            //если выходит со 2 смены - увеличиваем кол-во дней
            if(workShift == WorkShift.Second)
            {
                range += 3;
            }

            if (range % 9 < 3)
            {
                if(repairWorkShift == 1)
                {
                    return DriverWorkDateType.ToRepair;
                }
                return DriverWorkDateType.WorkFirst;
            }
            else if(range % 9 < 6)
            {
                if (repairWorkShift == 2)
                {
                    return DriverWorkDateType.ToRepair;
                }
                return DriverWorkDateType.WorkSecond;
            }
            else
            {
                return DriverWorkDateType.NotWork;
            }
        }
    }
}
