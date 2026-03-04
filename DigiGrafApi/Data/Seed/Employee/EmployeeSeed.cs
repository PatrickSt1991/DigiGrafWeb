using DigiGrafWeb.Models;
using Microsoft.AspNetCore.Identity;

namespace DigiGrafWeb.Data.Seed.Employee
{
    public static class EmployeeSeed
    {
        public static async Task SeedAsync(AppDbContext db, UserManager<ApplicationUser> userManager)
        {
            if (db.Employees.Any())
                return;

            var user = await userManager.FindByEmailAsync("admin@digigraf.local");

            db.Employees.Add(new Models.Employee
            {
                Id = Guid.NewGuid(),
                IsActive = true,
                Initials = "A.",
                FirstName = "Admin",
                LastName = "DigiGraf",  
                BirthDate = DateOnly.FromDateTime(DateTime.Now),
                Email = "admin@digigraf.local",
                StartDate = DateOnly.FromDateTime(DateTime.Now),
                UserId = user?.Id
            });

            await db.SaveChangesAsync();
        }
    }
}
