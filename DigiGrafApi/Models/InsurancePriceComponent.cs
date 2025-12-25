using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigiGrafWeb.Models
{
    public class InsurancePriceComponent
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid InsurancePartyId { get; set; }

        [ForeignKey(nameof(InsurancePartyId))]
        public InsuranceParty InsuranceParty { get; set; } = null!;

        [Required]
        public string Omschrijving { get; set; } = "";

        public int Aantal { get; set; } = 1;
        [Precision(18, 2)]
        public decimal Bedrag { get; set; }
        public bool IsActive { get; set; } = true;
        public int SortOrder { get; set; } = 0;
    }
}
