using ExternalApi.Interface.Http;
using System.Collections.Generic;

namespace ExternalApi.Interface.Models.Sd.Contract
{
    /// <summary>
    /// Фильтр модель для договоров
    /// </summary>
    public class ExtContractFilterRequest : CommonHttpFilterV2
    {
        /// <summary> Номер договора </summary>
        public string ContractNumber { get; set; }

        /// <summary>
        /// ИД контрагентов (клиента)
        /// </summary>
        public IList<int> ContractClientIds { get; set; }

        /// <summary> Способ отправки документов </summary>
        public string SendDocsType { get; set; }

        /// <summary> Статус подписания договора </summary>
        public bool? ContractStatus { get; set; }

        /// <summary> Прикрепленные к договорам площадки с контейнерами </summary>
        public IList<int> AttachedStands { get; set; }

        /// <summary> Прикрепленные к договорам площадки с контейнерами </summary>
        public IList<int> AttachedContainers { get; set; }

        /// <summary> Флаг проверки записи сотрудником </summary>
        public bool? IsValidated { get; set; }

        public ExtContractSortModel Sorting { get; set; }
    }
}
