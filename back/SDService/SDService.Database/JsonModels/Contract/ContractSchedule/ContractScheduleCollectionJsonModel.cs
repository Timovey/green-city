namespace SDService.Database.JsonModels.Contract.ContractSchedule
{
    public class ContractScheduleCollectionJsonModel
    {
        /// <summary>
        /// Расписание договора
        /// </summary>
        public ContractScheduleJsonModel ContractSchedule { get; set; }

        public string CreateDate { get; set; }

        /// <summary>
        /// Дата действия начала действия расписания
        /// </summary>
        public string ScheduleDateFrom { get; set; }

        /// <summary>
        /// Дата окончания действия расписания
        /// </summary>
        public string ScheduleDateTo { get; set; }
    }
}
