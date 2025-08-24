using DigiGrafWeb.DTOs;
using DigiGrafWeb.Models;

namespace DigiGrafWeb.Mappers
{
    public static class DeceasedMapper
    {
        public static Deceased ToEntity(DeceasedDto dto, Guid dossierId)
        {
            var deceasedId = dto.Id == null || dto.Id == Guid.Empty ? Guid.NewGuid() : dto.Id.Value;

            return new Deceased
            {
                Id = deceasedId,
                DossierId = dossierId,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Salutation = dto.Salutation,
                Dob = dto.DOB,
                PlaceOfBirth = dto.PlaceOfBirth,
                PostalCode = dto.PostalCode,
                Street = dto.Street,
                HouseNumber = dto.HouseNumber,
                HouseNumberAddition = dto.HouseNumberAddition,
                City = dto.City,
                County = dto.County,
                HomeDeceased = dto.HomeDeceased
            };
        }

        public static DeceasedDto ToDto(Deceased entity)
        {
            return new DeceasedDto
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Salutation = entity.Salutation,
                DOB = entity.Dob,
                PlaceOfBirth = entity.PlaceOfBirth,
                PostalCode = entity.PostalCode,
                Street = entity.Street,
                HouseNumber = entity.HouseNumber,
                HouseNumberAddition = entity.HouseNumberAddition,
                City = entity.City,
                County = entity.County,
                HomeDeceased = entity.HomeDeceased
            };
        }
    }
}
