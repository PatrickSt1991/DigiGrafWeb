using DigiGrafWeb.DTOs;
using DigiGrafWeb.Models;

namespace DigiGrafWeb.Mappers
{
    public static class InsuranceMapper
    {
        // ===================== INSURANCE PARTY =====================

        public static InsurancePartyDto ToDto(this InsuranceParty entity) => new()
        {
            Id = entity.Id,

            Name = entity.Name,
            IsActive = entity.IsActive,

            IsInsurance = entity.IsInsurance,
            IsAssociation = entity.IsAssociation,
            HasMembership = entity.HasMembership,
            HasPackage = entity.HasPackage,
            IsHerkomst = entity.IsHerkomst,

            CorrespondenceType = entity.CorrespondenceType switch
            {
                CorrespondenceType.Mailbox => "mailbox",
                _ => "address"
            },

            Address = entity.Address,
            HouseNumber = entity.HouseNumber,
            HouseNumberSuffix = entity.HouseNumberSuffix,
            PostalCode = entity.PostalCode,
            City = entity.City,
            Country = entity.Country,
            Phone = entity.Phone,

            MailboxName = entity.MailboxName,
            MailboxAddress = entity.MailboxAddress,

            BillingType = entity.BillingType switch
            {
                BillingType.DerdePartij => "Derde partij",
                BillingType.OpdrachtgeverEnDerdePartij => "Opdrachtgever & Derde partij",
                _ => "Opdrachtgever"
            }
        };

        public static InsuranceParty ToEntity(this InsurancePartyDto dto) => new()
        {
            Id = dto.Id ?? Guid.NewGuid(),

            Name = dto.Name,
            IsActive = dto.IsActive,

            IsInsurance = dto.IsInsurance,
            IsAssociation = dto.IsAssociation,
            HasMembership = dto.HasMembership,
            HasPackage = dto.HasPackage,
            IsHerkomst = dto.IsHerkomst,

            CorrespondenceType = dto.CorrespondenceType == "mailbox"
                ? CorrespondenceType.Mailbox
                : CorrespondenceType.Address,

            Address = dto.Address,
            HouseNumber = dto.HouseNumber,
            HouseNumberSuffix = dto.HouseNumberSuffix,
            PostalCode = dto.PostalCode,
            City = dto.City,
            Country = dto.Country,
            Phone = dto.Phone,

            MailboxName = dto.MailboxName,
            MailboxAddress = dto.MailboxAddress,

            BillingType = dto.BillingType switch
            {
                "Derde partij" => BillingType.DerdePartij,
                "Opdrachtgever & Derde partij" => BillingType.OpdrachtgeverEnDerdePartij,
                _ => BillingType.Opdrachtgever
            }
        };

        // ===================== INSURANCE POLICY =====================

        public static InsurancePolicyDto ToDto(this InsurancePolicy entity) => new()
        {
            Id = entity.Id,
            OverledeneId = entity.OverledeneId,
            InsurancePartyId = entity.InsurancePartyId,
            PolicyNumber = entity.PolicyNumber,
            Premium = entity.Premium
        };

        public static InsurancePolicy ToEntity(this InsurancePolicyDto dto) => new()
        {
            Id = dto.Id ?? Guid.NewGuid(),
            OverledeneId = dto.OverledeneId,
            InsurancePartyId = dto.InsurancePartyId,
            PolicyNumber = dto.PolicyNumber,
            Premium = dto.Premium
        };

        // ===================== COLLECTION HELPERS =====================

        public static IEnumerable<InsurancePartyDto> ToDtoList(this IEnumerable<InsuranceParty> entities) =>
            entities.Select(e => e.ToDto());

        public static IEnumerable<InsuranceParty> ToEntityList(this IEnumerable<InsurancePartyDto> dtos) =>
            dtos.Select(d => d.ToEntity());

        public static IEnumerable<InsurancePolicyDto> ToDtoList(this IEnumerable<InsurancePolicy> entities) =>
            entities.Select(e => e.ToDto());

        public static IEnumerable<InsurancePolicy> ToEntityList(this IEnumerable<InsurancePolicyDto> dtos) =>
            dtos.Select(d => d.ToEntity());
    }
}
