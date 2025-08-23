namespace DigiGrafWeb.DTOs
{
    public class NewDossierRequest
    {
        public int Id { get; set; }
        public Guid FuneralGuid { get; set; }
        public string FuneralCode { get; set; } = string.Empty;
        public string FuneralLeader { get; set; } = string.Empty;
        public bool Voorregeling { get; set; }
        public string FuneralType { get; set; } = string.Empty;
        public bool NewDossierCreation { get; set; }
        public bool DossierCompleted { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
