namespace DigiGrafWeb.DTOs
{
    public class PostalAddressDto
    {
        public string Street { get; set; } = null!;
        public string HouseNumber { get; set; } = null!;
        public string? Suffix { get; set; }
        public string ZipCode { get; set; } = null!;
        public string City { get; set; } = null!;
    }
    public class PostboxDto
    {
        public string Address { get; set; } = null!;
        public string ZipCode { get; set; } = null!;
        public string City { get; set; } = null!;
    }
}
