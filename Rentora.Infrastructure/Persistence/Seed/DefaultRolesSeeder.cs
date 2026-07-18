using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Rentora.Domain.Entities.Authentication;

namespace Rentora.Infrastructure.Persistence.Seed
{
    public sealed class DefaultRolesSeeder
    {
        public static async Task SeedAsync(RoleManager<ApplicationRole> _roleManager)
        {
            var roles = new List<ApplicationRole>
        {
            new()
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                Name = "Super Admin",
                NormalizedName = "SUPER ADMIN",
                Description = "System Super Administrator",
                IsActive = true,
                IsSystem = true
            },
            new()
            {
                Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                Name = "Admin",
                NormalizedName = "ADMIN",
                Description = "Administrator",
                IsActive = true,
                IsSystem = true
            },
            new()
            {
                Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                Name = "Owner",
                NormalizedName = "OWNER",
                Description = "Property Owner",
                IsActive = true,
                IsSystem = true
            },
            new()
            {
                Id = Guid.Parse("44444444-4444-4444-4444-444444444444"),
                Name = "Tenant",
                NormalizedName = "TENANT",
                Description = "Property Tenant",
                IsActive = true,
                IsSystem = true
            }
        };

            var existingRoles = await _roleManager.Roles.ToDictionaryAsync(r => r.Id);

            foreach (var role in roles)
            {
                if (!existingRoles.TryGetValue(role.Id, out var existingRole))
                {
                    await _roleManager.CreateAsync(role);
                }
            }
        }
    }
}
