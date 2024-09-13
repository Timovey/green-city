using System.ComponentModel.DataAnnotations;

namespace ExternalApi.Interface.Models.Transport.TransportShedule
{
    public enum ExtTransportWorkDateType : byte
    {
        /// <summary> Неизвестный </summary>
        [Display(Name = "Неизвестно")]
        Unknown = 0,
        /// <summary> Работает по расписанию 3/3 </summary>
        [Display(Name = "3/3")]
        WorkThreeThree = 1,
        /// <summary> Работает по расписанию 5/2 </summary>
        [Display(Name = "5/2")]
        WorkFiveTwo = 2,
        /// <summary> Работает по расписанию 6/3 </summary>
        [Display(Name = "6/3")]
        WorkSixThree = 3,
        /// <summary> Чинится в 1 смену </summary>
        [Display(Name = "Ремонт 1 смена")]
        RepairFirst = 4,
        /// <summary> Чинится в 2 смену </summary>
        [Display(Name = "Ремонт 2 смена")]
        RepairSecond = 5,
        /// <summary> Чинится весь день </summary>
        [Display(Name = "Ремонт")]
        RepairAll = 6,
        /// <summary> Проходит ТО </summary>
        [Display(Name = "ТО")]
        TO = 7,
        /// <summary> Работает без графика </summary>
        [Display(Name = "Нет графика")]
        NotShedule = 8,
        /// <summary> Нет водителей на данное число и транспорт не чинится </summary>
        [Display(Name = "Нет водителей")]
        NotDrivers = 9,
        /// <summary> Выходные у работников </summary>
        [Display(Name = "Выходной")]
        Holidays = 10,
        /// <summary> В аренде</summary>
        [Display(Name = "В аренде")]
        Rent = 11,
        /// <summary> Дежурная смена</summary>
        [Display(Name = "Дежурная смена")]
        DutyShift = 12,
    }
}
