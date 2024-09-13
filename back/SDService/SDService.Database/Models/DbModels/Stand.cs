using GreenCity.Db.BaseModels;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SDService.Database.Models.DbModels
{
    /// <summary> Контейнерная площадка </summary>
    [Table("stand")]
    public class Stand : BaseDbModel
    {
        /// <summary> Признак режимного объекта </summary>
        [Column("is_high_security_object")]
        public bool IsHighSecurityObject { get; set; }

        /// <summary> Адрес </summary>
        [Column("address"), MaxLength(300)]
        public string Address { get; set; }

        /// <summary> Сериализованный координаты </summary>
        [Column("coordinates_json")]
        public string CoordinatesJson { get; set; }

        /// <summary> Путь подъезда </summary>
        [Column("drive_way"), MaxLength(500)]
        public string DriveWay { get; set; }

        /// <summary> Контакты </summary>
        [Column("contacts"), MaxLength(300)]
        public string Contacts { get; set; }

        /// <summary>
        /// Тип контейнерной площадки
        /// 0 - Неизвестно
        /// 1 - Обычный (с договором и контрагентами)
        /// 2 - Жилой фонд
        /// </summary>
        [Column("stand_type"), MaxLength(300)]
        public byte StandType { get; set; }

        /// <summary> Есть ли основание </summary>
        [Column("have_base"),Comment("Есть ли основание")]
        public bool HaveBase { get; set; }

        /// <summary> Есть ли крыша </summary>
        [Column("have_roof"),Comment("Есть ли крыша")]
        public bool HaveRoof { get; set; }

        /// <summary> Есть ли ограждение </summary>
        [Column("have_fence"),Comment("Есть ли ограждение")]
        public bool HaveFence { get; set; }

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
