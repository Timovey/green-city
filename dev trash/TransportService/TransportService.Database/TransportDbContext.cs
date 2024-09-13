using Microsoft.EntityFrameworkCore;
using TransportService.Database.Models;

namespace TransportService.Database
{
    public class TransportDbContext : DbContext
    {
        internal DbSet<Transport> Transports { get; set; }

        public TransportDbContext(DbContextOptions<TransportDbContext> options)
            : base(options)
        {
        }
    }
}
