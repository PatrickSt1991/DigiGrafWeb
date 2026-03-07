using System.ComponentModel.DataAnnotations.Schema;

namespace DigiGrafWeb.Models
{
    public class InsurancePriceComponentInsuranceParty
    {
        public Guid InsurancePriceComponentId { get; set; }
        public InsurancePriceComponent InsurancePriceComponent { get; set; } = null!;

        public Guid InsurancePartyId { get; set; }
        public InsuranceParty InsuranceParty { get; set; } = null!;
    }
}