namespace DigiGrafWeb.DTOs
{
    public class SupplierDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public SupplierTypeDto Type { get; set; } = null!;

        public string? Description { get; set; }

        public PostalAddressDto? Address { get; set; }

        public PostboxDto? Postbox { get; set; }

        public bool IsActive { get; set; }
    }
    public class SupplierTypeDto
    {
        public string Code { get; set; } = null!;
        public string Label { get; set; } = null!;
    }
}
