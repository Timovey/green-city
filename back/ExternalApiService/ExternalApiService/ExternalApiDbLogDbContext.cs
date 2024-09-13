using GreenCity.Logging.DbLogging;
using Microsoft.EntityFrameworkCore;

namespace ExternalApiService
{
    internal class ExternalApiDbLogDbContext : LogDbContext
    {
        public ExternalApiDbLogDbContext(DbContextOptions<LogDbContext> options)
            : base(options)
        {
        }
    }
}
