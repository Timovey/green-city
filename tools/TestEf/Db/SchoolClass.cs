using System.ComponentModel.DataAnnotations;

namespace TestEf.Db
{
    public class SchoolClass
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsDeleted { get; set; }
    }
}
