using GreenCity.CommonUtils.CommonModels;

namespace ExternalApi.Interface.Models.Sd.Contract
{
    public class ExtContractSortModel : CommonSortingModel
    {
        /// <summary> Номер договора </summary>
        public SortModel ContractNumber { get; set; }

        /// <summary> Дата создания договора </summary>
        public SortModel CreateDate { get; set; }

        /// <summary> Дата начала действия договора </summary>
        public SortModel StartDate { get; set; }

        /// <summary> Срок договора </summary>
        public SortModel EndDate { get; set; }

        /// <summary> Способ отправки документов </summary>
        public SortModel SendDocsType { get; set; }

        public SortModel ContractStatus { get; set; }

        /// <summary> Флаг проверки записи сотрудником </summary>
        public SortModel IsValidated { get; set; }
    }
}
