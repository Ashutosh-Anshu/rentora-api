using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Rentora.Domain.Entities.Authentication;

namespace Rentora.Infrastructure.Persistence.Context
{
    public class RentoraDbContext: IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public RentoraDbContext(DbContextOptions<RentoraDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(RentoraDbContext).Assembly);
        }

        public DbSet<Menu> Menus => Set<Menu>();
        public DbSet<Permission> Permissions => Set<Permission>();
        public DbSet<MenuPermission> MenuPermissions => Set<MenuPermission>();
        public DbSet<RolePermission> RolePermissions => Set<RolePermission>();
    }
}
