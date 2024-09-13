using SDContracts.DataContracts.Contract.ScheduleModels.Mission;
using SDContracts.Filter;
using SDContracts.ViewModels.ContractSchedule.Mission;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SDService.Database.Interfaces
{
    public interface IContractScheduleMissionStorage
    {
        /// <summary>
        /// Получить задания на вывоз по фильтру
        /// </summary>
        /// <param name="filter"> Фильтр </param>
        /// <returns></returns>
        Task<IList<ContractScheduleMissionViewModel>> GetFilteredContractScheduleMission(ContractScheduleMissionFilterModel filter);

        /// <summary>
        /// Добавление заданий с параметрами в транзакции
        /// </summary>
        /// <param name="missions"> Миссии </param>
        /// <param name="from"> Параметр настроек удаления, с какой даты удалять задачи </param>
        /// <param name="to"> Параметр настроек удаления, по какоую дату удалять задачи </param>
        /// <param name="deletePrevCompleted"> Параметр настроек удаления, флаг необходимости удаления завершенных задач на дату </param>
        /// <param name="deleteExistingNotFailedOnDate"> Параметр настроек удаления, флаг необходимости удаления уже созданных на диапазон непровальных задач </param>
        /// <returns></returns>
        Task<bool> AddMissionsWithParamsAsync(
            IList<CreateMissionContract> missions,
            DateTime from = default,
            DateTime to = default,
            bool deletePrevCompleted = false,
            bool deleteExistingNotFailedOnDate = false
        );
    }
}
