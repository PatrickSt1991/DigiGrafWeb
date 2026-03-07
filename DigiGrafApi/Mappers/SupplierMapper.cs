using DigiGrafWeb.DTOs;
using DigiGrafWeb.Models;
using Microsoft.OpenApi.Extensions;

namespace DigiGrafWeb.Mappers
{
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
            => entities.Select(ToDto);

        public static IEnumerable<Suppliers> ToEntityList(IEnumerable<SupplierDto> dtos)
            => dtos.Select(ToEntity);

        public static IEnumerable<SupplierTypeDto> ToTypeDtoList()
        {
            return Enum.GetValues<SupplierType>()
                .Select(t => new SupplierTypeDto
                {
                    Code = t.ToString(),
                    Label = t.GetDisplayName()
                });
        }
    }
}
