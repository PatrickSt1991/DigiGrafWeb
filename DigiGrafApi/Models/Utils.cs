namespace DigiGrafWeb.Models
{
    public class Salutation
    {
        public Guid? Id { get; set; } = Guid.NewGuid();
        public string Code { get; set; } = null!;
        public string Label { get; set; } = null!;
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;
    }

    public class BodyFinding
    {
        public Guid? Id { get; set; } = Guid.NewGuid();
        public string Code { get; set; } = null!;
        public string Label { get; set; } = null!;
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;
    }

    public class Origins
    {
        public Guid? Id { get; set; } = Guid.NewGuid();
        public string Code { get; set; } = null!;
        public string Label { get; set; } = null!;
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;
    }
    public class MaritalStatus
    {
        public Guid? Id { get; set; } = Guid.NewGuid();
        public string Code { get; set; } = null!;
        public string Label { get; set; } = null!;
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
