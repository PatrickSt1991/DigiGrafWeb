using DigiGrafWeb.DTOs;
using DigiGrafWeb.Models;

namespace DigiGrafWeb.Mappers
{
    public static class InsurancePriceComponentMapper
    {
        public static InsurancePriceComponentDto ToDto(InsurancePriceComponent entity)
        {
            return new InsurancePriceComponentDto
            {
                Id = entity.Id,
                Omschrijving = entity.Omschrijving,
                Bedrag = entity.Bedrag,
                FactuurBedrag = entity.FactuurBedrag,
                VerzekerdAantal = entity.VerzekerdAantal,
                StandaardPM = entity.StandaardPM,
                SortOrder = entity.SortOrder,
                IsActive = entity.IsActive,
                InsurancePartyIds = entity.InsuranceParties
                    .Select(x => x.InsurancePartyId)
                    .ToList()
            };
        }

        public static IEnumerable<InsurancePriceComponentDto> ToDtoList(IEnumerable<InsurancePriceComponent> entities)
            => entities.Select(ToDto);

        public static InsurancePriceComponent ToEntity(InsurancePriceComponentDto dto)
        {
            var id = dto.Id == null || dto.Id == Guid.Empty ? Guid.NewGuid() : dto.Id.Value;

            var entity = new InsurancePriceComponent
            {
                Id = id,
                Omschrijving = dto.Omschrijving,
                Bedrag = dto.Bedrag,
                FactuurBedrag = dto.FactuurBedrag,
                VerzekerdAantal = dto.VerzekerdAantal,
                StandaardPM = dto.StandaardPM,
                SortOrder = dto.SortOrder,
                IsActive = dto.IsActive,
            };

            // join rows
            entity.InsuranceParties = dto.InsurancePartyIds
                .Distinct()
                .Select(pid => new InsurancePriceComponentInsuranceParty
                {
                    InsurancePriceComponentId = entity.Id,
                    InsurancePartyId = pid
                })
                .ToList();

            return entity;
        }

        /// <summary>
        /// Apply dto changes to existing tracked entity (incl. m2m join update)
        /// </summary>
        public static void ApplyToEntity(InsurancePriceComponent entity, InsurancePriceComponentDto dto)
        {
            entity.Omschrijving = dto.Omschrijving;
            entity.Bedrag = dto.Bedrag;
            entity.FactuurBedrag = dto.FactuurBedrag;
            entity.VerzekerdAantal = dto.VerzekerdAantal;
            entity.StandaardPM = dto.StandaardPM;
            entity.SortOrder = dto.SortOrder;
            entity.IsActive = dto.IsActive;

            var desired = new HashSet<Guid>(dto.InsurancePartyIds.Distinct());

            // remove missing
            for (int i = entity.InsuranceParties.Count - 1; i >= 0; i--)
            {
                var existing = entity.InsuranceParties.ElementAt(i);
                if (!desired.Contains(existing.InsurancePartyId))
                {
                    entity.InsuranceParties.Remove(existing);
                }
            }

            // add new
            var existingIds = new HashSet<Guid>(entity.InsuranceParties.Select(x => x.InsurancePartyId));
            foreach (var id in desired)
            {
                if (!existingIds.Contains(id))
                {
                    entity.InsuranceParties.Add(new InsurancePriceComponentInsuranceParty
                    {
                        InsurancePriceComponentId = entity.Id,
                        InsurancePartyId = id
                    });
                }
            }
        }
    }
}