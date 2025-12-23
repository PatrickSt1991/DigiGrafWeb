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
                Id = dto.Id == Guid.Empty ? Guid.NewGuid() : dto.Id,
                IsActive = dto.IsActive,

                Initials = dto.Initials,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Tussenvoegsel = dto.Tussenvoegsel,

                BirthPlace = dto.BirthPlace,
                BirthDate = dto.BirthDate,

                Email = dto.Email,
                Mobile = dto.Mobile,

                StartDate = dto.StartDate
                // ❌ NO ROLE LOGIC HERE
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

                RoleId = Guid.Empty, // ✅ filled in controller when needed
                StartDate = entity.StartDate
            };
        }

        public static EmployeeOverviewDto ToOverviewDto(Employee entity)
        {
            return new EmployeeOverviewDto
            {
                Id = entity.Id,
                FullName = entity.FullName,
                Role = entity.Role, // existing string display is fine here
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

                RoleId = Guid.Empty,      // ✅ filled in controller
                RoleName = string.Empty, // ✅ filled in controller

                StartDate = e.StartDate,

                HasLogin = e.UserId != null,
                LoginIsActive = e.User?.IsActive
            };
        }

        public static IEnumerable<EmployeeDto> ToDtoList(IEnumerable<Employee> entities) =>
            entities.Select(ToDto);

        public static IEnumerable<Employee> ToEntityList(IEnumerable<EmployeeDto> dtos) =>
            dtos.Select(ToEntity);

        public static IEnumerable<EmployeeOverviewDto> ToOverviewDtoList(IEnumerable<Employee> entities) =>
            entities.Select(ToOverviewDto);

        public static IEnumerable<AdminEmployeeDto> ToAdminDtoList(IEnumerable<Employee> entities) =>
            entities.Select(ToAdminDto);
    }
}
