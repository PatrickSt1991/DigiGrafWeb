using DigiGrafWeb.DTOs;
using DigiGrafWeb.Interface;

namespace DigiGrafWeb.Mappers
{
    // ------------------------------------------------------------
    // Usage examples:
    //   GeneralMapper<Salutation>.ToDto(entity)
    //   GeneralMapper<BodyFinding>.ToEntityList(dtos)
    // ------------------------------------------------------------
    public static class GeneralMapper<TEntity> where TEntity : IGeneralEntity, new()
    {
        public static TEntity ToEntity(GeneralDto dto)
        {
            return new TEntity
            {
                Id = dto.Id == null || dto.Id == Guid.Empty ? Guid.NewGuid() : dto.Id.Value,
                Code = dto.Code,
                Label = dto.Label,
                Description = dto.Description,
                IsActive = dto.IsActive
            };
        }

        public static GeneralDto ToDto(TEntity entity)
        {
            return new GeneralDto
            {
                Id = entity.Id,
                Code = entity.Code,
                Label = entity.Label,
                Description = entity.Description,
                IsActive = entity.IsActive
            };
        }

        public static IEnumerable<GeneralDto> ToDtoList(IEnumerable<TEntity> entities)
            => entities.Select(ToDto);

        public static IEnumerable<TEntity> ToEntityList(IEnumerable<GeneralDto> dtos)
            => dtos.Select(ToEntity);
    }
}