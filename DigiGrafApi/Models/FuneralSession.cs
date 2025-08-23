namespace DigiGrafWeb.Models
{
    public class FuneralSession
    {
        public string? FuneralNumber { get; set; }
        public string? FuneralLeader { get; set; }
        public bool DossierCompleted { get; set; }
        public bool NewDossierCreation { get; set; }
        public bool Voorregeling { get; set; }
        public string? FuneralType { get; set; }
    }

}
