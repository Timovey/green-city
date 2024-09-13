using System.Collections.Generic;

namespace DispatcherService.Contracts.DataContracts.ShiftTask.Report
{
    public class ShiftTaskReportModel
    {
        /// <summary>
        /// Номер путевого листа
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Название организации
        /// </summary>
        public string Organization { get; set; }

        /// <summary>
        /// Дата
        /// </summary>
        public string Date { get; set; }

        /// <summary>
        /// Транспорт
        /// </summary>
        public string Transport { get; set; }

        /// <summary>
        /// Водитель
        /// </summary>
        public string Driver { get; set; }

        /// <summary>
        /// Информация по КП
        /// </summary>
        public IList<ShiftTaskRouteReportModel> ShiftTaskRoutes { get; set; }
    }
}
