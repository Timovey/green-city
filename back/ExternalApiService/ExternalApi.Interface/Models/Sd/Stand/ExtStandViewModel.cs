using ExternalApi.Interface.Models.Sd.Container;
using System.Collections.Generic;

namespace ExternalApi.Interface.Models.Sd.Stand
{
    public class ExtStandViewModel
    {
        public int StandId { get; set; }

        /// <summary> Признак режимного объекта </summary>
        public bool IsHighSecurityObject { get; set; }

        /// <summary> Адрес </summary>
        public string Address { get; set; }

        /// <summary> Сериализованный координаты </summary>
        public ExtStandCoordinatesModel Coordinates { get; set; }

        /// <summary> Путь подъезда </summary>
        public string DriveWay { get; set; }

        /// <summary> Фото в виде массива байт </summary>
        public byte[] Photo { get; set; }

        /// <summary> Контакты </summary>
        public string Contacts { get; set; }

        /// <summary>
        /// Тип контейнерной площадки
        /// 0 - Неизвестно
        /// 1 - Обычный (с договором и контрагентами)
        /// 2 - Жилой фонд
        /// </summary>
        public ExtStandType StandType { get; set; }

        /// <summary> Есть ли основание </summary>
        public bool HaveBase { get; set; }

        /// <summary> Есть ли крыша </summary>
        public bool HaveRoof { get; set; }

        /// <summary> Есть ли ограждение </summary>
        public bool HaveFence { get; set; }

        public string StandTypeName { get; set; }


        /// <summary> Флаг проверки записи сотрудником </summary>
        public bool IsValidated { get; set; }

        /// <summary>
        /// ИД прикрепленных контейнеров
        /// TODO! Убрать это
        /// </summary>
        public IList<ExtContainerDropDownViewModel> AttachedContainers { get; set; }

        /// <summary> Имя пользователя создавшего запись </summary>
        public string CreatedUserName { get; set; }
    }
}
