namespace DigiGrafWeb.DTOs
{
    public class SalutationDto
    {
        public Guid? Id { get; set; } = Guid.NewGuid();
        public string Code { get; set; } = null!;
        public string Label { get; set; } = null!;
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;
    }
    public class BodyFindingDto
    {
        public Guid? Id { get; set; } = Guid.NewGuid();
        public string Code { get; set; } = null!;
        public string Label { get; set; } = null!;
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;
    }
    public class OriginsDto
    {
        public Guid? Id { get; set; } = Guid.NewGuid();
        public string Code { get; set; } = null!;
        public string Label { get; set; } = null!;
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;
    }
    public class MaritalStatusDto
    {
        public Guid? Id { get; set; } = Guid.NewGuid();
        public string Code { get; set; } = null!;
        public string Label { get; set; } = null!;
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;
    }
    public class CoffinsDto
    {
        public Guid? Id { get; set; } = Guid.NewGuid();
        public string Code { get; set; } = null!;
        public string Label { get; set; } = null!;
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;
    }
    public class CoffinLengthsDto
    {
        public Guid? Id { get; set; } = Guid.NewGuid();
        public string Code { get; set; } = null!;
        public string Label { get; set; } = null!;
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;
    }
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
