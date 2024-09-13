using System.Collections.Generic;

namespace ReportService.Contracts.DataContracts
{
    public class ShiftTaskRoute
    {
        /// <summary>
        /// Номер рейса
        /// </summary>
        public string RouteNumber { get; set; }

        /// <summary>
        /// КП в рейсе
        /// </summary>
        public IList<ShiftTaskStand> ShiftTaskStands { get; set; }
    }
}