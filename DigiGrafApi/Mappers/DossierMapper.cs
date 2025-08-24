using DigiGrafWeb.DTOs;
using DigiGrafWeb.Models;

namespace DigiGrafWeb.Mappers
{
    public static class DossierMapper
    {
        public static Dossier ToEntity(DossierDto dto)
        {
            var dossierId = dto.Id == null || dto.Id == Guid.Empty ? Guid.NewGuid() : dto.Id.Value;
            
            return new Dossier
            {
                Id = dossierId,
                FuneralLeader = dto.FuneralLeader,
                FuneralNumber = dto.FuneralNumber,
                FuneralType = dto.FuneralType,
                Voorregeling = dto.Voorregeling,
                DossierCompleted = dto.DossierCompleted,

                Deceased = dto.Deceased != null ? new Deceased
                {
                    FirstName = dto.Deceased.FirstName,
                    LastName = dto.Deceased.LastName,
                    Salutation = dto.Deceased.Salutation,
                    Dob = dto.Deceased.DOB,
                    PlaceOfBirth = dto.Deceased.PlaceOfBirth,
                    PostalCode = dto.Deceased.PostalCode,
                    Street = dto.Deceased.Street,
                    HouseNumber = dto.Deceased.HouseNumber,
                    HouseNumberAddition = dto.Deceased.HouseNumberAddition,
                    City = dto.Deceased.City,
                    County = dto.Deceased.County,
                    HomeDeceased = dto.Deceased.HomeDeceased
                } : null,

                DeathInfo = dto.DeathInfo != null ? new DeathInfo
                {
                    DateOfDeath = dto.DeathInfo.DateOfDeath,
                    TimeOfDeath = dto.DeathInfo.TimeOfDeath,
                    LocationOfDeath = dto.DeathInfo.LocationOfDeath,
                    PostalCodeOfDeath = dto.DeathInfo.PostalCodeOfDeath,
                    HouseNumberOfDeath = dto.DeathInfo.HouseNumberOfDeath,
                    HouseNumberAdditionOfDeath = dto.DeathInfo.HouseNumberAdditionOfDeath,
                    StreetOfDeath = dto.DeathInfo.StreetOfDeath,
                    CityOfDeath = dto.DeathInfo.CityOfDeath,
                    CountyOfDeath = dto.DeathInfo.CountyOfDeath,
                    BodyFinding = dto.DeathInfo.BodyFinding,
                    Origin = dto.DeathInfo.Origin
                } : null
            };
        }

        public static DossierDto ToDto(Dossier entity)
        {
            return new DossierDto
            {
                Id = entity.Id,
                FuneralLeader = entity.FuneralLeader,
                FuneralNumber = entity.FuneralNumber,
                FuneralType = entity.FuneralType,
                Voorregeling = entity.Voorregeling,
                DossierCompleted = entity.DossierCompleted,

                Deceased = entity.Deceased != null ? new DeceasedDto
                {
                    FirstName = entity.Deceased.FirstName,
                    LastName = entity.Deceased.LastName,
                    Salutation = entity.Deceased.Salutation,
                    DOB = entity.Deceased.Dob,
                    PlaceOfBirth = entity.Deceased.PlaceOfBirth,
                    PostalCode = entity.Deceased.PostalCode,
                    Street = entity.Deceased.Street,
                    HouseNumber = entity.Deceased.HouseNumber,
                    HouseNumberAddition = entity.Deceased.HouseNumberAddition,
                    City = entity.Deceased.City,
                    County = entity.Deceased.County,
                    HomeDeceased = entity.Deceased.HomeDeceased
                } : null,

                DeathInfo = entity.DeathInfo != null ? new DeathInfoDto
                {
                    DateOfDeath = entity.DeathInfo.DateOfDeath,
                    TimeOfDeath = entity.DeathInfo.TimeOfDeath,
                    LocationOfDeath = entity.DeathInfo.LocationOfDeath,
                    PostalCodeOfDeath = entity.DeathInfo.PostalCodeOfDeath,
                    HouseNumberOfDeath = entity.DeathInfo.HouseNumberOfDeath,
                    HouseNumberAdditionOfDeath = entity.DeathInfo.HouseNumberAdditionOfDeath,
                    StreetOfDeath = entity.DeathInfo.StreetOfDeath,
                    CityOfDeath = entity.DeathInfo.CityOfDeath,
                    CountyOfDeath = entity.DeathInfo.CountyOfDeath,
                    BodyFinding = entity.DeathInfo.BodyFinding,
                    Origin = entity.DeathInfo.Origin
                } : null
            };
        }
    }
}
