using GreenCity.Logging.DbLogging;
using Microsoft.EntityFrameworkCore;

namespace FileService.Database
{
    public class FileLogDbContext : LogDbContext
    {
        public FileLogDbContext(DbContextOptions<LogDbContext> options)
            : base(options)
        {
        }
    }
}
