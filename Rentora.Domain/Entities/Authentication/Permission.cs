using Rentora.Domain.Common;

namespace Rentora.Domain.Entities.Authentication
{
    public class Permission : BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        public string Code { get; set; } = string.Empty;

        public string? Description { get; set; }

        public bool IsActive { get; set; } = true;

        public ICollection<MenuPermission> MenuPermissions { get; set; } = [];

        public ICollection<RolePermission> RolePermissions { get; set; } = [];
    }
}
