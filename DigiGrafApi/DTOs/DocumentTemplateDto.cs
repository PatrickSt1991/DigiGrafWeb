using Newtonsoft.Json;

namespace DigiGrafWeb.DTOs
{
    public class DocumentSectionDto
    {
        public Guid Id { get; set; } = Guid.NewGuid(); // Include Id for tracking sections
        [JsonProperty("type")]
        public string Type { get; set; } = "text";
        [JsonProperty("label")]
        public string Label { get; set; } = string.Empty;
        [JsonProperty("value")]
        public string Value { get; set; } = string.Empty;
    }

    public class DocumentTemplateDto
    {
        public Guid Id { get; set; }
        public Guid? OverledeneId { get; set; }
        public string Title { get; set; } = null!;
        [JsonProperty("sections")]
        public List<DocumentSectionDto> Sections { get; set; } = new();
        public bool IsDefault { get; set; } = false; // Optional: include to indicate default templates in FE
    }
}
