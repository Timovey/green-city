using MissionService.Contracts.Enums;
using System;

namespace MissionService.Contracts.ViewModels.App
{
    public class AppFromMapViewModel
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public int ContractClientId { get; set; }

        public int ContractId { get; set; }

        public string Comment { get; set; }

        /// <summary> Долгота </summary>
        public decimal Longitude { get; set; }

        /// <summary> Широта </summary>
        public decimal Latitude { get; set; }

        /// <summary> Примерный объем </summary>
        public decimal CustomVolume { get; set; }

        public MissionStatus MissionStatus { get; set; }
    }
}
