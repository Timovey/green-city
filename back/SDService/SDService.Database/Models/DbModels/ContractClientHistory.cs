using GreenCity.Db.BaseModels;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SDService.Database.Models.DbModels
{
    /// <summary> История изменений контрагента </summary>
    [Table("contract_client_history")]
    public class ContractClientHistory : BaseHistoryTableModel
    {
        /// <summary> Наименование клиента </summary>
        [Column("client_name"), MaxLength(500)]
        public string ClientName { get; set; }

        /// <summary> Контактный телефон </summary>
        [Column("phone"), MaxLength(200)]
        public string ClientContact { get; set; }

        /// <summary> Электронный адрес (email) </summary>
        [Column("email"), MaxLength(100)]
        public string Email { get; set; }

        /// <summary> ИНН </summary>
        [Column("inn"), MaxLength(20)]
        public string Inn { get; set; }

        /// <summary> КПП </summary>
        [Column("kpp"), MaxLength(30)]
        public string Kpp { get; set; }

        /// <summary> ОГРН </summary>
        [Column("ogrn"), MaxLength(20)]
        public string Ogrn { get; set; }

        /// <summary> Расчетный счет </summary>
        [Column("r_schet"), MaxLength(20)]
        public string Rschet { get; set; }

        /// <summary> Банк </summary>
        [Column("bank"), MaxLength(300)]
        public string Bank { get; set; }

        /// <summary> Бик </summary>
        [Column("bik"), MaxLength(300)]
        public string Bik { get; set; }

        /// <summary> Корреспондентский счет </summary>
        [Column("k_schet"), MaxLength(50)]
        public string Kschet { get; set; }

        /// <summary> Юридический адрес </summary>
        [Column("legal_address"), MaxLength(300)]
        public string LegalAddress { get; set; }

        /// <summary> Почтовый адрес </summary>
        [Column("postal_address"), MaxLength(300)]
        public string PostalAddress { get; set; }

        /// <summary> Руководитель </summary>
        [Column("supervizor_name"), MaxLength(300)]
        public string SupervizorName { get; set; }

        /// <summary> Должность руководителя </summary>
        [Column("supervizor_position"), MaxLength(300)]
        public string SupervizorPosition { get; set; }

        /// <summary> Ответственное лицо - ФИО </summary>
        [Column("responsible_tko_person_fio"), MaxLength(200)]
        public string ResponsibleTkoPersonName { get; set; }

        /// <summary> Контактный телефон ответственного лица </summary>
        [Column("responsible_tko_person_phone"), MaxLength(200)]
        public string ResponsibleTkoPersonPhone { get; set; }

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
