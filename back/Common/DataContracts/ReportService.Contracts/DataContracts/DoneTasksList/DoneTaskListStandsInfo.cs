using System.ComponentModel;
using ReportService.Contracts.DataContracts.DoneTasksList.Helpers;

namespace ReportService.Contracts.DataContracts.DoneTasksList
{
    public class DoneTaskListStandsInfo
    {
        [Description("Место загрузки (адрес)")]
        public string StandAddress { get; set; }

        [Description("Объем загруженных ТКО")]
        [Merge]
        public string TotalVolumeByStand { get; set; }
    }
}
