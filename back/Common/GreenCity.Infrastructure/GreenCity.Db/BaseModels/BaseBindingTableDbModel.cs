using System.ComponentModel.DataAnnotations.Schema;

namespace GreenCity.Db.BaseModels
{
    public class BaseBindingTableDbModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("data_id")]
        public int DataId { get; set; }
    }
}
