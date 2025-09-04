namespace DigiGrafWeb.DTOs
{
    public class InvoiceDto
    {
        public Guid Id { get; set; }
        public Guid DeceasedId { get; set; }
        public string SelectedVerzekeraar { get; set; } = "";
        public decimal DiscountAmount { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Total { get; set; }
        public List<PriceComponentDto> PriceComponents { get; set; } = new();
    }

    public class PriceComponentDto
    {
        public Guid Id { get; set; }
        public string Omschrijving { get; set; } = "";
        public int Aantal { get; set; }
        public decimal Bedrag { get; set; }
    }

}
