using System.ComponentModel.DataAnnotations;

namespace ExternalApi.Interface.Models.Mission.App
{
    /// <summary>
    /// Тип миссии
    /// </summary>
    public enum ExtAppType : byte
    {
        /// <summary> Неизвестный </summary>
        [Display(Name = "Неизвестный")]
        Unknown = 0,

        /// <summary> По существующим данным (КА и Договор) </summary>
        [Display(Name = "По существующим данным (КА и Договор)")]
        ByContractClient = 1,

        /// <summary> Точка на карте </summary>
        [Display(Name = "Точка на карте")]
        FromMap = 2
    }
}
