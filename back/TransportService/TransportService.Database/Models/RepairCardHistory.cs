using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace TransportService.Database.Models
{
    /// <summary> Карточка ремонта/ТО - Историческая таблица </summary>
    [Table("repair_card_history"), Comment("Карточка ремонта/ТО - Историческая таблица")]
    public class RepairCardHistory : DateRangeHistory
    {
        /// <summary> Событие </summary>
        [Column("event"), Comment("Событие")]
        public byte Event { get; set; }

        /// <summary> ИД организации </summary>
        [Column("organization_id"), Comment("ИД организации")]
        public int? OrganizationId { get; set; }

        /// <summary> ИД транспортного средства </summary>
        [Column("transport_id"), Comment("ИД транспортного средства")]
        public int TransportId { get; set; }

        /// <summary>
        /// В какую смену производиться ремонт/ТО при графике 6/3
        /// </summary>
        [Column("work_shift"), Comment("В какую смену производиться ремонт/ТО при графике 6/3")]
        public byte WorkShift { get; set; }
    }
}
