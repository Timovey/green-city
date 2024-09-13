using System;
using TransportContracts.DataContracts.Enums;
using TransportContracts.DateRange;

namespace TransportContracts.ViewModels
{
    public class Driver2TransportViewModel
    {
        /// <summary> ИД Сущности отношения водителя к транспорту</summary>
        public int Driver2TransportId { get; set; }

        /// <summary> Марка транспорта </summary>
        public string TransportName { get; set; }

        /// <summary> ИД Организации транспорта</summary>
        public int? TransportOrganizationId { get; set; }

        /// <summary> Тип транспорта</summary>
        public TransportType TransportType { get; set; }

        /// <summary> Регистрационный номер </summary>
        public string RegNumber { get; set; }

        /// <summary> ИД транспорта</summary>
        public int TransportId { get; set; }

        /// <summary> ИД Водителя</summary>
        public int DriverId { get; set; }

        /// <summary>
        /// С какой смены выходит водитель 
        /// </summary>
        public WorkShift WorkShift { get; set; }

        /// <summary> Настройка времени существования графика водителя</summary>
        public DateRangeBaseModel DateRange { get; set; }

        /// <summary> Ид, которое подставляем при работе с DateRange </summary>
        public Guid Guid { get; set; }

        /// <summary> Имя водителя </summary>
        public string DriverName { get; set; }

        /// <summary> Фамилия водителя </summary>
        public string DriverSurname { get; set; }

        /// <summary> Отчество водителя </summary>
        public string DriverPatronymic { get; set; }

        /// <summary> ИД Организации водителя </summary>
        public int? DriverOrganizationId { get; set; }

        /// <summary> Должность </summary>
        public DriverPositionType DriverPositionType { get; set; }
    }
}
