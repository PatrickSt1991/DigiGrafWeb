using DigiGrafWeb.DTOs;
using DigiGrafWeb.Models;

namespace DigiGrafWeb.Mappers
{
    public static class DeathInfoMapper
    {
        public static DeathInfo ToEntity(DeathInfoDto dto, Guid dossierId)
        {
            var deathInfoId = dto.Id == null || dto.Id == Guid.Empty ? Guid.NewGuid() : dto.Id.Value;

            return new DeathInfo
            {
                Id = dto.Id,
                DossierId = dossierId,
                DateOfDeath = dto.DateOfDeath,
                TimeOfDeath = dto.TimeOfDeath,
                LocationOfDeath = dto.LocationOfDeath,
                PostalCodeOfDeath = dto.PostalCodeOfDeath,
                HouseNumberOfDeath = dto.HouseNumberOfDeath,
                HouseNumberAdditionOfDeath = dto.HouseNumberAdditionOfDeath,
                StreetOfDeath = dto.StreetOfDeath,
                CityOfDeath = dto.CityOfDeath,
                CountyOfDeath = dto.CountyOfDeath,
                BodyFinding = dto.BodyFinding,
                Origin = dto.Origin
            };
        }

        public static DeathInfoDto ToDto(DeathInfo entity)
        {
            return new DeathInfoDto
            {
                Id = entity.Id,
                DateOfDeath = entity.DateOfDeath,
                TimeOfDeath = entity.TimeOfDeath,
                LocationOfDeath = entity.LocationOfDeath,
                PostalCodeOfDeath = entity.PostalCodeOfDeath,
                HouseNumberOfDeath = entity.HouseNumberOfDeath,
                HouseNumberAdditionOfDeath = entity.HouseNumberAdditionOfDeath,
                StreetOfDeath = entity.StreetOfDeath,
                CityOfDeath = entity.CityOfDeath,
                CountyOfDeath = entity.CountyOfDeath,
                BodyFinding = entity.BodyFinding,
                Origin = entity.Origin
            };
        }
    }
}
