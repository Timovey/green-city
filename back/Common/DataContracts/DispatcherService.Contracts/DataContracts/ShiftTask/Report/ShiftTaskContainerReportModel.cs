namespace DispatcherService.Contracts.DataContracts.ShiftTask.Report
{
    public class ShiftTaskContainerReportModel
    {
        /// <summary>
        /// Контрагенты контейнера
        /// </summary>
        public string Contragents { get; set; }

        /// <summary>
        /// Имя контейнера
        /// </summary>
        public string ContainerName { get; set; }
        
        /// <summary>
        /// Тип мусора
        /// </summary>
        public string TrashType { get; set; }

        /// <summary>
        /// Тип КП
        /// </summary>
        public string StandType { get; set; }

        /// <summary>
        /// Комментарий к заявке
        /// </summary>
        public string Comment { get; set; }
    }
}
