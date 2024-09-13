using GreenCity.Http;

namespace LogistService.Contracts.DataContracts.Route
{
    public class CloseRouteContract : CommonHttpContract
    {
        /// <summary> ИД рейса </summary>
        public int Id { get; set; }
        
        /// <summary> Место выгрузки </summary>
        public string RouteFinishPlace { get; set; }

        /// <summary> Объем выгрузки </summary>
        public decimal RouteTotalVolumeOnFinish { get; set; }
    }
}