namespace DataGenerationService.Contracts.DataContracts.Sd
{
    public class ReadContainerContract
    {
        /// <summary> Номер </summary>
        public int Number { get; set; }

        /// <summary> Наименование контейнера </summary>
        public string Name { get; set; }

        /// <summary>
        /// Тип тары
        /// </summary>
        public int ContainerTypeNumber { get; set; }

        /// <summary>
        /// Тип мусора
        /// 0 - Неизвестно
        /// 1 - ТКО
        /// 2 - КГО
        /// 3 - 4 Класс
        /// 4 - 5 Класс
        /// 5 - Строительный
        /// </summary>
        public byte TrashType { get; set; }

        /// <summary> Номер контейнерной площадки </summary>
        public int? ContainerStandNumber { get; set; }

        /// <summary> Номер пользователя создавшего запись </summary>
        public int CreatedUserNumber { get; set; }
    }
}