using DigiGrafWeb.Data;
using DigiGrafWeb.Services;
using DigiGrafWeb.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DigiGrafWeb.DTOs;

namespace DigiGrafWeb.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class LicenseController : ControllerBase
    {
        private readonly LicenseService _licenseService;
        private readonly AppDbContext _db;

        public LicenseController(LicenseService licenseService, AppDbContext db)
        {
            _licenseService = licenseService;
            _db = db;
        }

        [HttpGet("info")]
        public IActionResult GetInfo()
        {
            var license = _licenseService.GetLicense();
            license.CurrentUsers = _db.Users.Count(u => u.IsActive);
            return Ok(LicenseMapper.ToDto(license));
        }

        [HttpPost("upload")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadLicense([FromForm] LicenseUploadDto dto)
        {
            if (dto.LicenseFile == null || dto.LicenseFile.Length == 0)
                return BadRequest(new { message = "No license file uploaded." });

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "license.lic");
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await dto.LicenseFile.CopyToAsync(stream);
            }

            var license = _licenseService.GetLicense();
            license.CurrentUsers = _db.Users.Count(u => u.IsActive);
            return Ok(LicenseMapper.ToDto(license));
        }

        [HttpPost("validate-key")]
        public IActionResult ValidateKey([FromBody] Dictionary<string, string> payload)
        {
            if (!payload.TryGetValue("licenseKey", out var key) || string.IsNullOrWhiteSpace(key))
                return BadRequest(new { message = "License key missing." });

            var license = _licenseService.ValidateLicenseKey(key);
            if (!license.IsValid)
                return BadRequest(new { message = license.Message });

            license.CurrentUsers = _db.Users.Count(u => u.IsActive);
            return Ok(LicenseMapper.ToDto(license));
        }
    }
}
