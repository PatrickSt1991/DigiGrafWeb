using DigiGrafWeb.Data;
using DigiGrafWeb.DTOs;
using DigiGrafWeb.Mappers;
using DigiGrafWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DigiGrafWeb.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
    public class InsuranceController(AppDbContext db, FuneralSession session) : ControllerBase
    {
        [HttpGet("companies")]
        public async Task<IActionResult> GetCompanies()
        {
            var companies = await db.InsuranceCompanies
                .Where(ic => ic.IsActive)
                .ToListAsync();

            return Ok(companies.ToDtoList());
        }

        [HttpPost("addinsurance")]
        public async Task<IActionResult> AddInsurances(
            [FromBody] List<InsurancePolicyDto> insurances,
            [FromQuery] Guid overledeneId)
        {
            var entities = insurances
                .Select(i => i.ToEntity())
                .Select(e =>
                {
                    e.Id = Guid.NewGuid();
                    e.OverledeneId = overledeneId;
                    return e;
                })
                .ToList();

            db.InsurancePolicies.AddRange(entities);
            await db.SaveChangesAsync();

            return Ok();
        }
    }

}
