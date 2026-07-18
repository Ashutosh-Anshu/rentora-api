using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rentora.Domain.Constants;
using Rentora.Domain.Entities.Authentication;

namespace Rentora.Infrastructure.Persistence.Configurations.Authentication
{
    public class ApplicationRoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
    {
        public void Configure(EntityTypeBuilder<ApplicationRole> builder)
        {
            builder.Property(x => x.Description)
                .HasMaxLength(EntityLength.Description);

            builder.Property(x => x.IsActive)
                .HasDefaultValue(true);

            builder.Property(x => x.IsSystem)
                .HasDefaultValue(false);
        }
    }
}
