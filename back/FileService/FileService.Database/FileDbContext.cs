using FileService.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace FileService.Database
{
    public class FileDbContext : DbContext
    {
        internal DbSet<FileInStore> FileInStores { get; set; }
        internal DbSet<FileInStoreHistory> FileHistories { get; set; }

        public FileDbContext(DbContextOptions<FileDbContext> options)
        : base(options)
        {
        }
    }
}
