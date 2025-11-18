using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BeFit.Models;

namespace BeFit.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ExType> ExType { get; set; } = default!;
        public DbSet<SessionInfo> SessionInfo { get; set; } = default!;
        public DbSet<ExConn> ExConn { get; set; } = default!;
    }
}
