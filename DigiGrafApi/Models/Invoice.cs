using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigiGrafWeb.Models
{
    public class Invoice
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid DeceasedId { get; set; }
        public Deceased Deceased { get; set; }

        [Required]
        public string SelectedVerzekeraar { get; set; }
        public Guid SelectedVerzekeraarId { get; set; }

        public decimal DiscountAmount { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Total { get; set; }

        public ICollection<PriceComponent> PriceComponents { get; set; }
    }

    public class PriceComponent
    {
        [Key]
        public Guid Id { get; set; }
        
        [Required]
        public string Omschrijving { get; set; }
        public int Aantal { get; set; }
        public decimal Bedrag { get; set; }

        [ForeignKey("Invoice")]
        public Guid InvoiceId { get; set; }
        public Invoice Invoice { get; set; }
    }
}
