using System;
using TransportContracts.DataContracts.Enums;
using TransportContracts.DateRange;

namespace TransportContracts.ViewModels
{
    public class RepairCardViewModel
    {
        /// <summary> ИД карточки ремонта </summary>
        public int RepairCardId { get; set; }

        /// <summary> Событие </summary>
        public RepairType Event { get; set; }

        /// <summary> ИД организации </summary>
        public int? OrganizationId { get; set; }

        /// <summary> ИД транспортного средства </summary>
        public int TransportId { get; set; }

        /// <summary> Марка транспорта </summary>
        public string TransportName { get; set; }

        /// <summary> ИД Организации транспорта</summary>
        public int? TransportOrganizationId { get; set; }

        /// <summary> Тип транспорта</summary>
        public TransportType TransportType { get; set; }

        /// <summary> Регистрационный номер </summary>
        public string RegNumber { get; set; }

        /// <summary>
        /// В какую смену производиться ремонт/ТО при графике 6/3
        /// При добавлении сущности нужно сразу проставлять, если весь день будет чинится
        /// </summary>
        public WorkShift WorkShift { get; set; }

        /// <summary> Настройка времени существования ТО/Ремонта транспорта</summary>
        public DateRangeBaseModel DateRange { get; set; }

        /// <summary> Ид, которое подставляем при работе с DateRange </summary>
        public Guid Guid {get; set;}
    }
}
