using DigiGrafWeb.Data;
using DigiGrafWeb.DTOs;
using DigiGrafWeb.Mappers;
using DigiGrafWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DigiGrafWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentTemplatesController : ControllerBase
    {
        private readonly AppDbContext context;

        public DocumentTemplatesController(AppDbContext context)
        {
            this.context = context;
        }

        // GET: api/documenttemplates/defaults
        [HttpGet("defaults")]
        public async Task<ActionResult> GetDefaults()
        {
            var defaults = await context.DocumentTemplates
                .Where(d => d.IsDefault)
                .Include(d => d.Sections)
                .ToListAsync();

            // Ensure Sections is never null
            foreach (var def in defaults)
                def.Sections ??= new List<DocumentSection>();

            var dtos = defaults.Select(DocumentTemplateMapper.ToDto).ToList();
            return Ok(new { templates = dtos });
        }

        // GET: api/documenttemplates/{overledeneId}
        [HttpGet("{overledeneId:guid}")]
        public async Task<ActionResult> Get(Guid overledeneId)
        {
            var templates = await context.DocumentTemplates
                .Where(d => d.OverledeneId == overledeneId)
                .Include(d => d.Sections)
                .ToListAsync();

            if (!templates.Any())
            {
                var defaults = await context.DocumentTemplates
                    .Where(d => d.IsDefault)
                    .Include(d => d.Sections)
                    .ToListAsync();

                templates = defaults.Select(def => new DocumentTemplate
                {
                    Id = Guid.NewGuid(),
                    OverledeneId = overledeneId,
                    Title = def.Title,
                    Sections = def.Sections?.Select(s => new DocumentSection
                    {
                        Type = s.Type,
                        Label = s.Label,
                        Value = s.Value
                    }).ToList() ?? new List<DocumentSection>(),
                    IsDefault = false
                }).ToList();

                context.DocumentTemplates.AddRange(templates);
                await context.SaveChangesAsync();
            }

            // Ensure Sections is never null
            foreach (var t in templates)
                t.Sections ??= new List<DocumentSection>();

            var dtos = templates.Select(DocumentTemplateMapper.ToDto).ToList();
            return Ok(new { templates = dtos });
        }

        // PUT: api/documenttemplates/{id}
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Put(Guid id, DocumentTemplateDto dto)
        {
            var template = await context.DocumentTemplates
                .Include(t => t.Sections)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (template == null) return NotFound();

            template.Title = dto.Title;

            // Update sections
            template.Sections.Clear();
            foreach (var sectionDto in dto.Sections)
            {
                template.Sections.Add(new DocumentSection
                {
                    Type = sectionDto.Type,
                    Label = sectionDto.Label,
                    Value = sectionDto.Value
                });
            }

            await context.SaveChangesAsync();
            return NoContent();
        }

        // POST: api/documenttemplates
        [HttpPost]
        public async Task<ActionResult> Post(DocumentTemplateDto dto)
        {
            var entity = new DocumentTemplate
            {
                Id = Guid.NewGuid(),
                OverledeneId = dto.OverledeneId,
                Title = dto.Title,
                Sections = dto.Sections?.Select(s => new DocumentSection
                {
                    Type = s.Type,
                    Label = s.Label,
                    Value = s.Value
                }).ToList() ?? new List<DocumentSection>()
            };

            context.DocumentTemplates.Add(entity);
            await context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { overledeneId = dto.OverledeneId },
                new { templates = new[] { DocumentTemplateMapper.ToDto(entity) } });
        }
    }
}
