using GreenCity.CommonUtils.CommonModels;
using System.Collections.Generic;

namespace SDContracts.SpecificModels.ContainerStand
{
    /// <summary>
    /// Координаты контейнерной площадки
    /// </summary>
    public class StandCoordinatesModel
    {
        /// <summary>
        /// Информация по координатам (пары широта-долгота)
        /// </summary>
        public IList<Coordinates> Coordinates { get; set; }

        /// <summary>
        /// Комментарий к координатам (иногда вместо координат написан комментарий)
        /// </summary>
        public string CoordinatesComment { get; set; }
    }
}
