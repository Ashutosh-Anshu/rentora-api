using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rentora.Domain.Constants;
using Rentora.Domain.Entities.Authentication;

namespace Rentora.Infrastructure.Persistence.Configurations.Authentication
{
    public sealed class ActionPermissionConfiguration : IEntityTypeConfiguration<ActionPermission>
    {
        public void Configure(EntityTypeBuilder<ActionPermission> builder)
        {
            builder.ToTable("ActionPermissions");

            builder.Property(x => x.Id)
                .HasColumnName("ActionPermissionId");

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(EntityLength.Name);

            builder.Property(x => x.Code)
                .IsRequired()
                .HasMaxLength(EntityLength.Code);

            builder.Property(x => x.Description)
                .HasMaxLength(EntityLength.Description);

            builder.Property(x => x.IsActive)
                .HasDefaultValue(true);

            builder.HasOne(x => x.Menu)
                .WithMany(x => x.ActionPermissions)
                .HasForeignKey(x => x.MenuId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.RolePermissions)
                .WithOne(x => x.ActionPermission)
                .HasForeignKey(x => x.ActionPermissionId);

            builder.HasIndex(x => x.Code)
                .IsUnique();

            builder.HasIndex(x => new
            {
                x.MenuId,
                x.Name
            }).IsUnique();
        }
    }
}
