namespace DataGenerationService.Contracts.DataContracts.Sd
{
    public class ReadContract2StandContract
    {
        /// <summary> Номер договора </summary>
        public int ContractNumber { get; set; }

        /// <summary> Номер КП </summary>
        public int StandNumber { get; set; }

        /// <summary> Время работы от </summary>
        public DateTime StartDate { get; set; }

        /// <summary> Время работы до </summary>
        public DateTime EndDate { get; set; }
    }
}