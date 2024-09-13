using Microsoft.EntityFrameworkCore;
using MissionService.Database.Models.DbModels;

namespace MissionService.Database
{
    public class MissionDbContext : DbContext
    {
        public DbSet<App> Apps { get; set; }
        public DbSet<AppHistory> AppHistories { get; set; }
        public DbSet<App2Mission> App2Missions { get; set; }
        public DbSet<App2MissionHistory> App2MissionHistories { get; set; }
        public DbSet<Mission> Missions { get; set; }
        public DbSet<MissionHistory> MissionHistories { get; set; }

        public MissionDbContext(DbContextOptions<MissionDbContext> options)
            : base(options)
        {
        }
    }
}
