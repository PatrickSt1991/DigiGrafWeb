using DigiGrafWeb.DTOs;
using DigiGrafWeb.Models;

namespace DigiGrafWeb.Mappers
{
    public static class InsuranceMapper
    {
        public static InsuranceCompanyDto ToDto(this InsuranceCompany entity) => new()
        {
            Id = entity.Id,
            Name = entity.Name,
            IsActive = entity.IsActive,
            OriginEnabled = entity.OriginEnabled
        };

        public static InsuranceCompany ToEntity(this InsuranceCompanyDto dto) => new()
        {
            Id = dto.Id != Guid.Empty ? dto.Id : Guid.NewGuid(),
            Name = dto.Name,
            IsActive = dto.IsActive,
            OriginEnabled = dto.OriginEnabled
        };

        public static InsurancePolicyDto ToDto(this InsurancePolicy entity) => new()
        {
            Id = entity.Id,
            InsuranceCompanyId = entity.InsuranceCompanyId,
            PolicyNumber = entity.PolicyNumber,
            Premium = entity.Premium
        };

        public static InsurancePolicy ToEntity(this InsurancePolicyDto dto) => new()
        {
            Id = dto.Id != Guid.Empty ? dto.Id : Guid.NewGuid(),
            InsuranceCompanyId = dto.InsuranceCompanyId,
            PolicyNumber = dto.PolicyNumber,
            Premium = dto.Premium
        };

        public static IEnumerable<InsuranceCompanyDto> ToDtoList(this IEnumerable<InsuranceCompany> entities) =>
            entities.Select(e => e.ToDto());

        public static IEnumerable<InsuranceCompany> ToEntityList(this IEnumerable<InsuranceCompanyDto> dtos) =>
            dtos.Select(d => d.ToEntity());

        public static IEnumerable<InsurancePolicyDto> ToDtoList(this IEnumerable<InsurancePolicy> entities) =>
            entities.Select(e => e.ToDto());

        public static IEnumerable<InsurancePolicy> ToEntityList(this IEnumerable<InsurancePolicyDto> dtos) =>
            dtos.Select(d => d.ToEntity());
    }
}
