namespace ExternalApi.Interface.Models.Logist.Route
{
    public class ExtFilteredRouteViewModel
    {
        /// <summary> ИД рейса </summary>
        public int Id { get; set; }

        /// <summary> Название рейса </summary>
        public string RouteName { get; set; }

        /// <summary> Информация по машине </summary>
        public string CarInfo { get; set; }

        /// <summary> Информация по водителю </summary>
        public string DriverInfo { get; set; }
    }
}