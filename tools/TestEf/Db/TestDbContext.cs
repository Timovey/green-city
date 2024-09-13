using Microsoft.EntityFrameworkCore;

namespace TestEf.Db
{
    public class TestDbContext : DbContext
    {
        internal DbSet<SchoolClass> SchoolClasses { get; set; }
        internal DbSet<Student> Students { get; set; }
        internal DbSet<StudentHistory> StudentHistories { get; set; }
        internal DbSet<StudentItem> StudentItems { get; set; }

        public TestDbContext(DbContextOptions<TestDbContext> options)
            : base(options)
        {
        }
    }
}
