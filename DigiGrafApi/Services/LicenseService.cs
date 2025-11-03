using DigiGrafWeb.Data;
using DigiGrafWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Text.Json;


namespace DigiGrafWeb.Services
{
    public class LicenseService
    {
        private readonly AppDbContext _db;


        public LicenseService(AppDbContext db)
        {
            _db = db;
        }


        public async Task<License> GetLicenseAsync()
        {
            var entity = await _db.Licenses.FirstOrDefaultAsync();
            if (entity == null)
            {
                return new License
                {
                    Plan = "Free",
                    IsValid = false,
                    CurrentUsers = 0,
                    MaxUsers = 0,
                    CanAddUsers = false,
                    Message = "No license active"
                };
            }
            return entity;
        }


        public async Task<(bool success, string message)> LoadLicenseFromKeyAsync(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                return (false, "License key cannot be empty");


            var license = ParseKey(key);
            if (license == null)
                return (false, "Invalid license format");


            var entity = await _db.Licenses.FirstOrDefaultAsync();
            if (entity == null)
            {
                license.LicenseKey = key;
                _db.Licenses.Add(license);
            }
            else
            {
                entity.Plan = license.Plan;
                entity.IsValid = license.IsValid;
                entity.CurrentUsers = license.CurrentUsers;
                entity.MaxUsers = license.MaxUsers;
                entity.CanAddUsers = license.CanAddUsers;
                entity.ExpiresAt = license.ExpiresAt;
                entity.Features = string.Join(",", license.Features.Split(","));
                entity.Message = license.Message;
                entity.LicenseKey = key;
            }


            await _db.SaveChangesAsync();
            return (true, "License activated successfully");
        }


        public async Task<(bool success, string message)> LoadLicenseFromFileAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return (false, "No file uploaded");


            using var ms = new MemoryStream();
            await file.CopyToAsync(ms);
            var key = Encoding.UTF8.GetString(ms.ToArray()).Trim();
            return await LoadLicenseFromKeyAsync(key);
        }


        private License? ParseKey(string key)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(key))
                    return null;

                // Normalize: remove CR/LF, spaces, tabs, BOMs
                key = key.Replace("\r", "")
                         .Replace("\n", "")
                         .Replace(" ", "")
                         .Replace("\t", "")
                         .Trim();

                byte[] bytes;

                // Try strict Base64 first
                try
                {
                    bytes = Convert.FromBase64String(key);
                }
                catch
                {
                    // If it fails, assume plain JSON instead of Base64
                    bytes = Encoding.UTF8.GetBytes(key);
                }

                var json = Encoding.UTF8.GetString(bytes);

                // Sometimes the decoded string still contains leading BOMs or quotes
                json = json.Trim('\uFEFF', '\"', '\'', ' ', '\r', '\n');

                // Parse JSON safely
                using var doc = JsonDocument.Parse(json);

                if (!doc.RootElement.TryGetProperty("license", out var licEl))
                    return null;

                var features = new List<string>();
                if (licEl.TryGetProperty("features", out var fEl) && fEl.ValueKind == JsonValueKind.Array)
                {
                    foreach (var f in fEl.EnumerateArray())
                    {
                        var val = f.GetString();
                        if (!string.IsNullOrWhiteSpace(val))
                            features.Add(val);
                    }
                }

                var plan = licEl.TryGetProperty("plan", out var planEl)
                    ? planEl.GetString() ?? "Free"
                    : "Free";

                var maxUsers = licEl.TryGetProperty("maxUsers", out var maxEl)
                    ? maxEl.GetInt32()
                    : 0;

                DateTime? expires = null;
                if (licEl.TryGetProperty("expiresDate", out var expEl))
                {
                    var expStr = expEl.GetString();
                    if (DateTime.TryParse(expStr, out var parsed))
                        expires = parsed;
                }

                return new License
                {
                    Plan = plan,
                    IsValid = true,
                    CurrentUsers = 0,
                    MaxUsers = maxUsers,
                    CanAddUsers = true,
                    ExpiresAt = expires,
                    Features = string.Join(",", features),
                    Message = "License loaded successfully"
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ParseKey failed: {ex.Message}");
                return null;
            }
        }

    }
}