using SDContracts.Enums;

namespace SDContracts.ViewModels.Container
{
    /// <summary>
    /// Вью модель для выпадающих списков
    /// </summary>
    public class ContainerDropDownViewModel
    {
        public int ContainerId { get; set; }

        public string ContainerName { get; set; }

        /// <summary>
        /// Тип мусора
        /// 0 - Неизвестно
        /// 1 - ТКО
        /// 2 - КГО
        /// 3 - 4 Класс
        /// 4 - 5 Класс
        /// 5 - Строительный
        /// </summary>
        public TrashType TrashType { get; set; }

        public bool IsInContract { get; set; }

        /// <summary> Флаг проверки записи сотрудником </summary>
        public bool IsValidated { get; set; }
    }
}
