using DigiGrafWeb.Models;

namespace DigiGrafWeb.Data.Seed.Reference
{
    public static class BodyFindingSeed
    {
        public static async Task SeedAsync(AppDbContext db)
        {
            if (db.BodyFindings.Any())
                return;

            db.BodyFindings.AddRange(
                new BodyFinding { Id = Guid.NewGuid(), Code = "ND_NL", Label = "Natuurlijke dood + geen lijkvinding" },
                new BodyFinding { Id = Guid.NewGuid(), Code = "ND_L", Label = "Natuurlijke dood + lijkvinding" },
                new BodyFinding { Id = Guid.NewGuid(), Code = "NND_L", Label = "Niet natuurlijke dood + lijkvinding" },
                new BodyFinding { Id = Guid.NewGuid(), Code = "NND_NL", Label = "Niet natuurlijke dood + geen lijkvinding" }
            );

            await db.SaveChangesAsync();
        }
    }
}
