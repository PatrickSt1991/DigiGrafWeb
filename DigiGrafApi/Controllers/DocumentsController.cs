using DigiGrafWeb.Data;
using DigiGrafWeb.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace DigiGrafWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentsController(AppDbContext context) : ControllerBase
    {
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<DocumentTemplateDto>> GetDocument(Guid overledeneId)
        {
            var doc = await context.DocumentTemplates
                .FirstOrDefaultAsync(d => d.OverledeneId == overledeneId);

            if (doc == null) return NotFound();

            var fields = JsonConvert.DeserializeObject<Dictionary<string, FieldDto>>(doc.FieldsJson);

            return new DocumentTemplateDto
            {
                Title = doc.Title,
                Fields = fields
            };
        }

        [HttpPut("{overledeneId}")]
        public async Task<IActionResult> UpdateDocument(Guid overledeneId, DocumentTemplateDto dto)
        {
            var doc = await context.DocumentTemplates
                .FirstOrDefaultAsync(d => d.OverledeneId == overledeneId);

            if (doc == null) return NotFound();

            doc.Title = dto.Title;
            doc.FieldsJson = JsonConvert.SerializeObject(dto.Fields);

            await context.SaveChangesAsync();

            return NoContent();
        }
    }
}
