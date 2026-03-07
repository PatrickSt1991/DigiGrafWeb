using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DigiGrafWeb.Models
{
    public class InsurancePriceComponent
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public string Omschrijving { get; set; } = "";
        [Precision(18, 2)]
        public decimal Bedrag { get; set; }
        [Precision(18, 2)]
        public decimal FactuurBedrag { get; set; }
        public int VerzekerdAantal { get; set; } = 1;
        public bool StandaardPM { get; set; } = false;
        public int SortOrder { get; set; } = 0;
        public bool IsActive { get; set; } = true;
        public ICollection<InsurancePriceComponentInsuranceParty> InsuranceParties { get; set; }
            = new List<InsurancePriceComponentInsuranceParty>();
    }
}