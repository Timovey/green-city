using ExternalApi.Interface.Models.Transport.Transport;
using ExternalApi.Interface.Models.Transport.TransportShedule;
using System;

namespace ExternalApi.Interface.Models.Transport.TransportWorkShift
{
    public class ExtTransportWorkShiftViewModel
    {
        /// <summary>
        /// ИД обновляемой сущности
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// ID транспорта
        /// </summary>
        public int TransportId { get; set; }

        /// <summary>
        /// Название транспортного средства
        /// </summary>
        public string TransportName { get; set; }

        /// <summary> ИД Организации транспорта</summary>
        public int? TransportOrganizationId { get; set; }

        /// <summary> Тип транспорта</summary>
        public ExtTransportType TransportType { get; set; }

        /// <summary> Регистрационный номер </summary>
        public string RegNumber { get; set; }

        /// <summary>
        /// Дата выхода
        /// </summary>
        public DateTime DateTime { get; set; }

        /// <summary>
        /// Id водителя
        /// </summary>
        public int DriverId { get; set; }

        public string DriverFio { get; set; }

        /// <summary>
        /// Смена водителя
        /// </summary>
        public ExtDriverWorkDateType DriverWorkDateType { get; set; }
    }
}
