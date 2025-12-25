using DigiGrafWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace DigiGrafWeb.Data.Seed.Example
{
    public static class DossierSeed
    {
        public static async Task SeedAsync(AppDbContext db)
        {
            if (await db.Dossiers.AnyAsync())
                return;

            var dossierId = Guid.NewGuid();

            // ---------------- DOSSIER ----------------
            var dossier = new Dossier
            {
                Id = dossierId,
                FuneralLeader = "Patrick Stel",
                FuneralNumber = "UIT-2024-001",
                FuneralType = "Begrafenis",
                Voorregeling = false,
                DossierCompleted = false
            };

            // ---------------- DECEASED ----------------
            var deceased = new Deceased
            {
                Id = Guid.NewGuid(),
                DossierId = dossierId,

                Salutation = "Dhr.",
                FirstName = "Jan",
                LastName = "Jansen",
                SocialSecurity = "123456789",
                Dob = new DateTime(1950, 5, 12),
                Age = 73,
                PlaceOfBirth = "Groningen",

                Street = "Hoofdstraat",
                HouseNumber = "12",
                HouseNumberAddition = "A",
                PostalCode = "9671AB",
                City = "Winschoten",
                County = "Groningen",

                HomeDeceased = true,
                Gp = "Dr. Peters",
                GpPhone = "0597-123456",
                Me = "Ziekenhuis OZG"
            };

            // ---------------- DEATH INFO ----------------
            var deathInfo = new DeathInfo
            {
                Id = Guid.NewGuid(),
                DossierId = dossierId,

                DateOfDeath = DateTime.UtcNow.Date.AddDays(-2),
                TimeOfDeath = new TimeSpan(14, 30, 0),
                LocationOfDeath = "Thuis",
                StreetOfDeath = "Hoofdstraat",
                HouseNumberOfDeath = "12",
                HouseNumberAdditionOfDeath = "A",
                PostalCodeOfDeath = "9671AB",
                CityOfDeath = "Winschoten",
                CountyOfDeath = "Groningen",

                BodyFinding = "Natuurlijk overlijden",
                Origin = "Huisarts"
            };

            db.Dossiers.Add(dossier);
            db.Deceased.Add(deceased);
            db.DeathInfos.Add(deathInfo);

            await db.SaveChangesAsync();
        }
    }
}
