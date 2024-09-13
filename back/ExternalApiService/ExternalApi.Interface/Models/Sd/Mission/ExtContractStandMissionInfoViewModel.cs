using ExternalApi.Interface.Models.Sd.Stand;
using System.Collections.Generic;

namespace ExternalApi.Interface.Models.Sd.Mission
{
    public class ExtContractStandMissionInfoViewModel
    {
        public int StandId { get; set; }

        /// <summary> Адрес </summary>
        public string Address { get; set; }

        /// <summary> Координаты </summary>
        public ExtStandCoordinatesModel Coordinates { get; set; }

        /// <summary>
        /// Тип контейнерной площадки
        /// 0 - Неизвестно
        /// 1 - Обычный (с договором и контрагентами)
        /// 2 - Жилой фонд
        /// </summary>
        public ExtStandType StandType { get; set; }

        /// <summary>
        /// ИД прикрепленных контейнеров
        /// </summary>
        public IList<ExtContractContainerInfoMissionViewModel> AttachedContainers { get; set; }
    }
}
