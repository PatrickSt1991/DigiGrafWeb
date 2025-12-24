namespace DigiGrafWeb.Models
{
    public class InsurancePolicy
    {
        public Guid Id { get; set; }

        public Guid OverledeneId { get; set; }

        public Guid InsurancePartyId { get; set; }

        public string PolicyNumber { get; set; } = string.Empty;
        public decimal? Premium { get; set; }

        public Deceased Overledene { get; set; } = null!;
        public InsuranceParty InsuranceParty { get; set; } = null!;
    }
}
