namespace DataGenerationService.Contracts.DataContracts.Sd
{
    public class ReadContractContract
    {
        /// <summary> Номер договора </summary>
        public int Number { get; set; }

        /// <summary> Номер контрагента </summary>
        public int? ContractClientNumber { get; set; }

        /// <summary> Статус подписания договора </summary>
        public bool ContractStatus { get; set; }

        /// <summary> Дата создания договора </summary>
        public DateTime CreateDate { get; set; }

        /// <summary> Дата начала действия договора </summary>
        public DateTime StartDate { get; set; }

        /// <summary> Срок договора </summary>
        public DateTime? EndDate { get; set; }

        /// <summary> Способ отправки документов </summary>
        public string SendDocsType { get; set; }

        /// <summary> Комментарий к договору </summary>
        public string Comment { get; set; }

        /// <summary> Включить ли автопродление графика </summary>
        public bool IsAutorenewSchedules { get; set; }

        /// <summary> Номер пользователя создавшего запись </summary>
        public int CreatedUserNumber { get; set; }
    }
}