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

                Deceased = dto.Deceased != null 
                    ? DeceasedMapper.ToEntity(dto.Deceased, dossierId) 
                    : null,

                DeathInfo = dto.DeathInfo != null 
                    ? DeathInfoMapper.ToEntity(dto.DeathInfo, dossierId) 
                    : null
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

                Deceased = entity.Deceased != null 
                    ? DeceasedMapper.ToDto(entity.Deceased) 
                    : null,

                DeathInfo = entity.DeathInfo != null 
                    ? DeathInfoMapper.ToDto(entity.DeathInfo) 
                    : null
            };
        }
    }
}
