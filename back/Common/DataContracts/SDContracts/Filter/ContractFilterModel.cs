using GreenCity.Http;
using SDContracts.SortModels;
using System.Collections.Generic;

namespace SDContracts.Filter
{
    /// <summary>
    /// Фильтр модель для договоров
    /// </summary>
    public class ContractFilterModel : CommonHttpFilter
    {
        /// <summary> Идентификаторы договоров </summary>
        public IList<int> ContractIds { get; set; }

        /// <summary> Номер договора </summary>
        public string ContractNumber { get; set; }

        /// <summary>
        /// ИД контрагентов (клиента)
        /// </summary>
        public IList<int> ContractClientIds { get; set; }

        /// <summary> Статус подписания договора </summary>
        public string Status { get; set; }

        /// <summary> Способ отправки документов </summary>
        public string SendDocsType { get; set; }

        public bool? ContractStatus { get; set; }

        /// <summary> Прикрепленные к договорам площадки с контейнерами </summary>
        public IList<int> AttachedStands { get; set; }

        /// <summary> Прикрепленные к договорам площадки с контейнерами </summary>
        public IList<int> AttachedContainers { get; set; }

        /// <summary> Флаг проверки записи сотрудником </summary>
        public bool? IsValidated { get; set; }

        public ContractSortModel Sorting { get; set; }
    }
}
