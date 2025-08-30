namespace DigiGrafWeb.DTOs
{
    public class FuneralLeaderDto
    {
        public Guid Id { get; set; } = Guid.Empty;
        public string Value { get; set; } = string.Empty;
        public string Label { get; set; } = string.Empty;
    }
    public class CaretakerDto
    {
        public Guid? Id { get; set; } = Guid.Empty;
        public string DisplayName { get; set; } = null!;
        public string Email { get; set; } = null!;
    }
}
