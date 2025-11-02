using DigiGrafWeb.DTOs;
using DigiGrafWeb.Models;

namespace DigiGrafWeb.Mappers
{
    public static class LicenseMapper
    {
        public static LicenseDto ToDto(License entity)
        {
            return new LicenseDto
            {
                Plan = entity.Plan,
                IsValid = entity.IsValid,
                CurrentUsers = entity.CurrentUsers,
                MaxUsers = entity.MaxUsers,
                CanAddUsers = entity.CanAddUsers,
                ExpiresAt = entity.ExpiresAt,
                Features = entity.Features,
                Message = entity.Message
            };
        }

        public static License ToEntity(LicenseDto dto)
        {
            return new License
            {
                Plan = dto.Plan,
                IsValid = dto.IsValid,
                CurrentUsers = dto.CurrentUsers,
                MaxUsers = dto.MaxUsers,
                ExpiresAt = dto.ExpiresAt,
                Features = dto.Features,
                Message = dto.Message ?? ""
            };
        }
    }
}
