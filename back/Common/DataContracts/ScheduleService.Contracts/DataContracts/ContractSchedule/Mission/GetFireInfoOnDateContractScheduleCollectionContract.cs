using GreenCity.Http;
using GreenCity.Http.Attributes.Validation;
using System;
using System.Collections.Generic;

namespace ScheduleService.Contracts.DataContracts.ContractSchedule.Mission
{
    /// <summary>
    /// Запрос на получение информации по датам сложного диапзонного графика вывоза контейнера
    /// </summary>
    public class GetFireInfoOnDateContractScheduleCollectionContract : CommonHttpContract
    {
        /// <summary>
        /// Ненарезанная коллекция графиков контейнера
        /// </summary>
        [GCRequired]
        public IList<SSContractScheduleCollectionModel> NotSlicedContractScheduleCollection { get; set; }

        /// <summary>
        /// Дата на которую необходимо проверить график
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Даты срочного вывоза
        /// </summary>
        public IList<DateTime> UrgentDates { get; set; }

        /// <summary>
        /// Даты срочной отмены вывоза
        /// </summary>
        public IList<DateTime> UrgentCancelDates { get; set; }
    }
}
