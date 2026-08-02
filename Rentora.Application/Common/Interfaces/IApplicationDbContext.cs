using Microsoft.EntityFrameworkCore;
using Rentora.Domain.Entities.Authentication;

namespace Rentora.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Menu> Menus { get; }
        DbSet<ActionPermission> ActionPermissions { get; }
        DbSet<RolePermission> RolePermissions { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
