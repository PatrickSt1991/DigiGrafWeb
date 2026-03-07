using DigiGrafWeb.DTOs;
using DigiGrafWeb.Models;

namespace DigiGrafWeb.Mappers
{
    public static class CaretakerMapper
    {
        public static CaretakerDto ToDto(ApplicationUser user)
        {
            return new CaretakerDto
            {
                Id = user.Id,
                DisplayName = $"{user.FullName}",
                Email = user.Email
            };
        }

        public static IEnumerable<CaretakerDto> ToDtoList(IEnumerable<ApplicationUser> users)
            => users.Select(ToDto);
    }

}
