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

            var asr = await db.InsuranceParties.FirstOrDefaultAsync(p => p.Name == "ASR");
            var al = await db.InsuranceParties.FirstOrDefaultAsync(p => p.Name == "Allianz");

            if (asr == null && al == null)
                return;

            var list = new List<InsurancePriceComponent>();

            // Example: create separate templates per insurer (same as before),
            // but now attach with join table rows.
            if (asr != null)
            {
                list.AddRange(new[]
                {
                    Make(asr.Id, "Uitvaartkist", 1, 100m, 0m, 10),
                    Make(asr.Id, "Vervoer",     1, 75m,  0m, 20),
                    Make(asr.Id, "Dienst",      1, 250m, 0m, 30),
                });
            }

            if (al != null)
            {
                list.AddRange(new[]
                {
                    Make(al.Id, "Uitvaartkist", 1, 150m, 0m, 10),
                    Make(al.Id, "Vervoer",     1, 95m,  0m, 20),
                    Make(al.Id, "Dienst",      1, 300m, 0m, 30),
                });
            }

            db.InsurancePriceComponents.AddRange(list);
            await db.SaveChangesAsync();
        }

        private static InsurancePriceComponent Make(
            Guid partyId,
            string omschrijving,
            int aantal,
            decimal bedrag,
            decimal factuurBedrag,
            int sortOrder)
        {
            var id = Guid.NewGuid();

            return new InsurancePriceComponent
            {
                Id = id,
                Omschrijving = omschrijving,
                VerzekerdAantal = aantal,
                Bedrag = bedrag,
                FactuurBedrag = factuurBedrag,
                SortOrder = sortOrder,
                IsActive = true,
                InsuranceParties = new List<InsurancePriceComponentInsuranceParty>
                {
                    new InsurancePriceComponentInsuranceParty
                    {
                        InsurancePriceComponentId = id,
                        InsurancePartyId = partyId
                    }
                }
            };
        }
    }
}