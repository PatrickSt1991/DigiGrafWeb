using DigiGrafWeb.Data.Seed.Core;
using DigiGrafWeb.Data.Seed.Documents;
using DigiGrafWeb.Data.Seed.Employee;
using DigiGrafWeb.Data.Seed.Example;
using DigiGrafWeb.Data.Seed.Insurance;
using DigiGrafWeb.Data.Seed.Reference;
using DigiGrafWeb.Data.Seed.Supplier;
using DigiGrafWeb.Models;
using Microsoft.AspNetCore.Identity;

namespace DigiGrafWeb.Data.Seed
{
    public static class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            await RoleSeed.SeedAsync(scope.ServiceProvider);
            await UserSeed.SeedAsync(scope.ServiceProvider);
            await EmployeeSeed.SeedAsync(db, userManager);

            await SalutationSeed.SeedAsync(db);
            await BodyFindingSeed.SeedAsync(db);
            await OriginsSeed.SeedAsync(db);
            await MaritalStatusSeed.SeedAsync(db);
            await CoffinSeed.SeedAsync(db);
            await CoffinLengthSeed.SeedAsync(db);

            await SupplierSeed.SeedAsync(db);
            await InsurancePartySeed.SeedAsync(db);
            await InsurancePriceComponentSeed.SeedAsync(db);

            await DocumentTemplateSeed.SeedAsync(db);

            await DossierSeed.SeedAsync(db);
            await InvoiceSeed.SeedAsync(db);
        }
    }
}
