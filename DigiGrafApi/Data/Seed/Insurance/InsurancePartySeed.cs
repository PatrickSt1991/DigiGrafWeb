using DigiGrafWeb.Models;

namespace DigiGrafWeb.Data.Seed.Insurance
{
    public static class InsurancePartySeed
    {
        public static async Task SeedAsync(AppDbContext db)
        {
            if (db.InsuranceParties.Any())
                return;

            db.InsuranceParties.AddRange(
                new InsuranceParty
                {
                    Name = "Allianz",
                    IsInsurance = true,
                    IsActive = true
                },
                new InsuranceParty
                {
                    Name = "ASR",
                    IsInsurance = true,
                    IsActive = true
                },
                new InsuranceParty
                {
                    Name = "Algemeen Belang Winschoten",
                    IsAssociation = true,
                    IsActive = true
                }
            );

            await db.SaveChangesAsync();
        }
    }
}
