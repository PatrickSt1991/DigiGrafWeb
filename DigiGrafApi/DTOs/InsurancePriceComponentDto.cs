namespace DigiGrafWeb.DTOs
{
    public class InsurancePriceComponentDto
    {
        public Guid? Id { get; set; } = Guid.NewGuid();
        public string Omschrijving { get; set; } = "";
        public decimal Bedrag { get; set; }
        public decimal FactuurBedrag { get; set; }
        public int VerzekerdAantal { get; set; } = 1;
        public List<Guid> InsurancePartyIds { get; set; } = new();
        public bool StandaardPM { get; set; } = false;
        public int SortOrder { get; set; } = 0;
        public bool IsActive { get; set; } = true;
    }
}