namespace SDContracts.ViewModels.Stand
{
    public class StandForMissionInfoViewModel
    {
        /// <summary> Долгота </summary>
        public decimal Longitude { get; set; }

        /// <summary> Широта </summary>
        public decimal Latitude { get; set; }

        /// <summary> Примерный объем </summary>
        public decimal CustomVolume { get; set; }
    }
}
