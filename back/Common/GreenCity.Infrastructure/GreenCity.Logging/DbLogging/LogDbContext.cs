using Microsoft.EntityFrameworkCore;

namespace GreenCity.Logging.DbLogging
{
    public class LogDbContext : DbContext
    {
        public DbSet<LogDbModel> Logs { get; set; }

        public LogDbContext(DbContextOptions<LogDbContext> options)
            : base(options)
        {
        }
    }
}
