using GreenCity.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace ScheduleService.Contracts.DataContracts.RedDates
{
    /// <summary> Создание красного дня календаря </summary>
    public class CreateRedDateContract : CommonHttpContract
    {
        /// <summary> Дата праздника </summary>
        public DateTime Date { get; set; }

        /// <summary> Название праздника </summary>
        [MaxLength(512)]
        public string Name { get; set; }
    }
}
