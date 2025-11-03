using Microsoft.AspNetCore.Mvc;

namespace DigiGrafWeb.DTOs
{
    public class LicenseUploadDto
    {
        [FromForm(Name = "licenseFile")]
        public IFormFile LicenseFile { get; set; }
    }

}