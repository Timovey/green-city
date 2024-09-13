using System;
using System.Collections.Generic;

namespace SDContracts.ViewModels
{
    public class ContractViewModel
    {
        /// <summary> ИД договора </summary>
        public int ContractId { get; set; }

        /// <summary> Номер договора </summary>
        public string ContractNumber { get; set; }

        /// <summary> ИД контрагента </summary>
        public ContractContractClientViewModel ContractClient { get; set; }

        /// <summary> Статус подписания договора </summary>
        public string Status { get; set; }

        /// <summary> Дата создания договора </summary>
        public DateTime CreateDate { get; set; }

        /// <summary> Дата начала действия договора </summary>
        public DateTime StartDate { get; set; }

        /// <summary> Срок договора </summary>
        public DateTime? EndDate { get; set; }

        /// <summary> Способ отправки документов </summary>
        public string SendDocsType { get; set; }

        /// <summary> Норматив </summary>
        public string Normativ { get; set; }

        public bool ContractStatus { get; set; }

        /// <summary> Комментарий к договору </summary>
        public string Comment { get; set; }

        /// <summary> Прикрепленные к договорам площадки с контейнерами </summary>
        public IList<ContractStandViewModel> AttachedStands { get; set; }

        public bool IsValidated { get; set; }

        /// <summary> Включить ли автопродление графика </summary>
        public bool IsAutorenewSchedules { get; set; }

        /// <summary> Имя пользователя создавшего запись </summary>
        public string CreatedUserName { get; set; }
    }
}
