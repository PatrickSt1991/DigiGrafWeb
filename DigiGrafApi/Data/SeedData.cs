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

            string[] roles = { "Admin", "Uitvaartleider", "Gebruiker", "Financieel" };
            foreach (var roleName in roles)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    var role = new ApplicationRole
                    {
                        Name = roleName,
                        NormalizedName = roleName.ToUpper()
                    };
                    await roleManager.CreateAsync(role);
                }
            }

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
            {
                if (!context.Salutations.Any(x => x.Code == s.Code))
                {
                    context.Salutations.Add(new Salutation
                    {
                        Id = Guid.NewGuid(),
                        Code = s.Code,
                        Label = s.Label,
                        IsActive = true
                    });
                }
            }

            if (!context.BodyFindings.Any())
            {
                context.BodyFindings.AddRange(
                    new BodyFinding { Code = "ND_NL", Label = "Natuurlijke dood + geen lijkvinding" },
                    new BodyFinding { Code = "ND_L", Label = "Natuurlijke dood + lijkvinding" },
                    new BodyFinding { Code = "NND_L", Label = "Niet natuurlijke dood + lijkvinding" },
                    new BodyFinding { Code = "NND_NL", Label = "Niet natuurlijke dood + geen lijkvinding" }
                );
            }

            if (!context.Origins.Any())
            {
                context.Origins.AddRange(
                    new Origins { Code = "OO_01", Label = "Origin 01" },
                    new Origins { Code = "OO_02", Label = "Origin 02" }
                );
            }


            await context.SaveChangesAsync();

        }
    }
}
