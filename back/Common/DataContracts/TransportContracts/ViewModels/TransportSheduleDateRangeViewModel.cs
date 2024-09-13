using System.Collections.Generic;
using TransportContracts.DataContracts.Enums;

namespace TransportContracts.ViewModels
{
    public class TransportSheduleDateRangeViewModel
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
        public TransportType TransportType { get; set; }

        /// <summary> Регистрационный номер </summary>
        public string RegNumber { get; set; }

        /// <summary>
        /// ТО и ремонты предстоящие
        /// </summary>
        public IList<RepairCardViewModel> RepairCards { get; set; }

        /// <summary>
        /// График машины
        /// </summary>
        public IList<TransportSheduleViewModel> TransportShedules { get; set; }

        /// <summary>
        /// Аренда транспорта
        /// </summary>
        public IList<RentViewModel> Rents { get; set; }

        /// <summary>
        /// Дежурные смены
        /// </summary>
        public IList<TransportDutyShiftViewModel> TransportDutyShifts { get; set; }

        /// <summary>
        /// Занятость водителей на машине
        /// </summary>
        public IList<DriverSheduleViewModel> DriverShedules { get; set; }
    }
}
