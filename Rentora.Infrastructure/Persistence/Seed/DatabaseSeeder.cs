using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Rentora.Domain.Entities.Authentication;
using Rentora.Infrastructure.Persistence.Context;

namespace Rentora.Infrastructure.Persistence.Seed
{
    public static class DatabaseSeeder
    {
        public static async Task SeedAsync(IServiceProvider services)
        {
            var context = services.GetRequiredService<RentoraDbContext>();
            await context.Database.ExecuteSqlRawAsync("EXEC dbo.SeedSystemData");
        }
    }
}
