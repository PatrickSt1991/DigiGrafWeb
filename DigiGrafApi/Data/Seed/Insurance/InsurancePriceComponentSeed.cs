using DigiGrafWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace DigiGrafWeb.Data.Seed.Insurance
{
    public static class InsurancePriceComponentSeed
    {
        public static async Task SeedAsync(AppDbContext db)
        {
            if (db.InsurancePriceComponents.Any())
                return;

            // Pick specific insurers by name from your seed
            var asr = await db.InsuranceParties.FirstOrDefaultAsync(p => p.Name == "ASR");
            var al = await db.InsuranceParties.FirstOrDefaultAsync(p => p.Name == "Allianz");

            // If you don’t have ASR in seed yet, you should add it in InsurancePartySeed.
            if (asr == null && al == null)
                return;

            var list = new List<InsurancePriceComponent>();

            if (asr != null)
            {
                list.AddRange(new[]
                {
                    new InsurancePriceComponent { Id = Guid.NewGuid(), InsurancePartyId = asr.Id, Omschrijving = "Uitvaartkist", Aantal = 1, Bedrag = 100, SortOrder = 10, IsActive = true },
                    new InsurancePriceComponent { Id = Guid.NewGuid(), InsurancePartyId = asr.Id, Omschrijving = "Vervoer",     Aantal = 1, Bedrag = 75,  SortOrder = 20, IsActive = true },
                    new InsurancePriceComponent { Id = Guid.NewGuid(), InsurancePartyId = asr.Id, Omschrijving = "Dienst",      Aantal = 1, Bedrag = 250, SortOrder = 30, IsActive = true },
                });
            }

            if (al != null)
            {
                list.AddRange(new[]
                {
                    new InsurancePriceComponent { Id = Guid.NewGuid(), InsurancePartyId = al.Id, Omschrijving = "Uitvaartkist", Aantal = 1, Bedrag = 150, SortOrder = 10, IsActive = true },
                    new InsurancePriceComponent { Id = Guid.NewGuid(), InsurancePartyId = al.Id, Omschrijving = "Vervoer",     Aantal = 1, Bedrag = 95,  SortOrder = 20, IsActive = true },
                    new InsurancePriceComponent { Id = Guid.NewGuid(), InsurancePartyId = al.Id, Omschrijving = "Dienst",      Aantal = 1, Bedrag = 300, SortOrder = 30, IsActive = true },
                });
            }

            db.InsurancePriceComponents.AddRange(list);
            await db.SaveChangesAsync();
        }
    }
}
