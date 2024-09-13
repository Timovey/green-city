using GreenCity.Http;
using GreenCity.Http.Attributes.Validation;
using SDContracts.Enums;
using SDContracts.SpecificModels.ContainerStand;
using System.Collections.Generic;

namespace SDContracts.DataContracts.Stand
{
    public class UpdateStandContract : CommonHttpContract
    {
        [GCRequired]
        public int StandId { get; set; }

        /// <summary> Признак режимного объекта </summary>
        public bool IsHighSecurityObject { get; set; }

        /// <summary> Адрес </summary>
        [GCRequired, GCMaxLength(300)]
        public string Address { get; set; }

        /// <summary> Сериализованный координаты </summary>
        public StandCoordinatesModel Coordinates { get; set; }

        /// <summary> Путь подъезда </summary>
        [GCMaxLength(500)]
        public string DriveWay { get; set; }

        /// <summary> Фото в виде массива байт </summary>
        public byte[] Photo { get; set; }

        /// <summary> Контакты </summary>
        [GCMaxLength(300)]
        public string Contacts { get; set; }

        /// <summary>
        /// Тип контейнерной площадки
        /// 0 - Неизвестно
        /// 1 - Обычный (с договором и контрагентами)
        /// 2 - Жилой фонд
        /// </summary>
        [GCRange(0, 2)]
        public StandType StandType { get; set; }

        /// <summary> Есть ли основание </summary>
        public bool HaveBase { get; set; }

        /// <summary> Есть ли крыша </summary>
        public bool HaveRoof { get; set; }

        /// <summary> Есть ли ограждение </summary>
        public bool HaveFence { get; set; }

        /// <summary> Флаг проверки записи сотрудником </summary>
        public bool IsValidated { get; set; }

        /// <summary> Привязанные контейнеры </summary>
        public IList<int> AttachedContainerIds { get; set; }
    }
}
