using DigiGrafWeb.Models;

namespace DigiGrafWeb.Data.Seed.Documents
{
    public static class DocumentTemplateSeed
    {
        public static async Task SeedAsync(AppDbContext db)
        {
            if (db.DocumentTemplates.Any())
                return;

            var koffieId = Guid.NewGuid();
            var standaardId = Guid.NewGuid();

            var templates = new List<DocumentTemplate>
            {
                new DocumentTemplate
                {
                    Id = koffieId,
                    Title = "Koffie Document",
                    IsDefault = true,
                    Sections = new()
                    {
                        new DocumentSection
                        {
                            Id = Guid.Parse("314AE355-82C9-4933-9822-11B88C7E2D0A"),
                            Type = "textarea",
                            Label = "Body",
                            Value = @"<div style=""font-family: Arial, sans-serif; padding: 20px;"">...</div>"
                        },
                        new DocumentSection
                        {
                            Id = Guid.Parse("97604C6C-BF86-49BC-84AE-64DD91518CB4"),
                            Type = "text",
                            Label = "Footer",
                            Value = "Dit document is automatisch gegenereerd"
                        }
                    }
                },
                new DocumentTemplate
                {
                    Id = standaardId,
                    Title = "Standaard Document",
                    IsDefault = true,
                    Sections = new()
                    {
                        new DocumentSection
                        {
                            Id = Guid.Parse("40CA01C6-EF3E-4CF9-BFE8-67A59632A53B"),
                            Type = "text",
                            Label = "Header",
                            Value = @"<div style=""text-align:center"">Logo</div>"
                        },
                        new DocumentSection
                        {
                            Id = Guid.Parse("B5BEF6E8-71CE-4CD4-8CB0-97E891127234"),
                            Type = "textarea",
                            Label = "Body",
                            Value = "Hier komt de hoofdtekst van het document..."
                        }
                    }
                }
            };

            db.DocumentTemplates.AddRange(templates);
            await db.SaveChangesAsync();
        }
    }
}
