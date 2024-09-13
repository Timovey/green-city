using GreenCity.Http.Attributes.Validation;
using System.Collections.Generic;

namespace SDContracts.DataContracts.Contract.ScheduleModels
{
    /// <summary>
    /// Модель расписания для договора (сериализуемая часть)
    /// </summary>
    public class ContractScheduleSerializedModel
    {
        /// <summary> Задание графика с начальной датой и шагом </summary>
        public StepInitialDateScheduleModel StepInitialDateSchedule { get; set; }

        /// <summary> Крон графики </summary>
        public IList<string> Crons { get; set; }

        /// <summary> Исключающие кроны </summary>
        public IList<string> ExcludeCrons { get; set; }

        /// <summary> Настройки графика </summary>
        [GCRequired("Настройки графика", "Заполните настройки графика")]
        public ContractScheduleSettings ContractScheduleSettings { get; set; }
    }
}
