using GreenCity.Db.BaseModels;
using Microsoft.EntityFrameworkCore;
using SDContracts.DataContracts.Contract.ScheduleModels;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SDService.Database.Models.DbModels.ContractSchedule
{
    /// <summary> Таблица-связка контейнера с графиком вывоза и договором </summary>
    [Table("contract_schedule__container2schedule"), Comment("Таблица-связка контейнера с графиком вывоза и договором")]
    public class Container2Schedule : BaseDbModel
    {
        /// <summary> ИД контейнера </summary>
        [Column("container_id")]
        public int ContainerId { get; set; }

        /// <summary> Сериализованный экземпляр объекта расписания </summary>
        [Column("contract_schedule_json"), Comment($"Сериализованный экземпляр массива объектов расписания {nameof(ContractScheduleModel)}")]
        public string ContractScheduleJson { get; set; }

        /// <summary> Комментарий к графику вывоза </summary>
        [Column("schedule_comment"), MaxLength(500)]
        public string ScheduleComment { get; set; }

        /// <summary> С какого времени можно вывести мусор </summary>
        [Column("work_time_from"), Comment("С какого времени можно вывести мусор")]
        public TimeSpan WorkTimeFrom { get; set; }

        /// <summary> До какого времени можно вывести мусор </summary>
        [Column("work_time_to"), Comment("До какого времени можно вывести мусор")]
        public TimeSpan WorkTimeTo { get; set; }

        /// <summary> Флаг нахождения контейнера в гос реестре контейнеров </summary>
        [Column("is_in_registry"), Comment("Флаг нахождения контейнера в гос реестре контейнеров")]
        public bool IsInRegistry { get; set; }

        /// <summary> С какой даты контейнер находится в гос реестре </summary>
        [Column("in_registry_from"), Comment("С какой даты контейнер находится в гос реестре")]
        public DateTime? InRegistryFromDate { get; set; }

        /// <summary> ИД договора </summary>
        [Column("contract_id")]
        public int ContractId { get; set; }
    }
}
