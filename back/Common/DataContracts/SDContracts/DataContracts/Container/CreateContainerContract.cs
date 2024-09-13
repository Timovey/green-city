using GreenCity.Http;
using GreenCity.Http.Attributes.Validation;
using SDContracts.Enums;

namespace SDContracts.DataContracts.Container
{
    public class CreateContainerContract : CommonHttpContract
    {
        /// <summary> Наименование контейнера </summary>
        [GCMaxLength(300, "Наименование контейнера"), GCRequired("Наименование контейнера")]
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
        public TrashType TrashType { get; set; }

        /// <summary> Id контейнерной площадки </summary>
        public int? ContainerStandId { get; set; }

        /// <summary> Флаг проверки записи сотрудником </summary>
        public bool IsValidated { get; set; }
    }
}
