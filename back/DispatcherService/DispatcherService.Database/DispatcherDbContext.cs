using DispatcherService.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace DispatcherService.Database
{
    public class DispatcherDbContext : DbContext
    {
        public DbSet<AuxiliaryWorker> AuxiliaryWorkers { get; set; }
        public DbSet<AuxiliaryWorkerHistory> AuxiliaryWorkerHistories { get; set; }
        public DbSet<Brigade> Brigades { get; set; }
        public DbSet<BrigadeHistory> BrigadeHistories { get; set; }
        public DbSet<Brigade2AuxWorker> Brigade2AuxWorkers { get; set; }
        public DbSet<Brigade2AuxWorkerHistory> Brigade2AuxWorkerHistories { get; set; }
        public DbSet<Brigade2Route> Brigade2Routes { get; set; }
        public DbSet<Brigade2RouteHistory> Brigade2RouteHistories { get; set; }
        public DbSet<TravelSheet> TravelSheets { get; set; }
        public DbSet<TravelSheetHistory> TravelSheetHistories { get; set; }
        public DbSet<ShiftTask> ShiftTasks { get; set; }
        public DbSet<ShiftTaskHistory> ShiftTaskHistories { get; set; }
        public DbSet<ShiftTask2Route> ShiftTask2Routes { get; set; }
        public DbSet<ShiftTask2RouteHistory> ShiftTask2RouteHistories { get; set; }

        public DispatcherDbContext(DbContextOptions<DispatcherDbContext> options)
            : base(options)
        {
        }
    }
}
