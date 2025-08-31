using DigiGrafWeb.DTOs;
using DigiGrafWeb.Models;

namespace DigiGrafWeb.Mappers
{
    public static class DocumentTemplateMapper
    {
        public static DocumentTemplateDto ToDto(DocumentTemplate entity)
        {
            var sections = entity.Sections?.Select(s => new DocumentSectionDto
            {
                Id = s.Id, // preserve section Id
                Type = s.Type,
                Label = s.Label,
                Value = s.Value
            }).ToList() ?? new List<DocumentSectionDto>();

            return new DocumentTemplateDto
            {
                Id = entity.Id,
                OverledeneId = entity.OverledeneId,
                Title = entity.Title,
                Sections = sections,
                IsDefault = entity.IsDefault
            };
        }

        public static DocumentTemplate ToEntity(DocumentTemplateDto dto)
        {
            var sections = dto.Sections?.Select(s => new DocumentSection
            {
                Id = s.Id, // preserve section Id
                Type = s.Type,
                Label = s.Label,
                Value = s.Value
            }).ToList() ?? new List<DocumentSection>();

            return new DocumentTemplate
            {
                Id = dto.Id,
                OverledeneId = dto.OverledeneId,
                Title = dto.Title,
                Sections = sections,
                IsDefault = dto.IsDefault
            };
        }
    }
}
