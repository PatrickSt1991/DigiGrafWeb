using DigiGrafWeb.DTOs;
using DigiGrafWeb.Models;
using System.Linq;

namespace DigiGrafWeb.Mappers
{
    public static class InvoiceMapper
    {
        public static InvoiceDto ToDto(Invoice entity)
        {
            var priceComponents = entity.PriceComponents?.Select(pc => new PriceComponentDto
            {
                Id = pc.Id,
                Omschrijving = pc.Omschrijving,
                Aantal = pc.Aantal,
                Bedrag = pc.Bedrag
            }).ToList() ?? new List<PriceComponentDto>();

            return new InvoiceDto
            {
                Id = entity.Id,
                DeceasedId = entity.DeceasedId,
                SelectedVerzekeraar = entity.SelectedVerzekeraar,
                DiscountAmount = entity.DiscountAmount,
                Subtotal = entity.Subtotal,
                Total = entity.Total,
                PriceComponents = priceComponents
            };
        }

        public static Invoice ToEntity(InvoiceDto dto)
        {
            var priceComponents = dto.PriceComponents?.Select(pc => new PriceComponent
            {
                Id = pc.Id,
                Omschrijving = pc.Omschrijving,
                Aantal = pc.Aantal,
                Bedrag = pc.Bedrag
            }).ToList() ?? new List<PriceComponent>();

            return new Invoice
            {
                Id = dto.Id,
                DeceasedId = dto.DeceasedId,
                SelectedVerzekeraar = dto.SelectedVerzekeraar,
                DiscountAmount = dto.DiscountAmount,
                Subtotal = dto.Subtotal,
                Total = dto.Total,
                PriceComponents = priceComponents
            };
        }
    }
}
