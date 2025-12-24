namespace DigiGrafWeb.Models
{
    public class Employee
    {
        public Guid Id { get; set; }
        public bool IsActive { get; set; }
        public string Initials { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Tussenvoegsel { get; set; }
        public string FullName => $"{FirstName} {Tussenvoegsel} {LastName}".Replace("  ", " ").Trim();
        public string? BirthPlace { get; set; }
        public DateOnly BirthDate { get; set; }
        public string Email { get; set; } = null!;
        public string? Mobile { get; set; }
        public DateOnly StartDate { get; set; }

        public Guid? UserId { get; set; }
        public ApplicationUser? User { get; set; }
    }
}
