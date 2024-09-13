using LogistService.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace LogistService.Database
{
    public class LogistDbContext : DbContext
    {
        internal DbSet<BaseRoute> BaseRoutes { get; set; }
        internal DbSet<BaseRouteHistory> BaseRouteHistories { get; set; }
        internal DbSet<BaseRoute2Stand> BaseRoute2Stands { get; set; }
        internal DbSet<BaseRoute2StandHistory> BaseRoute2StandHistories { get; set; }
        internal DbSet<Route> Routes { get; set; }
        internal DbSet<RouteHistory> RouteHistories { get; set; }

        public LogistDbContext(DbContextOptions<LogistDbContext> options)
            : base(options)
        {
        }
    }
}