namespace DigiGrafWeb.DTOs
{
    public class FieldDto
    {
        public string Type { get; set; } = null!;//"text", "textarea", "checkbox" etc..
        public string Label { get; set; } = null!;
        public object Value { get; set; } = null!;
    }
    public class DocumentTemplateDto
    {
        public string Title { get; set; } = null!;
        public Dictionary<string, FieldDto> Fields { get; set; } = null!;
        public string? FuneralLeader { get; set; }
        public string? FuneralNumber { get; set; }
    }
}
