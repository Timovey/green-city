using GreenCity.Db.BaseModels;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace DispatcherService.Database.Models
{
    /// <summary> Историческая таблица связок бригады с подсобными рабочими </summary>
    [Table("brigade_2_aux_worker_history"), Comment("Историческая таблица связок бригады с подсобными рабочими")]
    public class Brigade2AuxWorkerHistory : BaseHistoryTableModel
    {
        /// <summary> ИД бригады </summary>
        [Column("brigade_id"), Comment("ИД бригады")]
        public int BrigadeId { get; set; }

        /// <summary> ИД подсобного рабочего </summary>
        [Column("aux_worker_id"), Comment("ИД подсобного рабочего")]
        public int AuxWorkerId { get; set; }
    }
}
