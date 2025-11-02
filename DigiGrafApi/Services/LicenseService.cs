using System.Text.Json;
using DigiGrafWeb.Models;

namespace DigiGrafWeb.Services
{
    public class LicenseService
    {
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<LicenseService> _logger;
        private const string LicenseFileName = "license.lic";

        public LicenseService(IWebHostEnvironment env, ILogger<LicenseService> logger)
        {
            _env = env;
            _logger = logger;
        }

        private string LicenseFilePath => Path.Combine(_env.ContentRootPath, LicenseFileName);

        public License GetLicense()
        {
            if (!File.Exists(LicenseFilePath))
            {
                return new License(); // default free tier
            }

            try
            {
                var json = File.ReadAllText(LicenseFilePath);
                var license = JsonSerializer.Deserialize<License>(json);
                return license ?? new License { IsValid = false, Message = "Invalid license file." };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to read license file");
                return new License { IsValid = false, Message = "Corrupted license file." };
            }
        }

        public void SaveLicense(License license)
        {
            var json = JsonSerializer.Serialize(license, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(LicenseFilePath, json);
        }

        public License ValidateLicenseKey(string key)
        {
            // 💡 Replace this logic with your actual key validation
            if (key == "DG-COMMERCIAL-2025")
            {
                var license = new License
                {
                    Plan = "Commercial",
                    IsValid = true,
                    MaxUsers = 999,
                    Features = new[] { "All Features", "Advanced Analytics" },
                    ExpiresAt = DateTime.UtcNow.AddYears(1),
                    Message = "Commercial license activated."
                };

                SaveLicense(license);
                return license;
            }

            return new License { IsValid = false, Message = "Invalid or expired license key." };
        }
    }
}
