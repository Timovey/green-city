using System.ComponentModel;

namespace ReportService.Contracts.DataContracts
{
    public class ShiftTaskContainer
    {
        /// <summary>
        /// Контрагенты контейнера
        /// </summary>
        [Description("Контрагенты")]
        public string Contragents { get; set; }

        /// <summary>
        /// Имя контейнера
        /// </summary>
        [Description("Наименование контейнера")]
        public string ContainerName { get; set; }
        
        /// <summary>
        /// Тип мусора
        /// </summary>
        [Description("Тип мусора")]
        public string TrashType { get; set; }

        /// <summary>
        /// Тип КП
        /// </summary>
        [Description("Тип КП")]
        public string StandType { get; set; }

        /// <summary>
        /// Комментарий к заявке
        /// </summary>
        [Description("Комментарий к заявке")]
        public string Comment { get; set; }
    }
}