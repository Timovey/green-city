using System.Collections.Generic;

namespace DispatcherService.Contracts.DataContracts.DoneTaskList.Report
{
    public class DoneTaskListRouteReportModel
    {
        /// <summary> Дата </summary>
        public string RouteDate { get; set; }
        
        /// <summary> Название </summary>
        public string RouteName { get; set; }
        
        /// <summary> Номер автотранспортного средства </summary>
        public string CarInfo { get; set; }

        /// <summary> Фамилия и инициалы </summary>
        public string DriverShortName { get; set; }

        /// <summary> Тип транспортного средства </summary>
        public string TransportType { get; set; }

        /// <summary> "Время выезда" </summary>
        public string DepartureTime { get; set; }
        
        /// <summary> КП </summary>
        public IList<DoneTaskListStandReportModel> StandsInfo { get; set; }

        /// <summary> Место выгрузки </summary>
        public string RouteFinishPlace { get; set; }

        /// <summary> Финальный выгруженный объем </summary>
        public string RouteTotalVolumeOnFinish { get; set; }
    }
}