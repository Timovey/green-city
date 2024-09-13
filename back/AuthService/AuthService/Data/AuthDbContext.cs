using AuthService.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AuthService.Models.Db;

namespace AuthService.Data
{
    public class AuthDbContext : IdentityDbContext<GreenCityIdentityUser, GreenCityIdentityRole, int>
    {
        internal DbSet<DbOrganization> Organizations { get; set; }
        internal DbSet<DbOrganizationHistory> DbOrganizationHistories { get; set; }
        internal DbSet<DbUserToOrgs> DbUserToOrgs { get; set; }
        internal DbSet<DbUserToOrgsHistory> DbUserToOrgsHistories { get; set; }

        public AuthDbContext(DbContextOptions<AuthDbContext> options)
            : base(options)
        {
        }
    }
}
