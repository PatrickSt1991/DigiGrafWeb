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
    public class RouwbrievenController : ControllerBase
    {
        private readonly AppDbContext _db;

        public RouwbrievenController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetRouwbrieven()
        {
            var items = await _db.Rouwbrieven
                .Where(r => r.IsActive)
                .OrderBy(r => r.Code)
                .ToListAsync();

            return Ok(GeneralMapper<Rouwbrief>.ToDtoList(items));
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllRouwbrieven()
        {
            var items = await _db.Rouwbrieven
                .OrderBy(r => r.Code)
                .ToListAsync();

            return Ok(GeneralMapper<Rouwbrief>.ToDtoList(items));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetRouwbrief(Guid id)
        {
            var item = await _db.Rouwbrieven.FindAsync(id);
            if (item == null)
                return NotFound();

            return Ok(GeneralMapper<Rouwbrief>.ToDto(item));
        }

        [HttpPost("createRouwbrief")]
        public async Task<IActionResult> CreateRouwbrief([FromBody] GeneralDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Code))
                return BadRequest("Name is required.");

            var entity = GeneralMapper<Rouwbrief>.ToEntity(dto);

            _db.Rouwbrieven.Add(entity);
            await _db.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetRouwbrief),
                new { id = entity.Id },
                GeneralMapper<Rouwbrief>.ToDto(entity)
            );
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateRouwbrief(Guid id, [FromBody] GeneralDto dto)
        {
            if (dto.Id == null || dto.Id == Guid.Empty)
                return BadRequest("Id is required.");

            if (id != dto.Id.Value)
                return BadRequest("ID mismatch.");

            if (string.IsNullOrWhiteSpace(dto.Code))
                return BadRequest("Name is required.");

            var entity = await _db.Rouwbrieven.FindAsync(id);
            if (entity == null)
                return NotFound();

            entity.Code = dto.Code;
            entity.IsActive = dto.IsActive;

            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteRouwbrief(Guid id)
        {
            var entity = await _db.Rouwbrieven.FindAsync(id);
            if (entity == null)
                return NotFound();

            entity.IsActive = false;
            await _db.SaveChangesAsync();

            return NoContent();
        }
    }
}