using Rentora.Domain.Common;

namespace Rentora.Domain.Entities.Authentication
{
    public class MenuPermission
    {
        public Guid MenuId { get; set; }
        public Guid PermissionId { get; set; }

        public Menu Menu { get; set; } = default!;
        public Permission Permission { get; set; } = default!;
    }
}
