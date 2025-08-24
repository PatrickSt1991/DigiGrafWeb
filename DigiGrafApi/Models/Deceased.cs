using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigiGrafWeb.Models
{
    public class Deceased
    {
        [Key]
        public Guid Id { get; set; }

        public string SocialSecurity { get; set; } = "";
        public string Salutation { get; set; } = "";
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public DateTime? Dob { get; set; }
        public string PlaceOfBirth { get; set; } = "";
        public int? Age { get; set; }
        public string PostalCode { get; set; } = "";
        public string HouseNumber { get; set; } = "";
        public string HouseNumberAddition { get; set; } = "";
        public string Street { get; set; } = "";
        public string City { get; set; } = "";
        public string County { get; set; } = "";
        public bool HomeDeceased { get; set; }
        public string Gp { get; set; } = "";
        public string GpPhone { get; set; } = "";
        public string Me { get; set; } = "";

        // Foreign key
        public Guid DossierId { get; set; }
        [ForeignKey("DossierId")]
        public Dossier? Dossier { get; set; } = null;
    }
}
