using GreenCity.Http;
using System.Collections.Generic;

namespace ExternalApi.Interface.Models.Sd.Container
{
    public class ExtUpdateContainerRequest : CommonHttpContract
    {
        public IList<int> ContainerIds { get; set; }

        /// <summary> Наименование контейнера </summary>
        public string Name { get; set; }

        /// <summary>
        /// Тип тары
        /// </summary>
        public int ContainerTypeId { get; set; }

        /// <summary>
        /// Тип мусора
        /// 0 - Неизвестно
        /// 1 - ТКО
        /// 2 - КГО
        /// 3 - 4 Класс
        /// 4 - 5 Класс
        /// 5 - Строительный
        /// </summary>
        public ExtTrashType TrashType { get; set; }

        /// <summary> Id контейнерной площадки </summary>
        public int? ContainerStandId { get; set; }

        /// <summary> Флаг проверки записи сотрудником </summary>
        public bool IsValidated { get; set; }
    }
}
