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
    public class EmployeeController(
        AppDbContext db,
        UserManager<ApplicationUser> userManager
        ) : Controller
    {
        [HttpGet("employees")]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetEmployees()
        {
            var employees = await db.Employees
                .OrderBy(e => e.LastName)
                .ToListAsync();

            return Ok(EmployeeMapper.ToDtoList(employees));
        }
        [HttpGet("overview")]
        public async Task<ActionResult<IEnumerable<EmployeeOverviewDto>>> GetEmployeesOverview()
        {
            var employees = await db.Employees
                .Include(e => e.User)
                .OrderBy(e => e.LastName)
                .ToListAsync();

            return Ok(EmployeeMapper.ToOverviewDtoList(employees));
        }
        //[Authorize(Roles = "Admin")]
        [HttpGet("employeesadmin")]
        public async Task<ActionResult<IEnumerable<AdminEmployeeDto>>> GetEmployeesForAdmin()
        {
            var employees = await db.Employees
                .Include(e => e.User)
                .OrderBy(e => e.LastName)
                .ToListAsync();

            return Ok(EmployeeMapper.ToAdminDtoList(employees));
        }
        //[Authorize(Roles = "Admin")]
        [HttpPost("{employeeId}/login")]
        public async Task<IActionResult> CreateEmployeeLogin(Guid employeeId)
        {
            var employee = await db.Employees
                .Include(e => e.User)
                .FirstOrDefaultAsync(e => e.Id == employeeId);

            if(employee == null)
                return NotFound("Employee not found.");

            if(employee.UserId != null)
                return BadRequest("Employee already has a user account.");

            if(string.IsNullOrWhiteSpace(employee.Email))
                return BadRequest("Employee must have an email to create a login.");

            var user = new ApplicationUser
            {
                UserName = employee.Email,
                Email = employee.Email,
                FullName = employee.FullName,
                IsActive = true,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(user, "Temp123!");

            if(!result.Succeeded)
                return BadRequest(result.Errors);

            await userManager.AddToRoleAsync(user, "Gebruiker");

            employee.UserId = user.Id;
            db.Employees.Update(employee);
            await db.SaveChangesAsync();

            return Ok();
        }
        //[Authorize(Roles = "Admin")]
        [HttpPost("{employeeId}/login/block")]
        public async Task<IActionResult> BlockLogin(Guid employeeId)
        {
            var employee = await db.Employees
                .Include(e => e.User)
                .FirstOrDefaultAsync(e => e.Id == employeeId);

            if (employee?.User == null)
                return NotFound("Login not found");

            employee.User.IsActive = false;
            await db.SaveChangesAsync();

            return Ok();
        }
        //[Authorize(Roles = "Admin")]
        [HttpPost("{employeeId}/login/unblock")]
        public async Task<IActionResult> UnblockLogin(Guid employeeId)
        {
            var employee = await db.Employees
                .Include(e => e.User)
                .FirstOrDefaultAsync(e => e.Id == employeeId);

            if (employee?.User == null)
                return NotFound("Login not found");

            employee.User.IsActive = true;
            await db.SaveChangesAsync();

            return Ok();
        }

    }
}
