using SDContracts.Enums;
using SDContracts.SpecificModels.ContainerStand;
using System.Collections.Generic;

namespace SDContracts.ViewModels.ContractSchedule.Mission
{
    public class ContractStandMissionInfoViewModel
    {
        public int StandId { get; set; }

        /// <summary> Адрес </summary>
        public string Address { get; set; }

        /// <summary> Координаты </summary>
        public StandCoordinatesModel Coordinates { get; set; }

        /// <summary>
        /// Тип контейнерной площадки
        /// 0 - Неизвестно
        /// 1 - Обычный (с договором и контрагентами)
        /// 2 - Жилой фонд
        /// </summary>
        public StandType StandType { get; set; }

        /// <summary>
        /// ИД прикрепленных контейнеров
        /// </summary>
        public IList<ContractContainerInfoMissionViewModel> AttachedContainers { get; set; }
    }
}
