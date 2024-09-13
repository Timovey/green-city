using GreenCity.Http;
using System;
using System.Collections.Generic;

namespace ExternalApi.Interface.Models.Sd.Contract
{
    public class ExtCreateContractRequest : CommonHttpContract
    {
        /// <summary> Номер договора </summary>
        public string ContractNumber { get; set; }

        /// <summary> ИД контрагента </summary>
        public int ContractClientId { get; set; }

        /// <summary> Дата создания договора </summary>
        public DateTime CreateDate { get; set; }

        /// <summary> Дата начала действия договора </summary>
        public DateTime StartDate { get; set; }

        /// <summary> Срок договора </summary>
        public DateTime? EndDate { get; set; }

        /// <summary> Способ отправки документов </summary>
        public string SendDocsType { get; set; }

        /// <summary> Статус подписания договора </summary>
        public bool ContractStatus { get; set; }

        /// <summary> Комментарий к договору </summary>
        public string Comment { get; set; }

        /// <summary> Флаг проверки записи сотрудником </summary>
        public bool IsValidated { get; set; }

        /// <summary> Включить ли автопродление графика </summary>
        public bool IsAutorenewSchedules { get; set; }

        /// <summary> Прикрепленные к договорам площадки с контейнерами </summary>
        public IList<ExtCreateContainer2ScheduleRequest> AttachedContainers { get; set; }
    }
}
