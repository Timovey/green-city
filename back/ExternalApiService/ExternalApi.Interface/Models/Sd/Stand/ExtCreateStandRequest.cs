using GreenCity.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ExternalApi.Interface.Models.Sd.Stand
{
    public class ExtCreateStandRequest : CommonHttpContract
    {
        /// <summary> Признак режимного объекта </summary>
        public bool IsHighSecurityObject { get; set; }

        /// <summary> Адрес </summary>
        [MaxLength(300)]
        public string Address { get; set; }

        /// <summary> Сериализованный координаты </summary>
        public ExtStandCoordinatesModel Coordinates { get; set; }

        /// <summary> Путь подъезда </summary>
        [MaxLength(500)]
        public string DriveWay { get; set; }

        /// <summary> Фото в виде массива байт </summary>
        public byte[] Photo { get; set; }

        /// <summary> Контакты </summary>
        [MaxLength(300)]
        public string Contacts { get; set; }

        /// <summary>
        /// Тип контейнерной площадки
        /// 0 - Неизвестно
        /// 1 - Обычный (с договором и контрагентами)
        /// 2 - Жилой фонд
        /// </summary>
        [Range(0, 2)]
        public ExtStandType StandType { get; set; }

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
