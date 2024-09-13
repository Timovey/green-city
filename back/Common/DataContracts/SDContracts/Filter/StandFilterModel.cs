using GreenCity.Http;
using SDContracts.Enums;
using SDContracts.SortModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SDContracts.Filter
{
    /// <summary>
    /// Фильтр модель для контейнерных площадок
    /// </summary>
    public class StandFilterModel : CommonHttpFilter
    {
        /// <summary> Признак режимного объекта </summary>
        public bool? IsHighSecurityObject { get; set; }

        /// <summary> Адрес </summary>
        [MaxLength(300)]
        public string Address { get; set; }

        /// <summary> Путь подъезда </summary>
        [MaxLength(500)]
        public string DriveWay { get; set; }

        /// <summary> Контакты </summary>
        [MaxLength(300)]
        public string Contacts { get; set; }

        /// <summary>
        /// Тип контейнерной площадки
        /// 0 - Неизвестно
        /// 1 - Обычный (с договором и контрагентами)
        /// 2 - Жилой фонд
        /// </summary>
        public IList<StandType> StandTypes { get; set; }

        /// <summary> Есть ли основание </summary>
        public bool? HaveBase { get; set; }

        /// <summary> Есть ли крыша </summary>
        public bool? HaveRoof { get; set; }

        /// <summary> Есть ли ограждение </summary>
        public bool? HaveFence { get; set; }

        /// <summary> ИД контейнеров </summary>
        public IList<int> ContainerIds { get; set; }

        /// <summary> Флаг проверки записи сотрудником </summary>
        public bool? IsValidated { get; set; }

        public MissionInfoEntityType MissionInfoEntityType { get; set; } = MissionInfoEntityType.Real;

        /// <summary> Настройки сортировки </summary>
        public StandSortModel Sorting { get; set; }
    }
}
