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
        public static EmployeeOverviewDto ToOverviewDto(Employee entity)
        {
            return new EmployeeOverviewDto
            {
                Id = entity.Id,
                FullName = entity.FullName,
                Role = entity.Role,
                IsActive = entity.IsActive,

                HasLogin = entity.UserId != null,
                LoginIsActive = entity.User?.IsActive,
                LoginEmail = entity.User?.Email
            };
        }
        public static AdminEmployeeDto ToAdminDto(Employee e)
        {
            return new AdminEmployeeDto
            {
                Id = e.Id,
                Status = e.IsActive ? "active" : "inactive",

                Initials = e.Initials,
                FirstName = e.FirstName,
                LastName = e.LastName,
                Tussenvoegsel = e.Tussenvoegsel,

                FullName = e.FullName,

                BirthPlace = e.BirthPlace,
                BirthDate = e.BirthDate,

                Email = e.Email,
                Mobile = e.Mobile,
                Role = e.Role,
                StartDate = e.StartDate,

                HasLogin = e.UserId != null,
                LoginIsActive = e.User?.IsActive
            };
        }

        public static IEnumerable<EmployeeDto> ToDtoList(IEnumerable<Employee> entities)
        {
            return entities.Select(e => ToDto(e));
        }
        public static IEnumerable<Employee> ToEntityList(IEnumerable<EmployeeDto> dtos)
        {
            return dtos.Select(d => ToEntity(d));
        }
        public static IEnumerable<EmployeeOverviewDto> ToOverviewDtoList(IEnumerable<Employee> entities)
        {
            return entities.Select(e => ToOverviewDto(e));
        }
        public static IEnumerable<AdminEmployeeDto> ToAdminDtoList(IEnumerable<Employee> entities)
        {
            return entities.Select(e => ToAdminDto(e));
        }
    }
}
