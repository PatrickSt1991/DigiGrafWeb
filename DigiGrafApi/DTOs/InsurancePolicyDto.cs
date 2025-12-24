namespace DigiGrafWeb.DTOs
{
    public class InsurancePolicyDto
    {
        public Guid? Id { get; set; }

        public Guid OverledeneId { get; set; }
        public Guid InsurancePartyId { get; set; }

        public string PolicyNumber { get; set; } = "";
        public decimal? Premium { get; set; }
    }
}