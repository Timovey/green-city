using GreenCity.Logging.DbLogging;
using Microsoft.EntityFrameworkCore;

namespace SDService.Database
{
    public class SdLogDbContext : LogDbContext
    {
        public SdLogDbContext(DbContextOptions<LogDbContext> options)
            : base(options)
        {
        }
    }
}
