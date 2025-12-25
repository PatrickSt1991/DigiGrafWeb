using DigiGrafWeb.Models;

namespace DigiGrafWeb.Data.Seed.Supplier
{
    public static class SupplierSeed
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            if (context.Suppliers.Any())
                return;

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
                        Suffix = null,
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
                        Suffix = null,
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
                        Suffix = null,
                        ZipCode = "7202CP",
                        City = "Zutphen"
                    }
                },
                new Suppliers
                {
                    Id = Guid.NewGuid(),
                    Name = "Algemene Uitvaart Services",
                    Description = "Algemene leverancier (overig)",
                    Type = SupplierType.Overig,
                    IsActive = true,
                    Address = new PostalAddress
                    {
                        Street = "Hoofdstraat",
                        HouseNumber = "1",
                        Suffix = "A",
                        ZipCode = "9671AB",
                        City = "Winschoten"
                    },
                    Postbox = new Postbox
                    {
                        Address = "Postbus 50",
                        Zipcode = "9670AA",
                        City = "Winschoten"
                    }
                }
            );

            await context.SaveChangesAsync();
        }
    }
}
