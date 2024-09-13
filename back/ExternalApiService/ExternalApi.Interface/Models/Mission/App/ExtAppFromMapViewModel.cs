namespace ExternalApi.Interface.Models.Mission.App
{
    public class ExtAppFromMapViewModel
    {
        public int Id { get; set; }

        public string Date { get; set; }

        public int ContractClientId { get; set; }

        public int ContractId { get; set; }

        public string Comment { get; set; }

        /// <summary> Долгота </summary>
        public decimal Longitude { get; set; }

        /// <summary> Широта </summary>
        public decimal Latitude { get; set; }

        /// <summary> Примерный объем </summary>
        public decimal CustomVolume { get; set; }

        public ExtMissionStatus MissionStatus { get; set; }
    }
}
