using System;

namespace DigiGrafWeb.DTOs
{
    public class DossierDto
    {
        public Guid? Id { get; set; }

        // Funeral info
        public string FuneralLeader { get; set; } = "";
        public string FuneralNumber { get; set; } = "";
        public string FuneralType { get; set; } = "";
        public bool Voorregeling { get; set; }
        public bool DossierCompleted { get; set; }

        // Nested DTOs
        public DeceasedDto? Deceased { get; set; }
        public DeathInfoDto? DeathInfo { get; set; }
    }
}
