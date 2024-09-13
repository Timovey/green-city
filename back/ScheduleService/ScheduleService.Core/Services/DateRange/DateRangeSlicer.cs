using ScheduleService.Contracts.DataContracts.DateRange;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ScheduleService.Core.Services.DateRange
{
    public class DateRangeSlicer : IDateRangeSlicer
    {
        public IList<T> GetSortedTimeSlots<T>(IList<T> originalSlots, TimeSpan? borderOffsetValue = null)
            where T : BaseDateRangeModel, new()
        {
            if (originalSlots == null || originalSlots.Count == 0)
            {
                return originalSlots;
            }
            //!_! Задаем смещение по умолчанию в 1 день
            TimeSpan newBorderOffsetValue = TimeSpan.FromDays(1);
            if (borderOffsetValue != null)
                newBorderOffsetValue = borderOffsetValue.Value;

            //!_! Берем все границы в общую кучу чтобы нарезать тупые слоты
            var times = originalSlots.Select(ts => ts.StartDate.Date)
                .Concat(originalSlots.Select(ts => ts.EndDate.Date))
                .OrderBy(dt => dt);

            //!_! Нарезаем тупые слоты по отсортированным уникальным точкам
            var slicedSlots = Enumerable.Range(0, times.Count() - 1)
                .Select(i => new T { StartDate = times.ElementAt(i), EndDate = times.ElementAt(i + 1) });

            var result = new List<T>();
            var prevSlot = default(T);
            foreach (var ts in slicedSlots)
            {
                //!_! Ищем все заданные слоты которые попали в один из нарезанных тупых слотов +
                // сразу избавляемся от наложенных друг на друга диапазонов отдавая приоритет более свежему
                var originalSlotsPerSlot = originalSlots
                    .Where(origTS => ts.StartDate.Date >= origTS.StartDate.Date && ts.EndDate.Date <= origTS.EndDate.Date)
                    .OrderByDescending(x => x.CreateDate)
                    .FirstOrDefault();

                if (originalSlotsPerSlot == null)
                {
                    continue;
                }

                var newSlot = (T)originalSlotsPerSlot.Clone();

                if (prevSlot == default)
                {
                    newSlot.StartDate = ts.StartDate;
                    newSlot.EndDate = ts.EndDate;
                }
                else
                {
                    //!_! Если предыдущий слот - тот же слот что и сейчас, то делаем из них один большой слот
                    if (prevSlot.Id.Equals(newSlot.Id))
                    {
                        result.Remove(prevSlot);
                        newSlot.StartDate = prevSlot.StartDate;
                    }
                    else
                    {
                        newSlot.StartDate = ts.StartDate;

                        //!_! Если дата начала нового слота оказалась равна дате окончания предыдущего, то нужно применит смещение
                        // потому что неясно к какому диапазону отнести эту конфликтную точку
                        if (prevSlot.EndDate.Date == newSlot.StartDate.Date)
                        {
                            //!_! Если предыдущий слот имеет больший приоритет,
                            // то необходимо сместить начало текущего слота
                            if (prevSlot.CreateDate > newSlot.CreateDate)
                            {
                                //!_! Если еще можем смещать, то смещаем
                                if (CanApplyOffset(newSlot))
                                {
                                    newSlot.StartDate = newSlot.StartDate.Add(newBorderOffsetValue);
                                }
                                else //!_! Если больше не можем смещать, то просто пропускаем добавление нового слота
                                {
                                    continue;
                                }
                            }
                            else //!_! Иначе смещаем конец предыдущего слота назад
                            {
                                //!_! Если еще можем смещать, то смещаем
                                if (CanApplyOffset(prevSlot))
                                {
                                    prevSlot.EndDate = prevSlot.EndDate.Add(-newBorderOffsetValue);
                                }
                                else //!_! Если больше не можем смещать, то удаляем предыдущий слот
                                {
                                    result.Remove(prevSlot);
                                }
                            }
                        }
                    }
                }

                newSlot.EndDate = ts.EndDate;
                prevSlot = newSlot;

                result.Add(newSlot);
            }

            return result;
        }

        private bool CanApplyOffset<T>(T slot)
            where T : BaseDateRangeModel
        {
            //!_! Смещение нельзя применять если слот уже некуда уменьшать
            return slot.StartDate.Date != slot.EndDate.Date;
        }
    }
}
