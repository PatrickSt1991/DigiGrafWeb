using DigiGrafWeb.DTOs;
using DigiGrafWeb.Models;

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
}
