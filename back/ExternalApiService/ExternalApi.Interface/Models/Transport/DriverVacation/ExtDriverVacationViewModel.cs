using ExternalApi.Interface.Models.Transport.DateRange;
using ExternalApi.Interface.Models.Transport.Driver;

namespace ExternalApi.Interface.Models.Transport.DriverVacation
{
    public class ExtDriverVacationViewModel
    {
        /// <summary> ИД отпуска/больничного Водителя </summary>
        public int DriverVacationId { get; set; }

        /// <summary> ИД Водителя </summary>
        public int DriverId { get; set; }

        /// <summary> Имя водителя </summary>
        public string DriverName { get; set; }

        /// <summary> Фамилия водителя </summary>
        public string DriverSurname { get; set; }

        /// <summary> Отчество водителя </summary>
        public string DriverPatronymic { get; set; }

        /// <summary> ИД Организации водителя </summary>
        public int? DriverOrganizationId { get; set; }

        /// <summary> Должность </summary>
        public ExtDriverPositionType DriverPositionType { get; set; }

        /// <summary> ИД сменного Водителя </summary>
        public int? ReplacementDriverId { get; set; }

        /// <summary> Имя сменного водителя </summary>
        public string ReplacementDriverName { get; set; }

        /// <summary> Фамилия сменного водителя </summary>
        public string ReplacementDriverSurname { get; set; }

        /// <summary> Отчество сменного водителя </summary>
        public string ReplacementDriverPatronymic { get; set; }

        /// <summary> ИД Организации сменного водителя </summary>
        public int? ReplacementDriverOrganizationId { get; set; }

        /// <summary> Должность сменного водителя </summary>
        public ExtDriverPositionType ReplacementDriverPositionType { get; set; }

        /// <summary> Отпуск/Больничный</summary>
        public ExtDriverVacationType VacationType { get; set; }

        /// <summary> Настройка времени существования отпуска/больничного водителя</summary>
        public ExtDateRangeBaseModel DateRange { get; set; }
    }
}
