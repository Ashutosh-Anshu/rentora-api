using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rentora.Domain.Constants;
using Rentora.Domain.Entities.Authentication;

namespace Rentora.Infrastructure.Persistence.Configurations.Authentication
{
    public sealed class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(x => x.FullName)
                .IsRequired()
                .HasMaxLength(EntityLength.FullName);

            builder.Property(x => x.Email)
                .HasMaxLength(EntityLength.Email);

            builder.Property(x => x.NormalizedEmail)
                .HasMaxLength(EntityLength.Email);

            builder.Property(x => x.PhoneNumber)
                .HasMaxLength(EntityLength.PhoneNumber);

            builder.Property(x => x.IsActive)
                .HasDefaultValue(true);

            builder.Property(x => x.IsSystem)
                .HasDefaultValue(false);

            builder.Property(x => x.IsDeleted)
                .HasDefaultValue(false);

            builder.Property(x => x.TermsAccepted)
                .HasDefaultValue(false);

            builder.Property(x => x.CreatedAt)
                .IsRequired();
        }
    }
}
