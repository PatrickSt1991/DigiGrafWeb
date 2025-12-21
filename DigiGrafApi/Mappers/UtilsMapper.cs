using DigiGrafWeb.DTOs;
using DigiGrafWeb.Models;
using Microsoft.OpenApi.Extensions;

namespace DigiGrafWeb.Mappers
{
    public static class SalutationsMapper
    {
        public static Salutation ToEntity(SalutationDto dto)
        {
            var salutationdId = dto.Id == null || dto.Id == Guid.Empty ? Guid.NewGuid() : dto.Id.Value;

            return new Salutation
            {
                Id = salutationdId,
                Code = dto.Code,
                Label = dto.Label,
                Description = dto.Description,
                IsActive = dto.IsActive
            };
        }

        public static SalutationDto ToDto(Salutation entity)
        {
            return new SalutationDto
            {
                Id = entity.Id,
                Code = entity.Code,
                Label = entity.Label,
                Description = entity.Description,
                IsActive = entity.IsActive
            };
        }

        public static IEnumerable<SalutationDto> ToDtoList(IEnumerable<Salutation> entities)
        {
            return entities.Select(ToDto);
        }

        public static IEnumerable<Salutation> ToEntityList(IEnumerable<SalutationDto> dtos)
        {
            return dtos.Select(ToEntity);
        }
    }
    public static class BodyFindingsMapper
    {
        public static BodyFinding ToEntity(BodyFindingDto dto)
        {
            var salutationdId = dto.Id == null || dto.Id == Guid.Empty ? Guid.NewGuid() : dto.Id.Value;

            return new BodyFinding
            {
                Id = salutationdId,
                Code = dto.Code,
                Label = dto.Label,
                Description = dto.Description,
                IsActive = dto.IsActive
            };
        }

        public static BodyFindingDto ToDto(BodyFinding entity)
        {
            return new BodyFindingDto
            {
                Id = entity.Id,
                Code = entity.Code,
                Label = entity.Label,
                Description = entity.Description,
                IsActive = entity.IsActive
            };
        }

        public static IEnumerable<BodyFindingDto> ToDtoList(IEnumerable<BodyFinding> entities)
        {
            return entities.Select(ToDto);
        }

        public static IEnumerable<BodyFinding> ToEntityList(IEnumerable<BodyFindingDto> dtos)
        {
            return dtos.Select(ToEntity);
        }
    }
    public static class OriginsMapper
    {
        public static Origins ToEntity(OriginsDto dto)
        {
            var originId = dto.Id == null || dto.Id == Guid.Empty ? Guid.NewGuid() : dto.Id.Value;

            return new Origins
            {
                Id = originId,
                Code = dto.Code,
                Label = dto.Label,
                Description = dto.Description,
                IsActive = dto.IsActive
            };
        }

        public static OriginsDto ToDto(Origins entity)
        {
            return new OriginsDto
            {
                Id = entity.Id,
                Code = entity.Code,
                Label = entity.Label,
                Description = entity.Description,
                IsActive = entity.IsActive
            };
        }

        public static IEnumerable<OriginsDto> ToDtoList(IEnumerable<Origins> entities)
        {
            return entities.Select(ToDto);
        }

        public static IEnumerable<Origins> ToEntityList(IEnumerable<OriginsDto> dtos)
        {
            return dtos.Select(ToEntity);
        }
    }
    public static class MaritalStatusMapper
    {
        public static MaritalStatus ToEntity(MaritalStatusDto dto)
        {
            var maritaldId = dto.Id == null || dto.Id == Guid.Empty ? Guid.NewGuid() : dto.Id.Value;

            return new MaritalStatus
            {
                Id = maritaldId,
                Code = dto.Code,
                Label = dto.Label,
                Description = dto.Description,
                IsActive = dto.IsActive
            };
        }

        public static MaritalStatusDto ToDto(MaritalStatus entity)
        {
            return new MaritalStatusDto
            {
                Id = entity.Id,
                Code = entity.Code,
                Label = entity.Label,
                Description = entity.Description,
                IsActive = entity.IsActive
            };
        }

        public static IEnumerable<MaritalStatusDto> ToDtoList(IEnumerable<MaritalStatus> entities)
        {
            return entities.Select(ToDto);
        }

