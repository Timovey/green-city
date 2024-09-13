using GreenCity.Http;
using SDContracts.SortModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SDContracts.Filter
{
    public class ContainerFilterModel : CommonHttpFilter
    {
        /// <summary> Наименование контейнера </summary>
        [MaxLength(300)]
        public string Name { get; set; }

        /// <summary>
        /// Тип тары
        /// </summary>
        public IList<int> ContainerTypeIds { get; set; }

        /// <summary>
        /// Тип мусора
        /// 0 - Неизвестно
        /// 1 - ТКО
        /// 2 - КГО
        /// 3 - 4 Класс
        /// 4 - 5 Класс
        /// 5 - Строительный
        /// </summary>
        public IList<byte> TrashTypes { get; set; }

        /// <summary> Id контейнерной площадки </summary>
        public IList<int> ContainerStandIds { get; set; }

        /// <summary> Флаг обозначающий, что контейнер фигурирует как минимум в одном договоре </summary>
        public bool? IsInContract { get; set; }

        /// <summary> Флаг проверки записи сотрудником </summary>
        public bool? IsValidated { get; set; }

        public ContainerSortModel Sorting { get; set; }
    }
}
