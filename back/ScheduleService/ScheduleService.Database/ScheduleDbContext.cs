using Microsoft.EntityFrameworkCore;
using ScheduleService.Database.Models;

namespace ScheduleService.Database
{
    public class ScheduleDbContext : DbContext
    {
        internal DbSet<RedDate> RedDates { get; set; }
        internal DbSet<WeekendWorkDate> WeekendWorkDays { get; set; }

        public ScheduleDbContext(DbContextOptions<ScheduleDbContext> options)
           : base(options)
        {
        }
    }
}
