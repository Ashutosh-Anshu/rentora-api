using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rentora.Domain.Constants;
using Rentora.Domain.Entities.Authentication;
using System.Data;

namespace Rentora.Infrastructure.Persistence.Configurations.Authentication
{
    public sealed class ApplicationRoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
    {
        public void Configure(EntityTypeBuilder<ApplicationRole> builder)
        {
            builder.Property(x => x.Name)
                .HasMaxLength(EntityLength.Name);

            builder.Property(x => x.NormalizedName)
                .HasMaxLength(EntityLength.Name);

            builder.Property(x => x.Description)
                .HasMaxLength(EntityLength.Description);

            builder.Property(x => x.IsActive)
                .HasDefaultValue(true);

            builder.Property(x => x.IsSystem)
                .HasDefaultValue(false);

            builder.HasMany(x => x.RolePermissions)
                .WithOne(x => x.Role)
                .HasForeignKey(x => x.RoleId);
        }
    }
}
