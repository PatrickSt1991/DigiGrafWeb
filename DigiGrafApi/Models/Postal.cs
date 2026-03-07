namespace DigiGrafWeb.Models
{
    public class PostalAddress
    {
        public string Street { get; set; } = null!;
        public string HouseNumber { get; set; } = null!;
        public string? Suffix { get; set; }
        public string ZipCode { get; set; } = null!;
        public string City { get; set; } = null!;
    }
    public class Postbox
    {
        public string Address { get; set; } = null!;
        public string Zipcode { get; set; } = null!;
        public string City { get; set; } = null!;
    }
}
