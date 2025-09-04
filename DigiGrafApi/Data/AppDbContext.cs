using DigiGrafWeb.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DigiGrafWeb.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Dossier> Dossiers { get; set; }
        public DbSet<Deceased> Deceased { get; set; }
        public DbSet<DeathInfo> DeathInfos { get; set; }
        public DbSet<Salutation> Salutations { get; set; } = null!;
        public DbSet<BodyFinding> BodyFindings { get; set; } = null!;
        public DbSet<Origins> Origins { get; set; } = null!;
        public DbSet<MaritalStatus> MaritalStatuses { get; set; } = null!;
        public DbSet<InsuranceCompany> InsuranceCompanies { get; set; } = null!;
        public DbSet<InsurancePolicy> InsurancePolicies { get; set; } = null!;
        public DbSet<Coffins> Coffins { get; set; } = null!;
        public DbSet<CoffinLengths> CoffinsLengths { get; set; } = null!;
        public DbSet<DocumentTemplate> DocumentTemplates { get; set; } = null!;
        public DbSet<DocumentSection> DocumentSections { get; set; } = null!;
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<PriceComponent> PriceComponents { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<DocumentTemplate>()
                .HasMany(d => d.Sections)
                .WithOne(s => s.Template)
                .HasForeignKey(s => s.DocumentTemplateId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<DocumentSection>()
                .Property(s => s.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<DocumentSection>()
                .Property(s => s.Label)
                .IsRequired();

            builder.Entity<DocumentSection>()
                .Property(s => s.Type)
                .IsRequired();
        }
    }
}
