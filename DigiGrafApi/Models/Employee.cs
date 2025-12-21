namespace DigiGrafWeb.Models
{
    public class Employee
    {
        public Guid Id { get; set; }
        public bool IsActive { get; set; }
        public string Initials { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Tussenvoegsel { get; set; }
        public string FullName => $"{FirstName} {Tussenvoegsel} {LastName}".Replace("  ", " ").Trim();
        public string BirthPlace { get; set; }
        public DateOnly BirthDate { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Role { get; set; }
        public DateOnly StartDate { get; set; }
    }
}
