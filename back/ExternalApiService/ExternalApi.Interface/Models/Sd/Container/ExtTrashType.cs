using System.ComponentModel.DataAnnotations;

namespace ExternalApi.Interface.Models.Sd.Container
{
    public enum ExtTrashType : byte
    {
        [Display(Name = "Неизвестно")]
        Unknown = 0,
        [Display(Name = "ТКО")]
        TKO = 1,
        [Display(Name = "КГО")]
        KGO = 2,
        [Display(Name = "4 Класс")]
        Class4 = 3,
        [Display(Name = "5 Класс")]
        Class5 = 4,
        [Display(Name = "Строительный")]
        Construct = 5
    }
}
