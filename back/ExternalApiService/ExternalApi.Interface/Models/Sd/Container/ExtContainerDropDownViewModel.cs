namespace ExternalApi.Interface.Models.Sd.Container
{
    /// <summary>
    /// Вью модель для выпадающих списков
    /// </summary>
    public class ExtContainerDropDownViewModel
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
        public ExtTrashType TrashType { get; set; }

        public bool IsInContract { get; set; }

        /// <summary> Флаг проверки записи сотрудником </summary>
        public bool IsValidated { get; set; }
    }
}
