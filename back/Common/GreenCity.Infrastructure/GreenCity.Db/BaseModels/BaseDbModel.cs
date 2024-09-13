using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreenCity.Db.BaseModels
{
    public class BaseDbModel<T> where T : struct
    {
        /// <summary> Первичный ключ </summary>
        [Key, Column("id"), Comment("Первичный ключ")]
        public T Id { get; set; }

        /// <summary> Признак удаленности записи </summary>
        [Column("is_deleted"), Comment("Признак удаленности записи")]
        public bool IsDeleted { get; set; }
    }

    public class BaseDbModel : BaseDbModel<int>
    {
    }
}
