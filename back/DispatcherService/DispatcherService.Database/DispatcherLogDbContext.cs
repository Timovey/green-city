using Microsoft.EntityFrameworkCore;
using GreenCity.Logging.DbLogging;


namespace DispatcherService.Database
{
    public class DispatcherLogDbContext : LogDbContext
    {
        public DispatcherLogDbContext(DbContextOptions<LogDbContext> options)
            : base(options)
        {
        }
    }

}
