using System.ComponentModel.DataAnnotations;

namespace ExternalApi.Interface.Models.Transport.RepairCard
{
    public enum ExtRepairType
    {
        [Display(Name = "Неизвестно")]
        /// <summary> Неизвестный </summary>
        Unknown = 0,

        [Display(Name = "ТО")]
        /// <summary> ТО </summary>
        TO = 1,

        [Display(Name = "Ремонт")]
        /// <summary> Ремонт </summary>
        Repair = 2
    }
}
