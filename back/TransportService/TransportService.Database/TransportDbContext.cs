using Microsoft.EntityFrameworkCore;
using TransportService.Database.Models;

namespace TransportService.Database
{
    public class TransportDbContext : DbContext
    {
        internal DbSet<Transport> Transports { get; set; }
        internal DbSet<TransportHistory> TransportHistories { get; set; }
        internal DbSet<TransportShedule> TransportShedules { get; set; }
        internal DbSet<TransportSheduleHistory> TransportSheduleHistories { get; set; }
        internal DbSet<TransportWorkShift> TransportWorkShifts { get; set; }
        internal DbSet<TransportWorkShiftHistory> TransportWorkShiftHistories { get; set; }
        internal DbSet<Driver> Drivers { get; set; }
        internal DbSet<DriverHistory> DriverHistories { get; set; }
        internal DbSet<DriverVacation> DriverVacations { get; set; }
        internal DbSet<DriverVacationHistory> DriverVacationHistories { get; set; }
        internal DbSet<Driver2Transport> Driver2Transports { get; set; }
        internal DbSet<Driver2TransportHistory> Driver2TransportHistories { get; set; }
        internal DbSet<RepairCard> RepairCards { get; set; }
        internal DbSet<RepairCardHistory> RepairCardHistories { get; set; }
        internal DbSet<DateRange> DateRanges { get; set; }
        internal DbSet<DateRangeHistory> DateRangeHistories { get; set; }
        internal DbSet<Rent> Rents { get; set; }
        internal DbSet<RentHistory> RentHistories { get; set; }
        internal DbSet<TransportDutyShift> TransportDutyShifts { get; set; }
        internal DbSet<TransportDutyShiftHistory> TransportDutyShiftHistories { get; set; }

        public TransportDbContext(DbContextOptions<TransportDbContext> options)
        : base(options)
        {
        }
    }
}
