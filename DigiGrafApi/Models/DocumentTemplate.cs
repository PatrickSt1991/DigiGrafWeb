using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigiGrafWeb.Models
{
    public class DocumentTemplate
    {
        [Key]
        public Guid Id { get; set; }

        public Guid? OverledeneId { get; set; }

        [Required]
        public string Title { get; set; } = null!;

        [Required]
        public List<DocumentSection> Sections { get; set; } = new();

        public bool IsDefault { get; set; } = false;
    }

    public class DocumentSection
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        // Foreign key
        public Guid DocumentTemplateId { get; set; }

        [Required]
        public string Type { get; set; } = "text";

        [Required]
        public string Label { get; set; } = string.Empty;

        public string Value { get; set; } = string.Empty;

        // Navigation property (optional)
        [ForeignKey("DocumentTemplateId")]
        public DocumentTemplate? Template { get; set; }
    }
}
