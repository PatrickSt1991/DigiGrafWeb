using DigiGrafWeb.Models;

namespace DigiGrafWeb.Data.Seed.Reference
{
    public static class SalutationSeed
    {
        public static async Task SeedAsync(AppDbContext db)
        {
            if (db.Salutations.Any())
                return;

            db.Salutations.AddRange(
                new Salutation { Id = Guid.NewGuid(), Code = "DHR", Label = "Dhr.", IsActive = true },
                new Salutation { Id = Guid.NewGuid(), Code = "MEVR", Label = "Mevr.", IsActive = true },
                new Salutation { Id = Guid.NewGuid(), Code = "MW", Label = "Mw.", IsActive = true },
                new Salutation { Id = Guid.NewGuid(), Code = "HR", Label = "Hr.", IsActive = true },
                new Salutation { Id = Guid.NewGuid(), Code = "DR", Label = "Dr.", IsActive = true },
                new Salutation { Id = Guid.NewGuid(), Code = "MX", Label = "Mx.", IsActive = true }
            );

            await db.SaveChangesAsync();
        }
    }
}