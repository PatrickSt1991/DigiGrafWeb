using DigiGrafWeb.Data;
using DigiGrafWeb.DTOs;
using DigiGrafWeb.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DigiGrafWeb.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InsurancePriceComponentsController : ControllerBase
    {
        private readonly AppDbContext _db;

        public InsurancePriceComponentsController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetInsurancePriceComponents()
        {
            var items = await _db.InsurancePriceComponents
                .Where(x => x.IsActive)
                .Include(x => x.InsuranceParties)
                .OrderBy(x => x.SortOrder)
                .ThenBy(x => x.Omschrijving)
                .ToListAsync();

            return Ok(InsurancePriceComponentMapper.ToDtoList(items));
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllInsurancePriceComponents()
        {
            var items = await _db.InsurancePriceComponents
                .Include(x => x.InsuranceParties)
                .OrderBy(x => x.SortOrder)
                .ThenBy(x => x.Omschrijving)
                .ToListAsync();

            return Ok(InsurancePriceComponentMapper.ToDtoList(items));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetInsurancePriceComponent(Guid id)
        {
            var item = await _db.InsurancePriceComponents
                .Include(x => x.InsuranceParties)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (item == null)
                return NotFound();

            return Ok(InsurancePriceComponentMapper.ToDto(item));
        }

        [HttpPost("createInsurancePriceComponent")]
        public async Task<IActionResult> CreateInsurancePriceComponent([FromBody] InsurancePriceComponentDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Omschrijving))
                return BadRequest("Omschrijving is required.");

            if (dto.InsurancePartyIds == null || dto.InsurancePartyIds.Count == 0)
                return BadRequest("Selecteer minimaal 1 verzekering.");

            var entity = InsurancePriceComponentMapper.ToEntity(dto);

            _db.InsurancePriceComponents.Add(entity);
            await _db.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetInsurancePriceComponent),
                new { id = entity.Id },
                InsurancePriceComponentMapper.ToDto(entity)
            );
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateInsurancePriceComponent(Guid id, [FromBody] InsurancePriceComponentDto dto)
        {
            if (dto.Id == null || dto.Id == Guid.Empty)
                return BadRequest("Id is required.");

            if (id != dto.Id.Value)
                return BadRequest("ID mismatch.");

            if (string.IsNullOrWhiteSpace(dto.Omschrijving))
                return BadRequest("Omschrijving is required.");

            if (dto.InsurancePartyIds == null || dto.InsurancePartyIds.Count == 0)
                return BadRequest("Selecteer minimaal 1 verzekering.");

            var entity = await _db.InsurancePriceComponents
                .Include(x => x.InsuranceParties)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null)
                return NotFound();

            InsurancePriceComponentMapper.ApplyToEntity(entity, dto);

            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteInsurancePriceComponent(Guid id)
        {
            var entity = await _db.InsurancePriceComponents.FindAsync(id);
            if (entity == null)
                return NotFound();

            entity.IsActive = false;
            await _db.SaveChangesAsync();

            return NoContent();
        }
    }
}