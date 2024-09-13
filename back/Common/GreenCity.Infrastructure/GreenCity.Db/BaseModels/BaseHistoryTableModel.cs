using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreenCity.Db.BaseModels
{
    /// <summary>
    /// Базовая историческая таблица
    /// </summary>
    /// <typeparam name="T1"> ИД исторической записи </typeparam>
    /// <typeparam name="T2"> ИД отслеживаемой записи </typeparam>
    /// <typeparam name="T3"> ИД пользователя изсенившего запись </typeparam>
    public class BaseHistoryTableModel<T1, T2, T3> : BaseDbModel<T1>
        where T1 : struct
        where T2 : struct
    {
        /// <summary> ИД отслеживаемой записи </summary>
        [Column("data_id"), Comment("ИД отслеживаемой записи")]
        public T2 DataId { get; set; }

        /// <summary> Дата изменения записи </summary>
        [Column("change_dt"), Comment("Дата изменения записи")]
        public DateTime ChangeDt { get; set; }

        /// <summary> ИД пользователя изменившего запись </summary>
        [Column("change_user_id"), Comment("ИД пользователя изменившего запись")]
        public T3 ChangeUserId { get; set; }

        /// <summary> Имя пользователя изменившего запись </summary>
        [Column("change_user_name"), Comment("Имя пользователя изменившего запись"), MaxLength(500)]
        public string ChangeUserName { get; set; }
    }

    /// <summary>
    /// Стандартная реализация сущностей в базе
    /// </summary>
    public class BaseHistoryTableModel : BaseHistoryTableModel<int, int, int>
    {
    }
}
