using DigiGrafWeb.Models;

namespace DigiGrafWeb.Data.Seed.Reference
{
    public static class OriginsSeed
    {
        public static async Task SeedAsync(AppDbContext db)
        {
            if (db.Origins.Any())
                return;

            db.Origins.AddRange(
                new Origins { Id = Guid.NewGuid(), Code = "OO_01", Label = "Origin 01" },
                new Origins { Id = Guid.NewGuid(), Code = "OO_02", Label = "Origin 02" }
            );

            await db.SaveChangesAsync();
        }
    }
}
