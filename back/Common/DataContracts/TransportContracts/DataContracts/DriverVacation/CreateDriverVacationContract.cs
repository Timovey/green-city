using GreenCity.Http;
using TransportContracts.DataContracts.Enums;
using TransportContracts.DateRange;

namespace TransportContracts.DataContracts.DriverVacation
{
    /// <summary> Создание отпуска/больничного водителя</summary>
    public class CreateDriverVacationContract : CommonHttpContract
    {
        /// <summary> ИД Водителя </summary>
        public int DriverId { get; set; }

        /// <summary> ИД сменного Водителя </summary>
        public int? ReplacementDriverId { get; set; }

        /// <summary> Отпуск/Больничный</summary>
        public DriverVacationType VacationType { get; set; }

        /// <summary> Настройка времени существования отпуска/больничного водителя</summary>
        public DateRangeBaseModel DateRange { get; set; }
    }
}
