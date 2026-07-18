using Microsoft.AspNetCore.Identity;

namespace Rentora.Domain.Entities.Authentication
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        public string? Description { get; set; }

        public bool IsActive { get; set; } = true;

        public bool IsSystem { get; set; }

        public ICollection<RolePermission> RolePermissions { get; set; } = [];

    }
}
