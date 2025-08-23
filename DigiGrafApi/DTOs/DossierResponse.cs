namespace DigiGrafWeb.DTOs
{
    public class DossierResponse
    {
        public string FuneralCode { get; set; } = string.Empty;
        public Guid FuneralGuid { get; set; }
        public string FuneralLeader { get; set; } = string.Empty;
        public bool Voorregeling { get; set; }
        public string FuneralType { get; set; } = string.Empty;
        public bool NewDossierCreation { get; set; }
        public bool DossierCompleted { get; set; }
    }
}
