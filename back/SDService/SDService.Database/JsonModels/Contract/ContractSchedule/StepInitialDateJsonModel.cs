namespace SDService.Database.JsonModels.Contract.ContractSchedule
{
    public class StepInitialDateJsonModel
    {
        /// <summary>
        /// Начальная дата
        /// </summary>
        public string InitialDate { get; set; }

        /// <summary>
        /// Величина шага
        /// </summary>
        public int? StepValue { get; set; }
    }
}
