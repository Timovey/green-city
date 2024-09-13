using System;
using System.Collections.Generic;

namespace LogistService.Contracts.ViewModels
{
    public class RouteViewModel
    {
        /// <summary> ИД рейса </summary>
        public int Id { get; set; }

        public string RouteName { get; set; }

        /// <summary> Дата рейса </summary>
        public DateTime Date { get; set; }

        /// <summary> Готовность рейса </summary>
        public bool RouteReady { get; set; }

        public bool RouteCompleted { get; set; }

        public string RouteFinishPlace { get; set; }

        /// <summary> Объем выгрузки </summary>
        public decimal RouteTotalVolumeOnFinish { get; set; }

        /// <summary> ИД экипажа </summary>
        public int TransportWorkShiftId { get; set; }

        /// <summary> Массив ИД контейнерных площадок </summary>
        public IList<int> StandIds { get; set; }
    }
}