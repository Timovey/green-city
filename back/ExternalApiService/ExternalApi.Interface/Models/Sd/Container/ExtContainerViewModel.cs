namespace ExternalApi.Interface.Models.Sd.Container
{
    public class ExtContainerViewModel
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
        public ExtTrashType TrashType { get; set; }

        public string TrashTypeName { get; set; }

        /// <summary> Id контейнерной площадки </summary>
        public int? ContainerStandId { get; set; }

        public string ContainerStandAddress { get; set; }

        /// <summary> Флаг обозначающий, что контейнер фигурирует как минимум в одном договоре </summary>
        public bool IsInContract { get; set; }

        /// <summary> Имя пользователя создавшего запись </summary>
        public string CreatedUserName { get; set; }

        public bool IsValidated { get; set; }
    }
}
