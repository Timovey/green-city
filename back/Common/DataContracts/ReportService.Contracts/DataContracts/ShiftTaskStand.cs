using System.Collections.Generic;
using System.ComponentModel;

namespace ReportService.Contracts.DataContracts
{
    public class ShiftTaskStand
    {
        /// <summary>
        /// Адрес КП
        /// </summary>
        [Description("Адрес КП")]
        public string StandAdress { get; set; }

        /// <summary>
        /// Контакты КП
        /// </summary>
        [Description("Контакты КП")]
        public string StandContacts { get; set; }

        /// <summary>
        /// Контейнеры в КП
        /// </summary>
        public IList<ShiftTaskContainer> ShiftTaskContainers { get; set; }
    }
}