using Rentora.Domain.Common;

namespace Rentora.Domain.Entities.Authentication
{
    public class RolePermission
    {
        public Guid RoleId { get; set; }
        public Guid PermissionId { get; set; }

        public ApplicationRole Role { get; set; } = default!;
        public Permission Permission { get; set; } = default!;
    }
}
