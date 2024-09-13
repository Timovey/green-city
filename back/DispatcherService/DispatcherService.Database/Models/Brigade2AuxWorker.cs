using GreenCity.Db.BaseModels;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace DispatcherService.Database.Models
{
    /// <summary> Связка бригады с подсобным рабочим </summary>
    [Table("brigade_2_aux_worker"), Comment("Связка бригады с подсобным рабочим")]
    public class Brigade2AuxWorker : BaseDbModel
    {
        /// <summary> ИД бригады </summary>
        [Column("brigade_id"), Comment("ИД бригады")]
        public int BrigadeId { get; set; }

        /// <summary> ИД подсобного рабочего </summary>
        [Column("aux_worker_id"), Comment("ИД подсобного рабочего")]
        public int AuxWorkerId { get; set; }
    }
}
