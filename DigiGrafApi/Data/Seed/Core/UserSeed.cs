using DigiGrafWeb.Models;
using Microsoft.AspNetCore.Identity;

namespace DigiGrafWeb.Data.Seed.Core
{
    public static class UserSeed
    {
        public static async Task SeedAsync(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

            const string adminEmail = "admin@digigraf.local";

            if (await userManager.FindByEmailAsync(adminEmail) != null)
                return;

            var user = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                FullName = "System Admin",
                IsActive = true,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(user, "Admin123!");

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "Admin");
            }
        }
    }
}
