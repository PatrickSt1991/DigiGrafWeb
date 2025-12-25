using DigiGrafWeb.Models;

namespace DigiGrafWeb.Data.Seed.Reference
{
    public static class CoffinLengthSeed
    {
        public static async Task SeedAsync(AppDbContext db)
        {
            if (db.CoffinsLengths.Any())
                return;

            db.CoffinsLengths.AddRange(
                new CoffinLengths { Id = Guid.NewGuid(), Code = "SHORT", Label = "Small", Description = "Klein" },
                new CoffinLengths { Id = Guid.NewGuid(), Code = "NORM", Label = "Regular", Description = "Normaal" },
                new CoffinLengths { Id = Guid.NewGuid(), Code = "XL", Label = "XL-b", Description = "Extra lang & breed" },
                new CoffinLengths { Id = Guid.NewGuid(), Code = "XXL", Label = "XXL-b", Description = "Extra extra lang & breed" }
            );

            await db.SaveChangesAsync();
        }
    }
}
