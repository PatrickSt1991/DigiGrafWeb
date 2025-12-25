using DigiGrafWeb.Models;

namespace DigiGrafWeb.Data.Seed.Reference
{
    public static class CoffinSeed
    {
        public static async Task SeedAsync(AppDbContext db)
        {
            if (db.Coffins.Any())
                return;

            db.Coffins.AddRange(
                new Coffins { Id = Guid.NewGuid(), Code = "E-023", Label = "MMHG", Description = "Massief mahonie, hoogglans gelakt" },
                new Coffins { Id = Guid.NewGuid(), Code = "E-022", Label = "MEOG", Description = "Massief eiken, ongelakt, geprofileerdt" },
                new Coffins { Id = Guid.NewGuid(), Code = "E-029h", Label = "LUXE", Description = "Uitvaartkist wit met waxfolie en houtnerf, luxe katoenen interieur met sierdeken en houten beslag" },
                new Coffins { Id = Guid.NewGuid(), Code = "E-028", Label = "LAK_KATOEN", Description = "Uitvaartkist, lak zwart gespoten / katoen" },
                new Coffins { Id = Guid.NewGuid(), Code = "E-012 Steiger", Label = "STEIGER", Description = "Massief Steigerhout, onbehandeld" }
            );

            await db.SaveChangesAsync();
        }
    }
}
