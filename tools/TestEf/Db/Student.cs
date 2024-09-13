using System.ComponentModel.DataAnnotations;

namespace TestEf.Db
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        public string Fio { get; set; }

        public int? ClassId { get; set; }

        public string Info { get; set; }
        public string Info2 { get; set; }

        public int IsDeleted { get; set; }
    }
}
