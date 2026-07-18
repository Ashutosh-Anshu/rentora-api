using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rentora.Domain.Constants;
using Rentora.Domain.Entities.Authentication;

namespace Rentora.Infrastructure.Persistence.Configurations.Authentication
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(x => x.FullName)
                .HasMaxLength(EntityLength.Name)
                .IsRequired();

            builder.Property(x => x.IsActive)
                .HasDefaultValue(true);

            builder.Property(x => x.IsSystem)
                .HasDefaultValue(false);

            builder.Property(x => x.IsDeleted)
                .HasDefaultValue(false);

            builder.Property(x => x.TermsAccepted)
                .HasDefaultValue(false);
        }
    }
}
