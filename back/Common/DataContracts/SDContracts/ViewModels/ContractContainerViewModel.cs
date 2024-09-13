using SDContracts.Enums;

namespace SDContracts.ViewModels
{
    public class ContractContainerViewModel
    {
        /// <summary> ИД контейнера </summary>
        public int ContainerId { get; set; }

        /// <summary> Наименование контейнера </summary>
        public string Name { get; set; }

        /// <summary>
        /// Тип тары
        /// 0 - Неизвестно
        /// 1 - ПМЛ 0.75
        /// 2 - ЕК 0.36
        /// 3 - ЕК 0.24
        /// 4 - 4м3
        /// 5 - 8м3
        /// 6 - Пластик ЕК 0.77
        /// 7 - Пластик ЕК 1.1
        /// 9 - Оцинковка ЕК 0.77
        /// 10 - Оцинковка ЕК 1.1
        /// </summary>
        public int ContainerTypeId { get; set; }

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

        public Container2ScheduleViewModel Container2Schedule { get; set; }

        /// <summary> Находится ли контейнер в договоре </summary>
        public bool IsInContract { get; set; }
    }
}
