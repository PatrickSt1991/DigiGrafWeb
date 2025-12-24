using DigiGrafWeb.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DigiGrafWeb.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<License> Licenses { get; set; }
        public DbSet<Dossier> Dossiers { get; set; }
        public DbSet<Deceased> Deceased { get; set; }
        public DbSet<DeathInfo> DeathInfos { get; set; }
        public DbSet<Salutation> Salutations { get; set; } = null!;
        public DbSet<BodyFinding> BodyFindings { get; set; } = null!;
        public DbSet<Origins> Origins { get; set; } = null!;
        public DbSet<Suppliers> Suppliers { get; set; } = null!;
        public DbSet<MaritalStatus> MaritalStatuses { get; set; } = null!;

        // ✅ NEW / FIXED
        public DbSet<InsuranceParty> InsuranceParties { get; set; } = null!;
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

            // ===================== EMPLOYEE =====================
            builder.Entity<Employee>()
                .HasOne(e => e.User)
                .WithMany()
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.SetNull);

            // ===================== DOCUMENT TEMPLATES =====================
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

            // ===================== SUPPLIERS =====================
            builder.Entity<Suppliers>(entity =>
            {
                entity.OwnsOne(s => s.Address);
                entity.OwnsOne(s => s.Postbox);
            });

            // ===================== INSURANCE PARTY =====================

            builder.Entity<InsuranceParty>()
                .Property(p => p.CorrespondenceType)
                .HasConversion<int>();

            builder.Entity<InsuranceParty>()
                .Property(p => p.BillingType)
                .HasConversion<int>();

            // ===================== INSURANCE POLICY =====================

            builder.Entity<InsurancePolicy>()
                .HasOne(p => p.InsuranceParty)
                .WithMany(c => c.Policies)
                .HasForeignKey(p => p.InsurancePartyId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<InsurancePolicy>()
                .HasOne(p => p.Overledene)
                .WithMany()
                .HasForeignKey(p => p.OverledeneId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}