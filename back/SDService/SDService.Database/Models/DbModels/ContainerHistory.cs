using GreenCity.Db.BaseModels;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SDService.Database.Models.DbModels
{
    /// <summary> История изменений контейнера (тары) </summary>
    [Table("container_history")]
    public class ContainerHistory : BaseHistoryTableModel
    {
        /// <summary> Наименование контейнера </summary>
        [Column("container_name"), MaxLength(300)]
        public string Name { get; set; }

        /// <summary> Тип контейнера </summary>
        [Column("container_type"), Comment("Тип контейнера")]
        public int ContainerTypeId { get; set; }

        /// <summary> Id контейнерной площадки </summary>
        [Column("stand_id")]
        public int? ContainerStandId { get; set; }

        /// <summary>
        /// Тип мусора
        /// 0 - Неизвестно
        /// 1 - ТКО
        /// 2 - КГО
        /// 3 - 4 Класс
        /// 4 - 5 Класс
        /// 5 - Строительный
        /// </summary>
        [Column("trash_type"), Comment("Тип мусора")]
        public byte TrashType { get; set; }

        /// <summary> Флаг обозначающий, что контейнер фигурирует как минимум в одном договоре </summary>
        [Column("is_in_contract")]
        public bool IsInContract { get; set; }

        /// <summary> Флаг проверки записи сотрудником </summary>
        [Column("is_validated"), Comment("Флаг проверки записи сотрудником")]
        public bool IsValidated { get; set; }

        /// <summary> Тип сущности - доступная или недоступная для отображения пользователю </summary>
        [Column("entity_type"), Comment("Тип сущности - доступная или недоступная для отображения пользователю")]
        public byte EntityType { get; set; }

        /// <summary> ИД пользователя в базе AuthService создавшего запись </summary>
        [Column("created_user_id"), Comment("ИД пользователя в базе AuthService создавшего запись")]
        public int CreatedUserId { get; set; }

        /// <summary> Имя пользователя создавшего запись </summary>
        [Column("created_user_name"), Comment("Имя пользователя создавшего запись"), MaxLength(500)]
        public string CreatedUserName { get; set; }
    }
}
