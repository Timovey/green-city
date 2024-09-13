using GreenCity.Logging.DbLogging;
using Microsoft.EntityFrameworkCore;

namespace ScheduleService.Database
{
    public class ScheduleLogDbContext : LogDbContext
    {
        public ScheduleLogDbContext(DbContextOptions<LogDbContext> options)
            : base(options)
        {
        }
    }
}
