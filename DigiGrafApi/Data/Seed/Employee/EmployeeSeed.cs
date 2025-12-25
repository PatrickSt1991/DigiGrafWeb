namespace DigiGrafWeb.Data.Seed.Employee
{
    public static class EmployeeSeed
    {
        public static async Task SeedAsync(AppDbContext db)
        {
            if (db.Employees.Any())
                return;

            db.Employees.Add(new Models.Employee
            {
                Id = Guid.NewGuid(),
                FirstName = "Patrick",
                LastName = "Stel",
                Initials = "P.S.",
                Email = "patrick@digigraf.local",
                IsActive = true
            });

            await db.SaveChangesAsync();
        }
    }
}
