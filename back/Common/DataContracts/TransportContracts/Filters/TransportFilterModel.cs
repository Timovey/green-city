using GreenCity.Http;
using System.Collections.Generic;
using TransportContracts.SortModels;

namespace TransportContracts.Filters
{
    public class TransportFilterModel : CommonHttpFilter
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

        public TransportSortModel Sorting { get; set; }
    }
}
