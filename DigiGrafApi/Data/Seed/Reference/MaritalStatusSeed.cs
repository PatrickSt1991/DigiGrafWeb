using DigiGrafWeb.Models;

namespace DigiGrafWeb.Data.Seed.Reference
{
    public static class MaritalStatusSeed
    {
        public static async Task SeedAsync(AppDbContext db)
        {
            if (db.MaritalStatuses.Any())
                return;

            db.MaritalStatuses.AddRange(
                new MaritalStatus { Id = Guid.NewGuid(), Code = "ONB", Label = "Onbekend" },
                new MaritalStatus { Id = Guid.NewGuid(), Code = "ONH", Label = "Ongehuwd" },
                new MaritalStatus { Id = Guid.NewGuid(), Code = "GEH", Label = "Gehuwd" },
                new MaritalStatus { Id = Guid.NewGuid(), Code = "GES", Label = "Gescheiden" },
                new MaritalStatus { Id = Guid.NewGuid(), Code = "WED", Label = "Weduwe/Weduwnaar" },
                new MaritalStatus { Id = Guid.NewGuid(), Code = "SMP", Label = "Samenwonend / partnerschap" }
            );

            await db.SaveChangesAsync();
        }
    }
}