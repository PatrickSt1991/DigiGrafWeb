using DigiGrafWeb.Data;
using DigiGrafWeb.DTOs;
using DigiGrafWeb.Mappers;
using DigiGrafWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DigiGrafWeb.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AsbestemmingController : ControllerBase
    {
        private readonly AppDbContext _db;

        public AsbestemmingController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsbestemmingen()
        {
            var items = await _db.Asbestemmingen
                .Where(a => a.IsActive)
                .OrderBy(a => a.Description)
                .ToListAsync();

            return Ok(GeneralMapper<Asbestemming>.ToDtoList(items));
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllAsbestemmingen()
        {
            var items = await _db.Asbestemmingen
                .OrderBy(a => a.Description)
                .ToListAsync();

            return Ok(GeneralMapper<Asbestemming>.ToDtoList(items));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetAsbestemming(Guid id)
        {
            var item = await _db.Asbestemmingen.FindAsync(id);
            if (item == null)
                return NotFound();

            return Ok(GeneralMapper<Asbestemming>.ToDto(item));
        }

        [HttpPost("createAsbestemming")]
        public async Task<IActionResult> CreateAsbestemming([FromBody] GeneralDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Description))
                return BadRequest("Description is required.");

            var entity = GeneralMapper<Asbestemming>.ToEntity(dto);
            _db.Asbestemmingen.Add(entity);
            await _db.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetAsbestemming),
                new { id = entity.Id },
                GeneralMapper<Asbestemming>.ToDto(entity)
            );
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateAsbestemming(Guid id, [FromBody] GeneralDto dto)
        {
            if (dto.Id == null || dto.Id == Guid.Empty)
                return BadRequest("Id is required.");

            if (id != dto.Id.Value)
                return BadRequest("ID mismatch.");

            if (string.IsNullOrWhiteSpace(dto.Description))
                return BadRequest("Description is required.");

            var entity = await _db.Asbestemmingen.FindAsync(id);
            if (entity == null)
                return NotFound();

            entity.Description = dto.Description;
            entity.IsActive = dto.IsActive;

            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteAsbestemming(Guid id)
        {
            var entity = await _db.Asbestemmingen.FindAsync(id);
            if (entity == null)
                return NotFound();

            entity.IsActive = false;
            await _db.SaveChangesAsync();

            return NoContent();
        }
    }
}