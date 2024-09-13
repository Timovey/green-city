using System.Collections.Generic;
using System.ComponentModel;
using ReportService.Contracts.DataContracts.DoneTasksList.Helpers;

namespace ReportService.Contracts.DataContracts.DoneTasksList
{
    public class DoneTaskListRouteInfo
    {
        [Description("Маршрутный журнал от")] 
        public string RouteDate { get; set; }

        [Description] 
        public string RouteName { get; set; }

        [Description("Государственный номер автотранспортного средства")]
        public string CarInfo { get; set; }

        /// <summary> Фамилия и инициалы </summary>
        [Description("Водитель ФИО")]
        public string DriverShortName { get; set; }

        [Description("Тип транспортного средства")]
        public string TransportType { get; set; }

        [Description("Время выезда")] 
        public string DepartureTime { get; set; }
        
        public IList<DoneTaskListStandsInfo> StandsInfo { get; set; }

        /// <summary> Место выгрузки </summary>
        [Description("Место выгрузки (адрес)")]
        [EndListValue]
        public string RouteFinishPlace { get; set; }

        /// <summary> Финальный выгруженный объем </summary>
        [Description("Объем (масса) выгруженных ТКО")]
        [Merge]
        [EndListValue]
        public string RouteTotalVolumeOnFinish { get; set; }
    }
}