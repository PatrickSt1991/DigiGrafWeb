using DigiGrafWeb.Data;
using DigiGrafWeb.DTOs;
using DigiGrafWeb.Mappers;
using DigiGrafWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DigiGrafWeb.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
    public class DossierUtilsController(AppDbContext db, UserManager<ApplicationUser> userManager) : ControllerBase
    {
        [HttpGet("funeral-leaders")]
        public async Task<IActionResult> GetFuneralLeaders()
        {
            var users = await userManager.GetUsersInRoleAsync("Uitvaartleider");

            var result = users.Select(u => new FuneralLeaderDto
            {
                Id = u.Id,
                Value = u.Id.ToString(),
                Label = u.FullName ?? u.FullName
            }).ToList();

            return Ok(result);
        }

        [HttpGet("salutations")]
        public async Task<ActionResult<IEnumerable<Salutation>>> GetSalutations()
        {
            var salutations = await db.Salutations
                .Where(s => s.IsActive)
                .OrderBy(s => s.Label)
                .ToListAsync();

            return Ok(SalutationsMapper.ToDtoList(salutations));
        }

        [HttpGet("bodyfindings")]
        public async Task<ActionResult<IEnumerable<BodyFinding>>> GetBodyFindings()
        {
            var bodyfindings = await db.BodyFindings
                .Where(b => b.IsActive)
                .OrderBy(b => b.Label)
                .ToListAsync();

            return Ok(BodyFindingsMapper.ToDtoList(bodyfindings));
        }

        [HttpGet("origins")]
        public async Task<ActionResult<IEnumerable<Origins>>> GetOrigins()
        {
            var origins = await db.Origins
                .Where(o => o.IsActive)
                .OrderBy(o => o.Label)
                .ToListAsync();

            return Ok(OriginsMapper.ToDtoList(origins));
        }
    }
}