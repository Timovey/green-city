using System.ComponentModel.DataAnnotations;

namespace TestEf.Db
{
    public class StudentItem
    {
        [Key]
        public int Id { get; set; }

        public string ItemName { get; set; }

        public int? StudentId { get; set; }

        public bool IsDeleted { get; set; }
    }
}
