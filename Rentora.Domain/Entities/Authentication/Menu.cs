using Rentora.Domain.Common;

namespace Rentora.Domain.Entities.Authentication
{
    public class Menu : BaseAuditableEntity
    {
        public Guid? ParentId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string DisplayName { get; set; } = string.Empty;

        public string? Route { get; set; }

        public string? Icon { get; set; }

        public int OrderNum { get; set; }

        public bool IsActive { get; set; } = true;

        public Menu? Parent { get; set; }

        public ICollection<Menu> Children { get; set; } = [];

        public ICollection<MenuPermission> MenuPermissions { get; set; } = [];
    }
}
