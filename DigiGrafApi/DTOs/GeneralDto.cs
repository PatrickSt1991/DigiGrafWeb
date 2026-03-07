namespace DigiGrafWeb.DTOs
{
    public class GeneralDto
    {
        public Guid? Id { get; set; } = Guid.NewGuid();
        public string Code { get; set; } = null!;
        public string Label { get; set; } = null!;
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
