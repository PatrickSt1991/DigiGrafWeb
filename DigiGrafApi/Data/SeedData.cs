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

            // Ensure database is created/migrated
            await context.Database.MigrateAsync();

            // -------------------------
            // 1️⃣ Seed Identity Roles
            // -------------------------
            string[] identityRoleNames = { "Admin", "Uitvaartleider", "Gebruiker", "Financieel" };
            foreach (var roleName in identityRoleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    var identityRole = new ApplicationRole
                    {
                        Id = Guid.NewGuid(),
                        Name = roleName,
                        NormalizedName = roleName.ToUpper()
                    };
                    await roleManager.CreateAsync(identityRole);
                }
            }

            // -------------------------
            // 2️⃣ Seed Identity Admin User
            // -------------------------
            var adminEmail = "admin@local.local";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true,
                    FullName = "System Administrator",
                    RoleDescription = "Default system admin"
                };

                var result = await userManager.CreateAsync(adminUser, "Admin123!");
                if (!result.Succeeded)
                    throw new Exception($"Failed to create admin user: {string.Join(", ", result.Errors.Select(e => e.Description))}");

                await userManager.AddToRoleAsync(adminUser, "Admin");
            }

            // -------------------------
            // 3️⃣ Seed Custom Roles
            // -------------------------
            string[] customRoleNames = { "Admin", "Uitvaartleider", "Gebruiker", "Financieel" };
            foreach (var roleName in customRoleNames)
            {
                if (!context.Roles.Any(r => r.Name == roleName))
                {
                    context.Roles.Add(new Role
                    {
                        Id = Guid.NewGuid(),
                        Name = roleName
                    });
                }
            }
            await context.SaveChangesAsync();

            // -------------------------
            // 4️⃣ Seed Permissions
            // -------------------------
            if (!context.Permissions.Any())
            {
                var permissions = new[]
                {
                    new Permission { Id = Guid.NewGuid(), Name = "ManageFinance" },
                    new Permission { Id = Guid.NewGuid(), Name = "ManageReports" },
                    new Permission { Id = Guid.NewGuid(), Name = "ManageSystem" },
                    new Permission { Id = Guid.NewGuid(), Name = "ViewDossiers" },
                    new Permission { Id = Guid.NewGuid(), Name = "CreateDossier" }
                };

                context.Permissions.AddRange(permissions);
                await context.SaveChangesAsync();
            }

            // -------------------------
            // 5️⃣ Assign ManageSystem to Admin (custom Role)
            // -------------------------
            var adminCustomRole = await context.Roles.FirstAsync(r => r.Name == "Admin");
            var manageSystemPermission = await context.Permissions.FirstAsync(p => p.Name == "ManageSystem");

            if (!context.RolePermissions.Any(rp => rp.RoleId == adminCustomRole.Id && rp.PermissionId == manageSystemPermission.Id))
            {
                context.RolePermissions.Add(new RolePermission
                {
                    Id = Guid.NewGuid(),
                    RoleId = adminCustomRole.Id,
                    PermissionId = manageSystemPermission.Id
                });
                await context.SaveChangesAsync();
            }

            // -------------------------
            // 6️⃣ Assign Admin user to custom UserRole
            // -------------------------
            if (!context.UserRoles.Any(ur => ur.UserId == adminUser.Id && ur.RoleId == adminCustomRole.Id))
            {
                context.UserRoles.Add(new UserRole
                {
                    Id = Guid.NewGuid(),
                    UserId = adminUser.Id,
                    RoleId = adminCustomRole.Id
                });
                await context.SaveChangesAsync();
            }

        }
    }
}