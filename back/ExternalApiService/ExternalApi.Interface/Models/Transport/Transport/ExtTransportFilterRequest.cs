using ExternalApi.Interface.Http;
using System.Collections.Generic;

namespace ExternalApi.Interface.Models.Transport.Transport
{
    public class ExtTransportFilterRequest : CommonHttpFilterV2
    {
        /// <summary>
        /// Название транспортного средства
        /// </summary>
        public string Name { get; set; }

        /// <summary> ИД организации </summary>
        public IList<int> OrganizationIds { get; set; }

        /// <summary>
        /// Регистрационный номер
        /// </summary>
        public string RegNumber { get; set; }

        /// <summary>
        /// Тип транспортного средства
        /// </summary>
        public IList<byte> TransportTypes { get; set; }

        /// <summary>
        /// Цвет кузова
        /// </summary>
        public string BodyColor { get; set; }

        /// <summary>
        /// Забор бака
        /// </summary>
        public string TankFence { get; set; }

        /// <summary>
        /// Объем кузова
        /// </summary>
        public decimal? CargoVolume { get; set; }

        public ExtTransportSortModel Sorting { get; set; }
    }
}
