using System.Collections.Generic;

namespace ExternalApi.Interface.Models.Sd.Contract.ScheduleModels
{
    /// <summary>
    /// Модель расписания для договора (сериализуемая часть)
    /// </summary>
    public class ExtContractScheduleSerializedModel
    {
        /// <summary> Задание графика с начальной датой и шагом </summary>
        public ExtStepInitialDateSchedule StepInitialDateSchedule { get; set; }

        /// <summary> Крон графики </summary>
        public IList<string> Crons { get; set; }

        /// <summary> Исключающие кроны </summary>
        public IList<string> ExcludeCrons { get; set; }

        /// <summary> Настройки графика </summary>
        public ExtContractScheduleSettings ContractScheduleSettings { get; set; }
    }
}
