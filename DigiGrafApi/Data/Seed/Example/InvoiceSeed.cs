using DigiGrafWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace DigiGrafWeb.Data.Seed.Example
{
    public static class InvoiceSeed
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            if (context.Invoices.Any())
                return;

            var deceased = await context.Deceased.FirstOrDefaultAsync();
            if (deceased == null)
                return;

            var insurer = await context.InsuranceParties.FirstOrDefaultAsync();
            if (insurer == null)
                return;

            var invoiceId = Guid.NewGuid();

            var invoice = new Invoice
            {
                Id = invoiceId,
                DeceasedId = deceased.Id,
                SelectedVerzekeraar = insurer.Name,
                SelectedVerzekeraarId = insurer.Id,
                DiscountAmount = 250,
                Subtotal = 3750,
                Total = 3500,
                PriceComponents = new List<PriceComponent>
                {
                    new PriceComponent
                    {
                        Id = Guid.NewGuid(),
                        InvoiceId = invoiceId,
                        Omschrijving = "Uitvaartdienst",
                        Aantal = 1,
                        Bedrag = 2500
                    },
                    new PriceComponent
                    {
                        Id = Guid.NewGuid(),
                        InvoiceId = invoiceId,
                        Omschrijving = "Kist",
                        Aantal = 1,
                        Bedrag = 900
                    },
                    new PriceComponent
                    {
                        Id = Guid.NewGuid(),
                        InvoiceId = invoiceId,
                        Omschrijving = "Vervoer",
                        Aantal = 1,
                        Bedrag = 350
                    }
                }
            };

            context.Invoices.Add(invoice);
            await context.SaveChangesAsync();
        }
    }
}
