namespace DigiGrafWeb.DTOs
{
    public class EmployeeDto
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

        public string Role { get; set; } = null!;
        public DateOnly StartDate { get; set; }
    }
    public class EmployeeOverviewDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Role { get; set; }
        public bool IsActive { get; set; }
        public bool HasLogin { get; set; }
        public bool? LoginIsActive { get; set; }
        public string? LoginEmail { get; set; }
    }
    public class AdminEmployeeDto
    {
        public Guid Id { get; set; }

        public string Status { get; set; } = null!;

        public string Initials { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Tussenvoegsel { get; set; }

        public string FullName { get; set; } = null!;

        public string? BirthPlace { get; set; }
        public DateOnly BirthDate { get; set; }

        public string Email { get; set; } = null!;
        public string? Mobile { get; set; }

        public string Role { get; set; } = null!;
        public DateOnly StartDate { get; set; }

        public bool HasLogin { get; set; }
        public bool? LoginIsActive { get; set; }
    }

}
