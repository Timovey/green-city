using GreenCity.Http.Attributes.Validation;
using System.Collections.Generic;

namespace ScheduleService.Contracts.DataContracts.ContractSchedule
{
    /// <summary>
    /// Модель расписания для договора (сериализуемая часть)
    /// </summary>
    public class SSContractScheduleSerializedModel
    {
        /// <summary> Задание графика с начальной датой и шагом </summary>
        public SSStepInitialDateSchedule StepInitialDateSchedule { get; set; }

        /// <summary> Крон графики </summary>
        public IList<string> Crons { get; set; }

        /// <summary> Исключающие кроны </summary>
        public IList<string> ExcludeCrons { get; set; }

        /// <summary> Настройки графика </summary>
        [GCRequired]
        public SSContractScheduleSettings ContractScheduleSettings { get; set; }
    }
}
