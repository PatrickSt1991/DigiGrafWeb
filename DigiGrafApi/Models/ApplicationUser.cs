using Microsoft.AspNetCore.Identity;

namespace DigiGrafWeb.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string? FullName { get; set; }
        public string? RoleDescription { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }

    }

    public class ApplicationRole : IdentityRole<Guid>
    {
        public string? Description { get; set; }
    }
}
