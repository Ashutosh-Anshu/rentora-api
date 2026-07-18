using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rentora.Domain.Constants;
using Rentora.Domain.Entities.Authentication;

namespace Rentora.Infrastructure.Persistence.Configurations.Authentication
{
    public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.ToTable("Permissions");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .HasMaxLength(EntityLength.Name)
                .IsRequired();

            builder.Property(x => x.Code)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Description)
                .HasMaxLength(EntityLength.Description);

            builder.Property(x => x.IsActive)
                .HasDefaultValue(true);

            builder.HasIndex(x => x.Code)
                .IsUnique();
        }
    }
}
