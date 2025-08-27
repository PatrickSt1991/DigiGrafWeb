using DigiGrafWeb.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DigiGrafWeb.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Domain-specific tables
        public DbSet<Dossier> Dossiers { get; set; }
        public DbSet<Deceased> Deceased { get; set; }
        public DbSet<DeathInfo> DeathInfos { get; set; }
        public DbSet<Salutation> Salutations { get; set; } = null!;
        public DbSet<BodyFinding> BodyFindings { get; set; } = null!;
        public DbSet<Origins> Origins { get; set; } = null;


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

        }
    }
}
