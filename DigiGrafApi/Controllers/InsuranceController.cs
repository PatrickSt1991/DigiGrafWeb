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
    public class InsuranceController : ControllerBase
    {
        private readonly AppDbContext _db;

        public InsuranceController(AppDbContext db)
        {
            _db = db;
        }

        // ===================== INSURANCE PARTIES =====================

        /// <summary>
        /// Get all active insurance parties
        /// </summary>
        [HttpGet("parties")]
        public async Task<IActionResult> GetParties()
        {
            var parties = await _db.InsuranceParties
                .Where(p => p.IsActive)
                .OrderBy(p => p.Name)
                .ToListAsync();

            return Ok(parties.ToDtoList());
        }

        /// <summary>
        /// Get insurance party by id
        /// </summary>
        [HttpGet("parties/{id:guid}")]
        public async Task<IActionResult> GetParty(Guid id)
        {
            var party = await _db.InsuranceParties.FindAsync(id);
            if (party == null)
                return NotFound();

            return Ok(party.ToDto());
        }

        /// <summary>
        /// Create insurance party
        /// </summary>
        [HttpPost("parties")]
        public async Task<IActionResult> CreateParty([FromBody] InsurancePartyDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                return BadRequest("Name is required.");

            var entity = dto.ToEntity();
            entity.Id = Guid.NewGuid();

            _db.InsuranceParties.Add(entity);
            await _db.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetParty),
                new { id = entity.Id },
                entity.ToDto()
            );
        }

        /// <summary>
        /// Update insurance party
        /// </summary>
        [HttpPut("parties/{id:guid}")]
        public async Task<IActionResult> UpdateParty(Guid id, [FromBody] InsurancePartyDto dto)
        {
            if (id != dto.Id)
                return BadRequest("ID mismatch.");

            var entity = await _db.InsuranceParties.FindAsync(id);
            if (entity == null)
                return NotFound();

            // Core
            entity.Name = dto.Name;
            entity.IsActive = dto.IsActive;

            // Flags
            entity.IsInsurance = dto.IsInsurance;
            entity.IsAssociation = dto.IsAssociation;
            entity.HasMembership = dto.HasMembership;
            entity.HasPackage = dto.HasPackage;
            entity.IsHerkomst = dto.IsHerkomst;

            // Correspondence
            entity.CorrespondenceType =
                dto.CorrespondenceType == "mailbox"
                    ? CorrespondenceType.Mailbox
                    : CorrespondenceType.Address;

            entity.Address = dto.Address;
            entity.HouseNumber = dto.HouseNumber;
            entity.HouseNumberSuffix = dto.HouseNumberSuffix;
            entity.PostalCode = dto.PostalCode;
            entity.City = dto.City;
            entity.Country = dto.Country;
            entity.Phone = dto.Phone;

            entity.MailboxName = dto.MailboxName;
            entity.MailboxAddress = dto.MailboxAddress;

            // Billing
            entity.BillingType = dto.BillingType switch
            {
                "Derde partij" => BillingType.DerdePartij,
                "Opdrachtgever & Derde partij" => BillingType.OpdrachtgeverEnDerdePartij,
                _ => BillingType.Opdrachtgever
            };

            await _db.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// Soft delete insurance party
        /// </summary>
        [HttpDelete("parties/{id:guid}")]
        public async Task<IActionResult> DeleteParty(Guid id)
        {
            var entity = await _db.InsuranceParties.FindAsync(id);
            if (entity == null)
                return NotFound();

            entity.IsActive = false;
            await _db.SaveChangesAsync();

            return NoContent();
        }

        // ===================== INSURANCE POLICIES =====================

        /// <summary>
        /// Get policies for a deceased
        /// </summary>
        [HttpGet("policies")]
        public async Task<IActionResult> GetPolicies([FromQuery] Guid overledeneId)
        {
            if (overledeneId == Guid.Empty)
                return BadRequest("OverledeneId is required.");

            var policies = await _db.InsurancePolicies
                .Where(p => p.OverledeneId == overledeneId)
                .ToListAsync();

            return Ok(policies.ToDtoList());
        }

        /// <summary>
        /// Add policies for a deceased
        /// </summary>
        [HttpPost("policies")]
        public async Task<IActionResult> AddPolicies(
            [FromBody] List<InsurancePolicyDto> policies,
            [FromQuery] Guid overledeneId)
        {
            if (overledeneId == Guid.Empty)
                return BadRequest("OverledeneId is required.");

            var exists = await _db.Deceased.AnyAsync(d => d.Id == overledeneId);
            if (!exists)
                return NotFound("Overledene not found.");

            var entities = policies
                .Select(dto => dto.ToEntity())
                .Select(entity =>
                {
                    entity.Id = Guid.NewGuid();
                    entity.OverledeneId = overledeneId;
                    return entity;
                })
                .ToList();

            _db.InsurancePolicies.AddRange(entities);
            await _db.SaveChangesAsync();

            return Ok();
        }

        /// <summary>
        /// Delete a policy
        /// </summary>
        [HttpDelete("policies/{id:guid}")]
        public async Task<IActionResult> DeletePolicy(Guid id)
        {
            var policy = await _db.InsurancePolicies.FindAsync(id);
            if (policy == null)
                return NotFound();

            _db.InsurancePolicies.Remove(policy);
            await _db.SaveChangesAsync();

            return NoContent();
        }
    }
}
