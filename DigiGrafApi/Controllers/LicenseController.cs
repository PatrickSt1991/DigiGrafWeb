using Microsoft.AspNetCore.Mvc;
using DigiGrafWeb.Services;
using DigiGrafWeb.DTOs;
using DigiGrafWeb.Models;
using System.Linq;

namespace DigiGrafWeb.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LicenseController : ControllerBase
    {
        private readonly LicenseService _service;

        public LicenseController(LicenseService service)
        {
            _service = service;
        }

        [HttpGet("info")]
        public async Task<IActionResult> GetInfo()
        {
            var license = await _service.GetLicenseAsync();

            // Convert DB model → Frontend DTO
            var dto = new LicenseDto
            {
                Plan = license.Plan,
                IsValid = license.IsValid,
                CurrentUsers = license.CurrentUsers,
                MaxUsers = license.MaxUsers,
                CanAddUsers = license.CanAddUsers,
                ExpiresAt = license.ExpiresAt,
                Features = (license.Features ?? "")
                    .Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(f => f.Trim())
                    .ToArray(),
                Message = license.Message,
                LicenseKey = license.LicenseKey
            };

            return Ok(dto);
        }

        [HttpPost("upload")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Upload([FromForm] LicenseUploadDto dto)
        {
            var (success, message) = await _service.LoadLicenseFromFileAsync(dto.LicenseFile);
            return success ? Ok(new { message }) : BadRequest(new { message });
        }

        [HttpPost("validate-key")]
        public async Task<IActionResult> ValidateKey([FromBody] LicenseKeyRequestDto request)
        {
            var (success, message) = await _service.LoadLicenseFromKeyAsync(request.LicenseKey);
            return success ? Ok(new { message }) : BadRequest(new { message });
        }
    }
}
