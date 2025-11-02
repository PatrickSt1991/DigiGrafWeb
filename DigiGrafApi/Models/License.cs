namespace DigiGrafWeb.Models
{
    public class License
    {
        public string Plan { get; set; } = "Free";
        public bool IsValid { get; set; } = true;
        public int MaxUsers { get; set; } = 10;
        public int CurrentUsers { get; set; } = 0;
        public bool CanAddUsers => CurrentUsers < MaxUsers;
        public DateTime? ExpiresAt { get; set; }
        public string[] Features { get; set; } = [];
        public string Message { get; set; } = "Free tier license active.";
    }
}
