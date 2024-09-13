using GreenCity.Logging.DbLogging;
using Microsoft.EntityFrameworkCore;

namespace LogistService.Database
{
    public class LogistLogsDbContext : LogDbContext
    {
        public LogistLogsDbContext(DbContextOptions<LogDbContext> options)
            : base(options)
        {
        }
    }
}
