using GreenCity.Logging.DbLogging;
using Microsoft.EntityFrameworkCore;

namespace TransportService.Database
{
    public class TransportLogDbContext : LogDbContext
    {
        public TransportLogDbContext(DbContextOptions<LogDbContext> options)
            : base(options)
        {
        }
    }
}
