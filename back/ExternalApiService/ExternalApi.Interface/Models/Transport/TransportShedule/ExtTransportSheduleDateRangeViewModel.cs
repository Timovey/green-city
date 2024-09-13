using ExternalApi.Interface.Models.Transport.Rent;
using ExternalApi.Interface.Models.Transport.RepairCard;
using ExternalApi.Interface.Models.Transport.Transport;
using ExternalApi.Interface.Models.Transport.TransportDutyShift;
using System.Collections.Generic;

namespace ExternalApi.Interface.Models.Transport.TransportShedule
{
    public class ExtTransportSheduleDateRangeViewModel
    {
        /// <summary>
        /// ИД сущности
        /// </summary>
        public int Id { get; set; }

        /// <summary> ИД транспортного средства </summary>
        public int TransportId { get; set; }

        /// <summary> Марка транспорта </summary>
        public string TransportName { get; set; }

        /// <summary> ИД Организации транспорта</summary>
        public int? TransportOrganizationId { get; set; }

        /// <summary> Тип транспорта</summary>
        public ExtTransportType TransportType { get; set; }

        /// <summary> Регистрационный номер </summary>
        public string RegNumber { get; set; }

        /// <summary>
        /// ТО и ремонты предстоящие
        /// </summary>
        public IList<ExtRepairCardViewModel> RepairCards { get; set; }

        /// <summary>
        /// График машины
        /// </summary>
        public IList<ExtTransportSheduleViewModel> TransportShedules { get; set; }

        /// <summary>
        /// Аренда транспорта
        /// </summary>
        public IList<ExtRentViewModel> Rents { get; set; }

        /// <summary>
        /// Дежурные смены
        /// </summary>
        public IList<ExtTransportDutyShiftViewModel> TransportDutyShifts { get; set; }

        /// <summary>
        /// Занятость водителей на машине
        /// </summary>
        public IList<ExtDriverSheduleViewModel> DriverShedules { get; set; }
    }
}
