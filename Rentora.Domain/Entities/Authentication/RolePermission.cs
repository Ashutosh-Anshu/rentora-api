using Rentora.Domain.Common;

namespace Rentora.Domain.Entities.Authentication
{
    public class RolePermission
    {
        public Guid RoleId { get; set; }

        public Guid ActionPermissionId { get; set; }

        public ApplicationRole Role { get; set; } = default!;

        public ActionPermission ActionPermission { get; set; } = default!;
    }
}
