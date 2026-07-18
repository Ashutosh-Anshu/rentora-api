using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rentora.Domain.Entities.Authentication;

namespace Rentora.Infrastructure.Persistence.Configurations.Authentication
{
    public class MenuPermissionConfiguration : IEntityTypeConfiguration<MenuPermission>
    {
        public void Configure(EntityTypeBuilder<MenuPermission> builder)
        {
            builder.ToTable("MenuPermissions");

            builder.HasKey(x => new
            {
                x.MenuId,
                x.PermissionId
            });

            builder.HasOne(x => x.Menu)
                .WithMany(x => x.MenuPermissions)
                .HasForeignKey(x => x.MenuId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Permission)
                .WithMany(x => x.MenuPermissions)
                .HasForeignKey(x => x.PermissionId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
