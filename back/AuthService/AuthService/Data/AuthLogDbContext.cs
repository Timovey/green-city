using GreenCity.Logging.DbLogging;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Data
{
    public class AuthLogDbContext : LogDbContext
    {
        public AuthLogDbContext(DbContextOptions<LogDbContext> options)
            : base(options)
        {
        }
    }
}
