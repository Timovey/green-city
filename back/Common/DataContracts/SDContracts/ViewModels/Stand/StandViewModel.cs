using SDContracts.Enums;
using SDContracts.SpecificModels.ContainerStand;
using SDContracts.ViewModels.Container;
using System.Collections.Generic;

namespace SDContracts.ViewModels.Stand
{
    public class StandViewModel
    {
        public int StandId { get; set; }

        /// <summary> Признак режимного объекта </summary>
        public bool IsHighSecurityObject { get; set; }

        /// <summary> Адрес </summary>
        public string Address { get; set; }

        /// <summary> Координаты </summary>
        public StandCoordinatesModel Coordinates { get; set; }

        /// <summary> Путь подъезда </summary>
        public string DriveWay { get; set; }

        /// <summary> Контакты </summary>
        public string Contacts { get; set; }

        /// <summary>
        /// Тип контейнерной площадки
        /// 0 - Неизвестно
        /// 1 - Обычный (с договором и контрагентами)
        /// 2 - Жилой фонд
        /// </summary>
        public StandType StandType { get; set; }

        /// <summary> Есть ли основание </summary>
        public bool HaveBase { get; set; }

        /// <summary> Есть ли крыша </summary>
        public bool HaveRoof { get; set; }

        /// <summary> Есть ли ограждение </summary>
        public bool HaveFence { get; set; }

        /// <summary>
        /// ИД прикрепленных контейнеров
        /// </summary>
        public IList<ContainerDropDownViewModel> AttachedContainers { get; set; }

        public bool IsValidated { get; set; }

        /// <summary> Имя пользователя создавшего запись </summary>
        public string CreatedUserName { get; set; }
    }
}
