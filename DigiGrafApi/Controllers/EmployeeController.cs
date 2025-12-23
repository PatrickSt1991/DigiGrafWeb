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
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager
        ) : Controller
    {
        [HttpGet("employees")]
        public async Task<ActionResult<IEnumerable<AdminEmployeeDto>>> GetEmployees()
        {
            // Load employees + user (no UserRoles navigation on ApplicationUser in your setup)
            var employees = await db.Employees
                .Include(e => e.User)
                .OrderBy(e => e.LastName)
                .ToListAsync();

            // Map to Admin DTOs, then enrich with RoleId/RoleName via Identity APIs
            var adminDtos = EmployeeMapper.ToAdminDtoList(employees).ToList();

            for (int i = 0; i < employees.Count; i++)
            {
                var emp = employees[i];

                if (emp.User != null)
                {
                    var roleNames = await userManager.GetRolesAsync(emp.User);
                    var roleName = roleNames.FirstOrDefault();

                    if (!string.IsNullOrWhiteSpace(roleName))
                    {
                        var role = await roleManager.FindByNameAsync(roleName);
                        adminDtos[i].RoleName = roleName;
                        adminDtos[i].RoleId = role?.Id ?? Guid.Empty;
                    }
                    else
                    {
                        adminDtos[i].RoleName = string.Empty;
                        adminDtos[i].RoleId = Guid.Empty;
                    }
                }
                else
                {
                    adminDtos[i].RoleName = string.Empty;
                    adminDtos[i].RoleId = Guid.Empty;
                }
            }

            return Ok(adminDtos);
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

            var adminDtos = EmployeeMapper.ToAdminDtoList(employees).ToList();

            for (int i = 0; i < adminDtos.Count; i++)
            {
                var employee = employees[i];

                if (employee.UserId != null && employee.User != null)
                {
                    var roles = await userManager.GetRolesAsync(employee.User);
                    var roleName = roles.FirstOrDefault();

                    if (!string.IsNullOrEmpty(roleName))
                    {
                        var role = await roleManager.FindByNameAsync(roleName);

                        adminDtos[i].RoleName = roleName;
                        adminDtos[i].RoleId = role?.Id ?? Guid.Empty;
                    }
                    else
                    {
                        adminDtos[i].RoleName = string.Empty;
                        adminDtos[i].RoleId = Guid.Empty;
                    }
                }
                else
                {
                    adminDtos[i].RoleName = string.Empty;
                    adminDtos[i].RoleId = Guid.Empty;
                }
            }

            return Ok(adminDtos);

        }

        //[Authorize(Roles = "Admin")]
        [HttpPost("createLogin/{employeeId}")]
        public async Task<IActionResult> CreateEmployeeLogin(Guid employeeId)
        {
            var employee = await db.Employees
                .Include(e => e.User)
                .FirstOrDefaultAsync(e => e.Id == employeeId);

            if (employee == null)
                return NotFound("Employee not found.");

            if (employee.UserId != null)
                return BadRequest("Employee already has a user account.");

            if (string.IsNullOrWhiteSpace(employee.Email))
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

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            await userManager.AddToRoleAsync(user, "Gebruiker");

            employee.UserId = user.Id;
            db.Employees.Update(employee);
            await db.SaveChangesAsync();

            return Ok();
        }

        //[Authorize(Roles = "Admin")]
        [HttpPost("blockLogin/{employeeId}")]
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
        [HttpPost("unblockLogin/{employeeId}")]
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

        //[Authorize(Roles = "Admin")]
        [HttpPost("createEmployee")]
        public async Task<ActionResult<EmployeeDto>> CreateEmployee([FromBody] EmployeeDto dto)
        {
            // Basic validation
            if (string.IsNullOrWhiteSpace(dto.FirstName) ||
                string.IsNullOrWhiteSpace(dto.LastName) ||
                string.IsNullOrWhiteSpace(dto.Email))
            {
                return BadRequest("First name, last name and email are required.");
            }

            // Prevent duplicate email (employee-level)
            var emailExists = await db.Employees.AnyAsync(e => e.Email == dto.Email);
            if (emailExists)
                return BadRequest("An employee with this email already exists.");

            var employee = EmployeeMapper.ToEntity(dto);

            db.Employees.Add(employee);
            await db.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetEmployeeById),
                new { id = employee.Id },
                EmployeeMapper.ToDto(employee)
            );
        }

        //[Authorize(Roles = "Admin")]
        [HttpPut("updateEmployee/{id}")]
        public async Task<IActionResult> UpdateEmployee(Guid id, [FromBody] EmployeeDto dto)
        {
            if (id != dto.Id)
                return BadRequest("ID mismatch.");

            var employee = await db.Employees
                .Include(e => e.User)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (employee == null)
                return NotFound();

            // Update employee fields
            employee.IsActive = dto.IsActive;
            employee.Initials = dto.Initials;
            employee.FirstName = dto.FirstName;
            employee.LastName = dto.LastName;
            employee.Tussenvoegsel = dto.Tussenvoegsel;
            employee.BirthPlace = dto.BirthPlace;
            employee.BirthDate = dto.BirthDate;
            employee.Email = dto.Email;
            employee.Mobile = dto.Mobile;
            employee.StartDate = dto.StartDate;

            // ---- ROLE UPDATE (THIS IS THE IMPORTANT PART) ----
            if (employee.User != null)
            {
                var user = employee.User;

                var existingRoles = await userManager.GetRolesAsync(user);

                var newRole = await roleManager.FindByIdAsync(dto.RoleId.ToString());

                if (newRole == null)
                    return BadRequest("Invalid role");

                await userManager.RemoveFromRolesAsync(user, existingRoles);
                await userManager.AddToRoleAsync(user, newRole.Name);
            }

            await db.SaveChangesAsync();
            return NoContent();
        }

        //[Authorize(Roles = "Admin")]
        [HttpGet("getEmployee/{id}")]
        public async Task<ActionResult<EmployeeDto>> GetEmployeeById(Guid id)
        {
            var employee = await db.Employees.FindAsync(id);
            if (employee == null)
                return NotFound();

            return Ok(EmployeeMapper.ToDto(employee));
        }

        [HttpGet("employeeRoles")]
        public async Task<ActionResult<List<RoleDto>>> GetRoles()
        {
            var roles = await db.Roles
                .Select(r => new RoleDto
                {
                    Id = r.Id,
                    Name = r.Name,
                    Description = r.Description
                })
                .OrderBy(r => r.Name)
                .ToListAsync();

            return Ok(roles);
        }
    }
}
