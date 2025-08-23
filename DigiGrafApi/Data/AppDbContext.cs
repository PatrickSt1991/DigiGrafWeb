using DigiGrafWeb.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DigiGrafWeb.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Dossier> Dossiers { get; set;}
        public DbSet<Deceased> Deceaseds { get; set; }
        public DbSet<DeathInfo> DeathInfos { get; set; }
    }

}