        public static IEnumerable<MaritalStatus> ToEntityList(IEnumerable<MaritalStatusDto> dtos)
        {
            return dtos.Select(ToEntity);
        }
    }
    public static class CoffinsMapper
    {
        public static Coffins ToEntity(CoffinsDto dto)
        {
            var coffinId = dto.Id == null || dto.Id == Guid.Empty ? Guid.NewGuid() : dto.Id.Value;

            return new Coffins
            {
                Id = coffinId,
                Code = dto.Code,
                Label = dto.Label,
                Description = dto.Description,
                IsActive = dto.IsActive
            };
        }

        public static CoffinsDto ToDto(Coffins entity)
        {
            return new CoffinsDto
            {
                Id = entity.Id,
                Code = entity.Code,
                Label = entity.Label,
                Description = entity.Description,
                IsActive = entity.IsActive
            };
        }

        public static IEnumerable<CoffinsDto> ToDtoList(IEnumerable<Coffins> entities)
        {
            return entities.Select(ToDto);
        }

        public static IEnumerable<Coffins> ToEntityList(IEnumerable<CoffinsDto> dtos)
        {
            return dtos.Select(ToEntity);
        }
    }
    public static class CoffinsLenghtsMapper
    {
        public static CoffinLengths ToEntity(CoffinLengthsDto dto)
        {
            var coffinlengthId = dto.Id == null || dto.Id == Guid.Empty ? Guid.NewGuid() : dto.Id.Value;

            return new CoffinLengths
            {
                Id = coffinlengthId,
                Code = dto.Code,
                Label = dto.Label,
                Description = dto.Description,
                IsActive = dto.IsActive
            };
        }

        public static CoffinLengthsDto ToDto(CoffinLengths entity)
        {
            return new CoffinLengthsDto
            {
                Id = entity.Id,
                Code = entity.Code,
                Label = entity.Label,
                Description = entity.Description,
                IsActive = entity.IsActive
            };
        }

        public static IEnumerable<CoffinLengthsDto> ToDtoList(IEnumerable<CoffinLengths> entities)
        {
            return entities.Select(ToDto);
        }

        public static IEnumerable<CoffinLengths> ToEntityList(IEnumerable<CoffinLengthsDto> dtos)
        {
            return dtos.Select(ToEntity);
        }
    }
    public static class CaretakerMapper
    {
        public static CaretakerDto ToDto(ApplicationUser user)
        {
            return new CaretakerDto
            {
                Id = user.Id,
                DisplayName = $"{user.FullName}",
                Email = user.Email
            };
        }

        public static IEnumerable<CaretakerDto> ToDtoList(IEnumerable<ApplicationUser> users)
        {
            return users.Select(ToDto);
        }
    }
    public static class SupplierMapper
    {
        public static Suppliers ToEntity(SupplierDto dto)
        {
            return new Suppliers
            {
                Id = dto.Id == Guid.Empty ? Guid.NewGuid() : dto.Id,
                Name = dto.Name,
                Description = dto.Description,
                IsActive = dto.IsActive,
                Type = Enum.Parse<SupplierType>(dto.Type.Code),

                Address = dto.Address == null ? null : new PostalAddress
                {
                    Street = dto.Address.Street,
                    HouseNumber = dto.Address.HouseNumber,
                    Suffix = dto.Address.Suffix,
                    ZipCode = dto.Address.ZipCode,
                    City = dto.Address.City
                },

                Postbox = dto.Postbox == null ? null : new Postbox
                {
                    Address = dto.Postbox.Address,
                    Zipcode = dto.Postbox.ZipCode,
                    City = dto.Postbox.City
                }
            };
        }

        public static SupplierDto ToDto(Suppliers entity)
        {
            return new SupplierDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                IsActive = entity.IsActive,

                Type = new SupplierTypeDto
                {
                    Code = entity.Type.ToString(),
                    Label = entity.Type.GetDisplayName()
                },

                Address = entity.Address == null ? null : new PostalAddressDto
                {
                    Street = entity.Address.Street,
                    HouseNumber = entity.Address.HouseNumber,
                    Suffix = entity.Address.Suffix,
                    ZipCode = entity.Address.ZipCode,
                    City = entity.Address.City
                },

                Postbox = entity.Postbox == null ? null : new PostboxDto
                {
                    Address = entity.Postbox.Address,
                    ZipCode = entity.Postbox.Zipcode,
                    City = entity.Postbox.City
                }
            };
        }

        public static IEnumerable<SupplierDto> ToDtoList(IEnumerable<Suppliers> entities)
        {
            return entities.Select(ToDto);
        }

        public static IEnumerable<Suppliers> ToEntityList(IEnumerable<SupplierDto> dtos)
        {
            return dtos.Select(ToEntity);
        }
    }
}
