using System.ComponentModel.DataAnnotations;

namespace DigiGrafWeb.Models
{
    public class Dossier
    {
        [Key]
        public Guid Id { get; set; }

        // Funeral info
        public string FuneralLeader { get; set; } = "";
        public string FuneralNumber { get; set; } = "";
        public string FuneralType { get; set; } = "";
        public bool Voorregeling { get; set; }

        public bool DossierCompleted { get; set; } = false;

        // Navigation properties
        public Deceased? Deceased { get; set; }
        public DeathInfo? DeathInfo { get; set; }
    }
}
