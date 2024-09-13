using Microsoft.EntityFrameworkCore;
using SDService.Database.Models.DbModels;
using SDService.Database.Models.DbModels.ContractSchedule;

namespace SDService.Database
{
    public class SdDbContext : DbContext
    {
        public DbSet<Container> Containers { get; set; }
        public DbSet<ContainerHistory> ContainerHistories { get; set; }
        public DbSet<Stand> Stands { get; set; }
        public DbSet<StandHistory> StandHistories { get; set; }
        public DbSet<ContractClient> ContractClients { get; set; }
        public DbSet<ContractClientHistory> ContractClientHistories { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<ContractHistory> ContractHistories { get; set; }
        public DbSet<Container2Schedule> Container2Schedules { get; set; }
        public DbSet<Container2ScheduleHistory> Container2ScheduleHistories { get; set; }
        public DbSet<ContainerType> ContainerTypes { get; set; }
        public DbSet<ContractScheduleMission> ContractScheduleMissions { get; set; }
        public DbSet<ContractScheduleMissionHistory> ContractScheduleMissionHistory { get; set; }
        public DbSet<Mission2Container> Mission2Containers { get; set; }
        public DbSet<Mission2ContainerHistory> Mission2ContainerHistory { get; set; }

        public SdDbContext(DbContextOptions<SdDbContext> options)
            : base(options)
        {
        }
    }
}
