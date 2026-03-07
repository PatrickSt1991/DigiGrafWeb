using System.ComponentModel.DataAnnotations;

namespace DigiGrafWeb.Models
{
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
}
