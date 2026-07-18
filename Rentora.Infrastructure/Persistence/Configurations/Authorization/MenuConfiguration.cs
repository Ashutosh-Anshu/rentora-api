using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rentora.Domain.Constants;
using Rentora.Domain.Entities.Authentication;

namespace Rentora.Infrastructure.Persistence.Configurations.Authentication
{
    public class MenuConfiguration : IEntityTypeConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> builder)
        {
            builder.ToTable("Menus");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .HasMaxLength(EntityLength.Name)
                .IsRequired();

            builder.Property(x => x.DisplayName)
                .HasMaxLength(EntityLength.Name)
                .IsRequired();

            builder.Property(x => x.Route)
                .HasMaxLength(250);

            builder.Property(x => x.Icon)
                .HasMaxLength(100);

            builder.Property(x => x.OrderNum)
                .HasDefaultValue(0);

            builder.Property(x => x.IsActive)
                .HasDefaultValue(true);

            builder.HasOne(x => x.Parent)
                .WithMany(x => x.Children)
                .HasForeignKey(x => x.ParentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
