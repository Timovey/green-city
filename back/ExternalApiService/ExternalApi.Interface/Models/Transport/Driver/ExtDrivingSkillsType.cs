using System.ComponentModel.DataAnnotations;

namespace ExternalApi.Interface.Models.Transport.Driver
{
    public enum ExtDrivingSkillsType : byte
    {
        [Display(Name = "Неизвестно")]
        Unknown = 0,
        /// <summary> Мусоровоз </summary>
        [Display(Name = "Мусоровоз")]
        GarbageTruck = 1,
        /// <summary> Бункеровоз </summary>
        [Display(Name = "Бункеровоз")]
        BunkerTruck = 2,
        /// <summary> Самосвал </summary>
        [Display(Name = "Самосвал")]
        DumpTruck = 3,
    }
}
