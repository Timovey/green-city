using System.ComponentModel.DataAnnotations;

namespace ExternalApi.Interface.Models.Sd.Stand
{
    /// <summary> Тип контейнерной площадки </summary>
    public enum ExtStandType : byte
    {
        [Display(Name = "Неизвестно")]
        Unknown = 0,

        /// <summary> Обычный с контграентами и договорами </summary>
        [Display(Name = "Обычный")]
        Default = 1,

        /// <summary> Жилой фонд </summary>
        [Display(Name = "Жилой фонд")]
        HouseStock = 2
    }
}
