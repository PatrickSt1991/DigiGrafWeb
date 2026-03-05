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
    public class CoffinsController : ControllerBase
    {
        private readonly AppDbContext _db;

        public CoffinsController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetCoffins()
        {
            var coffins = await _db.Coffins
                .Where(c => c.IsActive)
                .OrderBy(c => c.Code)
                .ToListAsync();

            return Ok(CoffinsMapper.ToDtoList(coffins));
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllCoffins()
        {
            var coffins = await _db.Coffins
                .OrderBy(c => c.Code)
                .ToListAsync();

            return Ok(CoffinsMapper.ToDtoList(coffins));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetCoffin(Guid id)
        {
            var coffin = await _db.Coffins.FindAsync(id);
            if (coffin == null)
                return NotFound();

            return Ok(CoffinsMapper.ToDto(coffin));
        }

        [HttpPost("createCoffin")]
        public async Task<IActionResult> CreateCoffin([FromBody] CoffinsDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Code))
                return BadRequest("Code is required.");

            if (string.IsNullOrWhiteSpace(dto.Label))
                return BadRequest("Label is required.");

            var entity = CoffinsMapper.ToEntity(dto);

            _db.Coffins.Add(entity);
            await _db.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetCoffin),
                new { id = entity.Id },
                CoffinsMapper.ToDto(entity)
            );
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateCoffin(Guid id, [FromBody] CoffinsDto dto)
        {
            if (dto.Id == null || dto.Id == Guid.Empty)
                return BadRequest("Id is required.");

            if (id != dto.Id.Value)
                return BadRequest("ID mismatch.");

            if (string.IsNullOrWhiteSpace(dto.Code))
                return BadRequest("Code is required.");

            if (string.IsNullOrWhiteSpace(dto.Label))
                return BadRequest("Label is required.");

            var entity = await _db.Coffins.FindAsync(id);
            if (entity == null)
                return NotFound();

            entity.Code = dto.Code;
            entity.Label = dto.Label;
            entity.Description = dto.Description;
            entity.IsActive = dto.IsActive;

            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteCoffin(Guid id)
        {
            var entity = await _db.Coffins.FindAsync(id);
            if (entity == null)
                return NotFound();

            entity.IsActive = false;
            await _db.SaveChangesAsync();

            return NoContent();
        }
    }
}