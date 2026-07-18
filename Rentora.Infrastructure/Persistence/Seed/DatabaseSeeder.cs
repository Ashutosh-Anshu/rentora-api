using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Rentora.Domain.Entities.Authentication;

namespace Rentora.Infrastructure.Persistence.Seed
{
    public static class DatabaseSeeder
    {
        public static async Task SeedAsync(IServiceProvider services)
        {
            var roleManager = services.GetRequiredService<RoleManager<ApplicationRole>>();
            await DefaultRolesSeeder.SeedAsync(roleManager);

        }
    }
}
