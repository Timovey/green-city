namespace DispatcherService.Contracts.DataContracts.DoneTaskList.Report
{
    public class DoneTaskListStandReportModel
    {
        /// <summary> Адрес </summary>
        public string StandAddress { get; set; }

        /// <summary> Объем загруженных ТКО </summary>
        public string TotalVolumeByStand { get; set; }
    }
}