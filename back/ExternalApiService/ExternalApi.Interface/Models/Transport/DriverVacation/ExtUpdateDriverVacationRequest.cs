using ExternalApi.Interface.Models.Transport.DateRange;
using GreenCity.Http;

namespace ExternalApi.Interface.Models.Transport.DriverVacation
{
    /// <summary> Обновление отпуска/больничного водителя</summary>
    public class ExtUpdateDriverVacationRequest : CommonHttpContract
    {
        /// <summary> ИД отпуска/больничного Водителя </summary>
        public int DriverVacationId { get; set; }

        /// <summary> ИД Водителя </summary>
        public int DriverId { get; set; }

        /// <summary> ИД сменного Водителя </summary>
        public int? ReplacementDriverId { get; set; }

        /// <summary> Отпуск/Больничный</summary>
        public ExtDriverVacationType VacationType { get; set; }

        /// <summary> Настройка времени существования отпуска/больничного водителя</summary>
        public ExtDateRangeBaseModel DateRange { get; set; }
    }
}
