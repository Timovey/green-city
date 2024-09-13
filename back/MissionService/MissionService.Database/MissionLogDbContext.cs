using GreenCity.Logging.DbLogging;
using Microsoft.EntityFrameworkCore;

namespace MissionService.Database
{
    public class MissionLogDbContext : LogDbContext
    {
        public MissionLogDbContext(DbContextOptions<LogDbContext> options)
            : base(options)
        {
        }
    }
}
