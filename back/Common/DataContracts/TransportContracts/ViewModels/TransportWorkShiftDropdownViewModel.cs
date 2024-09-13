using System.Collections.Generic;

namespace TransportContracts.ViewModels
{
    public class TransportWorkShiftDropdownViewModel
    {
        /// <summary>
        /// ИД сущности
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Список организаций, которым доступна рабочая смена
        /// </summary>
        public IList<int> OrganizationIds { get; set; }

        /// <summary>
        /// Данные для dropdown
        /// </summary>
        public string WorkshiftData { get; set; }
    }
}
