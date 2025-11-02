using DigiGrafWeb.Data;
using DigiGrafWeb.Services;
using DigiGrafWeb.Models;

namespace DigiGrafWeb.Processors
{
    public class LicenseProcessor
    {
        private readonly AppDbContext _db;
        private readonly LicenseService _licenseService;

        public LicenseProcessor(AppDbContext db, LicenseService licenseService)
        {
            _db = db;
            _licenseService = licenseService;
        }

        public void EnforceLicense()
        {
            var license = _licenseService.GetLicense();
            license.CurrentUsers = _db.Users.Count(u => u.IsActive);

            if (!license.IsValid)
                throw new UnauthorizedAccessException("License is invalid or missing.");

            if (license.ExpiresAt.HasValue && license.ExpiresAt.Value < DateTime.UtcNow)
                throw new UnauthorizedAccessException("License has expired.");

            if (license.CurrentUsers > license.MaxUsers)
                throw new UnauthorizedAccessException("User limit exceeded. Please upgrade your license.");
        }
    }
}
