using DigiGrafWeb.DTOs;
using DigiGrafWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DigiGrafWeb.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
    public class DossierUtilsController(UserManager<ApplicationUser> userManager) : ControllerBase
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
    }
}
