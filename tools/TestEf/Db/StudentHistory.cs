using System;
using System.ComponentModel.DataAnnotations;

namespace TestEf.Db
{
    public class StudentHistory
    {
        [Key]
        public int Id { get; set; }

        public string Fio { get; set; }

        public int? ClassId { get; set; }

        public string Info { get; set; }
        public string Info2 { get; set; }

        public int IsDeleted { get; set; }

        public int DataId { get; set; }

        public DateTime DtChange { get; set; }
    }
}
