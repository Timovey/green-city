namespace DataGenerationService.Contracts.DataContracts.Sd
{
    public class ReadContainerTypeContract
    {
        /// <summary> Номер </summary>
        public int Number { get; set; }

        /// <summary>
        /// Название типа тары
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Объем тары
        /// </summary>
        public decimal Volume { get; set; }
    }
}