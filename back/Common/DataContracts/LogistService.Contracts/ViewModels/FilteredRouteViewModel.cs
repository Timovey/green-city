namespace LogistService.Contracts.ViewModels
{
    public class FilteredRouteViewModel
    {
        /// <summary> ИД рейса </summary>
        public int Id { get; set; }

        /// <summary> Название рейса </summary>
        public string RouteName { get; set; }
    
        /// <summary> Информация по машине </summary>
        public string CarInfo { get; set; }

        /// <summary> Информация по водителю </summary>
        public string DriverInfo { get; set; }

        public bool RouteReady { get; set; }

        /// <summary> Завершенность рейса </summary>
        public bool RouteCompleted { get; set; }
    }
}