using System.ComponentModel.DataAnnotations;

namespace DigiGrafWeb.Models
{
    public class Salutation
    {
        [Key]
        public Guid? Id { get; set; } = Guid.NewGuid();
        public string Code { get; set; } = null!;
        public string Label { get; set; } = null!;
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;
    }

    public class BodyFinding
    {
        [Key]
        public Guid? Id { get; set; } = Guid.NewGuid();
        public string Code { get; set; } = null!;
        public string Label { get; set; } = null!;
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;
    }

    public class Origins
    {
        [Key]
        public Guid? Id { get; set; } = Guid.NewGuid();
        public string Code { get; set; } = null!;
        public string Label { get; set; } = null!;
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;
    }
    public class MaritalStatus
    {
        [Key]
        public Guid? Id { get; set; } = Guid.NewGuid();
        public string Code { get; set; } = null!;
        public string Label { get; set; } = null!;
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;
    }
    public class Coffins
    {
        [Key]
        public Guid? Id { get; set; } = Guid.NewGuid();
        public string Code { get; set; } = null!;
        public string Label { get; set; } = null!;
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;
    }
    public class CoffinLengths
    {
        [Key]
        public Guid? Id { get; set; } = Guid.NewGuid();
        public string Code { get; set; } = null!;
        public string Label { get; set; } = null!;
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;
    }
    public class Caretaker
    {
        [Key]
        public Guid? Id { get; set; } = Guid.NewGuid();
        public string DisplayName { get; set; } = null!;
        public string Email { get; set; } = null!;
    }
    public class Suppliers
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public SupplierType Type { get; set; }
        public PostalAddress? Address { get; set; }
        public Postbox? Postbox { get; set; }
        public bool IsActive { get; set; } = true;
    }
    public enum SupplierType
    {
        [Display(Name = "Kisten")]
        Kisten,

        [Display(Name = "Urn & Gedenksieraden")]
        UrnAndGedenksieraden,

        [Display(Name = "Bloemen")]
        Bloemen,

        [Display(Name = "Steenhouwer")]
        Steenhouwer,

        [Display(Name = "Overig")]
        Overig
    }
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
