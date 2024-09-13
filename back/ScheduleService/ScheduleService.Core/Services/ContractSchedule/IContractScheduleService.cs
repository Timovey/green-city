using ScheduleService.Contracts.DataContracts.ContractSchedule;
using ScheduleService.Contracts.DataContracts.ContractSchedule.Mission;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ScheduleService.Core.Services.ContractSchedule
{
    /// <summary>
    /// Основные методы для работы с гибкими диапазонными графиками
    /// </summary>
    public interface IContractScheduleService
    {
        /// <summary>
        /// Метод для получения ближайшей даты работы графика
        /// </summary>
        /// <param name="schedule"> Гибкий диапазонный график контейнера </param>
        /// <param name="fromDate"> С какой даты начинать поиск (эта дата также может оказаться рабочей) </param>
        /// <param name="urgentDates"> Даты в которые график точно должен отработать </param>
        /// <param name="urgentCancelDates"> Даты в которые график точно не должен отработать </param>
        /// <returns> Ближайшую дату или минимальную в случае если нету ни одной ближайшей даты, либо массив ошибок </returns>
        Task<(DateTime Date, IList<string> Errors)> GetNextFireContractScheduleDateOrEmptyAsync(
            SSContractScheduleModel schedule,
            DateTime fromDate,
            IList<DateTime> urgentDates = default,
            IList<DateTime> urgentCancelDates = default
        );

        /// <summary>
        /// Выпадает ли нарезанный на слоты график на конкретную дату
        /// </summary>
        /// <param name="slicedContractScheduleCollection"> Уже нарезанная слоторезкой коллекция графиков контейнеров </param>
        /// <param name="date"> Интересующая дата </param>
        /// <param name="urgentDates"> Даты из срочных заявок </param>
        /// <param name="urgentCancelDates"> Даты из срочных отмен </param>
        /// <returns> Кортеж из бульки - выпадает или нет и списка ошибок </returns>
        Task<(bool IsFire, IList<string> Errors)> IsContractScheduleCollectionFireOnDateAsync(
            IList<SSContractScheduleCollectionModel> slicedContractScheduleCollection,
            DateTime date,
            IList<DateTime> urgentDates = default,
            IList<DateTime> urgentCancelDates = default
        );
    }
}
