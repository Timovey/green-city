using ScheduleService.Contracts.DataContracts.ContractSchedule;
using ScheduleService.Contracts.DataContracts.ContractSchedule.Mission;
using ScheduleService.Core.Helpers;
using ScheduleService.Database.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduleService.Core.Services.ContractSchedule
{
    public class ContractScheduleService : IContractScheduleService
    {
        private readonly IRedDatesStorage _redDatesStorage;
        private readonly IWeekendWorkDatesStorage _weekendWorkDatesStorage;

        public ContractScheduleService(
            IRedDatesStorage redDatesStorage,
            IWeekendWorkDatesStorage weekendWorkDatesStorage
        )
        {
            _redDatesStorage = redDatesStorage;
            _weekendWorkDatesStorage = weekendWorkDatesStorage;
        }

        /// <summary> Возвращает следующую дату от переданной на которую выпадает график, либо может не вернуть ни одной </summary>
        public async Task<(DateTime Date, IList<string> Errors)> GetNextFireContractScheduleDateOrEmptyAsync(
            SSContractScheduleModel schedule,
            DateTime fromDate,
            IList<DateTime> urgentDates = default,
            IList<DateTime> urgentCancelDates = default
        )
        {
            //!_! TODO можно попробовать распаралелить с использованием ConcurentList
            var includeDates = new List<DateTime>();
            var excludeDates = new List<DateTime>();
            var errorsList = new List<string>();

            if (schedule.Crons != default && schedule.Crons.Count > 0)
            {
                foreach (var cron in schedule.Crons)
                {
                    var cronResult = CronHelper.GetNextFireDateByCron(cron, fromDate);
                    if (cronResult.Errors != default && cronResult.Errors.Length > 0)
                    {
                        errorsList.AddRange(cronResult.Errors);
                        return (DateTime.MaxValue, errorsList);
                    }

                    includeDates.Add(cronResult.NextFireDate.Date);
                }
            }

            if (schedule.StepInitialDateSchedule?.StepValue != default)
            {
                includeDates.Add(GetNextFireDateByStepInitialDateSchedule(schedule.StepInitialDateSchedule, fromDate).Date);
            }

            //!_! Добавим те даты в исключенные, которые набрались во включенные и подходят хотябы под один исключающий крон
            if (schedule.ExcludeCrons != default && schedule.ExcludeCrons.Count > 0)
            {
                var result = new List<DateTime>();
                foreach (var date in includeDates.Distinct())
                {
                    foreach (var excludeCron in schedule.ExcludeCrons)
                    {
                        var dateValidationResult = CronHelper.IsDateFireByCron(date, excludeCron);

                        if (dateValidationResult.Errors != default && dateValidationResult.Errors.Length > 0)
                        {
                            errorsList.AddRange(dateValidationResult.Errors);
                            return (DateTime.MaxValue, errorsList);
                        }

                        if (dateValidationResult.IsFire)
                        {
                            excludeDates.Add(date.Date);
                            break;
                        }
                    }
                }
            }

            //!_! Убираем все дубли чтобы не путаться
            includeDates = includeDates.Distinct().ToList();
            excludeDates = excludeDates.Distinct().ToList();

            //!_! Теперь надо заменить все даты которые выпадают на выходные или праздники в зависимости от настроек
            var redDatesHashSet = (await _redDatesStorage.GetActualRedDatesAsync()).Select(x => x.Date.Date).ToHashSet();
            var weekendWorkDates = (await _weekendWorkDatesStorage.GetActualWeekendWorkDatesAsync()).Select(x => x.Date.Date).ToList();

            var totalDates = new List<DateTime>();
            bool isWeekend;
            foreach (var date in includeDates)
            {
                //!_! Если график выпал на красный день смотрим на настройки
                if (redDatesHashSet.TryGetValue(date.Date, out DateTime result))
                {
                    if (schedule.ContractScheduleSettings.RedDateReactionType == SSNotWorkDayReactionType.NextWorkDay)
                    {
                        totalDates.Add(DateHelper.GetNextWorkDate(date.Date, weekendWorkDates, redDatesHashSet));
                        continue;
                    }

                    if (schedule.ContractScheduleSettings.RedDateReactionType == SSNotWorkDayReactionType.PrevWorkDat)
                    {
                        totalDates.Add(DateHelper.GetPrevWorkDate(date.Date, weekendWorkDates, redDatesHashSet));
                        continue;
                    }

                    if (schedule.ContractScheduleSettings.RedDateReactionType == SSNotWorkDayReactionType.NearestWorkingDay)
                    {
                        totalDates.Add(DateHelper.GetNearestWorkDate(date.Date, weekendWorkDates, redDatesHashSet));
                        continue;
                    }

                    if (schedule.ContractScheduleSettings.RedDateReactionType == SSNotWorkDayReactionType.Work)
                    {
                        totalDates.Add(date.Date);
                        continue;
                    }

                    if (schedule.ContractScheduleSettings.RedDateReactionType == SSNotWorkDayReactionType.Ignore)
                        continue;
                }

                //!_! Проверяем является ли день выходным
                isWeekend = false;
                if (date.Date.DayOfWeek == DayOfWeek.Saturday || date.Date.DayOfWeek == DayOfWeek.Sunday)
                {
                    //Если включен учет рабочих выходных и текущая дата есть среди рабочих выходных, то день считаем рабочим
                    if (
                        schedule.ContractScheduleSettings.EnableWeekendWorkDates &&
                        weekendWorkDates != default &&
                        weekendWorkDates.Count > 0 &&
                        weekendWorkDates.FirstOrDefault(x => x.Date == date.Date) != default)
                    {
                        isWeekend = false;
                    }
                    else
                    {
                        isWeekend = true;
                    }
                }

                //!_! Если график выпал на выходной день смотрим на настройки
                if (isWeekend)
                {
                    if (schedule.ContractScheduleSettings.WeekendReactionType == SSNotWorkDayReactionType.NextWorkDay)
                    {
                        totalDates.Add(DateHelper.GetNextWorkDate(date.Date, weekendWorkDates, redDatesHashSet));
                        continue;
                    }

                    if (schedule.ContractScheduleSettings.WeekendReactionType == SSNotWorkDayReactionType.PrevWorkDat)
                    {
                        totalDates.Add(DateHelper.GetPrevWorkDate(date.Date, weekendWorkDates, redDatesHashSet));
                        continue;
                    }

                    if (schedule.ContractScheduleSettings.WeekendReactionType == SSNotWorkDayReactionType.NearestWorkingDay)
                    {
                        totalDates.Add(DateHelper.GetNearestWorkDate(date.Date, weekendWorkDates, redDatesHashSet));
                        continue;
                    }

                    if (schedule.ContractScheduleSettings.WeekendReactionType == SSNotWorkDayReactionType.Work)
                    {
                        totalDates.Add(date.Date);
                        continue;
                    }

                    if (schedule.ContractScheduleSettings.WeekendReactionType == SSNotWorkDayReactionType.Ignore)
                        continue;
                }

                totalDates.Add(date.Date);
            }

            totalDates = totalDates.Distinct().ToList();

            //!_! Вычитаем из дней на которые должен выпасть дни на которые не должен выпасть
            totalDates = totalDates.Except(excludeDates).ToList();

            //!_! Добавляем все включающие даты (не проверяем их на праздники и выходные тк сотрудник сам ввел эту дату)
            if (schedule.ScheduleDates != default && schedule.ScheduleDates.Count > 0)
            {
                totalDates.AddRange(schedule.ScheduleDates.Select(x => x.Date));
            }

            //!_! Теперь добавляем исключающие даты
            if (schedule.ExcludeDates != default && schedule.ExcludeDates.Count > 0)
            {
                totalDates = totalDates.Except(schedule.ExcludeDates.Select(x => x.Date)).ToList();
            }

            //!_! Учитываем срочные заявки
            if (urgentDates != default && urgentDates.Count > 0)
            {
                totalDates.AddRange(urgentDates);
            }

            if (urgentCancelDates != default && urgentCancelDates.Count > 0)
            {
                totalDates = totalDates.Except(urgentCancelDates).ToList();
            }

            return (totalDates.Select(date => date.Date).OrderBy(x => x).FirstOrDefault(x => x.Date >= fromDate.Date), default);
        }

        private DateTime GetNextFireDateByStepInitialDateSchedule(SSStepInitialDateSchedule stepInitialDateSchedule, DateTime currentDate)
        {
            //!_! На всякий случай чтобы не было непридвиденных переполнений в случае кривых графиков
            if (currentDate.Date.Year > 2070)
            {
                return DateTime.MaxValue;
            }

            var diffDays = (currentDate - stepInitialDateSchedule.InitialDate).Days;

            //!_! Если текущая дата оказалась меньше или равно начальной - то ближайший день это день который указан в initialDate
            if (diffDays < 1)
            {
                return stepInitialDateSchedule.InitialDate;
            }

            var mod = diffDays % stepInitialDateSchedule.StepValue.Value;
            if (mod == 0)
            {
                return currentDate;
            }

            //!_! Пример || 2.09.2022/10 || 15.09.2022 ||   13   || 3 || 15.09.2022 + (10-3)Day = 22.09.2022 || 
            return currentDate.AddDays(stepInitialDateSchedule.StepValue.Value - mod);
        }

        //Slice in business logic
        public async Task<(bool IsFire, IList<string> Errors)> IsContractScheduleCollectionFireOnDateAsync(
            IList<SSContractScheduleCollectionModel> slicedContractScheduleCollection,
            DateTime date,
            IList<DateTime> urgentDates = default,
            IList<DateTime> urgentCancelDates = default
        )
        {
            date = date.Date;

            //Если есть среди срочных отмен, то сразу возвращаем false
            if (urgentCancelDates != default && urgentCancelDates.Select(x => x.Date).Contains(date.Date))
            {
                return (false, default);
            }

            //Если есть среди срочных дат вывоза, то сразу возвращаем true
            if (urgentDates != default && urgentDates.Select(x => x.Date).Contains(date.Date))
            {
                return (true, default);
            }

            //Если нарезка произошла верно - то должен быть только один слот в который входит интересующая дата
            var interestedRanges = slicedContractScheduleCollection
                .Where(x =>
                    x.StartDate.Date <= date &&
                    x.EndDate.Date >= date)
                .ToList();

            if (interestedRanges.Count > 1)
            {
                return (false, new List<string> { "Слоты нарезаны неверно, переданная дата входит в несколько диапазонов сразу" });
            }

            //Если не найдено ни одного слота в который входит интересующая дата - то на этом все
            if (interestedRanges.Count == 0)
            {
                return (false, default);
            }

            var contractSchedule = interestedRanges.Single().ContractSchedule;

            //Если дата есть в исключающих - то на этом все
            if (contractSchedule.ExcludeDates != default && contractSchedule.ExcludeDates.Select(x => x.Date).Contains(date))
            {
                return (false, default);
            }

            //Если какой нибудь исключающий крон выпадает на дату - то возвраем false
            if (contractSchedule.ExcludeCrons != default)
            {
                foreach (var excludeCron in contractSchedule.ExcludeCrons)
                {
                    var dateValidationResult = CronHelper.IsDateFireByCron(date, excludeCron);

                    if (dateValidationResult.Errors != default && dateValidationResult.Errors.Length > 0)
                    {
                        return (false, dateValidationResult.Errors.ToList());
                    }

                    if (dateValidationResult.IsFire)
                    {
                        return (false, default);
                    }
                }
            }

            //Ищем сразу в списке дат (если была дата в другом диапазоне в который не входит переданная - это ошибка человека)
            var isFire = contractSchedule.ScheduleDates != default && contractSchedule.ScheduleDates.Select(x => x.Date).Contains(date);

            if (!isFire && contractSchedule.StepInitialDateSchedule?.StepValue != default)
            {
                isFire = IsStepInitialDateFireOnDate(contractSchedule.StepInitialDateSchedule, date);
            }

            if (!isFire && contractSchedule.Crons != default)
            {
                foreach (var excludeCron in contractSchedule.Crons)
                {
                    var dateValidationResult = CronHelper.IsDateFireByCron(date, excludeCron);

                    if (dateValidationResult.Errors != default && dateValidationResult.Errors.Length > 0)
                    {
                        //Значит крон невалидный передали - сразу выдаем ошибку
                        return (false, dateValidationResult.Errors.ToList());
                    }

                    if (dateValidationResult.IsFire)
                    {
                        //Если хоть по одному выпала дата - то на этом заканчиваем проверку всех кронов
                        isFire = true;
                        break;
                    }
                }
            }

            //Если ни один включающий параметр графика не стрельнул - то на этом все
            if (!isFire)
            {
                return (false, default);
            }

            //Теперь нужно проверить дату на настройки праздников и выходных
            var redDatesHashSet = (await _redDatesStorage.GetActualRedDatesAsync()).Select(x => x.Date.Date).ToHashSet();
            var weekendWorkDates = (await _weekendWorkDatesStorage.GetActualWeekendWorkDatesAsync()).Select(x => x.Date).ToList();

            //!_! Если график выпал на красный день смотрим на настройки
            if (redDatesHashSet.TryGetValue(date, out DateTime result))
            {
                //Если текущая дата это праздник и в настройках не РАБОТА то тогда день не выпадает и на этом все
                if (contractSchedule.ContractScheduleSettings.RedDateReactionType != SSNotWorkDayReactionType.Work)
                {
                    return (false, default);
                }
            }

            //!_! Проверяем является ли день выходным
            bool isWeekend = false;
            if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
            {
                //Если включен учет рабочих выходных и текущая дата есть среди рабочих выходных, то день считаем рабочим
                if (
                    contractSchedule.ContractScheduleSettings.EnableWeekendWorkDates &&
                    weekendWorkDates != default &&
                    weekendWorkDates.Count > 0 &&
                    weekendWorkDates.FirstOrDefault(x => x.Date == date) != default)
                {
                    isWeekend = false;
                }
                else
                {
                    isWeekend = true;
                }
            }

            //!_! Если график выпал на выходной день смотрим на настройки
            if (isWeekend)
            {
                //Если текущая дата это выходной и в настройках не РАБОТА то тогда день не выпадает и на этом все
                if (contractSchedule.ContractScheduleSettings.WeekendReactionType != SSNotWorkDayReactionType.Work)
                {
                    return (false, default);
                }
            }

            //Если все проверки пройдены то значит дата выпадает
            return (true, default);
        }

        private bool IsStepInitialDateFireOnDate(SSStepInitialDateSchedule stepInitialDateSchedule, DateTime date)
        {
            var diffDays = (date - stepInitialDateSchedule.InitialDate).Days;

            //!_! Если текущая дата оказалась меньше или равно начальной - то ближайший день это день который указан в initialDate
            if (diffDays < 0)
            {
                return false;
            }

            //!_! Если остаток от деления == 0 то значит выпадает на переданную дату
            var mod = diffDays % stepInitialDateSchedule.StepValue.Value;
            return mod == 0;
        }
    }
}
