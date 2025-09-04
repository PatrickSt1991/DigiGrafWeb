namespace DigiGrafWeb.DTOs
{
    public class InsuranceCompanyDto
    {
        public Guid? Id { get; set; }
        public string Label { get; set; } = "";
        public string Value { get; set; } = "";
        public bool IsActive { get; set; } = true;
    }

    public class InsurancePolicyDto
    {
        public Guid Id { get; set; }
        public Guid OverledeneId { get; set; }
        public Guid InsuranceCompanyId { get; set; }
        public string PolicyNumber { get; set; } = "";
        public decimal? Premium { get; set; }
    }

}
