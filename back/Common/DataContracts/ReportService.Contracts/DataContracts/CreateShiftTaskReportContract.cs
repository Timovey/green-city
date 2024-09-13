using System.Collections.Generic;
using System.ComponentModel;

namespace ReportService.Contracts.DataContracts
{
    public class CreateShiftTaskReportContract
    {
        /// <summary>
        /// Номер путевого листа
        /// </summary>
        [Description("Путевой лист")]
        public string Number { get; set; }

        /// <summary>
        /// Название организации
        /// </summary>
        [Description("Организация")]
        public string Organization { get; set; }

        /// <summary>
        /// Дата
        /// </summary>
        [Description("Дата")]
        public string Date { get; set; }

        /// <summary>
        /// Транспорт
        /// </summary>
        [Description("ТС")]
        public string Transport { get; set; }

        /// <summary>
        /// Водитель
        /// </summary>
        [Description("Водитель")]
        public string Driver { get; set; }

        /// <summary>
        /// Информация по КП
        /// </summary>
        public IList<ShiftTaskRoute> ShiftTaskRoutes { get; set; }
    }
}