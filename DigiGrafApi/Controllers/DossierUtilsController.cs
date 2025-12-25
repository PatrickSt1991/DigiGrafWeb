using DigiGrafWeb.Data;
using DigiGrafWeb.DTOs;
using DigiGrafWeb.Mappers;
using DigiGrafWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Extensions;

namespace DigiGrafWeb.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
    public class DossierUtilsController(AppDbContext db, UserManager<ApplicationUser> userManager) : ControllerBase
    {
        [HttpGet("funeral-leaders")]
        public async Task<ActionResult<IEnumerable<FuneralLeaderDto>>> GetFuneralLeaders()
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
        [HttpGet("maritalstatus")]
        public async Task<ActionResult<IEnumerable<MaritalStatus>>> GetMaritalStatuses()
        {
            var statuses = await db.MaritalStatuses
                .Where(m => m.IsActive)
                .OrderBy(m => m.Label)
                .ToListAsync();

            return Ok(MaritalStatusMapper.ToDtoList(statuses));
        }
        [HttpGet("coffins")]
        public async Task<ActionResult<IEnumerable<Coffins>>> GetCoffins()
        {
            var coffins = await db.Coffins
                .Where(c => c.IsActive)
                .OrderBy(c => c.Label)
                .ToListAsync();

            return Ok(CoffinsMapper.ToDtoList(coffins));
        }
        [HttpGet("coffins-length")]
        public async Task<ActionResult<IEnumerable<CoffinLengths>>> GetCoffinLenghts()
        {
            var coffinslenghts = await db.CoffinsLengths
                .Where(c => c.IsActive)
                .OrderBy(c => c.Label)
                .ToListAsync();

            return Ok(CoffinsLenghtsMapper.ToDtoList(coffinslenghts));
        }
        [HttpGet("caretakers")]
        public async Task<ActionResult<IEnumerable<CaretakerDto>>> GetCaretakers()
        {
            var roles = new[] { "Uitvaartleider", "Medewerker" };
            var caretakers = new List<ApplicationUser>();

            foreach (var role in roles)
            {
                var usersInRole = await userManager.GetUsersInRoleAsync(role);
                caretakers.AddRange(usersInRole);
            }

            var distinctCaretakers = caretakers.Distinct().ToList();

            return Ok(CaretakerMapper.ToDtoList(distinctCaretakers));
        }
        [HttpGet("suppliers")]
        public async Task<ActionResult<IEnumerable<SupplierDto>>> GetSuppliers()
        {
            var suppliers = await db.Suppliers
                .Where(s => s.IsActive)
                .Select(s => new Suppliers
                {
                    Id = s.Id,
                    Name = s.Name,
                    Description = s.Description,
                    Type = s.Type,
                    IsActive = s.IsActive
                })
                .OrderBy(s => s.Name)
                .ToListAsync();

            return Ok(SupplierMapper.ToDtoList(suppliers));
        }
        [HttpGet("supplier-types")]
        public ActionResult<IEnumerable<SupplierTypeDto>> GetSupplierTypes()
        {
            var result = Enum.GetValues<SupplierType>()
                .Select(t => new SupplierTypeDto
                {
                    Code = t.ToString(),
                    Label = t.GetDisplayName()
                });

            return Ok(result);
        }


    }
}