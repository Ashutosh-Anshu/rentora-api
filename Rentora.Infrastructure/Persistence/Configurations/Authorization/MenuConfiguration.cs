using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rentora.Domain.Constants;
using Rentora.Domain.Entities.Authentication;

namespace Rentora.Infrastructure.Persistence.Configurations.Authentication
{
    public sealed class MenuConfiguration : IEntityTypeConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> builder)
        {
            builder.ToTable("Menus");

            builder.Property(x =>x.Id)
                .HasColumnName("MenuId");

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(EntityLength.Name);

            builder.Property(x => x.DisplayName)
                .IsRequired()
                .HasMaxLength(EntityLength.DisplayName);

            builder.Property(x => x.Route)
                .HasMaxLength(EntityLength.Route);

            builder.Property(x => x.Icon)
                .HasMaxLength(EntityLength.Icon);

            builder.Property(x => x.IsActive)
                .HasDefaultValue(true);

            builder.HasOne(x => x.Parent)
                .WithMany(x => x.Children)
                .HasForeignKey(x => x.ParentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.ActionPermissions)
                .WithOne(x => x.Menu)
                .HasForeignKey(x => x.MenuId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(x => x.Name)
                .IsUnique();

            builder.HasIndex(x => x.OrderNum);
        }
    }
}
