using System;
using TransportContracts.DataContracts.Enums;
using TransportContracts.DateRange;

namespace TransportContracts.ViewModels
{
    public class TransportSheduleViewModel
    {
        /// <summary> ИД расписания транспорта</summary>
        public int TransportSheduleId { get; set; }

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

        /// <summary> Настройка времени существования расписания транспорта</summary>
        public DateRangeBaseModel DateRange { get; set; }

        /// <summary> Ид, которое подставляем при работе с DateRange </summary>
        public Guid Guid { get; set; }

        /// <summary>
        /// Типовой трафик
        /// </summary>
        public WorkScheduleType WorkSheduleType { get; set; }

    }
}
