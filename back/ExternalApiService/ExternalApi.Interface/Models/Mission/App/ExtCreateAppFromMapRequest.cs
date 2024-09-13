using GreenCity.Http;

namespace ExternalApi.Interface.Models.Mission.App
{
    public class ExtCreateAppFromMapRequest : CommonHttpContract
    {
        /// <summary> Дата заявки </summary>
        public string Date { get; set; }

        /// <summary> ИД контрагента </summary>
        public int? ContractClientId { get; set; }

        /// <summary> ИД договора </summary>
        public int? ContractId { get; set; }

        /// <summary> Комментарий </summary>
        public string Comment { get; set; }

        /// <summary> Долгота </summary>
        public decimal Longitude { get; set; }

        /// <summary> Широта </summary>
        public decimal Latitude { get; set; }

        /// <summary> Примерный объем </summary>
        public decimal CustomVolume { get; set; }
    }
}
