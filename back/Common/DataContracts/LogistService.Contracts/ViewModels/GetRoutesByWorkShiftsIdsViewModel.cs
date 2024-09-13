using System;
using System.Collections.Generic;

namespace LogistService.Contracts.ViewModels
{
    public class GetRoutesByWorkShiftsIdsViewModel
    {
        /// <summary> ИД рейса </summary>
        public int Id { get; set; }

        /// <summary> Название рейса </summary>
        public string RouteName { get; set; }

        /// <summary> Дата рейса </summary>
        public DateTime Date { get; set; }

        /// <summary> ИД экипажа </summary>
        public int TransportWorkShiftId { get; set; }
        
        /// <summary> Объем выгрузки </summary>
        public decimal RouteTotalVolumeOnFinish { get; set; }

        /// <summary> Массив контейнерных площадок </summary>
        public IList<Tuple<int, string>> Stands { get; set; }
    }
}