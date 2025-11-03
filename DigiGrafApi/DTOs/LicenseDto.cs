namespace DigiGrafWeb.DTOs
{
    public class LicenseDto
    {
        public string Plan { get; set; } = "Free";
        public bool IsValid { get; set; }
        public int CurrentUsers { get; set; }
        public int MaxUsers { get; set; }
        public bool CanAddUsers { get; set; }
        public DateTime? ExpiresAt { get; set; }
        public string[] Features { get; set; } = Array.Empty<string>();
        public string? Message { get; set; }
        public string LicenseKey { get; set; } = string.Empty;
    }
}