using DigiGrafWeb.Models;
using Microsoft.AspNetCore.Identity;

namespace DigiGrafWeb.Data.Seed.Core
{
    public static class RoleSeed
    {
        public static async Task SeedAsync(IServiceProvider services)
        {
            var roleManager = services.GetRequiredService<RoleManager<ApplicationRole>>();

            var roles = new[] { "Admin", "Uitvaartleider", "Medewerker", "Financieel" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new ApplicationRole { Name = role });
                }
            }
        }
    }
}
