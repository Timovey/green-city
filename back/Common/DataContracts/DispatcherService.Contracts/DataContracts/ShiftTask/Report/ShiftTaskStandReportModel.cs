using System.Collections.Generic;

namespace DispatcherService.Contracts.DataContracts.ShiftTask.Report
{
    public class ShiftTaskStandReportModel
    {
        /// <summary>
        /// Адрес КП
        /// </summary>
        public string StandAdress { get; set; }

        /// <summary>
        /// Контакты КП
        /// </summary>
        public string StandContacts { get; set; }

        /// <summary>
        /// Контейнеры в КП
        /// </summary>
        public IList<ShiftTaskContainerReportModel> ShiftTaskContainers { get; set; }
    }
}
