using DigiGrafWeb.DTOs;
using DigiGrafWeb.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DigiGrafWeb.Data
{
    public static class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

            await context.Database.MigrateAsync();

            // Roles
            string[] roles = { "Admin", "Uitvaartleider", "Gebruiker", "Financieel" };
            foreach (var roleName in roles)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                    await roleManager.CreateAsync(new ApplicationRole { Name = roleName, NormalizedName = roleName.ToUpper() });
            }

            // Admin user
            var adminEmail = "admin@local.local";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true,
                    FullName = "System Administrator"
                };
                var result = await userManager.CreateAsync(adminUser, "Admin123!");
                if (!result.Succeeded)
                    throw new Exception($"Failed to create admin user: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }
            if (!await userManager.IsInRoleAsync(adminUser, "Admin"))
                await userManager.AddToRoleAsync(adminUser, "Admin");

            // Salutations
            var dutchSalutations = new[]
            {
                new { Code = "DHR", Label = "Dhr." },
                new { Code = "MEVR", Label = "Mevr." },
                new { Code = "MW", Label = "Mw." },
                new { Code = "HR", Label = "Hr." },
                new { Code = "DR", Label = "Dr." },
                new { Code = "MX", Label = "Mx." }
            };
            foreach (var s in dutchSalutations)
                if (!context.Salutations.Any(x => x.Code == s.Code))
                    context.Salutations.Add(new Salutation { Id = Guid.NewGuid(), Code = s.Code, Label = s.Label, IsActive = true });

            // Body Findings
            if (!context.BodyFindings.Any())
            {
                context.BodyFindings.AddRange(
                    new BodyFinding { Id = Guid.NewGuid(), Code = "ND_NL", Label = "Natuurlijke dood + geen lijkvinding" },
                    new BodyFinding { Id = Guid.NewGuid(), Code = "ND_L", Label = "Natuurlijke dood + lijkvinding" },
                    new BodyFinding { Id = Guid.NewGuid(), Code = "NND_L", Label = "Niet natuurlijke dood + lijkvinding" },
                    new BodyFinding { Id = Guid.NewGuid(), Code = "NND_NL", Label = "Niet natuurlijke dood + geen lijkvinding" }
                );
            }

            // Origins
            if (!context.Origins.Any())
                context.Origins.AddRange(
                    new Origins { Id = Guid.NewGuid(), Code = "OO_01", Label = "Origin 01" },
                    new Origins { Id = Guid.NewGuid(), Code = "OO_02", Label = "Origin 02" }
                );

            // Marital Statuses
            if (!context.MaritalStatuses.Any())
            {
                context.MaritalStatuses.AddRange(
                    new MaritalStatus { Id = Guid.NewGuid(), Code = "ONB", Label = "Onbekend" },
                    new MaritalStatus { Id = Guid.NewGuid(), Code = "ONH", Label = "Ongehuwd" },
                    new MaritalStatus { Id = Guid.NewGuid(), Code = "GEH", Label = "Gehuwd" },
                    new MaritalStatus { Id = Guid.NewGuid(), Code = "GES", Label = "Gescheiden" },
                    new MaritalStatus { Id = Guid.NewGuid(), Code = "WED", Label = "Weduwe/Weduwnaar" },
                    new MaritalStatus { Id = Guid.NewGuid(), Code = "SMP", Label = "Samenwonend / partnerschap" }
                );
            }

            // Insurance Companies
            if (!context.InsuranceCompanies.Any())
            {
                context.InsuranceCompanies.AddRange(
                    new InsuranceCompany { Id = Guid.NewGuid(), Label = "Allianz", IsActive = true },
                    new InsuranceCompany { Id = Guid.NewGuid(), Label = "Aegon", IsActive = true },
                    new InsuranceCompany { Id = Guid.NewGuid(), Label = "Nationale Nederlanden",  IsActive = true }
                );
            }

            // Coffins
            if (!context.Coffins.Any())
            {
                context.Coffins.AddRange(
                    new Coffins { Id = Guid.NewGuid(), Code = "E-023", Label = "MMHG", Description = "Massief mahonie, hoogglans gelakt" },
                    new Coffins { Id = Guid.NewGuid(), Code = "E-022", Label = "MEOG", Description = "Massief eiken, ongelakt, geprofileerdt" },
                    new Coffins { Id = Guid.NewGuid(), Code = "E-029h", Label = "LUXE", Description = "Uitvaartkist wit met waxfolie en houtnerf, luxe katoenen interieur met sierdeken en houten beslag" },
                    new Coffins { Id = Guid.NewGuid(), Code = "E-028", Label = "LAK_KATOEN", Description = "Uitvaartkist, lak zwart gespoten / katoen" },
                    new Coffins { Id = Guid.NewGuid(), Code = "E-012 Steiger", Label = "STEIGER", Description = "Massief Steigerhout, onbehandeld" }
                );
            }

            // Coffin Lengths
            if (!context.CoffinsLengths.Any())
            {
                context.CoffinsLengths.AddRange(
                    new CoffinLengths { Id = Guid.NewGuid(), Code = "SHORT", Label = "Small", Description = "Klein" },
                    new CoffinLengths { Id = Guid.NewGuid(), Code = "NORM", Label = "Regular", Description = "Normaal" },
                    new CoffinLengths { Id = Guid.NewGuid(), Code = "XL", Label = "XL-b", Description = "Extra lang & breed" },
                    new CoffinLengths { Id = Guid.NewGuid(), Code = "XXL", Label = "XXL-b", Description = "Extra extra lang & breed" }
                );
            }

            // Document Templates
            if (!context.DocumentTemplates.Any())
            {
                context.DocumentTemplates.AddRange(
                    new DocumentTemplate
                    {
                        Id = Guid.NewGuid(),
                        OverledeneId = null,
                        Title = "Standaard Document",
                        Sections = new List<DocumentSection>
                        {
                            new DocumentSection { Id = Guid.NewGuid(), Type = "text", Label = "Header", Value = "Uitvaart Centrum Logo / Header" },
                            new DocumentSection { Id = Guid.NewGuid(), Type = "textarea", Label = "Body", Value = "Hier komt de hoofdtekst van het document..." },
                            new DocumentSection { Id = Guid.NewGuid(), Type = "text", Label = "Footer", Value = "Contactgegevens en eventuele voetnoten" }
                        },
                        IsDefault = true
                    },
                    new DocumentTemplate
                    {
                        Id = Guid.NewGuid(),
                        OverledeneId = null,
                        Title = "Koffie Document",
                        Sections = new List<DocumentSection>
                        {
                            new DocumentSection { Id = Guid.NewGuid(), Type = "text", Label = "Header", Value = "Koffie Document Header" },
                            new DocumentSection { Id = Guid.NewGuid(), Type = "textarea", Label = "Body", Value = "Aantal kannen koffie: 3\nAantal koeken: 2" },
                            new DocumentSection { Id = Guid.NewGuid(), Type = "text", Label = "Footer", Value = "Dit document is automatisch gegenereerd" }
                        },
                        IsDefault = true
                    }
                );
            }

            if (!context.Dossiers.Any())
            {
                var dossier1 = new Dossier { Id = Guid.NewGuid(), FuneralNumber  = "1", FuneralLeader = "Patrick 1" };
                var dossier2 = new Dossier { Id = Guid.NewGuid(), FuneralNumber = "2", FuneralLeader = "Patrick 2" };
                context.Dossiers.AddRange(dossier1, dossier2);
                await context.SaveChangesAsync();
            }


            // Seed Deceased + Invoices + PriceComponents
            if (!context.Deceased.Any())
            {
                var dossiers = context.Dossiers.Take(2).ToList();

                var deceased1 = new Deceased
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Jan",
                    LastName = "Jansen",
                    Dob = new DateTime(1945, 5, 10),
                    DossierId = dossiers[0].Id
                };

                var deceased2 = new Deceased
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Maria",
                    LastName = "de Vries",
                    Dob = new DateTime(1950, 8, 15),
                    DossierId = dossiers[1].Id
                };

                context.Deceased.AddRange(deceased1, deceased2);
                await context.SaveChangesAsync();

                var insuranceList = context.InsuranceCompanies.ToList();

                var invoice1 = new Invoice
                {
                    Id = Guid.NewGuid(),
                    DeceasedId = deceased1.Id,
                    SelectedVerzekeraar = insuranceList.First().Label,
                    DiscountAmount = 50,
                    Subtotal = 1000,
                    Total = 950,
                    PriceComponents = new List<PriceComponent>
                    {
                        new PriceComponent { Id = Guid.NewGuid(), Omschrijving = "Kist", Aantal = 1, Bedrag = 500 },
                        new PriceComponent { Id = Guid.NewGuid(), Omschrijving = "Rouwbloemen", Aantal = 2, Bedrag = 100 },
                        new PriceComponent { Id = Guid.NewGuid(), Omschrijving = "Ceremonie", Aantal = 1, Bedrag = 400 }
                    }
                };

                var invoice2 = new Invoice
                {
                    Id = Guid.NewGuid(),
                    DeceasedId = deceased2.Id,
                    SelectedVerzekeraar = insuranceList.Skip(1).First().Label,
                    DiscountAmount = 0,
                    Subtotal = 750,
                    Total = 750,
                    PriceComponents = new List<PriceComponent>
                    {
                        new PriceComponent { Id = Guid.NewGuid(), Omschrijving = "Kist", Aantal = 1, Bedrag = 400 },
                        new PriceComponent { Id = Guid.NewGuid(), Omschrijving = "Transport", Aantal = 1, Bedrag = 100 },
                        new PriceComponent { Id = Guid.NewGuid(), Omschrijving = "Bloemen", Aantal = 3, Bedrag = 250 }
                    }
                };

                context.Invoices.AddRange(invoice1, invoice2);
            }

            await context.SaveChangesAsync();
        }
    }
}
