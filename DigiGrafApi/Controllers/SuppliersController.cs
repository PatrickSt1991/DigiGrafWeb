using DigiGrafWeb.Data;
using DigiGrafWeb.DTOs;
using DigiGrafWeb.Mappers;
using DigiGrafWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Extensions;

namespace DigiGrafWeb.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SuppliersController : ControllerBase
    {
        private readonly AppDbContext _db;

        public SuppliersController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetSuppliers()
        {
            var suppliers = await _db.Suppliers
                .Where(s => s.IsActive)
                .OrderBy(s => s.Name)
                .ToListAsync();

            return Ok(SupplierMapper.ToDtoList(suppliers));
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllSuppliers()
        {
            var suppliers = await _db.Suppliers
                .OrderBy(s => s.Name)
                .ToListAsync();

            return Ok(SupplierMapper.ToDtoList(suppliers));
        }

        [HttpGet("types")]
        public Task<IActionResult> GetSupplierTypes()
        {
            return Task.FromResult<IActionResult>(Ok(SupplierMapper.ToTypeDtoList()));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetSupplier(Guid id)
        {
            var supplier = await _db.Suppliers.FindAsync(id);
            if (supplier == null)
                return NotFound();

            return Ok(SupplierMapper.ToDto(supplier));
        }

        [HttpGet("by-type/{type}")]
        public async Task<IActionResult> GetSuppliersByType(SupplierType type)
        {
            var suppliers = await _db.Suppliers
                .Where(s => s.IsActive && s.Type == type)
                .OrderBy(s => s.Name)
                .ToListAsync();

            return Ok(SupplierMapper.ToDtoList(suppliers));
        }

        [HttpPost("createSupplier")]
        public async Task<IActionResult> CreateSupplier([FromBody] SupplierDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                return BadRequest("Name is required.");

            if (dto.Type is null || string.IsNullOrWhiteSpace(dto.Type.Code))
                return BadRequest("Type is required.");

            var entity = SupplierMapper.ToEntity(dto);

            _db.Suppliers.Add(entity);
            await _db.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetSupplier),
                new { id = entity.Id },
                SupplierMapper.ToDto(entity)
            );
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateSupplier(Guid id, [FromBody] SupplierDto dto)
        {
            if (id != dto.Id)
                return BadRequest("ID mismatch.");

            if (string.IsNullOrWhiteSpace(dto.Name))
                return BadRequest("Name is required.");

            if (dto.Type is null || string.IsNullOrWhiteSpace(dto.Type.Code))
                return BadRequest("Type is required.");

            var entity = await _db.Suppliers.FindAsync(id);
            if (entity == null)
                return NotFound();

            entity.Name = dto.Name;
            entity.Description = dto.Description;
            entity.IsActive = dto.IsActive;

            if (!Enum.TryParse<SupplierType>(dto.Type.Code, ignoreCase: true, out var parsedType))
                return BadRequest($"Invalid supplier type code: '{dto.Type.Code}'");

            entity.Type = parsedType;

            if (dto.Address == null)
            {
                entity.Address = null;
            }
            else
            {
                entity.Address ??= new PostalAddress();
                entity.Address.Street = dto.Address.Street;
                entity.Address.HouseNumber = dto.Address.HouseNumber;
                entity.Address.Suffix = dto.Address.Suffix;
                entity.Address.ZipCode = dto.Address.ZipCode;
                entity.Address.City = dto.Address.City;
            }

            if (dto.Postbox == null)
            {
                entity.Postbox = null;
            }
            else
            {
                entity.Postbox ??= new Postbox();
                entity.Postbox.Address = dto.Postbox.Address;
                entity.Postbox.Zipcode = dto.Postbox.ZipCode;
                entity.Postbox.City = dto.Postbox.City;
            }

            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteSupplier(Guid id)
        {
            var entity = await _db.Suppliers.FindAsync(id);
            if (entity == null)
                return NotFound();

            entity.IsActive = false;
            await _db.SaveChangesAsync();

            return NoContent();
        }
    }
}