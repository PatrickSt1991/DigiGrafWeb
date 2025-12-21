using DigiGrafWeb.DTOs;
using DigiGrafWeb.Models;

namespace DigiGrafWeb.Mappers
{
    public static class EmployeeMapper
    {
        public static Employee ToEntity(EmployeeDto dto)
        {
            return new Employee
            {
                Id = dto.Id,
                IsActive = dto.IsActive,
                Initials = dto.Initials,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Tussenvoegsel = dto.Tussenvoegsel,
                BirthPlace = dto.BirthPlace,
                BirthDate = dto.BirthDate,
                Email = dto.Email,
                Mobile = dto.Mobile,
                Role = dto.Role,
                StartDate = dto.StartDate
            };
        }

        public static EmployeeDto ToDto(Employee entity)
        {
            return new EmployeeDto
            {
                Id = entity.Id,
                IsActive = entity.IsActive,
                Initials = entity.Initials,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Tussenvoegsel = entity.Tussenvoegsel,
                BirthPlace = entity.BirthPlace,
                BirthDate = entity.BirthDate,
                Email = entity.Email,
                Mobile = entity.Mobile,
                Role = entity.Role,
                StartDate = entity.StartDate
            };
        }

        // ✅ ADD THESE
        public static IEnumerable<EmployeeDto> ToDtoList(IEnumerable<Employee> entities)
        {
            return entities.Select(e => ToDto(e));
        }

        public static IEnumerable<Employee> ToEntityList(IEnumerable<EmployeeDto> dtos)
        {
            return dtos.Select(d => ToEntity(d));
        }
    }
}
