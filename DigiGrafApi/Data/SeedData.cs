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

            // --------- DEVELOPMENT ONLY: Drop and recreate DB ----------
            await context.Database.EnsureDeletedAsync();
            await context.Database.EnsureCreatedAsync();
            // ------------------------------------------------------------

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
                context.Salutations.Add(new Salutation { Id = Guid.NewGuid(), Code = s.Code, Label = s.Label, IsActive = true });

            var adminEmployee = new Employee
            {
                Id = Guid.NewGuid(),
                IsActive = true,
                Initials = "SYS",
                FirstName = "System",
                LastName = "Administrator",
                BirthPlace = "N/A",
                BirthDate = new DateOnly(1980, 1, 1),
                Email = adminUser.Email!,
                StartDate = new DateOnly(2020, 1, 1),
                UserId = adminUser.Id
            };

            var janEmployee = new Employee
            {
                Id = Guid.NewGuid(),
                IsActive = true,
                Initials = "J.D.",
                FirstName = "Jan",
                LastName = "Doe",
                Tussenvoegsel = "de",
                BirthPlace = "Amsterdam",
                BirthDate = new DateOnly(1985, 5, 15),
                Email = "j.doe@company.nl",
                Mobile = "06-12345678",
                StartDate = new DateOnly(2020, 1, 15)
            };

            context.Employees.AddRange(adminEmployee, janEmployee);
            await context.SaveChangesAsync();

            var janUser = await userManager.FindByEmailAsync(janEmployee.Email);
            if (janUser == null)
            {
                janUser = new ApplicationUser
                {
                    UserName = janEmployee.Email,
                    Email = janEmployee.Email,
                    EmailConfirmed = true,
                    FullName = "Jan de Doe",
                    IsActive = true
                };

                var result = await userManager.CreateAsync(janUser, "Employee123!");
                if (!result.Succeeded)
                    throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
            }

            if (!await userManager.IsInRoleAsync(janUser, "Gebruiker"))
            {
                await userManager.AddToRoleAsync(janUser, "Gebruiker");
            }

            janEmployee.UserId = janUser.Id;
            context.Employees.Update(janEmployee);
            await context.SaveChangesAsync();


            // Body Findings
            context.BodyFindings.AddRange(
                new BodyFinding { Id = Guid.NewGuid(), Code = "ND_NL", Label = "Natuurlijke dood + geen lijkvinding" },
                new BodyFinding { Id = Guid.NewGuid(), Code = "ND_L", Label = "Natuurlijke dood + lijkvinding" },
                new BodyFinding { Id = Guid.NewGuid(), Code = "NND_L", Label = "Niet natuurlijke dood + lijkvinding" },
                new BodyFinding { Id = Guid.NewGuid(), Code = "NND_NL", Label = "Niet natuurlijke dood + geen lijkvinding" }
            );

            // Origins
            context.Origins.AddRange(
                new Origins { Id = Guid.NewGuid(), Code = "OO_01", Label = "Origin 01" },
                new Origins { Id = Guid.NewGuid(), Code = "OO_02", Label = "Origin 02" }
            );

            // Marital Statuses
            context.MaritalStatuses.AddRange(
                new MaritalStatus { Id = Guid.NewGuid(), Code = "ONB", Label = "Onbekend" },
                new MaritalStatus { Id = Guid.NewGuid(), Code = "ONH", Label = "Ongehuwd" },
                new MaritalStatus { Id = Guid.NewGuid(), Code = "GEH", Label = "Gehuwd" },
                new MaritalStatus { Id = Guid.NewGuid(), Code = "GES", Label = "Gescheiden" },
                new MaritalStatus { Id = Guid.NewGuid(), Code = "WED", Label = "Weduwe/Weduwnaar" },
                new MaritalStatus { Id = Guid.NewGuid(), Code = "SMP", Label = "Samenwonend / partnerschap" }
            );

            // Insurance parties (verzekeraars)
            context.InsuranceParties.AddRange(
                new InsuranceParty { Id = Guid.NewGuid(), Name = "Allianz", IsActive = true, IsInsurance = true, IsAssociation = false },
                new InsuranceParty { Id = Guid.NewGuid(), Name = "Aegon", IsActive = true, IsInsurance = true, IsAssociation = false },
                new InsuranceParty { Id = Guid.NewGuid(), Name = "Nationale Nederlanden", IsActive = true, IsInsurance = true, IsAssociation = false }
            );

            // Coffins
            context.Coffins.AddRange(
                new Coffins { Id = Guid.NewGuid(), Code = "E-023", Label = "MMHG", Description = "Massief mahonie, hoogglans gelakt" },
                new Coffins { Id = Guid.NewGuid(), Code = "E-022", Label = "MEOG", Description = "Massief eiken, ongelakt, geprofileerdt" },
                new Coffins { Id = Guid.NewGuid(), Code = "E-029h", Label = "LUXE", Description = "Uitvaartkist wit met waxfolie en houtnerf, luxe katoenen interieur met sierdeken en houten beslag" },
                new Coffins { Id = Guid.NewGuid(), Code = "E-028", Label = "LAK_KATOEN", Description = "Uitvaartkist, lak zwart gespoten / katoen" },
                new Coffins { Id = Guid.NewGuid(), Code = "E-012 Steiger", Label = "STEIGER", Description = "Massief Steigerhout, onbehandeld" }
            );

            // Coffin Lengths
            context.CoffinsLengths.AddRange(
                new CoffinLengths { Id = Guid.NewGuid(), Code = "SHORT", Label = "Small", Description = "Klein" },
                new CoffinLengths { Id = Guid.NewGuid(), Code = "NORM", Label = "Regular", Description = "Normaal" },
                new CoffinLengths { Id = Guid.NewGuid(), Code = "XL", Label = "XL-b", Description = "Extra lang & breed" },
                new CoffinLengths { Id = Guid.NewGuid(), Code = "XXL", Label = "XXL-b", Description = "Extra extra lang & breed" }
            );
            // Suppliers
            context.Suppliers.AddRange(
                new Suppliers
                {
                    Id = Guid.NewGuid(),
                    Name = "Uitvaartkisten Noord",
                    Description = "Leverancier van uitvaartkisten",
                    Type = SupplierType.Kisten,
                    IsActive = true,
                    Address = new PostalAddress
                    {
                        Street = "Industrieweg",
                        HouseNumber = "12",
                        ZipCode = "9403AB",
                        City = "Assen"
                    }
                },
                new Suppliers
                {
                    Id = Guid.NewGuid(),
                    Name = "Bloemenservice De Roos",
                    Description = "Rouwbloemen en arrangementen",
                    Type = SupplierType.Bloemen,
                    IsActive = true,
                    Address = new PostalAddress
                    {
                        Street = "Bloemenstraat",
                        HouseNumber = "45",
                        ZipCode = "9712HG",
                        City = "Groningen"
                    }
                },
                new Suppliers
                {
                    Id = Guid.NewGuid(),
                    Name = "Urn & Gedenk Atelier",
                    Description = "Urnen en gedenksieraden",
                    Type = SupplierType.UrnAndGedenksieraden,
                    IsActive = true,
                    Postbox = new Postbox
                    {
                        Address = "Postbus 123",
                        Zipcode = "8000AA",
                        City = "Zwolle"
                    }
                },
                new Suppliers
                {
                    Id = Guid.NewGuid(),
                    Name = "Steenhouwerij Van Dijk",
                    Description = "Grafmonumenten en natuursteen",
                    Type = SupplierType.Steenhouwer,
                    IsActive = true,
                    Address = new PostalAddress
                    {
                        Street = "Ambachtslaan",
                        HouseNumber = "7",
                        ZipCode = "7202CP",
                        City = "Zutphen"
                    }
                }
            );

            // Document Templates & Sections (with exact previous IDs/content)
            var templateKoffieId = Guid.Parse("68316941-68A7-4ABF-BDCD-B0F0C5E8E9E7");
            var templateStandaardId = Guid.Parse("D2EB48A3-1D5E-4803-B7DF-F5D2B223437D");

            var templates = new List<DocumentTemplate>
            {
                new DocumentTemplate
                {
                    Id = templateKoffieId,
                    Title = "Koffie Document",
                    IsDefault = true,
                    Sections = new List<DocumentSection>
                    {
                        new DocumentSection
                        {
                            Id = Guid.Parse("314AE355-82C9-4933-9822-11B88C7E2D0A"),
                            Type = "textarea",
                            Label = "Body",
                            Value = @"<div style=""font-family: Arial, sans-serif; max-width: 800px; margin: 0 auto; padding: 20px;"">
  <div style=""margin-bottom: 30px;"">
    <div style=""display: flex; margin-bottom: 10px;"">
      <div style=""width: 200px; font-weight: bold;"">Dag</div>
      <div style=""flex: 1; border-bottom: 1px dotted #ccc;""></div>
    </div>
    <div style=""display: flex; margin-bottom: 10px;"">
      <div style=""width: 200px; font-weight: bold;"">Naam</div>
      <div style=""flex: 1; border-bottom: 1px dotted #ccc;""></div>
    </div>
    ...
</div>"
                        },
                        new DocumentSection
                        {
                            Id = Guid.Parse("97604C6C-BF86-49BC-84AE-64DD91518CB4"),
                            Type = "text",
                            Label = "Footer",
                            Value = "Dit document is automatisch gegenereerd"
                        }
                    }
                },
                new DocumentTemplate
                {
                    Id = templateStandaardId,
                    Title = "Standaard Document",
                    IsDefault = true,
                    Sections = new List<DocumentSection>
                    {
                        new DocumentSection
                        {
                            Id = Guid.Parse("40CA01C6-EF3E-4CF9-BFE8-67A59632A53B"),
                            Type = "text",
                            Label = "Header",
                            Value = @"<div style=""text-align: center; padding: 15px 0; border-bottom: 1px solid #e0e0e0; margin-bottom: 20px;"">
  <img src=""https://www.uitvaartverzorging-eefting.nl/templates/img/logo.jpg"" alt=""logo"" style=""max-height: 80px; margin-bottom: 15px;"" />
  <h1 style=""color: #333; margin: 0; font-size: 24px; font-weight: 600;"">Aanvraagformulier koffiekamer / condoleance</h1>
</div>"
                        },
                        new DocumentSection
                        {
                            Id = Guid.Parse("CD492801-DA2F-4138-84C8-3DAF0E8BEB2D"),
                            Type = "text",
                            Label = "Footer",
                            Value = "Contactgegevens en eventuele voetnoten"
                        },
                        new DocumentSection
                        {
                            Id = Guid.Parse("B5BEF6E8-71CE-4CD4-8CB0-97E891127234"),
                            Type = "textarea",
                            Label = "Body",
                            Value = "Hier komt de hoofdtekst van het document..."
                        },
                        new DocumentSection
                        {
                            Id = Guid.Parse("6A72F65C-A0DC-40CC-9CEE-40148D180CD0"),
                            Type = "text",
                            Label = "Header",
                            Value = "Uitvaart Centrum Logo / Header"
                        }
                    }
                }
            };
            context.DocumentTemplates.AddRange(templates);

            // Dossiers
            var dossier1 = new Dossier { Id = Guid.NewGuid(), FuneralNumber = "1", FuneralLeader = "Patrick 1" };
            var dossier2 = new Dossier { Id = Guid.NewGuid(), FuneralNumber = "2", FuneralLeader = "Patrick 2" };
            context.Dossiers.AddRange(dossier1, dossier2);

            await context.SaveChangesAsync();

            // Deceased + Invoices + PriceComponents
            var deceased1 = new Deceased
            {
                Id = Guid.NewGuid(),
                FirstName = "Jan",
                LastName = "Jansen",
                Dob = new DateTime(1945, 5, 10),
                DossierId = dossier1.Id
            };
            var deceased2 = new Deceased
            {
                Id = Guid.NewGuid(),
                FirstName = "Maria",
                LastName = "de Vries",
                Dob = new DateTime(1950, 8, 15),
                DossierId = dossier2.Id
            };
            context.Deceased.AddRange(deceased1, deceased2);
            await context.SaveChangesAsync();

            var insuranceList = context.InsuranceParties.ToList();

            var invoice1 = new Invoice
            {
                Id = Guid.NewGuid(),
                DeceasedId = deceased1.Id,
                SelectedVerzekeraar = insuranceList.First().Name,
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
                SelectedVerzekeraar = insuranceList.Skip(1).First().Name,
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
            await context.SaveChangesAsync();
        }
    }
}