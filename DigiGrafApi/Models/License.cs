using System;
using System.ComponentModel.DataAnnotations;


namespace DigiGrafWeb.Models
{
    public class License
    {
        [Key]
        public Guid Id { get; set; }


        public string Plan { get; set; } = "Free";
        public bool IsValid { get; set; }
        public int CurrentUsers { get; set; }
        public int MaxUsers { get; set; }
        public bool CanAddUsers { get; set; }
        public DateTime? ExpiresAt { get; set; }
        public string Features { get; set; } = string.Empty; // stored as comma-separated string
        public string? Message { get; set; }
        public string LicenseKey { get; set; } = string.Empty;
    }
}