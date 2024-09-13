using System.ComponentModel.DataAnnotations;

namespace ExternalApi.Interface.Models.Transport.TransportShedule
{
    public enum ExtWorkScheduleType : byte
    {
        /// <summary> Неизвестный </summary>
        [Display(Name = "Неизвестно")]
        Unknown = 0,
        /// <summary> 3/3 </summary>
        [Display(Name = "3/3")]
        ThreeThree = 1,
        /// <summary> 5/2 </summary>
        [Display(Name = "5/2")]
        FiveTwo = 2,
        /// <summary> 6/3 </summary>
        [Display(Name = "6/3")]
        SixThree = 3,
    }
}
