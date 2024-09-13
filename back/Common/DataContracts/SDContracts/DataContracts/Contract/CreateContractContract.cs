using GreenCity.Http;
using GreenCity.Http.Attributes.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SDContracts.DataContracts.Contract
{
    public class CreateContractContract : CommonHttpContract, IValidatableObject
    {
        /// <summary> Номер договора </summary>
        [GCRequired, GCMaxLength(100)]
        public string ContractNumber { get; set; }

        /// <summary> ИД контрагента </summary>
        [GCRequired]
        public int ContractClientId { get; set; }

        /// <summary> Дата создания договора </summary>
        public DateTime CreateDate { get; set; }

        /// <summary> Дата начала действия договора </summary>
        [GCRequired("Дата начала действия договора")]
        public DateTime StartDate { get; set; }

        /// <summary> Срок договора </summary>
        public DateTime? EndDate { get; set; }

        /// <summary> Способ отправки документов </summary>
        [GCMaxLength(300)]
        public string SendDocsType { get; set; }

        /// <summary> Статус подписания договора </summary>
        public bool ContractStatus { get; set; }

        /// <summary> Комментарий к договору </summary>
        [GCMaxLength(2048)]
        public string Comment { get; set; }

        /// <summary> Флаг проверки записи сотрудником </summary>
        public bool IsValidated { get; set; }

        /// <summary> Включить ли автопродление графика </summary>
        public bool IsAutorenewSchedules { get; set; }

        /// <summary> Прикрепленные к договорам площадки с контейнерами </summary>
        public IList<CreateContainer2ScheduleContract> AttachedContainers { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (EndDate.HasValue && EndDate.Value <= StartDate)
            {
                yield return new ValidationResult("Дата окончания договора должна быть больше даты начала");
            }
            //if (AttachedContainers == null || AttachedContainers.Count < 1)
            //{
            //    yield return new ValidationResult("К договору необходимо прикрепить контейнерные площадки с контейнерами");
            //}
        }
    }
}
